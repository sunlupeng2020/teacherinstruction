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
using System.Collections.Generic;
using System.Data.SqlTypes;
using Microsoft.ApplicationBlocks.Data;

public partial class teachermanage_yuzhizuoye : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindKechengZuoye();

    }
    protected bool CheckZuoyeName(int kechengid, string zuoyename, string username)//检查一个用户某课程某作业名称是否存在
    {
        bool yicunzai = false;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyeid from teacherzuoye where zuoyename='" + zuoyename + "' and teacherusername='" + username + "' and kechengid=" + kechengid;
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.HasRows)
        {
            yicunzai = true;
        }
        sdr.Close();
        conn.Close();
        return yicunzai;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        lbl_fankui.Text = "";
        string zuoyename = TextBoxzuoyename.Text.Trim();
        string tusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        try
        {
            conn.Open();
            comm.CommandText = "insert into teacherzuoye(zuoyename,teacherusername,kechengid,createtime) values(@zuoyename,@tusername,@kechengid,@createtime)";
            SqlParameter sqlzuoyename = new SqlParameter("@zuoyename", zuoyename);
            SqlParameter sqltusername = new SqlParameter("@tusername", tusername);
            SqlParameter sqlkechengid = new SqlParameter("@kechengid", kechengid);
            SqlParameter sqlctime = new SqlParameter("@createtime", DateTime.Now);
            comm.Parameters.Add(sqlzuoyename);
            comm.Parameters.Add(sqlctime);
            comm.Parameters.Add(sqltusername);
            comm.Parameters.Add(sqlkechengid);
            comm.ExecuteNonQuery();
            GridView1.DataBind();
            lbl_fankui.Text = "作业:" + zuoyename + "创建成功！";
            BindKechengZuoye();
        }
        catch (Exception ex)
        {
            lbl_fankui.Text = "作业:" + zuoyename + "创建失败！" + ex.Message;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

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
                ((HyperLink)(e.Row.Cells[6].FindControl("HyperLink2"))).Enabled = false;
                ((LinkButton)(e.Row.Cells[4].FindControl("LinkButton1"))).Enabled = false;
            }
            if (ZuoyeInfo.GetTimuCount(zuoyeid) <= 0)
            {
                ((LinkButton)(e.Row.Cells[7].FindControl("LinkButton2"))).Enabled = false;
            }
            else
            {
                ((LinkButton)(e.Row.Cells[7].FindControl("LinkButton2"))).Enabled = true;
            }

        }
    }
    protected void BindKechengZuoye()
    {
        string tusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        DataTable dt = TeacherInfo.GetTeacherKechengZuoye(tusername, kechengid);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string zuoyeid = ((LinkButton)sender).CommandArgument.Trim();
        string sqltxt = "delete from teacherzuoye where zuoyeid=@zuoyeid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@zuoyeid", zuoyeid);
        SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt, pa);
        BindKechengZuoye();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindKechengZuoye();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string zuoyeid = ((LinkButton)sender).CommandArgument.ToString();
        Response.Redirect("zuoye_id_buzhi01.aspx?zuoyeid=" + zuoyeid);
    }
}
