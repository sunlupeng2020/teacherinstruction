using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class teachermanage_zuoyebjqthz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Labelbanji.Text =BanjiInfo.GetBanjiName(int.Parse(Request.QueryString["banjiid"]));
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
    }
}
