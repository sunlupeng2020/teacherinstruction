using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
///BanjiInfo 的摘要说明
/// </summary>
public class BanjiInfo
{
	public BanjiInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static DataSet GetTeacherRenkeBanji(string teacherusername)//查找教师任某课程的班级
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT [banjiname],banjiid FROM tb_banji where [teacherusername] ='"+ teacherusername+"' order by createtime desc";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(ds);
        return ds;
    }
    public static DataTable GetStudent(int banjiid)//查找某班的学生信息
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT xingming,username FROM tb_student where username in(select studentusername from [tb_banjistudent] WHERE banjiid =" +banjiid+ ") order by username asc";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetStudentNameAndUsername(int banjiid)//查找某班的学生信息返回值为学号+姓名
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT [username]+[xingming] as stu,username FROM tb_student where username in(select studentusername from [tb_banjistudent] WHERE banjiid =" + banjiid + ") order by username asc";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;

    }
    public static string GetBanjiName(int banjiid)//查询班级名称
    {
        string banjiname = string.Empty;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT banjiname from tb_banji where banjiid=@banjiid";
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        conn.Open();
        banjiname = comm.ExecuteScalar().ToString();
        conn.Close();
        return banjiname;
    }

    /// <summary>
    /// 得到班级中学生用户名
    /// </summary>
    /// <param name="banjiid">班级id</param>
    /// <returns></returns>
    public static DataTable GetStudentUserName(int banjiid)//查找班级中学生的学号
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT studentusername from [tb_banjistudent] WHERE banjiid =" + banjiid.ToString() ;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static int GetStudentCount(int banjiid)//查找班级学生数量
    {
        int count = 0;
        string sqltxt = "select count(*) from tb_banjistudent where banjiid=@banjiid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@banjiid",banjiid);
        count =(int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa));
        return count;
    }
}
