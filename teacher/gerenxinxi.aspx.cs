using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class teachermanage_gerenxinxi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string oldpwd = TextBox1.Text;
        string newpwd = TextBox2.Text;
        string pwdindb = string.Empty;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select [password] from tb_teacher where [username]='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "'";
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            pwdindb = sdr.GetString(0).Trim();
        }
        sdr.Close();
        if (oldpwd == pwdindb)
        {
            comm.CommandText = "update tb_teacher set [password]='" + newpwd + "' where [username]='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "'";
            if (comm.ExecuteNonQuery() > 0)
                Label1.Text = "密码修改成功！";
            else
                Label1.Text = "密码修改失败！";
        }
        else
        {
            Label1.Text = "旧密码输入错误！";
        }
        conn.Close();
    }
}
