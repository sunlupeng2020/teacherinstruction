using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class teachermanage_myzuoye : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindZuoye();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)//设置作业列表中各控件的可用状态，自动编号
    {
        if (e.Row.RowIndex != -1)
        {
            ((Label)(e.Row.Cells[0].FindControl("Lbl_zybh"))).Text = (e.Row.RowIndex + 1).ToString();
            string zuoyeid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString().Trim();
            if (ZuoyeInfo.IsZuoyeUsed(zuoyeid))//如果作业已布置，则不允许修改题目
            {
                ((HyperLink)(e.Row.Cells[5].FindControl("HyperLink1"))).Enabled = false;
                ((LinkButton)(e.Row.Cells[4].FindControl("LinkButton1"))).Enabled = false;
            }
            else
            {
                ((HyperLink)(e.Row.Cells[5].FindControl("HyperLink1"))).Enabled = true;
                ((LinkButton)(e.Row.Cells[4].FindControl("LinkButton1"))).Enabled = true;
            }
            
            if (ZuoyeInfo.GetTimuCount(zuoyeid) <= 0)
            {
                ((LinkButton)(e.Row.Cells[7].FindControl("LinkButton3"))).Enabled = false;
            }
            else
            {
                ((LinkButton)(e.Row.Cells[7].FindControl("LinkButton3"))).Enabled = true;
            }
        }
    }
     protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindZuoyeBuzhi();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void BindZuoyeBuzhi()
    {
        string zuoyeid = GridView1.SelectedValue.ToString();
        lbl_zuoyemingcheng.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
        DataTable dt = ZuoyeInfo.GetZuoyeBanji(zuoyeid);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        string zuoyeid = GridView1.SelectedValue.ToString();
        lbl_zuoyemingcheng.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
        DataTable dt = ZuoyeInfo.GetZuoyeBanji(zuoyeid);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void BindZuoye()
    {
        string kechengid = Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string sqltxt = "SELECT [zuoyeid], [zuoyename], [createtime], [manfen] FROM [teacherzuoye] WHERE (([kechengid] = @kechengid) AND ([teacherusername] = @teacherusername)) order by createtime desc";
        SqlParameter[] pa = new SqlParameter[2];
        pa[0] = new SqlParameter("@kechengid", kechengid);
        pa[1] = new SqlParameter("@teacherusername", username);
        GridView1.DataSource = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa);
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string zuoyeid =GridView1.DataKeys[e.RowIndex].Value.ToString();
        string sqltxt = "delete from teacherzuoye where zuoyeid=@zuoyeid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@zuoyeid", zuoyeid);
        SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt, pa);
        BindZuoye();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindZuoye();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string zuoyeid = ((LinkButton)sender).CommandArgument.ToString();
        Response.Redirect("zuoye_id_buzhi01.aspx?zuoyeid="+zuoyeid);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)//删除作业布置
    {
        try
        {
            string zuoyebuzhiid = ((LinkButton)sender).CommandArgument.ToString();
            //删除作业布置信息，删除学生作业，删除学生作业题目
            ZuoyeInfo.DeleteZuoyeFromBanji(int.Parse(zuoyebuzhiid));
            BindZuoyeBuzhi();
            GridView1.SelectedIndex = -1;
        }
        catch
        {
        }
    }
}
