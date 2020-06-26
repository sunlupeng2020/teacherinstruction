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

public partial class manager_banjilist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ListBox1_DataBound(object sender, EventArgs e)
    {
        if (ListBox1.Items.Count > 0)
        {
            ListBox1.SelectedIndex = 0;
            GridView1.DataBind();
        }

    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ListBox1.SelectedIndex >= 0)
        {
            GridView1.DataBind();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)//删除一个班
    {
        //删除原则：教师自己建的班可以删，不能删除别的教师建的班
        //删除班级时，数据库会自动级联删除学生在该班的登记信息，但不会删除学生信息
        if (ListBox1.SelectedIndex >= 0)
        {
            string banjiid = ListBox1.SelectedValue.Trim();
            string banjiname = ListBox1.SelectedItem.Text.Trim();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "delete from tb_banji where banjiid=" + banjiid;
            try{
                conn.Open();
                comm.ExecuteNonQuery();
                ListBox1.DataBind();
                Labelfankui.Text = "删除班级：" + banjiname + " 成功！";
            }
            catch(Exception ex){
                 Labelfankui.Text = "删除班级：" + banjiname + " 失败！" +ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            Labelfankui.Text = "请选择班级！";
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        int i = 1;
        foreach (GridViewRow row in GridView1.Rows)
        {
            ((Literal)(row.Cells[0].FindControl("ltxh"))).Text = i.ToString();
            i++;
        }
    }
}
