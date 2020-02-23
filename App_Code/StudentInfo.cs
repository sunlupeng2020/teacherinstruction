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
    public static bool WeiWanchengCeshi(string username,int kechengid)//检查学生是否有未完成、未交卷的测试，用来控制学生不能在测试期间做作业、自测等
    {
        bool cunzai = false;
        DateTime kaishishijian;
        DateTime dangqian = DateTime.Now;
        bool yunxuzuoti;
        int timelength;
        bool chaoshi=true;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //查询开始时间不为空的测试
        comm.CommandText = "select tb_studentkaoshi.kaishi_shijian,tb_studentkaoshi.timeyanchang,tb_studentkaoshi.jiaojuan,tb_teachershijuan.timelength,tb_teachershijuan.yunxuzuoti,tb_studentkaoshi.yunxu from tb_studentkaoshi inner join tb_teachershijuan on tb_teachershijuan.shijuanid=tb_studentkaoshi.shijuanid where tb_studentkaoshi.studentusername='" + username + "' and tb_studentkaoshi.jiaojuan<>'已交卷' and tb_studentkaoshi.kaishi_shijian is not null and tb_studentkaoshi.kechengid="+kechengid;
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        while(sdr.Read())
        {
            kaishishijian = sdr.GetDateTime(0);//开始时间
            timelength = sdr.GetInt32(1) + sdr.GetInt32(3);//测试总时间
            yunxuzuoti = (sdr.GetString(4).Trim()=="允许"&&sdr.GetString(5).Trim()=="允许");//是否允许做题
            //判断是否超时
            if (kaishishijian.AddMinutes(timelength).CompareTo(dangqian) > 0)
                chaoshi = false;
            else
                chaoshi = true;
            if (!chaoshi && yunxuzuoti)
            {
                cunzai = true;
                break;
            }
        }
        sdr.Close();
        conn.Close();
        return cunzai;
    }
    /// <summary>
    /// 获取学生某课程作业
    /// </summary>
    /// <param name="kechengid">课程id</param>
    /// <param name="stuusername">学生用户名</param>
    /// <returns></returns>
    public static DataSet GetStuKechegZuoye(int kechengid, string stuusername)//得到学生某课程作业
    {
        SqlConnection conn = SqlDal.conn;
        string sqlstr = "SELECT tb_kecheng.kechengname 课程,teacherzuoye.zuoyename 作业名称,tb_studentzuoye.zuoyeid,tb_studentzuoye.buzhishijian 布置时间,tb_studentzuoye.shangjiaoqixian 上交期限,tb_studentzuoye.shangjiaoriqi 上交时间,tb_studentzuoye.zongfen 成绩,tb_studentzuoye.wancheng 状态,tb_studentzuoye.pingyu 评语,tb_studentzuoye.yunxuzuoti 允许做题,tb_studentzuoye.yunxuchakan 允许查看结果,tb_studentzuoye.shuoming 说明 from tb_studentzuoye join tb_kecheng on tb_kecheng.kechengid=tb_studentzuoye.kechengid join teacherzuoye on tb_studentzuoye.zuoyeid=teacherzuoye.zuoyeid where tb_studentzuoye.studentusername=@stuusername and tb_studentzuoye.kechengid=@kechengid";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@kechengid", kechengid);
        param[1] = new SqlParameter("@stuusername", stuusername);
        DataSet dt = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqlstr, param);
        return dt;
    }

    public static DataTable GetStuZuoyeTimu(int zuoyeid, string stuusername)//学生作业题目详情,按题型排序
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT tb_stuzuoyetimu.stuzuoyetimuid,tb_stuzuoyetimu.studentusername as 学号,tb_tiku.[type] as 题型,tb_tiku.timu as 题目,tb_tiku.answer as 参考答案,tb_stuzuoyetimu.answer as 回答, tb_stuzuoyetimu.filepath as 作业文件,tb_stuzuoyetimu.fenzhi as 分值,tb_stuzuoyetimu.defen as 得分,tb_tiku.filepath as 题目文件,tb_timuleixing.tixingid,tb_stuzuoyetimu.questionid FROM [tb_stuzuoyetimu] inner join tb_tiku on tb_tiku.questionid=tb_stuzuoyetimu.questionid join tb_timuleixing on tb_tiku.[type]=tb_timuleixing.mingcheng WHERE (tb_stuzuoyetimu.[zuoyeid] =@zuoyeid) and (tb_stuzuoyetimu.studentusername=@stuusername) order by tb_stuzuoyetimu.stuzuoyetimuid,tb_timuleixing.tixingid asc";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        comm.Parameters.AddWithValue("@stuusername", stuusername);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }

    public static bool IsStuHaveZuoye(int zuoyeid,string stuusername)
    {
        bool h = true;
        SqlConnection conn = SqlDal.conn;
        string sqlstr = "select count(*) from tb_studentzuoye where zuoyeid=@zuoyeid  and studentusername=@stuusername";
        SqlParameter[] pa = new SqlParameter[3];
        pa[0] = new SqlParameter("@zuoyeid", zuoyeid);
        pa[1] = new SqlParameter("@stuusername", stuusername);
        if ((int)(SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlstr, pa)) <= 0)
            h = false;
        return h;
    }

    public static DataTable GetStuZuoyeOnTime(string stuusername)//获取学生现在要做的作业
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT tb_kecheng.kechengname 课程,teacherzuoye.zuoyename 作业名称,tb_studentzuoye.zuoyeid,tb_studentzuoye.buzhishijian 布置时间,tb_studentzuoye.shangjiaoqixian 上交期限 from tb_studentzuoye inner join tb_kecheng on tb_kecheng.kechengid=tb_studentzuoye.kechengid inner join teacherzuoye on teacherzuoye.zuoyeid=tb_studentzuoye.zuoyeid where tb_studentzuoye.studentusername=@stuusername and tb_studentzuoye.yunxuzuoti='允许' and tb_studentzuoye.shangjiaoqixian>getdate() order by tb_studentzuoye.shangjiaoqixian desc";
        comm.Parameters.AddWithValue("@stuusername", stuusername);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataSet GetStuCeshiOntime(string kechengid, string stuusername)//获取学生某课程未完成的测试
    {
        string sqltxt = "SELECT tb_teachershijuan.ceshiname,tb_studentkaoshi.[shijuanid],tb_teachershijuan.timelength,tb_studentkaoshi.jiaojuan FROM [tb_studentkaoshi] inner join tb_teachershijuan on tb_teachershijuan.shijuanid=tb_studentkaoshi.shijuanid WHERE ([tb_studentkaoshi].[kechengid] = @kechengid and tb_teachershijuan.yunxuzuoti='允许' and [tb_studentkaoshi].studentusername=@stuusername and [tb_studentkaoshi].yunxu='允许' and [tb_studentkaoshi].jiaojuan<>'已交卷')";
        SqlParameter[] pa = new SqlParameter[2];
        pa[0] = new SqlParameter("@kechengid", kechengid);
        pa[1] = new SqlParameter("@stuusername", stuusername);
        return (SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa));
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
