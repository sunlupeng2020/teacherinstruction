using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

/// <summary>
///RenkeInfo 的摘要说明
/// </summary>
public class RenkeInfo
{
	public RenkeInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string GetTeacherusername(string kechengid, string banjiid)
    {
        string tusername = string.Empty;
        string sqltxt = "select teacherusername from tb_teacherrenke where kechengid=@kechengid and banjiid=@banjiid";
        SqlParameter[] pa = new SqlParameter[2];
        pa[0] = new SqlParameter("@kechengid", kechengid);
        pa[1] = new SqlParameter("@banjiid", banjiid);
        object obj = SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa);
        if (obj != null)
            tusername = obj.ToString();
        return tusername;
    }
    public static DataTable GetTeacherRenKeKecheng(string teacherusername)//得到教师任课课程名称、课程id
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select distinct kechengname,kechengid from tb_kecheng where  kechengid in(select kechengid from tb_teacherrenke where teacherusername='" + teacherusername + "')";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetRenkeInfo(string renkeid)//获取任课信息
    {
        DataTable dt = new DataTable();
        string sqltxt = "select teahcerusername,banjiid,kechengid where renkeid=@renkeid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@renkeid", renkeid);
        dt = (SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa)).Tables[0];
        return dt;
    }
    public static bool IsTeacherHaveZuoye(string renkeid)//根据任课id查询是否布置了作业
    {
        bool have =false;
        string sqltxt = "select count(*) from tb_zuoyebuzhi a,tb_teacherrenke b where a.teacherusername=b.teacherusername and a.kechengid=b.kechengid and a.banjiid=b.banjiid and b.renkeid=@renkeid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@renkeid", renkeid);
        if (((int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa))) > 0)
            have = true;
        return have;
    }
    public static bool IsTeacherHaveCeshi(string renkeid)//根据任课id查询是否创建了测试项目
    {
        bool have = false;
        string sqltxt = "select count(*) from tb_teachershijuan a,tb_teacherrenke b where a.teacherusername=b.teacherusername and a.kechengid=b.kechengid and a.banjiid=b.banjiid and b.renkeid=@renkeid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@renkeid", renkeid);
        if (((int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa))) > 0)
            have = true;
        return have;
    }
    public static bool DelRenke(string renkeid)
    {
        bool del = true;
        string sqltxt = "delete from tb_teacherrenke where renkeid=@renkeid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@renkeid", renkeid);
        if (SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, sqltxt, pa) <= 0)
            del = false;
        return del;
    }
}
