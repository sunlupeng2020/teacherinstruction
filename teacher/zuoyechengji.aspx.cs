using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class teachermanage_zuoyechengji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["zuoyeid"] != null && Request.QueryString["banjiid"] != null)
            {
                Labelbanji.Text = BanjiInfo.GetBanjiName(int.Parse(Request.QueryString["banjiid"]));
                Labelzuoyename.Text = ZuoyeInfo.getZuoyeName(int.Parse(Request.QueryString["zuoyeid"]));
                DataTable dt = ZuoyeInfo.GetQuantiStuZuoyeXinxi(int.Parse(Request.QueryString["banjiid"]), int.Parse(Request.QueryString["zuoyeid"]));
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
                Response.Redirect("default.aspx");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            Label pylbl =(Label)(e.Row.FindControl("Label1"));
            if (pylbl != null)
            {
                string py = pylbl.Text;
                if (py.Length > 20)
                {
                    pylbl.Text = py.Substring(0, 15);
                }
            }
        }
    }
}
