using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
/// <summary>
///StudentInfo 的摘要说明
/// </summary>
public class StudentInfo
{
	public StudentInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static DataSet GetStuInfoByXuehao(string stuusername)//根据学号查询学生信息
    {
        DataSet ds = new DataSet();
        SqlConnection conn = SqlDal.conn;
        string sqltxt = "select xingming,xingbie,tb_zhuanye.zhuanyename as zhuanye,username,tb_banji.banjiname from tb_student join tb_zhuanye on tb_zhuanye.zhuanyeid=tb_student.zhuanyeid join tb_banjistudent on tb_banjistudent.studentusername =tb_student.username join tb_banji on tb_banji.banjiid=tb_banjistudent.banjiid where tb_student.username=@stuusername";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@stuusername", stuusername);
        try
        {
            ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqltxt, pa);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    public static string GetStuXingming(string stuusername)
    {
        string xingming = "";
        string sqltxt = "select xingming from tb_student where username=@username";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@username", stuusername);
        xingming = SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa).ToString();
        return xingming;
    }
    public static DataSet GetStuInfoByXingming(string xingming)//根据姓名查询学生信息
    {
        DataSet ds = new DataSet();
        SqlConnection conn = SqlDal.conn;
        string sqltxt = "select xingming,xingbie,tb_zhuanye.zhuanyename as zhuanye,username,tb_banji.banjiname from tb_student join tb_zhuanye on tb_zhuanye.zhuanyeid=tb_student.zhuanyeid join tb_banjistudent on tb_banjistudent.studentusername =tb_student.username join tb_banji on tb_banji.banjiid=tb_banjistudent.banjiid where tb_student.xingming=@xingming";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@xingming", xingming);
        try
        {
            ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqltxt, pa);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
}
