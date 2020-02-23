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
///TeacherInfo 的摘要说明
/// </summary>
public class TeacherInfo
{
	public TeacherInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string TeacherXingMing(string teacherusername)
    {
        string tname = string.Empty;
        string sqltxt = "select  xingming from tb_teacher where username=@username";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@username",teacherusername);
        tname = SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa).ToString();
        return tname;
    }
    public static DataTable GetTeacherKechengZuoye(string teacherusername, string kechengid)
    {
        string sqltxt = "SELECT [zuoyeid], [zuoyename], [createtime], [manfen] FROM [teacherzuoye] WHERE (([kechengid] = @kechengid) AND ([teacherusername] = @teacherusername)) order by createtime desc";
        SqlParameter[] pa = new SqlParameter[2];
        pa[0] = new SqlParameter("@kechengid", kechengid);
        pa[1] = new SqlParameter("@teacherusername", teacherusername);
        return (SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa).Tables[0]);
    }
    public static bool IsSuperManager(string username)//判断用户是否超级管理员
    {
        bool issupermanager=false;
        string sqltxt = "select count(*) from tb_teacher where username=@username and manager='supermanager'";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@username", username);
        if ((int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa)) > 0)
            issupermanager = true;
        return issupermanager;
    }
    public static bool IsManager(string username)//判断教师是否管理员
    {
        bool ismanager = false;
        string sqltxt = "select count(*) from tb_teacher where username=@username and (manager='supermanager' or manager='manager')";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@username", username);
        if ((int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa)) > 0)
            ismanager = true;
        return ismanager;
    }
    public static string managerXibu(string username)//管理员管理的系部
    {
        string sqltxt = "select guanlixibuid from tb_teacher where username=@username";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@username", username);
        return (SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa).ToString());
    }
    public static bool IsTeacherBuzhiZuoye(string username)//查询教师是否布置过作业
    {
        bool yes = false;
        string sqltxt = "select count(*) from teacherzuoye where teacherusername='" + username + "'";
        if (((int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt))) > 0)
            yes = true;
        return yes;
    }
    public static bool IsTeacherCreateCeshi(string username)//查询教师是否创建过测试
    {
        bool yes = false;
        string sqltxt = "select count(*) from tb_teachershijuan where teacherusername='" + username + "'";
        if (((int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt))) > 0)
            yes = true;
        return yes;
    }
    public static int GetTeacherRenke_min_kechengid(string teacherusername)//得到教师任课的最小课程id
    {
        DataTable dt = RenkeInfo.GetTeacherRenKeKecheng(teacherusername);
        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0][1]);
        else
            return -1;
    }
}
