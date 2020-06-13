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

public partial class teachermanage_studentmanage_addstudent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox1.Attributes.Add("onblur", "setvalue()");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string fankui = "";
        Label1.Text = "";
        int banjiid = int.Parse(DropDownListbanji.SelectedValue);//班级ID
        string banjiname = DropDownListbanji.SelectedItem.Text;
        string zhuanyeid = DropDownListzhuanye.SelectedValue;//专业
        string username = TextBox1.Text.Trim().ToUpper();
        string xingming = TextBox2.Text;
        string xingbie = DropDownList4.SelectedValue;
        int xuhao = int.Parse(TextBoxxuhao.Text);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            if (studentkedaoru(username))
            {
                comm.CommandText = "insert into tb_student(username,xingming,xingbie,password,zhuanyeid,createtime) values('" + username + "','" + xingming + "','" + xingbie + "','" + username + "'," + zhuanyeid + ",'" + DateTime.Now.ToString() + "')";
                if (comm.ExecuteNonQuery() > 0)
                    fankui += "成功添加学生到学生信息表，";
                else
                    fankui += "添加学生到学生表信息失败，";
            }
            else
                fankui += "该学生在学生表信息表中已存在，";
            if (studentKeZhuanyi(banjiid, username))//是否能把学生添加到某班，如果能，则将学生加入某班
            {
                comm.CommandText = "insert into tb_banjistudent(banjiid,studentusername,xuhao) values(" + banjiid + ",'" + username + "'," + xuhao + ")";
                if (comm.ExecuteNonQuery() > 0)
                    fankui = "学生" + xingming + "已成功添加到班级：" + banjiname + ".";
                else
                    fankui = "学生" + xingming + "未能成功添加到班级：" + banjiname + ".";
            }
            else
            {
                fankui = "学生" + xingming + "在班级：" + banjiname + "中已存在.";
            }
            st.Commit();
        }
        catch (Exception e1)
        {
            st.Rollback();
            fankui += e1.Message;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            Label1.Text += fankui;
        }
    }
    protected string studentbanji(string username)
    {
        string banji = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select banji from tb_student where username='" + username + "'";
        conn.Open();
        SqlDataReader dr = comm.ExecuteReader();
        if (dr.Read())
        {
            banji = dr.GetString(0).ToUpper();
        }
        dr.Close();
        conn.Close();
        return banji;
    }
    protected bool studentkedaoru(string xuehao)
    {
        bool kedaoru = true;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        conn.Open();
        cmd.CommandText = "select studentid from tb_student where username='" + xuehao + "'";
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            kedaoru = false;
        }
        dr.Close();
        conn.Close();
        return kedaoru;
    }
    protected bool studentKeZhuanyi(int banjiid, string username)//判断学生是否已在该班，如果已在该班，返回false,否则返回true
    {
        bool kezhuanyi = true;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select banjistudentid from tb_banjistudent where banjiid=" + banjiid + " and studentusername='" + username + "'";
        conn.Open();
        SqlDataReader sdr = cmd.ExecuteReader();
        if (sdr.Read())
        {
            kezhuanyi = false;
        }
        sdr.Close();
        conn.Close();
        return kezhuanyi;
    }
    protected int BanjiMaxXuhao(int banjiid)//获取班级学生表中最大学生序号
    {
        int xuhao = 0;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select count(xuhao) as maxxuhao from tb_banjistudent where banjiid=" + banjiid;
        conn.Open();
        xuhao = (int)(cmd.ExecuteScalar());
        conn.Close();
        return xuhao;
    }
}
