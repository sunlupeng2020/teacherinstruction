using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class dayionline_MasterPageDayi : System.Web.UI.MasterPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["kechengid"] == null)
        {
            if (KechengInfo.GetKechengCount() > 0)
            {
                Session["kechengid"] = KechengInfo.MinKechengid().ToString();
            }
            else
            {
                Response.Redirect("../default.aspx");
            }
        }
        label_kecheng.Text = KechengInfo.getKechengname(Session["kechengid"].ToString());
        string username = string.Empty;
        string usershenfen = string.Empty;
        try
        {
            username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            usershenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
        }
        catch
        {
        }
        if (username != string.Empty && usershenfen != string.Empty)
        {
            Literal2.Text = username;
            user_login.Visible = false;
            user_logout.Visible = true;
        }
        else
        {
            Literal2.Text = "游客.";
            user_login.Visible = true;
            user_logout.Visible = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        

        //检测学生是否正在参加考试，若是，则不显示页面内容
        //HttpCookie zhuangtaicookie = Request.Cookies["zhuangtai"];
        //if (zhuangtaicookie != null)
        //{
        //    if (zhuangtaicookie.Value == "kaoshi")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('检测到您正在考试，请交卷后再打开学习页面！');", true);
        //        ContentPlaceHolder1.Visible = false;
        //    }
        //}
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count <= 0)
            Button1.Enabled = false;
        else
            Button1.Enabled = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["kechengid"] = DropDownList1.SelectedValue;
        label_kecheng.Text = DropDownList1.SelectedItem.Text;
        string pageuri = Request.Url.AbsoluteUri;
        Response.Redirect(pageuri);
    }
}
