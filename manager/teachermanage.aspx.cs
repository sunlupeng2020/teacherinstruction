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
public partial class manager_teachermanage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string xingming = TextBoxname.Text.Trim();
        string pwd = TextBoxpwd1.Text.Trim();
        string username = TextBoxusername.Text.Trim();
        string xingbie = DropDownList1.SelectedValue;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select teacherid from tb_teacher where [username]='" +username + "'";
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        bool b =true;
        if (sdr.Read())
        {
            b = false;
        }
        sdr.Close();
        conn.Close();
        if (b)
        {
            try
            {
                conn.Open();
                comm.CommandText = "insert into tb_teacher(username,xingming,xingbie,password) values('" + username + "','" + xingming + "','" + xingbie + "'," + ",'" + pwd + "')";
                if (comm.ExecuteNonQuery() > 0)
                {
                    Labelfanki.Text = "教师用户'" + username + "'添加成功！";
                }
                else
                {
                    Labelfanki.Text = "教师用户'" + username + "'添加失败！";

                }
            }
            catch (Exception ex)
            {
                Labelfanki.Text = ex.Message;
            }
            finally
            {
                if(conn.State==ConnectionState.Open)
                    conn.Close();
            }
        }
        else
        {
            Labelfanki.Text = "教师用户'" + username + "'已存在，添加失败！";
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        GridView1.Columns[5].Visible=TeacherInfo.IsSuperManager(username);
    }
}
