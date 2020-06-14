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
   
   
    protected void ListBoxbj_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBanjiStudent();
    }
    protected void BindBanjiStudent()
    {
        string banjiid = ListBoxbj.SelectedValue;
        DataTable dt = BanjiInfo.GetStudentNameAndUsername(int.Parse(banjiid));
        ListBox1.DataSource = dt;
        ListBox1.DataBind();
    }

}
