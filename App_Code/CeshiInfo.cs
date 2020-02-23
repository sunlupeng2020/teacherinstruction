using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
///CeshiInfo 的摘要说明
/// </summary>
public class CeshiInfo
{
	public CeshiInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static bool IsIpXianzhi(int shijuanid)//获得某次考试是否限制IP地址
    {
        string sqltxt = "select xianzhiip from tb_teachershijuan where shijuanid=" + shijuanid;
        string xianzhi = SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt).ToString().Trim();
        return (xianzhi == "是");
    }

    public static DataTable GetStuCeshi(string stuusername)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT tb_kecheng.kechengname,tb_teachershijuan.ceshiname,tb_studentkaoshi.shijuanid,tb_studentkaoshi.zongfen,tb_teachershijuan.timelength,tb_studentkaoshi.jiaojuan from tb_studentkaoshi inner join  tb_teachershijuan on tb_studentkaoshi.shijuanid=tb_teachershijuan.shijuanid inner join tb_kecheng on tb_kecheng.kechengid=tb_studentkaoshi.kechengid where tb_studentkaoshi.studentusername='"+stuusername+"' and tb_studentkaoshi.yunxu='允许' order by tb_teachershijuan.createtime desc";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable CeshiHuizong(string teacherusername, string kechengid, string banjiid)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        string tmptb = teacherusername + DateTime.Now.Ticks.ToString();
        comm.CommandText = "CREATE VIEW " + tmptb + " AS SELECT tb_Student.xingming as 姓名,tb_banjistudent.xuhao as 序号,tb_Student.username as 学号,tb_studentkaoshi.zongfen,tb_teachershijuan.ceshiname FROM tb_Student INNER JOIN tb_studentkaoshi ON tb_Student.username = tb_studentkaoshi.studentusername INNER JOIN tb_teachershijuan ON tb_studentkaoshi.shijuanid =tb_teachershijuan.shijuanid inner join tb_banjistudent on tb_banjistudent.studentusername= tb_student.username where tb_teachershijuan.teacherusername='" + teacherusername + "' and tb_teachershijuan.kechengid=" + kechengid + " and tb_teachershijuan.banjiid=" + banjiid + " and tb_banjistudent.banjiid=" + banjiid;
        conn.Open();
        comm.ExecuteNonQuery();
        comm.CommandText = "stuceshihuizong";
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add(new SqlParameter("@strTabName", tmptb));
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        comm.CommandType = CommandType.Text;
        try
        {
            comm.CommandText = "drop view " + tmptb;
            comm.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }
    public static DataSet GetTeacherCeshi(string teausername, string kechengid, string banjiid)
    {
        DataSet ds = new DataSet();
        string sqltxt = "SELECT [shijuanid], [ceshiname] FROM [tb_teachershijuan] WHERE (([teacherusername] = @teacherusername) AND ([kechengid] = @kechengid) AND ([banjiid] = @banjiid)) ORDER BY [createtime] DESC";
        SqlParameter[] pa = new SqlParameter[3];
        pa[0] = new SqlParameter("@teacherusername", teausername);
        pa[1] = new SqlParameter("@kechengid", kechengid);
        pa[2] = new SqlParameter("@banjiid", banjiid);
        ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa);
        return ds;
    }
    public static string GetCeshiName(int shijuanid)
    {
        string sqltxt = "select ceshiname from tb_teachershijuan where shijuanid=" + shijuanid;
        return SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt).ToString();
    }
    public static DataTable GetCeshiTimu(int shijuanid)
    {
        string sqltxt="select questionid,fenzhi,tihao from tb_teachershijuantimu where shijuanid=" + shijuanid;
        return SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt).Tables[0];
    }
}
