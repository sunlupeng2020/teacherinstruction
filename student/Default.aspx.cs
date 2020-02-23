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

public partial class studentstudy_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            DataTable dt = StudentInfo.GetStuZuoyeOnTime(stuusername);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BindCeshiOntime();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //检查是否超过上交期限，如果超过，则显示成绩，否则不显示
            DateTime shangjiaoqixian = DateTime.Parse(e.Row.Cells[3].Text);
            if (shangjiaoqixian <= DateTime.Now)
            {
                ((Label)(e.Row.Cells[6].FindControl("Label1"))).Visible = true;
            }
            else
            {
                ((Label)(e.Row.Cells[6].FindControl("Label1"))).Visible = false;
            }
        }
    }
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();//显示行号
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();//显示行号
        }
    }
    protected void BindCeshiOntime()
    {
        string kechengid = Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable dt = CeshiInfo.GetStuCeshi(username);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string kechengid = Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable dt = CeshiInfo.GetStuCeshi(username);
        GridView2.DataSource = dt;
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable dt = StudentInfo.GetStuZuoyeOnTime(stuusername);
        GridView1.DataSource = dt;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
}
