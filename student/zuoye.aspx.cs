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
public partial class studentstudy_zuoye : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //查找学生学习了哪些课程
        if (!IsPostBack)
        {
            BindZuoye();
        }
    }
    protected void BindZuoye()
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        DataTable dt = ZuoyeInfo.GetStuKechengZuoyeInfo(username, int.Parse(kechengid));
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
       protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //设置做作业和查看作业详情超链接的可用状态
        
        DateTime nowtime = DateTime.Now;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();//序号
            if ((DateTime.Parse(e.Row.Cells[4].Text.Trim()).CompareTo(nowtime) > 0) && (((Label)(e.Row.Cells[9].FindControl("Label2"))).Text.Trim() == "允许"))//未过期，并且允许做作业
            {
                ((HyperLink)(e.Row.Cells[7].Controls[0])).Enabled = true;
            }
            else
            {
                ((HyperLink)(e.Row.Cells[7].Controls[0])).Enabled = false;
            }
            //设置查看作业超链接的可用状态和成绩是否显示
            if (((Label)(e.Row.Cells[10].FindControl("Label3"))).Text.Trim() == "允许" && (DateTime.Parse(e.Row.Cells[4].Text.Trim()).CompareTo(nowtime) <= 0))//是否允许查看作业
            {
                ((HyperLink)(e.Row.Cells[8].Controls[0])).Enabled = true;//查看作业超链接
                ((Label)(e.Row.Cells[6].FindControl("Label1"))).Visible = true;//成绩
            }
            else
            {
                ((HyperLink)(e.Row.Cells[8].Controls[0])).Enabled = false;
                ((Label)(e.Row.Cells[6].FindControl("Label1"))).Visible = false;
            }
        }
        
    }
}
