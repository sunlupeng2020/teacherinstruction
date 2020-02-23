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

public partial class dayionline_xuanzkecheng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindKecheng();
        } 
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["kechengid"] = ddl_kecheng.SelectedValue;
    }
    protected void BindKecheng()
    {
        DataSet ds= KechengInfo.GetKecheng();
        ddl_kecheng.DataSource = ds.Tables[0].DefaultView;
        ddl_kecheng.DataTextField = "kechengname";
        ddl_kecheng.DataValueField = "kechengid";
        ddl_kecheng.DataBind();
    }
}
