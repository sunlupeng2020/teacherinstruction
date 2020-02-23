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

public partial class teachermanage_zuoyemanage_zuoyefenxi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["zuoyeid"] != null && Request.QueryString["banjiid"] != null)
            {
                Labelbanji.Text = BanjiInfo.GetBanjiName(int.Parse(Request.QueryString["banjiid"]));
                Labelzuoyename.Text = ZuoyeInfo.getZuoyeName(int.Parse(Request.QueryString["zuoyeid"]));
                BindFenxi();
                BindTimu();
                Labelsjrs.Text = ZuoyeInfo.GetZuoyeShangjiaoRenShu(int.Parse(Request.QueryString["zuoyeid"]), int.Parse(Request.QueryString["banjiid"])).ToString();
            }
            else
                Response.Redirect("default.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void BindTimu()
    {
        DataTable dt = ZuoyeInfo.GetTeacherZuoyeTimuOrderByTixing(int.Parse(Request.QueryString["zuoyeid"]));
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int banjiid=int.Parse(Request.QueryString["banjiid"]);
        int zuoyeid=int.Parse(Request.QueryString["zuoyeid"]);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            string questionid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[5].Text =string.Format("{0:P}", ZuoyeInfo.ZuoyeTimuDefenLv(banjiid, zuoyeid, int.Parse(questionid)));
            e.Row.Cells[6].Text = TimuInfo.TimuZhishidian(int.Parse(questionid));
        }
    }
    protected void BindFenxi()
    {
        DataTable dt = ZuoyeInfo.ZuoyeFenxi(int.Parse(Request.QueryString["banjiid"]), int.Parse(Request.QueryString["zuoyeid"]));
        FormView1.DataSource = dt;
        FormView1.DataBind();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }
}
