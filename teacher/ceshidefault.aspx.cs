using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class teachermanage_ceshidefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBanji();
        }
    }
    protected void BindBanji()
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid=Session["kechengid"].ToString();
        DataSet ds=BanjiInfo.GetTeacherRenkeBanji(username,int.Parse(kechengid));
        ListBoxbj.DataSource=ds;
        ListBoxbj.DataBind();
    }
    protected void Bindceshi()//绑定测试项目
    {
        string username= ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        string banjiid = ListBoxbj.SelectedValue;
        DataSet ds = CeshiInfo.GetTeacherCeshi(username, kechengid, banjiid);
        ListBoxcs.DataSource = ds;
        ListBoxcs.DataBind();
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (ListBoxbj.SelectedIndex >= 0)
        {
            string banjiid = ListBoxbj.SelectedValue;
            string urlx = "ceshihuizong.aspx?banjiid=" + banjiid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshihuizong", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshihz", "<script language='javascript'>alert('请选择班级！');</script>", false);

        }
    }
    protected void LinkButtonqtxscj_Click(object sender, EventArgs e)//某测试成绩
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid =ListBoxcs.SelectedValue;
            string urlx = "ceshichengji.aspx?shijuanid=" + shijuanid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshicj", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshicj", "<script language='javascript'>alert('请选择测试名称！');</script>", false);

        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)//自动批改测试的客观题并汇总学生成绩
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid = ListBoxcs.SelectedValue;
            string ceshiname = ListBoxcs.SelectedItem.Text;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select mingtifangshi from tb_teachershijuan where shijuanid=" + shijuanid;
            conn.Open();
            string mingtifangshi = comm.ExecuteScalar().ToString().Trim();
            conn.Close();
            if (mingtifangshi == "对学生保密")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('测试:" + ceshiname + "没有学生试卷，未能批改学生客观题！');</script>", false);
            }
            else
            {
                List<string> stuusername = new List<string>();
                comm.CommandText = "select studentusername from tb_studentkaoshi where shijuanid=" + shijuanid;
                conn.Open();
                SqlDataReader sdr = comm.ExecuteReader();
                while (sdr.Read())
                {
                    stuusername.Add(sdr.GetString(0).Trim());
                }
                sdr.Close();
                SqlTransaction st = conn.BeginTransaction();
                comm.Transaction = st;
                try
                {
                    comm.CommandText = "update tb_studentkaoshiti set defen=fenzhi where (shijuanid=" + shijuanid + ") and (answer=(select answer from tb_tiku where questionid=tb_studentkaoshiti.questionid)) and charindex((select [type] from tb_tiku where questionid=tb_studentkaoshiti.[questionid]),'单项选择题多项选择题判断题')>0";
                    comm.ExecuteNonQuery();
                    comm.CommandText = "update tb_studentkaoshiti set defen=0 where (shijuanid=" + shijuanid + ") and (answer is null or answer<>(select answer from tb_tiku where questionid=tb_studentkaoshiti.questionid)) and charindex((select [type] from tb_tiku where questionid=tb_studentkaoshiti.[questionid]),'单项选择题多项选择题判断题')>0";
                    comm.ExecuteNonQuery();
                    foreach (string stuun in stuusername)
                    {
                        comm.CommandText = "update tb_studentkaoshi set zongfen=(select sum(defen) from tb_studentkaoshiti where studentusername='" + stuun + "' and shijuanid=" + shijuanid + ") where studentusername='" + stuun + "' and shijuanid=" + shijuanid;
                        comm.ExecuteNonQuery();
                    }
                    st.Commit();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('测试:" + ceshiname + ",全部客观题批改成功！并已重新计算成绩！');</script>", false);
                }
                catch
                {
                    st.Rollback();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('全部客观题批改失败！');</script>", false);
                }
                finally
                {
                    if (conn.State.ToString() == "Opened")
                        conn.Close();
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshicj", "<script language='javascript'>alert('请选择测试名称！');</script>", false);

        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)//测试详细信息及修改
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid = ListBoxcs.SelectedValue;
            string urlx = "ceshidemo.aspx?shijuanid=" + shijuanid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshidm", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshidm", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
    protected void LinkButtonsjfx_Click(object sender, EventArgs e)
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid = ListBoxcs.SelectedValue;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select mingtifangshi from tb_teachershijuan where shijuanid=" + shijuanid;
            conn.Open();
            string mingtifangshi = comm.ExecuteScalar().ToString().Trim();
            conn.Close();
            if (mingtifangshi == "对学生保密")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshifx", "<script language='javascript'>alert('该测试没有学生试卷，不能进行分析！');</script>", false);
            }
            else
            {
                string urlx = "ceshifenxi.aspx?shijuanid=" + shijuanid;
                string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshifx", URL, false);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshifx", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
    protected void ListBoxcs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string shijuanid = ListBoxcs.SelectedValue;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select mingtifangshi,yunxuzuoti,yunxuchakan from tb_teachershijuan where shijuanid=" + shijuanid;
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetString(0).Trim() == "对学生保密")
                {
                    LinkButtonsjfx.Enabled = false;
                }
                else
                {
                    LinkButtonsjfx.Enabled = true;
                }
                if (sdr.GetString(1).Trim() == "允许")//允许做题
                {
                    LinkButtonyxzt.Text = "禁止学生进行测试";
                    LinkButtonyxzt.CommandArgument = "禁止";
                }
                else
                {
                    LinkButtonyxzt.Text = "允许学生进行测试";
                    LinkButtonyxzt.CommandArgument = "允许";

                }
                if (sdr.GetString(2).Trim() == "允许")
                {
                    LinkButtonyxchk.Text = "禁止学生查看测试结果";
                    LinkButtonyxchk.CommandArgument = "禁止";
                }
                else
                {
                    LinkButtonyxchk.Text = "允许学生查看测试结果";
                    LinkButtonyxchk.CommandArgument = "允许";
                }
            }
            sdr.Close();
            conn.Close();
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void LinkButtonyxzt_Click(object sender, EventArgs e)
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            bool chenggong = true;
            string shijuanid = ListBoxcs.SelectedValue;
            string yunxu = LinkButtonyxzt.CommandArgument.Trim();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "update tb_teachershijuan set yunxuzuoti='" + yunxu + "' where shijuanid=" + shijuanid;
            conn.Open();
            SqlTransaction st = conn.BeginTransaction();
            comm.Transaction = st;
            try
            {
                comm.ExecuteNonQuery();
                comm.CommandText = "update tb_studentkaoshi set yunxu='" + yunxu + "' where shijuanid=" + shijuanid;
                comm.ExecuteNonQuery();
                st.Commit();
            }
            catch
            {
                chenggong = false;
            }
            finally
            {
                conn.Close();
            }
            if (chenggong)
            {
                if (yunxu == "允许")
                    yunxu = "禁止";
                else
                    yunxu = "允许";
                LinkButtonyxzt.Text = yunxu + "学生进行测试";
                LinkButtonyxzt.CommandArgument = yunxu;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshiyxzt", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
    protected void LinkButtonyxchk_Click(object sender, EventArgs e)
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string yunxu = LinkButtonyxchk.CommandArgument.Trim();
            string shijuanid = ListBoxcs.SelectedValue;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "update tb_teachershijuan set yunxuchakan='" +yunxu + "' where shijuanid=" + shijuanid;
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            if (yunxu == "允许")
                yunxu = "禁止";
            else
                yunxu = "允许";
            LinkButtonyxchk.Text = yunxu + "学生查看测试结果";
            LinkButtonyxchk.CommandArgument = yunxu;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshiyxzt", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
    protected void LinkButtoncsgl_Click(object sender, EventArgs e)//在线实时测试管理
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid = ListBoxcs.SelectedValue;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select mingtifangshi from tb_teachershijuan where shijuanid=" + shijuanid;
            conn.Open();
            string mingtifangshi = comm.ExecuteScalar().ToString().Trim();
            conn.Close();
            if (mingtifangshi == "对学生保密")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshifx", "<script language='javascript'>alert('该测试没有学生试卷，学生不能进行测试，因此不能进行在线测试管理！');</script>", false);
            }
            else
            {
                string urlx = "ceshiguanlionline.aspx?shijuanid=" + shijuanid;
                string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshiglol", URL, false);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshiglol", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
    protected void LinkButtonpigai_Click(object sender, EventArgs e)
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid = ListBoxcs.SelectedValue;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select mingtifangshi from tb_teachershijuan where shijuanid=" + shijuanid;
            conn.Open();
            string mingtifangshi = comm.ExecuteScalar().ToString().Trim();
            conn.Close();
            if (mingtifangshi == "对学生保密")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshifx", "<script language='javascript'>alert('该测试没有学生试卷，学生不能进行测试，因此不能进行在线测试管理！');</script>", false);
            }
            else
            {
                //查询改试卷有无客观题

                string urlx = "ceshipigaizhgt.aspx?shijuanid=" + shijuanid;
                string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshiglol", URL, false);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshiglol", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)//一个学生各次测试信息
    {
        if (ListBox1.SelectedIndex >= 0 && ListBoxbj.SelectedIndex >= 0&&ListBoxcs.Items.Count>0)
        {
            string studentusername = ListBox1.SelectedValue;
            string banjiid = ListBoxbj.SelectedValue;
            string urlx = "ceshistukecheng.aspx?banjiid=" + banjiid + "&studentusername=" + studentusername;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", "<script language='javascript'>alert('未选择班级、学生,或者没有测试信息，不能显示！');</script>", false);

        }
    }
    protected void LinkButtonstucstm_Click(object sender, EventArgs e)//学生测试题目详情
    {
        if (ListBox1.SelectedIndex >= 0 && ListBoxcs.SelectedIndex >= 0)
        {
            string studentusername = ListBox1.SelectedValue;
            string shijuanid = ListBoxcs.SelectedValue;
            string urlx = "ceshistutimu.aspx?shijuanid=" +shijuanid+ "&studentusername=" + studentusername;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", "<script language='javascript'>alert('请选择学生、测试名称！');</script>", false);
        }
    }
    protected void LinkButton6_Click(object sender, EventArgs e)//学生自测详情
    {
        if (ListBox1.SelectedIndex >= 0 && ListBoxbj.SelectedIndex >= 0)
        {
            string studentusername = ListBox1.SelectedValue;
            string banjiid = ListBoxbj.SelectedValue;
            string urlx = "ceshistuzice.aspx?banjiid=" + banjiid + "&studentusername=" + studentusername;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", "<script language='javascript'>alert('请选择班级、学生！');</script>", false);

        }
    }
    protected void LinkButtonqtzice_Click(object sender, EventArgs e)//全体同学自测信息
    {
        if (ListBoxbj.SelectedIndex >= 0)
        {
            string banjiid = ListBoxbj.SelectedValue;
            string urlx = "ceshiqtzice.aspx?&banjiid=" + banjiid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", "<script language='javascript'>alert('请选择班级！');</script>", false);
        }
    }
    protected void LinkButton7_Click(object sender, EventArgs e)//删除测试
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid = ListBoxcs.SelectedValue;
            string shijuanname = ListBoxcs.SelectedItem.Text;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            conn.Open();
            SqlTransaction st = conn.BeginTransaction();
            comm.Transaction = st;
            try
            {
                comm.CommandText = "delete from tb_teachershijuan where shijuanid=" + shijuanid;
                comm.ExecuteNonQuery();
                comm.CommandText = "delete from tb_teachershijuantimu where shijuanid=" + shijuanid;
                comm.ExecuteNonQuery();
                comm.CommandText = "delete from tb_studentkaoshi where shijuanid=" + shijuanid;
                comm.ExecuteNonQuery();
                comm.CommandText = "delete from tb_studentkaoshiti where shijuanid=" + shijuanid;
                comm.ExecuteNonQuery();
                st.Commit();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshidel", "<script language='javascript'>alert('测试：" + shijuanname + ",删除成功！');</script>", false);
                Bindceshi();
            }
            catch
            {
                st.Rollback();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshidel", "<script language='javascript'>alert('测试：" + shijuanname + ",删除失败！');</script>", false);
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ceshiglol", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
    protected void LinkButton8_Click(object sender, EventArgs e)//为某学生的某测试项目组卷
    {
        Label1.Text = "";
        if (ListBoxcs.SelectedIndex >= 0 && ListBox1.SelectedIndex >= 0)
        {
            DataTable dt;
            SqlDataAdapter sda;
            string shijuanid = ListBoxcs.SelectedValue;
            string stuusername = ListBox1.SelectedValue;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            //查询命题方式
            comm.CommandText = "select mingtifangshi from tb_teachershijuan where shijuanid=" + shijuanid;
            conn.Open();
            string mingtifangshi = comm.ExecuteScalar().ToString().Trim();
            if (mingtifangshi == "对学生保密")
            {
                conn.Close();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('该试卷对学生保密，无需为学生组卷！');</script>", false);
            }
            else 
            {
                //查询学生是否有该试卷
                comm.CommandText = "select count(shijuanid) from tb_studentkaoshi where shijuanid=" + shijuanid + " and studentusername='" + stuusername + "'";
                if (((int)(comm.ExecuteScalar())) == 0)
                {
                    comm.CommandText = "select kechengid,banjiid,yunxuzuoti from tb_teachershijuan where shijuanid=" + shijuanid;
                    SqlDataReader sdr = comm.ExecuteReader();
                    sdr.Read();
                    string kechengid = sdr.GetInt32(0).ToString();
                    string banjiid = sdr.GetInt32(1).ToString();
                    string yunxu = sdr.GetString(2);
                    sdr.Close();
                    SqlTransaction st = conn.BeginTransaction();
                    comm.Transaction = st;
                    try
                    {
                        comm.CommandText = "insert into tb_studentkaoshi(shijuanid,studentusername,kechengid,banjiid,yunxu,jiaojuan) values(" + shijuanid + ",'" + stuusername + "'," + kechengid + "," + banjiid + ",'" + yunxu + "','未开始')";
                        comm.ExecuteNonQuery();
                        if (mingtifangshi == "全体学生相同")
                        {
                            comm.CommandText = "select tihao,questionid,fenzhi from tb_teachershijuantimu where shijuanid=" + shijuanid;
                            dt = new DataTable();
                            sda = new SqlDataAdapter(comm);
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                comm.CommandText = "insert into tb_studentkaoshiti(studentusername,shijuanid,questionid,timuhao,fenzhi,defen) values('" + stuusername + "'," + shijuanid + "," + dt.Rows[i][1].ToString() + "," + dt.Rows[i][0].ToString() + "," + dt.Rows[i][2].ToString() + ",0)";
                                comm.ExecuteNonQuery();
                            }
                            st.Commit();
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('为单个学生组卷成功！');</script>", false);
                        }
                        else//全体学生不同
                        {
                            //每种题型的题目数量
                            bool youti = false;
                            Dictionary<string, int> tixingshuliang = new Dictionary<string, int>();
                            comm.CommandText = "select [type],count([type]) as shuliang from tb_tiku where questionid in(select questionid from tb_studentkaoshiti where studentusername=(select top 1 studentusername from tb_studentkaoshi where shijuanid=" + shijuanid + ") and shijuanid=" + shijuanid + ") group by [type]";
                            sdr = comm.ExecuteReader();
                            if (sdr.HasRows)
                            {
                                youti = true;
                                while (sdr.Read())
                                {
                                    tixingshuliang.Add(sdr.GetString(0), sdr.GetInt32(1));
                                }
                            }
                            sdr.Close();
                            if (youti)
                            {
                                int tishu;
                                int xuantishu;
                                int tihao = 1;
                                Random r1;
                                int zongtishu;
                                int xuantihao;
                                List<int> xuantihaoList;
                                //随机选题组卷
                                foreach (string key in tixingshuliang.Keys)
                                {
                                    comm.CommandText = "select distinct tb_studentkaoshiti.questionid,tb_studentkaoshiti.fenzhi from tb_studentkaoshiti left join tb_tiku on tb_tiku.questionid=tb_studentkaoshiti.questionid where (tb_studentkaoshiti.shijuanid=" + shijuanid + " and tb_tiku.[type]='" + key + "')";
                                    dt = new DataTable();
                                    sda = new SqlDataAdapter(comm);
                                    sda.Fill(dt);
                                    zongtishu = dt.Rows.Count;
                                    tishu = tixingshuliang[key];
                                    xuantishu = 0;
                                    //tihao = 0;
                                    r1 = new Random();
                                    xuantihaoList = new List<int>();
                                    while (xuantishu < tishu)
                                    {
                                        xuantihao = r1.Next(tishu);
                                        if (!xuantihaoList.Contains(xuantihao))
                                        {
                                            xuantihaoList.Add(xuantihao);
                                            xuantishu++;
                                        }
                                    }
                                    foreach (int i in xuantihaoList)
                                    {
                                        comm.CommandText = "insert into tb_studentkaoshiti(shijuanid,studentusername,questionid,fenzhi,defen,timuhao) values(" + shijuanid + ",'" + stuusername + "'," + dt.Rows[i][0].ToString() + "," + dt.Rows[i][1].ToString() + ",0," + tihao + ")";
                                        comm.ExecuteNonQuery();
                                        tihao++;
                                    }
                                }
                                st.Commit();
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('为单个学生组卷成功！');</script>", false);
                            }
                            else//未找到题目
                            {
                                st.Rollback();
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('为单个学生组卷失败！');</script>", false);
                            }
                        }//end of 全体学生不同
                    }
                    catch (Exception ex)
                    {
                        //st.Rollback();
                        Label1.Text = ex.Message;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('为单个学生组卷失败！');</script>", false);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('该学生已有该试卷，无需组卷！');</script>", false);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('请选择学生、测试名称！');</script>", false);
        }
    }
    protected void ListBoxbj_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindceshi();
        BindBanjiStudent();
    }
    protected void BindBanjiStudent()
    {
        string banjiid = ListBoxbj.SelectedValue;
        DataTable dt = BanjiInfo.GetStudentNameAndUsername(int.Parse(banjiid));
        ListBox1.DataSource = dt;
        ListBox1.DataBind();
    }
    protected void LinkButtonqianyi_Click(object sender, EventArgs e)//将测试项目应用于其他班
    {
        if (ListBoxcs.SelectedIndex >= 0)
        {
            string shijuanid = ListBoxcs.SelectedValue;
            string urlx = "ceshiqianyi.aspx?shijuanid=" + shijuanid;
            string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", "<script language='javascript'>alert('请选择测试名称！');</script>", false);
        }
    }
}
