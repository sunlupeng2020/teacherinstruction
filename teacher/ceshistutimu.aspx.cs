using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class teachermanage_ceshistutimu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string shijuanid = Request.QueryString["shijuanid"];
            string stuun = Request.QueryString["studentusername"];
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select tb_teachershijuan.ceshiname,tb_banji.banjiname from tb_teachershijuan inner join tb_banji on tb_banji.banjiid=tb_teachershijuan.banjiid where tb_teachershijuan.shijuanid=" +shijuanid;
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                Labelcsmc.Text = sdr.GetString(0);
                Labelbj.Text = sdr.GetString(1);
            }
            sdr.Close();
            comm.CommandText = "select xingming from tb_student where username='" + stuun + "'";
            Labelxm.Text = comm.ExecuteScalar().ToString();
            conn.Close();
            Labelun.Text = stuun;
        }
    }
    protected void GridView3_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        string stun = Request.QueryString["studentusername"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        comm.CommandText = "update tb_studentkaoshi set zongfen=(select sum(defen) from tb_studentkaoshiti where studentusername='" + stun + "' and shijuanid=" + shijuanid + ") where studentusername='" + stun + "' and shijuanid=" + shijuanid;
        comm.ExecuteNonQuery();
        conn.Close();
        GridView3.DataBind();
    }
}
