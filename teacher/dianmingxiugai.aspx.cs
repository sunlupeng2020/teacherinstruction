using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
using Microsoft.ApplicationBlocks.Data;

public partial class teachermanage_dianmingxiugai : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBanji();
        }
    }
    protected void BindBanji()
    {
        int kechengid = int.Parse(Session["kechengid"].ToString());
        string tu = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DropDownListbj.DataSource = BanjiInfo.GetTeacherRenkeBanji(tu, kechengid);
        DropDownListbj.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)//删除点名信息
    {
        if (DropDownList1.SelectedIndex >= 0)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand cmd = conn.CreateCommand();
            string dianmingid = DropDownList1.SelectedValue;
            conn.Open();
            SqlTransaction st = conn.BeginTransaction();
            cmd.Transaction = st;
            try
            {
                cmd.CommandText = "delete from tb_dianming where dianmingid=" + dianmingid;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from tb_dianmingstu where dianmingid=" + dianmingid;
                cmd.ExecuteNonQuery();
                st.Commit();
                DropDownList1.DataBind();
                GridView1.DataBind();
            }
            catch
            {
                st.Rollback();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            BindDianmingList();
        }
    }
    protected void DropDownListbj_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDianmingList();
    }
    protected void BindDianmingList()
    {
        string sqltxt = "SELECT [dianmingid], [dianmingtime] FROM [tb_dianming] WHERE (([kechengid] = @kechengid) AND ([banjiid] = @banjiid) AND ([teacherusername] = @teacherusername)) ORDER BY [dianmingtime] DESC";
        SqlParameter[] pa = new SqlParameter[3];
        pa[0] = new SqlParameter("@kechengid", Session["kechengid"].ToString());
        pa[1] = new SqlParameter("@banjiid", DropDownListbj.SelectedValue);
        pa[2] = new SqlParameter("@teacherusername", ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name);
        DataSet ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa);
        DropDownList1.DataSource = ds;
        DropDownList1.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void DropDownListbj_DataBound(object sender, EventArgs e)
    {
        if (DropDownListbj.Items.Count > 0)
        {
            BindDianmingList();
        }
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count > 0)
        {
            GridView1.DataBind();
        }
    }
}
