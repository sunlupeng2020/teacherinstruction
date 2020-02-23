using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

public partial class dayionline_Mywenti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            string usershenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
            BindMywenti();
        }
        catch
        {
            Response.Redirect("default.aspx");
        }   
    }
    protected void BindMywenti()
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string usershenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
        DataTable dt = WentiInfo.GetWenti(username, usershenfen);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindMywenti();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }
}
