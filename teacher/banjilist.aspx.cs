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
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (!TeacherInfo.IsSuperManager(username))
        {
            string guanlixibuid = TeacherInfo.managerXibu(username);
            if (guanlixibuid != "0")
            {
                DropDownList1.SelectedValue = guanlixibuid;
                DropDownList1.Enabled = false;
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void ListBox1_DataBound(object sender, EventArgs e)
    {
        if (ListBox1.Items.Count > 0)
        {
            ListBox1.SelectedIndex = 0;
            lbl_banjiname.Text = ListBox1.Items[0].Text;
            Button2.Enabled = true;
        }
        else
        {
            lbl_banjiname.Text = "";
            Button2.Enabled = false;
        }
        GridView1.DataBind();
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ListBox1.SelectedIndex >= 0)
        {
            lbl_banjiname.Text = ListBox1.SelectedItem.Text;
            GridView1.DataBind();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)//删除一个班
    {
        //删除原则：教师自己建的班可以删，不能删除别的教师建的班
        //只能删除没有学生，也没有指定任课的班
        if (ListBox1.SelectedIndex >= 0)
        {
            bool xuesheng = false;//是否有学生
            bool renke = false;//是否有任课信息
            string banjiid = ListBox1.SelectedValue.Trim();
            string banjiname = ListBox1.SelectedItem.Text.Trim();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select count(studentusername) from tb_banjistudent where banjiid=" + banjiid;
            conn.Open();
            if (((int)(comm.ExecuteScalar())) <= 0)//是否有学生
                xuesheng = true;
            comm.CommandText = "select count(teacherusername) from tb_teacherrenke where banjiid=" + banjiid;
            if (((int)(comm.ExecuteScalar())) <= 0)
                renke = true;
            if (xuesheng && renke)
            {
                comm.CommandText = "delete from tb_banji where banjiid=" + banjiid;
                comm.ExecuteNonQuery();
                ListBox1.DataBind();
                Labelfankui.Text = "删除班级：" + banjiname + " 成功！";
            }
            else
            {
                string yuanyin = "";
                if (!xuesheng)
                    yuanyin += "该班有学生！";
                if (!renke)
                    yuanyin += "已有教师任该班的课！";
                Labelfankui.Text = "删除班级：" + banjiname + " 失败！" + yuanyin;
            }
            conn.Close();
        }
        else
        {
            Labelfankui.Text = "请选择班级！";
        }
    }
}
