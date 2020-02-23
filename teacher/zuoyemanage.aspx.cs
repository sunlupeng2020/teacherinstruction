using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;

public partial class teachermanage_zuoyemanage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBanji();
        }
    }
    protected void Buttonbanji01_Click(object sender, EventArgs e)//某班某课程全部作业统计
    {

    }
    protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)//选择班级后，重新绑定作业和学生
    {
        BindZuoye();
        //绑定学生信息
        BindStu();
    }
    protected void BindBanji()
    {
        string tun = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        DataSet banjidt = BanjiInfo.GetTeacherRenkeBanji(tun, int.Parse(kechengid));
        ListBoxbanji.DataSource = banjidt;
        ListBoxbanji.DataBind();
    }
    protected void BindZuoye()
    {
        string banjiid = ListBoxbanji.SelectedValue;
        string kechengid = Session["kechengid"].ToString();
        DataSet zuoyeds = ZuoyeInfo.GetZuoyeInfo(int.Parse(kechengid), int.Parse(banjiid), ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name);
        ListBoxzuoye.DataSource = zuoyeds;
        ListBoxzuoye.DataBind();
    }
    protected void BindStu()
    {
        string banjiid = ListBoxbanji.SelectedValue;
        //绑定学生信息
        DataTable studt = BanjiInfo.GetStudentNameAndUsername(int.Parse(banjiid));
        ListBoxstu.DataSource = studt;
        ListBoxstu.DataBind();
    }
    protected void ListBox3_SelectedIndexChanged(object sender, EventArgs e)//作业框选择后
    {
        string yunxu="";
        string yunxuchakan = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        string zuoyeid = ListBoxzuoye.SelectedValue;
        string banjiid = ListBoxbanji.SelectedValue;
        comm.CommandText = "select yunxuzuoti,yunxuchakan from tb_zuoyebuzhi where zuoyeid=@zuoyeid and banjiid=@banjiid";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            if (!sdr.IsDBNull(0))
            {
                yunxu = sdr.GetString(0).Trim();
            }
            if (!sdr.IsDBNull(1))
            {
                yunxuchakan = sdr.GetString(1).Trim();
            }
        }
        sdr.Close();
        conn.Close();
        if (yunxu == "允许")
        {
            LinkButton3.Text = "禁止学生做作业";
            LinkButton3.CommandArgument = "禁止";
        }
        else
        {
            LinkButton3.Text = "允许学生做作业";
            LinkButton3.CommandArgument = "允许";
        }
        if (yunxuchakan == "允许")
        {
            LinkButtonyxck.Text = "禁止学生查看作业结果";
            LinkButtonyxck.CommandArgument = "禁止";
        }
        else
        {
            LinkButtonyxck.Text = "允许学生查看作业结果";
            LinkButtonyxck.CommandArgument = "允许";
        }
    }
    protected void HyperLinkqtzytj_Click(object sender, EventArgs e)//全体学生作业统计
    {
        if (ListBoxbanji.SelectedIndex>=0&&ListBoxzuoye.Items.Count > 0)
        {
            string banjiid = ListBoxbanji.SelectedValue;
            string kechengid = Session["kechengid"].ToString();
            string urlx = "zuoyebjqthz.aspx?banjiid=" + banjiid + "&kechengid=" + kechengid;
            string URL = " <script   language= 'javascript'> window.open( '" + urlx + "','_blank');</script> ";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "","<script language='javascript'>alert('未选择班级,或给该班布置作业,无法统计作业！');</script>", false);
        }
    }
    protected void HyperLinkzuoyejbxx_Click(object sender, EventArgs e)//作业基本信息及修改
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string banjiid = ListBoxbanji.SelectedValue;
            DataTable zy = ZuoyeInfo.GetZuoyeBuzhiInfo(zuoyeid,banjiid);
            string zuoyebuzhiid = zy.Rows[0][0].ToString();
            string urlx = "zuoye_editbuzhi.aspx?zuoyebuzhiid=" +zuoyebuzhiid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx",URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择班级、作业！');</script>", false);
        }
    }
    protected void HyperLinkqtmzytj_Click(object sender, EventArgs e)//学生某作业情况统计
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string banjiid = ListBoxbanji.SelectedValue;
            string urlx = "zuoyechengji.aspx?zuoyeid=" + zuoyeid+"&banjiid="+banjiid;
            string URL = "<script  language= 'javascript'> window.open('" +urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择班级和作业！');</script>", false);
        }
    }
    protected void HyperLinkzypigai_Click(object sender, EventArgs e)//作业批改
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string banjiid = ListBoxbanji.SelectedValue;
            string urlx = "zuoyepigai.aspx?zuoyeid=" + zuoyeid+"&banjiid="+banjiid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择班级、作业！');</script>", false);
        }
    }
    protected void LinkButtonzuoyefenxi_Click(object sender, EventArgs e)//作业分析
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string banjiid = ListBoxbanji.SelectedValue;
            string urlx = "zuoyefenxi.aspx?zuoyeid=" + zuoyeid+"&banjiid="+banjiid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择班级、作业！');</script>", false);
        }
    }
    protected void HyperLinkstuqbzy_Click(object sender, EventArgs e)//学生某课程全部作业情况
    {
        if (ListBoxstu.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string kechengid = Session["kechengid"].ToString(); 
            string studentusername = ListBoxstu.SelectedValue;
            string banjiid = ListBoxbanji.SelectedValue;
            string urlx = "zuoyestukecheng.aspx?banjiid="+banjiid+"&studentusername="+studentusername;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择班级、学生！');</script>", false);
        }
    }
    protected void HyperLinkstumzy_Click(object sender, EventArgs e)//学生某作业题目详情
    {
        if (ListBoxzuoye.SelectedIndex>=0&& ListBoxstu.SelectedIndex >= 0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string studentusername = ListBoxstu.SelectedValue;
            string urlx = "zuoyestutimu.aspx?zuoyeid=" +zuoyeid  +"&studentusername=" + studentusername;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择作业、学生！');</script>", false);
        }
    }
    protected void LinkButtonchengjidj_Click(object sender, EventArgs e)//作业成绩登记
    {
        if (ListBoxzuoye.SelectedIndex >= 0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string urlx = "zuoyeshumiandengji.aspx?zuoyeid=" + zuoyeid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择作业！');</script>", false);
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)//允许学生做作业
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string yunxu = LinkButton3.CommandArgument.ToString().Trim();
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string banjiid=ListBoxbanji.SelectedValue;
            if (ZuoyeInfo.SetZuoyeYunxuZuoti(int.Parse(zuoyeid), int.Parse(banjiid), yunxu))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('" + yunxu + "学生做作业，成功！');</script>", false);
                BindZuoye();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('" + yunxu + "学生做作业，失败！');</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择班级、作业！');</script>", false);
        }
    }
    protected void LinkButtonyxck_Click(object sender, EventArgs e)//允许学生查看作业
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string yxck = LinkButtonyxck.CommandArgument.ToString();
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string banjiid = ListBoxbanji.SelectedValue;
            if (ZuoyeInfo.SetZuoyeYunxuChakan(int.Parse(zuoyeid), int.Parse(banjiid), yxck))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('" + yxck + "学生查看作业，成功！');</script>", false);
                BindZuoye();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('" + yxck + "学生查看作业，成功！');</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择班级、作业！');</script>", false);
        }
    }
    protected void LinkButtonshanchuzy_Click(object sender, EventArgs e)//删除作业
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string banjiid = ListBoxbanji.SelectedValue;
            if (ZuoyeInfo.DeleteZuoyeFromBanji(int.Parse(banjiid), int.Parse(zuoyeid)))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('作业删除成功！');</script>", false);
                BindZuoye();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('作业删除失败！');</script>", false);
            }
            
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zysc", "<script language='javascript'>alert('请选择班级和作业！');</script>", false);
        }

    }
    protected void LinkButtonzdpgkgt_Click(object sender, EventArgs e)//自动批改作业客观题并汇总成绩
    {
        if (ListBoxzuoye.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            int zuoyeid = int.Parse(ListBoxzuoye.SelectedValue);
            int banjiid = int.Parse(ListBoxbanji.SelectedValue);
            if (ZuoyeInfo.PigaiZuoyeKeguanti(zuoyeid,banjiid))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('客观题批改成功！并重新计算了学生成绩。');</script>", false);
                //GridView2.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('客观题批改失败！');</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('请选择班级、作业！');</script>", false);
        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)//将某作业布置给某学生
    {
        Label1.Text = "";
        if (ListBoxzuoye.SelectedIndex >= 0 & ListBoxstu.SelectedIndex >= 0&&ListBoxbanji.SelectedIndex>=0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string stuusername = ListBoxstu.SelectedValue;
            string kechengid = Session["kechengid"].ToString(); 
            string banjiid = ListBoxbanji.SelectedValue;
            if (StudentInfo.IsStuHaveZuoye(int.Parse(zuoyeid),stuusername))//判断作业是否已布置给学生
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('该作业在该学生作业表中已存在，无须重新布置!');</script>", false);
            }
            else
            {
                try
                {
                    ZuoyeInfo.ZuoyeFenfaToStudent(zuoyeid, stuusername, kechengid, banjiid);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('作业分发成功!');</script>", false);
                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('作业分发失败!');</script>", false);
                    Label1.Text = ex.Message;
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('请选择课程、班级、作业、学生!');</script>", false);
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)//将一个作业布置给其他班
    {
        if (ListBoxzuoye.SelectedIndex >= 0)
        {
            string zuoyeid = ListBoxzuoye.SelectedValue;
            string urlx = "zuoye_id_buzhi01.aspx?zuoyeid=" + zuoyeid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "zyxx", "<script language='javascript'>alert('请选择作业！');</script>", false);
        }
    }
    protected void ListBoxbanji_DataBound(object sender, EventArgs e)
    {
        if (ListBoxbanji.Items.Count > 0)
        {
            ListBoxbanji.SelectedIndex = 0;
            BindZuoye();
            BindStu();
        }
        else
        {
            ListBoxzuoye.Items.Clear();
            ListBoxstu.Items.Clear();
        }
    }
}
