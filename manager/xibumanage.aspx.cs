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
public partial class manager_xibumanage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        Button1.Enabled = GridView1.Columns[1].Visible = TeacherInfo.IsSuperManager(username);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string xibuname = TextBox1.Text.Trim();
        bool kejian = true;
        //检查系部名称是否存在
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString);
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select xibuid from tb_xibu where xibuname='" +xibuname + "'";
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            kejian = false;
            Label1.Text = "该系部名称已存在，请使用其他名称。";
        }
        sdr.Close();
        conn.Close();
        if (kejian)
        {
            try
            {
                SqlDataSource1.InsertParameters[0].DefaultValue = TextBox1.Text.Trim();
                SqlDataSource1.Insert();
                Label1.Text = "创建系部成功！";
                GridView1.DataBind();
            }
            catch (Exception e1)
            {
                Label1.Text = e1.Message;
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string xibuid = e.Keys[0].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString);
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select count(*) from tb_zhuanye where xibuid=" + xibuid;
        conn.Open();
        int zhuanyeshu = (int)comm.ExecuteScalar();
        comm.CommandText = "select count(*) from tb_banji where xibuid=" + xibuid;
        int banjishu = (int)comm.ExecuteScalar();
        conn.Close();
        if (zhuanyeshu > 0||banjishu>0)
        {
            e.Cancel = true;
            if (zhuanyeshu > 0&&banjishu>0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('该系部有专业和班级存在，不能删除!');", true);
            }
            else if (zhuanyeshu > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('该系部有专业存在，不能删除!');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('该系部有班级存在，不能删除!');", true);
            }
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string xibuid = e.Keys[0].ToString();
        string xibunewname = e.NewValues[0].ToString();
        string xibuoldname = e.OldValues[0].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString);
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select count(xibuid) from tb_xibu where xibuname='" + xibunewname + "' and  xibuid<>" + xibuid;
        conn.Open();
        int n = (int)comm.ExecuteScalar();
        conn.Close();
        if (n > 0)
        {
            e.Cancel = true;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('该系部名称已存在，更新失败!');", true);
        }
    }
}
