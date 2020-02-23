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

public partial class teachermanage_zuzhiceshi_ceshichengji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["shijuanid"] != null)
        {
            string shijuanid = Request.QueryString["shijuanid"];
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            conn.Open();
            comm.CommandText = "select ceshiname from tb_teachershijuan where shijuanid=" + shijuanid;
            Label3.Text = comm.ExecuteScalar().ToString();
            comm.CommandText = "select banjiname from tb_banji where banjiid=(select banjiid from tb_teachershijuan where shijuanid=" + shijuanid + ")";
            Label2.Text = comm.ExecuteScalar().ToString();
            conn.Close();
        }
        else
        {
            Response.Redirect("ceshidefault.aspx");
        }
    }    
}
