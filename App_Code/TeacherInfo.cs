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
}
