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
public partial class YoukeMasterPage : System.Web.UI.MasterPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Request.QueryString["kechengid"] != null)
        {
            Session["kechengid"] = Request.QueryString["kechengid"].Trim();
        }
        else
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
        }
        literal_kecheng.Text = KechengInfo.getKechengname(Session["kechengid"].ToString()); ;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["kechengid"] = DropDownList1.SelectedValue;
        literal_kecheng.Text = DropDownList1.SelectedItem.Text;
        string pageuri = Request.Url.AbsolutePath;
        Response.Redirect(pageuri);
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count <= 0)
            LinkButton1.Enabled = false;
        else
            LinkButton1.Enabled = true;

    }
}
