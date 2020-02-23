using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


public partial class teachermanage_ceshiqtzice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string banjiid = Request.QueryString["banjiid"];
            Labelbj.Text = BanjiInfo.GetBanjiName(int.Parse(banjiid));
        }

    }
    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        string stuun = "";
        string kechengid = Session["kechengid"].ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        SqlDataReader sdr;
        conn.Open();
        foreach (GridViewRow row in GridView2.Rows)
        {
            stuun = row.Cells[2].Text.Trim();
            comm.CommandText = "select count(ziceid),avg(fenshu),max(fenshu),min(fenshu) from tb_zice where kechengid=" + kechengid + " and username='" + stuun + "'";
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                ((Literal)(row.Cells[4].FindControl("Literal1"))).Text = sdr.GetValue(0).ToString();//自测次数
                ((Literal)(row.Cells[5].FindControl("Literal2"))).Text = sdr.GetValue(2).ToString();//最高分
                ((Literal)(row.Cells[6].FindControl("Literal3"))).Text = sdr.GetValue(3).ToString();//最低分
                if (sdr.GetValue(1).ToString().Trim() != "")
                {
                    //((Literal)(row.Cells[7].FindControl("Literal4"))).Text = sdr.GetValue(1).ToString();//平均分
                    ((Literal)(row.Cells[7].FindControl("Literal4"))).Text = Math.Round(double.Parse(sdr.GetValue(1).ToString())).ToString();//平均分
                }
                else
                {
                    ((Literal)(row.Cells[7].FindControl("Literal4"))).Text = "0";
                }
            }
            sdr.Close();
        }
        conn.Close();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string studentusername =((LinkButton)sender).CommandArgument;
        string banjiid = Request.QueryString["banjiid"];
        string urlx = "ceshistuzice.aspx?banjiid=" + banjiid + "&studentusername=" + studentusername;
        string URL = "<script  language= 'javascript'> window.open('" + urlx + "','_blank');</script>";
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "stucsxx", URL, false);
    }
}
