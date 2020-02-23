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

public partial class studentstudy_ceshichaxun : System.Web.UI.Page
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
        comm.CommandText = "select tb_studentkaoshi.shijuanid as 测试ID,tb_teachershijuan.ceshiname 测试名称,tb_studentkaoshi.zongfen 得分,tb_studentkaoshi.jiaojuanshijian  测试时间 from tb_studentkaoshi inner join tb_teachershijuan on tb_studentkaoshi.shijuanid=tb_teachershijuan.shijuanid where tb_studentkaoshi.kechengid=" + kechengid + " and tb_studentkaoshi.studentusername='" + stuusername + "' order by jiaojuanshijian desc";
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
        comm.CommandText = "select tb_studentkaoshiti.timuhao 题号,tb_tiku.timu 题目,tb_tiku.[type] 题型,tb_tiku.answer 参考答案,tb_studentkaoshiti.answer 回答,tb_tiku.filepath 资源文件,tb_studentkaoshiti.filepath 我的文件,tb_studentkaoshiti.defen 得分 from tb_studentkaoshiti inner join tb_tiku on tb_studentkaoshiti.questionid=tb_tiku.questionid where tb_studentkaoshiti.shijuanid=" + ceshiid + " and tb_studentkaoshiti.studentusername='" + stuusername + "' order by 题号";
        sda = new SqlDataAdapter(comm);
        dt = new DataTable();
        sda.Fill(dt);
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)//
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Server.HtmlDecode(e.Row.Cells[1].Text);
            string ziyuanwenjian = e.Row.Cells[5].Text;
            if (ziyuanwenjian.Trim().Length > 4)
            {
                e.Row.Cells[5].Text = "<a href='" + Server.MapPath(ziyuanwenjian) + "' target='_blank'>下载</a>";
            }
            string myfile = e.Row.Cells[6].Text;
            if (myfile.Trim().Length > 4)
            {
                e.Row.Cells[6].Text = "<a href='" + Server.MapPath(myfile) + "' target='_blank'>下载</a>";
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.DataSource = xianshiceshi();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            //是否显示成绩，能否查看题目详情
            string yunxuchakan = "";
            string jiaojuan = "";
            string shijuanid = ((LinkButton)e.Row.Cells[5].FindControl("LinkButton1")).CommandArgument;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select yunxuchakan from tb_teachershijuan where shijuanid=" + shijuanid;
            conn.Open();
            yunxuchakan = comm.ExecuteScalar().ToString().Trim();
            comm.CommandText = "select jiaojuan from tb_studentkaoshi where studentusername='" + stuusername + "' and shijuanid=" + shijuanid;
            jiaojuan = comm.ExecuteScalar().ToString().Trim();
            conn.Close();
            if (jiaojuan == "已交卷" && yunxuchakan == "允许")
            {
                ((LinkButton)e.Row.Cells[5].FindControl("LinkButton1")).Enabled = true;
                ((Label)e.Row.Cells[4].FindControl("Label2")).Visible = true;
            }
            else
            {
                ((LinkButton)e.Row.Cells[5].FindControl("LinkButton1")).Enabled = false;
                ((Label)e.Row.Cells[4].FindControl("Label2")).Visible = false;
            }
        }
    }
}
