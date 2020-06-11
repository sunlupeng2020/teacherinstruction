using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Text.RegularExpressions;
public partial class manager_studentdaoru : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)//从Excel表中读取数据,并显示
    {
        string xingming, xingbie, username, banjiname, zhuanye;
        banjiname = DropDownList3.SelectedItem.Text;
        int banjiid = int.Parse(DropDownList3.SelectedValue);
        int xuhao = BanjiMaxXuhao(banjiid)+1;//班级学生表中学生的最大序号
        int zhuanyeid;
        int xibuid = int.Parse(DropDownList1.SelectedValue);//系部id
        //string xibuxuehaoqianzhui = Xibu.GetXibuXhqianzhui(xibuid);//该系部学生学号的前缀，用于判断学号是否合法，学生学号前缀必须符合该系部学生学号前缀的规定
        StringBuilder sb = new StringBuilder();//反馈信息
        bool kedaoru, kezhuangyi;
        //先将EXCEL导入到数据库,一:先把EXCEL导入dateView,二:然后将dateView里的数据导入到数据库里面
        Label1.Text = "反馈信息：";
        if (!FileUpload1.HasFile)
        {
            Label1.Text = "请选择学生信息文件！";
            return;
        }
        Label1.Text = "上传文件，";
        string filename = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + DateTime.Now.Ticks.ToString();
        int dian = FileUpload1.PostedFile.FileName.LastIndexOf('.');
        string fileextendname = FileUpload1.PostedFile.FileName.Substring(dian + 1).ToLower();
        if (fileextendname == "" || fileextendname != "xls")
        {
            Label1.Text = "学生信息文件请使用Excel2003格式，扩展名为.xls.";
            return;
        }
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/studentdemo") + "/" + filename + ".xls");
        Label1.Text += "上传文件成功。";
        string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Server.MapPath("~/studentdemo/" + filename + ".xls") + ";" + "Extended Properties=Excel 8.0;";
        //建立EXCEL的连接
        OleDbConnection objConn = new OleDbConnection(sConnectionString);
        OleDbCommand objCmdSelect = objConn.CreateCommand();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        conn.Open();
        SqlTransaction st;
        try
        {
            objCmdSelect.CommandText = "SELECT 学号,姓名,性别,专业 FROM [Sheet1$] where ([学号] is not null) and 姓名<>'' and (性别='男' or 性别='女')";
            objConn.Open();
            OleDbDataReader objdr = objCmdSelect.ExecuteReader();
            Dictionary<string, int> zhuanyelist = ZhuanyeInfo.ZhuanyeDict();//专业字典
            //导入学生
            while (objdr.Read())
            {
                //username = xibuxuehaoqianzhui + objdr[0].ToString();
                username = objdr[0].ToString();
                xingming = objdr[1].ToString();
                xingbie = objdr[2].ToString(); ;
                zhuanye = objdr[3].ToString();
                if (zhuanyelist.ContainsKey(zhuanye))
                    zhuanyeid = zhuanyelist[zhuanye];
                else
                    zhuanyeid = ZhuanyeInfo.GetMinZhuanyeId();
                //if (XhQz_Xibu(username, xibuxuehaoqianzhui))
                //{
                    kedaoru = studentkedaoru(username);
                    kezhuangyi = studentKeZhuanyi(banjiid, username);
                    try
                    {
                        st = conn.BeginTransaction();
                        cmd.Transaction = st;
                        if (kedaoru)//该学生在学生信息表中不存在
                        {
                            //先插入到学生信息表
                            cmd.CommandText = "insert into tb_Student(username,xingming,xingbie,password,zhuanyeid,createtime) values('" + username + "','" + xingming + "','" + xingbie + "','" + username + "'," + zhuanyeid + ",'" + DateTime.Now.ToString() + "')";
                            cmd.ExecuteNonQuery();
                        }
                        //再插入到班级学生表
                        if (kezhuangyi)//判断该学生是否在该班中
                        {

                            cmd.CommandText = "insert into tb_banjistudent(banjiid,studentusername,xuhao) values(" + banjiid + ",'" + username + "'," + xuhao + ")";
                            cmd.ExecuteNonQuery();
                            //新添加了学生，把作业和测试信息分发给该学生，点名表中添加该学生？
                        }
                        st.Commit();
                        xuhao++;
                        sb.Append("<font color='blue'>" + username + "," + xingming + "导入成功！</font><br/>");
                    }
                    catch (Exception ex01)
                    {
                        sb.Append("font color='red'>" + username + "," + xingming + "导入失败！</font><br/>");
                        throw ex01;
                    }
                //}
                //else
                //{
                //    sb.Append("学生学号:"+username+"的学号不符合要求,需要前缀。<br/>");
                //}
            }
        }
        catch (Exception ee)
        {
            sb.Append("导入出错！详情：" + ee.Message + "<br/>");
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            if (objConn.State == ConnectionState.Open)
                objConn.Close();//关闭EXCEL的连接 
            Label1.Text = sb.ToString();
        }
    }
    protected bool studentkedaoru(string xuehao)
    {
        bool kedaoru = true;
        if (xuehao.Trim().Length > 0)
        {
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
        }
        else
        {
            kedaoru = false;
        }
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
    protected void DropDownList3_DataBound(object sender, EventArgs e)
    {
        if (DropDownList3.Items.Count > 0)
        {
            DropDownList3.SelectedIndex = 0;
        }
    }
    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    GridView2.DataBind();
    //}
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
    //protected bool XhQz_Xibu(string username, string qianzhui)//判断学生学号是否符合系部学号前缀
    //{
    //    if (qianzhui == string.Empty)//前缀为空，则所有学号均符合
    //        return true;
    //    else
    //    {
    //        int n = qianzhui.Trim().Length;
    //        if ((username.Length>=n)&&(username.Substring(0, n) == qianzhui))
    //            return true;
    //        else
    //            return false;
    //    }
    //}
}
