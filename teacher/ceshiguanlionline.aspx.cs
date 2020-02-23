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

public partial class teachermanage_zuzhiceshi_studentceshimanage2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //获取交卷状态的值
            Label jiaojuanlabel=((Label)(e.Row.Cells[6].FindControl("Label1")));
            string jiaojuanzhuangtai=jiaojuanlabel.Text.Trim();
            if (jiaojuanzhuangtai == "已交卷")
            {
                ((LinkButton)(e.Row.Cells[8].FindControl("LinkButton1"))).Visible = false;
                ((LinkButton)(e.Row.Cells[8].FindControl("LinkButton2"))).Visible = true;

            }
            else
            {
                ((LinkButton)(e.Row.Cells[8].FindControl("LinkButton1"))).Visible = true;
                ((LinkButton)(e.Row.Cells[8].FindControl("LinkButton2"))).Visible = false;
            }
            Label yunxulabel = ((Label)(e.Row.Cells[7].FindControl("Label2")));
            string yunxuzhuangtai = yunxulabel.Text.Trim();
            if (yunxuzhuangtai == "允许")
            {
                ((LinkButton)(e.Row.Cells[9].FindControl("LinkButton3"))).Visible = false;
                ((LinkButton)(e.Row.Cells[9].FindControl("LinkButton4"))).Visible = true;
            }
            else
            {
                ((LinkButton)(e.Row.Cells[9].FindControl("LinkButton3"))).Visible = true;
                ((LinkButton)(e.Row.Cells[9].FindControl("LinkButton4"))).Visible = false;
            }

        }
    }
    protected void LinkButton1_Command(object sender, CommandEventArgs e)//设为已交卷
    {
        int stks_id =int.Parse(e.CommandArgument.ToString());
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set jiaojuan='已交卷',jiaojuanshijian=getdate() where stks_id=" + stks_id;
        conn.Open();
        if (comm.ExecuteNonQuery() > 0)
        {
            GridView1.DataBind();

        }
        conn.Close();

    }
    protected void LinkButton2_Command(object sender, CommandEventArgs e)//设为未交卷
    {
        int stks_id = int.Parse(e.CommandArgument.ToString());
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set jiaojuan='未交卷',jiaojuanshijian=null where stks_id=" + stks_id;
        conn.Open();
        if(comm.ExecuteNonQuery()>0)
            GridView1.DataBind();
        conn.Close();

    }
    protected void LinkButton3_Command(object sender, CommandEventArgs e)//允许做题
    {
        int stks_id = int.Parse(e.CommandArgument.ToString());
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set yunxu='允许' where stks_id=" + stks_id;
        conn.Open();
        if (comm.ExecuteNonQuery()>0)
            GridView1.DataBind();
        conn.Close();

    }
    protected void LinkButton4_Command(object sender, CommandEventArgs e)//不允许做题
    {
        int stks_id = int.Parse(e.CommandArgument.ToString());
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set yunxu='禁止' where stks_id=" + stks_id;
        conn.Open();
        if (comm.ExecuteNonQuery()>0)
            GridView1.DataBind();
        conn.Close();
    }
    protected void LinkButton5_Command(object sender, CommandEventArgs e)//延长5分钟
    {
        int stks_id = int.Parse(e.CommandArgument.ToString());
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set timeyanchang=timeyanchang+5 where stks_id=" + stks_id;
        conn.Open();
        if (comm.ExecuteNonQuery()>0)
            GridView1.DataBind();
        conn.Close();

    }
    protected void LinkButton6_Command(object sender, CommandEventArgs e)//全部延长5分钟
    {
        string shijuanid = e.CommandArgument.ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set timeyanchang=timeyanchang+5 where shijuanid=" +shijuanid;
        conn.Open();
        if (comm.ExecuteNonQuery() > 0)
        {
            GridView1.DataBind();
            FormView1.DataBind();
        }
        conn.Close();
    }
    protected void LinkButton7_Command(object sender, CommandEventArgs e)//全部禁止做题
    {
        string shijuanid = e.CommandArgument.ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_teachershijuan set yunxuzuoti='禁止' where shijuanid=" + shijuanid;
            comm.ExecuteNonQuery();
            comm.CommandText = "update tb_studentkaoshi set yunxu='禁止' where shijuanid=" + shijuanid;
            comm.ExecuteNonQuery();
            st.Commit();
            GridView1.DataBind();
            FormView1.DataBind();
        }
        catch
        {
            st.Rollback();
        }
        finally
        {
            conn.Close();
        }
    }
    protected void LinkButton8_Command(object sender, CommandEventArgs e)//全部允许做题
    {
        string shijuanid = e.CommandArgument.ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_teachershijuan set yunxuzuoti='允许' where shijuanid=" + shijuanid;
            comm.ExecuteNonQuery();
            comm.CommandText = "update tb_studentkaoshi set yunxu='允许' where shijuanid=" + shijuanid;
            comm.ExecuteNonQuery();
            st.Commit();
            GridView1.DataBind();
            FormView1.DataBind();
        }
        catch
        {
            st.Rollback();
        }
        finally
        {
            conn.Close();
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void LinkButtonjiaojuan_Click(object sender, EventArgs e)//全部设为已交卷
    {
        string shijuanid = Request.QueryString["shijuanid"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_studentkaoshi set jiaojuanshijian='"+DateTime.Now.ToString()+"',jiaojuan='已交卷' where shijuanid=" + shijuanid;
            comm.ExecuteNonQuery();
            st.Commit();
            GridView1.DataBind();
            FormView1.DataBind();
        }
        catch
        {
            st.Rollback();
        }
        finally
        {
            conn.Close();
        }
    }
    protected void LBtnweijiaojuan_Click(object sender, EventArgs e)//全部设为未交卷
    {
        string shijuanid = Request.QueryString["shijuanid"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_studentkaoshi set jiaojuanshijian=null,jiaojuan='未交卷' where shijuanid=" + shijuanid;
            comm.ExecuteNonQuery();
            st.Commit();
            GridView1.DataBind();
            FormView1.DataBind();
        }
        catch
        {
            st.Rollback();
        }
        finally
        {
            conn.Close();
        }
    }
    protected void LinkButton11_Click(object sender, CommandEventArgs e)//清空学生IP地址
    {
        int stks_id = int.Parse(e.CommandArgument.ToString());
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set [ip]='' where stks_id=" + stks_id;
        conn.Open();
        if (comm.ExecuteNonQuery() > 0)
            GridView1.DataBind();
         conn.Close();
    }
}
