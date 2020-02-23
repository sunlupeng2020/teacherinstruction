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
using Microsoft.ApplicationBlocks.Data;

public partial class teachermanage_studentmanage_xueshengzhuanban : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //    BindBanji();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)(e.Row.Cells[6].Controls[2])).Attributes.Add("onclick", "return confirm('确定要从该班级删除该学生吗？');");
        }

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    //protected void BindBanji()
    //{
    //    string kechengid = Session["kechengid"].ToString();
    //    string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
    //    string sqltxt = "SELECT [banjiname],banjiid FROM [tb_banji] WHERE banjiid in (select banjiid from tb_teacherrenke where kechengid=@kechengid and teacherusername=@username)";
    //    SqlParameter[] pa = new SqlParameter[2];
    //    pa[0] = new SqlParameter("@kechengid", kechengid);
    //    pa[1] = new SqlParameter("@username", username);
    //    DataSet ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa);
    //    DropDownList3.DataSource = ds;
    //    DropDownList3.DataBind();
    //}
    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    //删除学生后，删除学生的作业、测试信息
    //    string stuun = e.Values["studentusername"].ToString();
    //    SqlConnection conn = new SqlConnection();
    //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    //    SqlCommand comm = conn.CreateCommand();
    //    string kechengid = Session["kechengid"].ToString();
    //    string banjiid = DropDownList3.SelectedValue;
    //    conn.Open();
    //    SqlTransaction st = conn.BeginTransaction();
    //    comm.Transaction = st;
    //    try
    //    {
    //        //删除测试信息
    //        comm.CommandText = "delete from tb_studentkaoshiti where shijuanid in(select shijuanid from tb_teachershijuan where kechengid=" + kechengid + " and banjiid=" + banjiid + ") and studentusername='" + stuun + "'";
    //        comm.ExecuteNonQuery();
    //        comm.CommandText = "delete from tb_studentkaoshi where kechengid=" + kechengid + " and banjiid=" + banjiid + " and studentusername='" + stuun + "'";
    //        comm.ExecuteNonQuery();
    //        //删除作业
    //        comm.CommandText = "delete from tb_stuzuoyetimu where zuoyeid in(select zuoyeid from tb_zuoyebuzhi where kechengid=" + kechengid + " and banjiid=" + banjiid + ") and studentusername='" + stuun + "'";
    //        comm.ExecuteNonQuery();
    //        comm.CommandText = "delete from tb_studentzuoye where zuoyeid in(select zuoyeid from tb_zuoyebuzhi where kechengid=" + kechengid + " and banjiid=" + banjiid + ") and studentusername='" + stuun + "'";
    //        comm.ExecuteNonQuery();
    //        //删除点名信息
    //        comm.CommandText = "delete from tb_dianmingstu where dianmingid in(select dianmingid from tb_dianming where kechengid=" + kechengid + " and banjiid=" + banjiid + ") and studentusername='" + stuun + "'";
    //        comm.ExecuteNonQuery();
    //        st.Commit();
    //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'> alert('学生" + stuun + "的全部作业、测试、点名信息已删除!');</script>", false);
    //    }
    //    catch
    //    {
    //        st.Rollback();
    //        e.Cancel = true;
    //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'> alert('学生" + stuun + "的全部作业、测试、点名信息删除失败!');</script>", false);
    //    }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //}
}
