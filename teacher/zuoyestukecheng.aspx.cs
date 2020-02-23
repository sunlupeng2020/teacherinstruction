using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class teachermanage_zuoyestukecheng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Labelstuusername.Text = Request.QueryString["studentusername"];
            Labelbanji.Text =BanjiInfo.GetBanjiName(int.Parse(Request.QueryString["banjiid"]));
            Labelxingming.Text = StudentInfo.GetStuXingming(Request.QueryString["studentusername"]);
            BindZyoye();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
        //评语截断
        Label pyLabel = (Label)(e.Row.FindControl("Label1"));
        if (pyLabel != null)
        {
            string py = pyLabel.Text;
            if (py!=string.Empty&&py.Length > 15)
            {
                pyLabel.Text = py.Substring(0, 10) + "...";
            }
        }
    }
    protected void BindZyoye()
    {
        DataSet dt =StudentInfo.GetStuKechegZuoye( int.Parse(Session["kechengid"].ToString()),Request.QueryString["studentusername"]);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string zuoyeid = GridView1.SelectedValue.ToString();
        string studentusername = Request.QueryString["studentusername"];
        DataTable dt = StudentInfo.GetStuZuoyeTimu(int.Parse(zuoyeid), studentusername);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
    }
}
