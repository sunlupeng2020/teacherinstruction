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

public partial class play_sva : System.Web.UI.Page
{
    public string ziyuanfile;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = "select ziyuanfile from tb_jiaoxueziyuan where jiaoxueziyuanid=" + Request["id"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = sql;
        conn.Open();
        ziyuanfile = (comm.ExecuteScalar()).ToString();
        ziyuanfile = ziyuanfile.Replace("~", "..");
        conn.Close();
    }
}
