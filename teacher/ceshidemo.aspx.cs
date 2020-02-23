using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class teachermanage_ceshidemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string shijuanid = Request.QueryString["shijuanid"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select tb_teachershijuan.ceshiname,tb_banji.banjiname from tb_teachershijuan inner join tb_banji on tb_banji.banjiid=tb_teachershijuan.banjiid where tb_teachershijuan.shijuanid='" + shijuanid+"'";
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            Labelcsname.Text = sdr.GetString(0).Trim();
            Labelbj.Text = sdr.GetString(1).Trim();
        }
        sdr.Close();
        conn.Close();
    }
}
