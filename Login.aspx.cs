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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Buttonloginin_Click(object sender, EventArgs e)
    {
        Labelerror.Text = "";
        bool yonghukeyong = true;
        string username = TextBoxusername.Text.Trim();
        string password = TextBoxpassword.Text.Trim();
        string shenfen = RadioButtonListshenfen.SelectedValue.ToString().Trim();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        bool chenggong = false;
        SqlDataReader sdr;
        conn.Open();
        if (shenfen == "teacher")
        {
            comm.CommandText = "select [password],[keyong] from [tb_teacher] where [username]=@username";
            comm.Parameters.AddWithValue("@username", username);
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetString(0).Trim() == password)
                {
                    chenggong = true;
                }
                if (sdr.GetString(1).Trim() == "否")
                {
                    yonghukeyong = false;
                }
            }
            sdr.Close();
        }
        else if (shenfen == "student")
        {
            comm.CommandText = "select [password] from [tb_Student] where [username]= @username";
            comm.Parameters.AddWithValue("@username", username);
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetString(0).Trim() == password)
                    chenggong = true;
            }
            sdr.Close();
        }
        else//管理员
        {
            comm.CommandText = "select [password],[manager],[keyong] from [tb_teacher] where [username]=@username";
            comm.Parameters.AddWithValue("@username", username);
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                string userpassword = sdr.GetString(0);
                if (!sdr.IsDBNull(1))
                {
                    string ismanager = sdr.GetString(1);
                    if (userpassword == password && (ismanager == "manager" || ismanager == "supermanager"))
                        chenggong = true;
                }
                if (sdr.GetString(2).Trim() == "否")
                {
                    yonghukeyong = false;
                }
            }
            sdr.Close();
        }
        conn.Close();
        comm.Dispose();
        //创建认证票据
        if (chenggong && yonghukeyong)
        {
            FormsAuthentication.Initialize();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,//票据版本号
                username,//票据持有者
                DateTime.Now,//分配票据的时间
                DateTime.Now.AddHours(3),//失效时间
                true,//需要用户的cookie
                shenfen,//用户的角色
                FormsAuthentication.FormsCookiePath);//cookie有效路径
            string hash = FormsAuthentication.Encrypt(ticket);//加密
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);//加密之后的cookie
            cookie.Expires = ticket.Expiration;
            Session["username"] = username;            
            //添加cookie到页面请求响应中
            Response.Cookies.Add(cookie);
            string returnurl = "";
            if (shenfen == "teacher")
                returnurl = "teacher/Default.aspx";
            else if (shenfen == "student")
            {
                if (KechengInfo.GetKechengCount() > 0)
                {
                    Session["kechengid"] = KechengInfo.MinKechengid();
                }
                returnurl = "student/Default.aspx";
            }
            else if (shenfen == "manager")
                returnurl = "manager/teachermanage.aspx";
            Response.Redirect(returnurl);
        }
        else
        {
            if (!yonghukeyong)
            {
                Labelerror.Text = "该用户已被禁用，请与管理员联系！";
            }
            else
            {
                Labelerror.Text = "用户名或密码错误！请重试！";
            }
        }
    }
}
