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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class manager_guanliyuanmanage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        string username = tbx_username.Text;
        string pwd = tbx_pwd.Text;
        string xibuid = ddl_xibu.SelectedValue;
        string xingbie=ddl_xb.SelectedValue;
        string xingming=tbx_xm.Text;
        string xibu=ddl_xibu.SelectedItem.Text;
        string sqltxt1="insert into tb_teacher(username,xingming,password,xingbie,xibuid,manager,guanlixibuid) values(@username,@xingming,@password,@xingbie,@xibuid,'manager',@xibuid)";
        SqlParameter[] pa = new SqlParameter[5];
        pa[0] = new SqlParameter("@username", username);
        pa[1] = new SqlParameter("@xingming", xingming);
        pa[2] = new SqlParameter("@password", pwd);
        pa[3] = new SqlParameter("@xingbie", xingbie);
        pa[4] = new SqlParameter("@xibuid", xibuid);
        string sqltxt2 = "update tb_teacher set manager='manager',guanlixibuid=@xibuid where username=@username";
        string sqltxt3 = "select count(*) from tb_teacher where username=@username";
        try
        {
            int n = (int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt3, pa));
            if (n > 0)
            {

                SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt2, pa);
                lbl_fankui.Text = "已将教师" + username + "升级为" + xibu + "的管理员！";
            }
            else
            {
                SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt1, pa);
                lbl_fankui.Text = "已添加教师用户：" + username + ",同时设置为" + xibu + "的管理员！";
            }
        }
        catch (Exception ex)
        {
            lbl_fankui.Text = "添加管理员失败！原因：" + ex.Message;
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (TeacherInfo.IsSuperManager(username))
        {
            MultiView1.ActiveViewIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script>alert('您无权添加管理员！');</script>", false);
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        MultiView1.ActiveViewIndex = 1;
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (TeacherInfo.IsSuperManager(username))
        {
            MultiView1.ActiveViewIndex = 2;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script>alert('您无权进行此操作！');</script>", false);
        }
    }
    protected void btn_upteacher_Click(object sender, EventArgs e)//将教师身份修改为系部管理员身份
    {

        lbl_fankui.Text = "";
        string xibuid = DDLView3xibu.SelectedValue;
        string username = DDL_v3_teacher.SelectedValue;
        if (TeacherInfo.IsSuperManager(username))
        {
            lbl_fankui.Text = "该教师是系统管理员，不能修改为系部管理员，操作失败！";
        }
        else
        {
            string txingming = DDL_v3_teacher.SelectedItem.Text;
            string xibuname = DDLView3xibu.SelectedItem.Text;
            string sqltxt = "update tb_teacher set manager='manager',guanlixibuid=@xibuid where username=@username";
            SqlParameter[] pa = new SqlParameter[2];
            pa[0] = new SqlParameter("@username", username);
            pa[1] = new SqlParameter("@xibuid", xibuid);
            try
            {
                SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt, pa);
                lbl_fankui.Text = "将教师：" + txingming + "修改为系部：" + xibuname + "的管理员，成功！";
            }
            catch (Exception ex)
            {
                lbl_fankui.Text = "将教师：" + txingming + "修改为系部：" + xibuname + "的管理员，失败，详情：" + ex.Message;
            }
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (TeacherInfo.IsSuperManager(username))
        {
            MultiView1.ActiveViewIndex = 3;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script>alert('您无权进行此操作！');</script>", false);
        }
    }
    protected void btn_delgly_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        string username = ddl_v4gly.SelectedValue;
        if (TeacherInfo.IsSuperManager(username))//判断是否系统管理员
        {
            lbl_fankui.Text = "该教师是系统管理员，不能删除其系统管理员身份！";
        }
        else
        {
            string xingming = ddl_v4gly.SelectedItem.Text;
            string xibuid = ddl_v4xibu.SelectedValue;
            string xibuname = ddl_v4xibu.SelectedItem.Text;
            string sqltxt = "update tb_teacher set manager='',guanlixibuid=-1 where username=@username";
            SqlParameter[] pa = new SqlParameter[1];
            pa[0] = new SqlParameter("@username", username);
            try
            {
                SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt, pa);
                lbl_fankui.Text = "已将" + xibuname + "的管理员" + xingming + "的管理员身份删除，保留其教师身份。";
            }
            catch (Exception ex)
            {
                lbl_fankui.Text = "管理员删除失败，详情：" + ex.Message;
            }
        }
    }
    protected void ddl_v4gly_DataBound(object sender, EventArgs e)
    {
        if (ddl_v4gly.Items.Count > 0)
            btn_delgly.Enabled = true;
        else
            btn_delgly.Enabled = false;
    }
    protected void DDL_v3_teacher_DataBound(object sender, EventArgs e)
    {
        if (DDL_v3_teacher.Items.Count > 0)
            btn_upteacher.Enabled = true;
        else
            btn_upteacher.Enabled = false;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
}
