using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class teachermanage_ceshipigaikgt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string shijuanid = Request.QueryString["shijuanid"];
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select tb_teachershijuan.ceshiname,tb_banji.banjiname,tb_teachershijuan.mingtifangshi from tb_teachershijuan inner join tb_banji on tb_banji.banjiid=tb_teachershijuan.banjiid where tb_teachershijuan.shijuanid=" + shijuanid;
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                Labelcsmc.Text = sdr.GetString(0).Trim();
                Labelbj.Text = sdr.GetString(1).Trim();
                Labelmtfs.Text = sdr.GetString(2).Trim();
            }
            sdr.Close();
            conn.Close();
        }
    }
    protected void DetailsView1_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        string studentusername = ListBox1.SelectedValue;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "update tb_studentkaoshi set zongfen=(select sum(defen) from tb_studentkaoshiti where shijuanid=" + shijuanid + " and studentusername='" + studentusername + "') where shijuanid=" + shijuanid + " and studentusername='" + studentusername + "'";
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    protected void ddl_defen_DataBinding(object sender, EventArgs e)
    {
        Label4.Text = "";
        try
        {
            int fenzhi = int.Parse(DetailsView1.Rows[8].Cells[1].Text.Trim());
            DropDownList defendrop = (DropDownList)(DetailsView1.FindControl("ddl_defen"));
            if (defendrop != null)
            {
                if (defendrop.Items.Count == 0)
                {
                    for (int i =fenzhi; i >= 0 ; i--)
                    {
                        defendrop.Items.Add(i.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Label4.Text = ex.Message;
        }
    }
}
