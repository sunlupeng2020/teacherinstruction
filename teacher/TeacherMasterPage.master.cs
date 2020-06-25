using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class TeacherMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (Session["kechengid"] == null)
        {

            int minkechengid = TeacherInfo.GetTeacherRenke_min_kechengid(username);
            if (minkechengid > 0)
            {
                Session["kechengid"] = minkechengid;
            }
            else if (KechengInfo.GetKechengCount() > 0)
                Session["kechengid"] = KechengInfo.MinKechengid();
            else
            {
                List<string> noneKechengPage = new List<string>();//不需要课程的页面
                noneKechengPage.Add("createcourse.aspx");
                noneKechengPage.Add("help.aspx");
                noneKechengPage.Add("addbanji.aspx");
                noneKechengPage.Add("default.aspx");
                noneKechengPage.Add("studentdaoru.aspx");
                noneKechengPage.Add("wanshanxinxi.aspx");
                noneKechengPage.Add("addstudent.aspx");
                noneKechengPage.Add("editstudent.aspx");
                noneKechengPage.Add("editstudentinfo.aspx");
                noneKechengPage.Add("gerenxinxi.aspx");
                noneKechengPage.Add("kcxianyou.aspx");//现有课程列表
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                string pageurl = url.Substring(url.LastIndexOf("/") + 1).ToLower();
                if (!noneKechengPage.Contains(pageurl))
                {
                    Response.Redirect("createcourse.aspx");
                }
            }
        }
        if (!IsPostBack)
        {
            Literal2.Text =TeacherInfo.TeacherXingMing(username) + "老师.";
            if (Session["kechengid"] != null)
            {
                lbl_kecheng.Text = KechengInfo.getKechengname(DropDownList1.SelectedValue = Session["kechengid"].ToString());
            }
            else
            {
                lbl_kecheng.Text = "未知";
            }
            lbtn_qiehuan.Visible = TeacherInfo.IsManager(username);//是否显示身份切换链接
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
            Response.Redirect("../login.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)//切换课程
    {
        Session["kechengid"] = DropDownList1.SelectedValue;
        lbl_kecheng.Text = DropDownList1.SelectedItem.Text;
        string pageuri = Request.Url.AbsoluteUri;
        Response.Redirect(pageuri);
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count <= 0)
            Button1.Enabled = false;
        else
            Button1.Enabled = true;

    }
    protected void LinkButton1_Click(object sender, EventArgs e)//身份切换
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string shenfen = "manager";
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
        Response.Redirect("../manager/teachermanage.aspx");
    }
}