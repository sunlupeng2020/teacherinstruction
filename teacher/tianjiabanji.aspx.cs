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

public partial class manager_tianjiabanji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string xibuid = DropDownList1.SelectedValue.Trim();
        string banjiname = TextBox1.Text.Trim();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select count(banjiid) from tb_banji where banjiname='" + banjiname + "'";
        conn.Open();
        int banjishu = (int)(comm.ExecuteScalar());
        conn.Close();
        if (banjishu > 0)
        {
            Labelfankui.Text = "该班级已存在，请使用其他名称。";
            return;
        }
        comm.CommandText = "insert into tb_banji(xibuid,banjiname,createtime,teacherusername) values(" + xibuid + ",'" + banjiname + "','" + DateTime.Now.ToString() + "','" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "')";
        try
        {
            conn.Open();
            if (comm.ExecuteNonQuery() > 0)
            {
                Labelfankui.Text = "添加班级成功！可前往<a href='studentdaoru.aspx'>导入学生信息</a>。";
                //ListBox1.DataBind();
            }
            else
                Labelfankui.Text = "添加班级失败！";
        }
        catch (Exception ex)
        {
            Labelfankui.Text = "添加班级失败！原因：" + ex.Message;
        }
        finally
        {
            conn.Close();
        }
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (!TeacherInfo.IsSuperManager(username))
        {
            string guanlixibuid = TeacherInfo.managerXibu(username);
            if (guanlixibuid != "0")
            {
                DropDownList1.SelectedValue = guanlixibuid;
                DropDownList1.Enabled = false;
            }
        }
    }
}
