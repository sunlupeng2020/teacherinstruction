using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class teachermanage_dianming : System.Web.UI.Page
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
        DropDownList2.DataSource = BanjiInfo.GetTeacherRenkeBanji(tu, kechengid);
        DropDownList2.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex >= 0 && GridView1.Rows.Count > 0)
        {
            int dianmingid = 0;
            string dmtm = DateTime.Now.ToString();
            int kechengid = int.Parse(Session["kechengid"].ToString());
            int banjiid = int.Parse(DropDownList2.SelectedValue);
            string tu = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            conn.Open();
            SqlTransaction st = conn.BeginTransaction();
            comm.Transaction = st;
            try
            {
                comm.CommandText = "insert into tb_dianming(teacherusername,kechengid,banjiid,dianmingtime) values('" + tu + "'," + kechengid + "," + banjiid + ",'" + dmtm + "')";
                comm.ExecuteNonQuery();
                comm.CommandText = "select dianmingid from tb_dianming where teacherusername='" + tu + "' and kechengid=" + kechengid + " and banjiid=" + banjiid + " and dianmingtime='" + dmtm + "'";
                dianmingid = (int)(comm.ExecuteScalar());
                //写入点名信息
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    comm.CommandText = "insert into tb_dianmingstu(studentusername,dianmingid,zhuangtai,banjiid) values('" + gvr.Cells[1].Text.Trim() + "'," + dianmingid + ",'" + ((RadioButtonList)(gvr.Cells[3].FindControl("RadioButtonList1"))).SelectedValue + "'," + banjiid + ")";
                    comm.ExecuteNonQuery();
                }
                st.Commit();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('考勤成功！');</script>", false);
            }
            catch
            {
                st.Rollback();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('考勤失败！');</script>", false);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
    protected void DropDownList2_DataBound(object sender, EventArgs e)
    {
        if (DropDownList2.Items.Count > 0)
            GridView1.DataBind();
    }
}
