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

public partial class student_myzice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataSource = xianshiceshi();
            GridView1.DataBind();
        }
    }
    protected DataTable xianshiceshi()
    {
        string kechengid = Session["kechengid"].ToString();
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        SqlDataAdapter sda;
        comm.CommandText = "select ziceid as 测试ID,ceshiname as 测试名称,fenshu as 得分,ceshitime as 测试时间 from tb_zice where (kechengid=" + kechengid + "  and  username='" + stuusername + "') order by ceshitime desc";
        sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)//显示某次测试的题目及得分等。
    {
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string ceshiid = GridView1.SelectedValue.ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        SqlDataAdapter sda;
        DataTable dt;
        comm.CommandText = "select tb_zicetimu.tihao 题号,tb_tiku.timu 题目,tb_tiku.[type] 题型,tb_tiku.answer 参考答案,tb_zicetimu.huida 回答 from tb_zicetimu inner join tb_tiku on tb_tiku.questionid=tb_zicetimu.questionid where tb_zicetimu.ziceid=" + ceshiid + " order by 题号";
        sda = new SqlDataAdapter(comm);
        dt = new DataTable();
        sda.Fill(dt);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
     protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.DataSource = xianshiceshi();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
     protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
         }
     }
}
