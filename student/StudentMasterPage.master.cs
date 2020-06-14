using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class StudentMasterPage : System.Web.UI.MasterPage
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
                Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            Literalxingming.Text = StudentInfo.GetStuXingming(stuusername);
        }
        lbl_kecheng.Text = KechengInfo.getKechengname(Session["kechengid"].ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {

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
}
