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

public partial class manager_MasterPageManager : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Literal1.Text =((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Clear();
        Response.Redirect("../default.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)//角色切换
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string shenfen = "teacher";
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
        //添加cookie到页面请求响应中
        Response.Cookies.Add(cookie);
        Response.Redirect("../teacher/default.aspx");
    }
}
