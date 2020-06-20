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
        ddlbj.DataSource=ds;
        ddlbj.DataBind();
    }

    protected void LinkButton6_Click(object sender, EventArgs e)//学生自测详情
    {
        //if (ListBox1.SelectedIndex >= 0 && ListBoxbj.SelectedIndex >= 0)
        //{
        //    string studentusername = ListBox1.SelectedValue;
        //    string banjiid = ListBoxbj.SelectedValue;
        //    string urlx = "ceshistuzice.aspx?banjiid=" + banjiid + "&studentusername=" + studentusername;
        //    string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", "<script language='javascript'>alert('请选择班级、学生！');</script>", false);

        //}
    }
    protected void LinkButtonqtzice_Click(object sender, EventArgs e)//全体同学自测信息
    {
        //if (ListBoxbj.SelectedIndex >= 0)
        //{
        //    string banjiid = ListBoxbj.SelectedValue;
        //    string urlx = "ceshiqtzice.aspx?&banjiid=" + banjiid;
        //    string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", "<script language='javascript'>alert('请选择班级！');</script>", false);
        //}
    }
   
   /// <summary>
   /// 选择班级后，显示该班学生测试情况
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void ListBoxbj_SelectedIndexChanged(object sender, EventArgs e)
    {
        //查询学生学号、姓名、自测次数、平均成绩，未完待续
        string sqltxt = "select xuehao,name  count(zice),avg(chengji) from tb_zice where studentid in(select studentid from tb_banjistudent where banjiid='+banjiid+') and kechengid= '" + kechengid + "' group by xuehao order by xuehao";  
        //BindBanjiStudent();
    }
    protected void BindBanjiStudent()
    {
        //string banjiid = ListBoxbj.SelectedValue;
        //DataTable dt = BanjiInfo.GetStudentNameAndUsername(int.Parse(banjiid));
        //ListBox1.DataSource = dt;
        //ListBox1.DataBind();
    }

}
