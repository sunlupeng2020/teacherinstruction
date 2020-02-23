using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;


public partial class manager_任课管理 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        string banjiid = ddl_banji.SelectedValue;
        string kechengid = ddl_kecheng.SelectedValue;
        string tusername=ddl_teacher.SelectedValue;
        string kechengname=ddl_kecheng.SelectedItem.Text;
        string banjiname=ddl_banji.SelectedItem.Text;
        string txingming=ddl_teacher.SelectedItem.Text;
        string teacherusername = RenkeInfo.GetTeacherusername(kechengid, banjiid);
        if (teacherusername != string.Empty)
        {
            lbl_fankui.Text = "该课程已由" +TeacherInfo.TeacherXingMing(teacherusername) + "担任,添加任课信息失败！";
        }
        else
        {
            string sqltxt = "insert into tb_teacherrenke(teacherusername,banjiid,kechengid,begintime) values(@tusername,@banjiid,@kechengid,@begintime)";
            SqlParameter[] pa = new SqlParameter[4];
            pa[0] = new SqlParameter("@tusername", tusername);
            pa[1] = new SqlParameter("@banjiid", banjiid);
            pa[2] = new SqlParameter("@kechengid", kechengid);
            pa[3] = new SqlParameter("@begintime", DateTime.Now.ToString());
            if (SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt, pa) > 0)
            {
                lbl_fankui.Text = banjiname + "的课程:" + kechengname + "由教师：" + txingming + "担任，添加成功！";
            }
            else
            {
                lbl_fankui.Text = banjiname + "的课程:" + kechengname + "由教师：" + txingming + "担任，添加失败！";
            }
        }
    }
    protected void ddl_xibu_DataBound(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (!TeacherInfo.IsSuperManager(username))
        {
            string guanlixibuid = TeacherInfo.managerXibu(username);
            if (guanlixibuid != "0")
            {
                ddl_xibu.SelectedValue = guanlixibuid;
                ddl_xibu.Enabled = false;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        grv_v2renke.DataBind();
    }
    protected void grv_v2renke_DataBound(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (!TeacherInfo.IsSuperManager(username))
        {
            string guanlixibuid = TeacherInfo.managerXibu(username);
            if (guanlixibuid != ddl_v2xibu.SelectedValue)
            {
                grv_v2renke.Columns[3].Visible = false;
            }
        }
    }
    protected void grv_v2renke_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lbl_fankui.Text="";
        string renkeid = e.Keys[0].ToString();
        if (RenkeInfo.IsTeacherHaveCeshi(renkeid))
        {
            lbl_fankui.Text = "教师已经为该班布置了测试项目，删除失败！";
            e.Cancel=true;
        }
        if(RenkeInfo.IsTeacherHaveZuoye(renkeid))
        {
            lbl_fankui.Text = "教师已经为该班布置了作业，删除失败！";
            e.Cancel=true;
        }
    }
    protected void Btnv3_banjirenke_Click(object sender, EventArgs e)
    {
        grv_v3renke.DataBind();
    }
    protected void grv_v3renke_DataBound(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (!TeacherInfo.IsSuperManager(username))
        {
            string guanlixibuid = TeacherInfo.managerXibu(username);
            if (guanlixibuid != ddl_v3xibu.SelectedValue)
            {
                grv_v3renke.Columns[3].Visible = false;
            }
        }
    }
    protected void grv_v3renke_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lbl_fankui.Text = "";
        string renkeid = e.Keys[0].ToString();
        if (RenkeInfo.IsTeacherHaveCeshi(renkeid))
        {
            lbl_fankui.Text = "教师已经为该班布置了测试项目，删除失败！";
            e.Cancel = true;
        }
        if (RenkeInfo.IsTeacherHaveZuoye(renkeid))
        {
            lbl_fankui.Text = "教师已经为该班布置了作业，删除失败！";
            e.Cancel = true;
        }
    }
    protected void btn_v4renke_Click(object sender, EventArgs e)
    {
        grv_v4renke.DataBind();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lbl_fankui.Text = "";
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        lbl_fankui.Text = "";

    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
        lbl_fankui.Text = "";

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;
        lbl_fankui.Text = "";

    }
}
