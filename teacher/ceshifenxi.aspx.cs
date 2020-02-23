using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class teachermanage_ceshifenxi : System.Web.UI.Page
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
                 Labelceshiname.Text = sdr.GetString(0).Trim();
                Labelbj.Text = sdr.GetString(1).Trim();
                Labelmtfs.Text = sdr.GetString(2).Trim();
            }
            sdr.Close();
            conn.Close();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            conn.Open();
            string questionid = e.Row.Cells[0].Text.Trim();
            comm.CommandText = "select sum(fenzhi),sum(defen),count(questionid) from tb_studentkaoshiti where shijuanid=" + Request.QueryString["shijuanid"] + " and questionid=" + questionid;
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(1).ToString().Trim().Length > 0 && sdr.GetValue(0).ToString().Trim().Length > 0)
                    ((Label)(e.Row.Cells[5].FindControl("Label3"))).Text = ((int)((double)(sdr.GetInt32(1) * 100) / (double)(sdr.GetInt32(0)))).ToString() + "%";
                else
                {
                    ((Label)(e.Row.Cells[5].FindControl("Label3"))).Text = "";
                }
                ((Label)(e.Row.Cells[4].FindControl("Label5"))).Text = sdr.GetInt32(2).ToString();
            }
            sdr.Close();
            comm.CommandText = "select jiegouname from tb_kechengjiegou where kechengjiegouid in(select kechengjiegouid from tb_timuzhishidian where questionid=" + questionid + ")";
            sdr = comm.ExecuteReader();
            Label l1 = (Label)(e.Row.Cells[6].FindControl("Label4"));
            l1.Text = "";
            while (sdr.Read())
            {
                l1.Text += sdr.GetString(0) + ",";
            }
            sdr.Close();
            conn.Close();
        }
    }
}
