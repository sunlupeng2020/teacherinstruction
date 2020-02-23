using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
///ZuoyeInfo 的摘要说明
/// </summary>
public class ZuoyeInfo
{
	public ZuoyeInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string GetZuoyeKechengName(int zuoyeid)//得到作业的课程名
    {
        string kechengname = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select kechengname from tb_kecheng where  kechengid=(select kechengid from teacherzuoye where zuoyeid=@zuoyeid)";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        conn.Open();
        kechengname = comm.ExecuteScalar().ToString();
        conn.Close();
        return kechengname;
    }
    public static DataSet GetZuoyeInfo(int kechengid, int banjiid, string teacherusername)//获取某班某课程某教师布置了哪些作业
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyeid,zuoyename from teacherzuoye where zuoyeid in(select zuoyeid from tb_zuoyebuzhi  where kechengid=@kechengid and banjiid=@banjiid and teacherusername=@teacherusername)";
        comm.Parameters.AddWithValue("@kechengid",kechengid);
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        comm.Parameters.AddWithValue("@teacherusername", teacherusername);
        //comm.CommandText = "SELECT [zuoyeid], [zuoyemingcheng] FROM [teacherzuoye] WHERE (([kechengid] =" + kechengid + ") AND ([teacherusername] ='" + teacherusername + "') and (banjiid=" + banjiid + "))";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(ds);
        return ds;
    }
    public static DataTable GetZuoyeInfo(int zuoyeid)//显示一个作业的信息
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyename,teacherzuoye.[createtime],wancheng,manfen,kechengname from teacherzuoye join tb_kecheng on teacherzuoye.kechengid=tb_kecheng.kechengid where zuoyeid=" + zuoyeid;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetStuZuoyeMemo(int zuoyeid,string stuusername)//单个学生一个作业情况
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT studentusername as 学号,[shangjiaoriqi] as 上交时间, [zongfen] as 成绩, [pingyu] as 评语,[shangjiaoqixian] as 上交期限,teacherzuoye.zuoyename as 作业名称 FROM [tb_studentzuoye] inner join teacherzuoye on teacherzuoye.zuoyeid=tb_studentzuoye.zuoyeid WHERE ([zuoyeid] = @zuoyeid and studentusername=@studentusername)";
        comm.Parameters.Add(new SqlParameter("@zuoyeid",zuoyeid));
        comm.Parameters.Add(new SqlParameter("@studentusername",stuusername));
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }

    public static DataTable GetQuantiStuZuoyeXinxi(int banjiid,int zuoyeid)//全体学生某作业信息
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT tb_student.xingming as 姓名,tb_studentzuoye.studentusername as 学号,shangjiaoriqi 上交时间,zongfen 成绩,pingyu 评语 from tb_studentzuoye join tb_student on tb_student.username=tb_studentzuoye.studentusername where tb_studentzuoye.zuoyeid=@zuoyeid and tb_studentzuoye.studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid) order by tb_studentzuoye.studentusername asc";
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable StuZuoyeHuiZong(int kechengid, int banjiid)//某班某课程作业汇总
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandType = CommandType.Text;
        string tmptb = "a"+ DateTime.Now.Ticks.ToString();
        comm.CommandText = "CREATE VIEW "+tmptb+" AS SELECT  tb_student.xingming  姓名,tb_studentzuoye.studentusername  学号,teacherzuoye.zuoyename,tb_studentzuoye.zongfen FROM tb_Studentzuoye inner JOIN tb_student ON tb_Student.username = tb_studentzuoye.studentusername inner JOIN teacherzuoye ON tb_studentzuoye.zuoyeid =teacherzuoye.zuoyeid  inner join tb_banjistudent on tb_banjistudent.studentusername= tb_student.username where tb_studentzuoye.studentusername in(select studentusername from  tb_banjistudent where banjiid="+banjiid+") and tb_studentzuoye.zuoyeid in(select zuoyeid from tb_zuoyebuzhi where kechengid="+kechengid+" and banjiid="+banjiid+")";
        //comm.Parameters.AddWithValue("@kechengid", kechengid);
        //comm.Parameters.AddWithValue("@banjiid", banjiid);
        conn.Open();
        comm.ExecuteNonQuery();
        comm.Parameters.Clear();
        comm.CommandText = "stuzuoyehuizong";
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.AddWithValue("@strTabName",tmptb);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        comm.CommandType = CommandType.Text;
        comm.CommandText = "drop view "+tmptb;
        comm.ExecuteNonQuery();
        conn.Close();
        return dt;
    }
    public static DataTable GetStuKechengZuoyeXinxi(int kechengid, string stuusername)//一个学生某课程全部作业信息
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select tb_studentzuoye.zuoyeid,tb_studentzuoye.wancheng 状态,tb_studentzuoye.shangjiaoriqi 上交时间,tb_studentzuoye.zongfen 成绩,tb_studentzuoye.pingyu 评语,teacherzuoye.zuoyename as 作业名称,tb_student.xingming as 姓名 from tb_studentzuoye inner join tb_student on tb_student.username=tb_studentzuoye.studentusername inner join teacherzuoye on teacherzuoye.zuoyeid=tb_studentzuoye.zuoyeid where tb_studentzuoye.studentusername=@stuusername and  teacherzuoye.kechengid =@kechengid";
        comm.Parameters.AddWithValue("@stuusername", stuusername);
        comm.Parameters.AddWithValue("@kechengid", kechengid);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetZuoyeStudent(int zuoyeid)//某作业布置给了那些学生
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandType = CommandType.Text;
        comm.CommandText = "select tb_student.xingming,tb_student.username from tb_student inner join tb_studentzuoye on tb_student.username=tb_studentzuoye.studentusername where(tb_studentzuoye.zuoyeid=" + zuoyeid + ")";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static int UpdateZuoyexinxi(int zuoyeid, string shangjiaoqixian, string shuoming, string yunxu,string yunxuchakan)
    {
        int xiugai = 0;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandType = CommandType.Text;
        comm.CommandText = "update teacherzuoye set shangjiaoqixian='" + shangjiaoqixian + "',beizhu='" + shuoming + "',yunxu='" + yunxu + "',yunxuchakan='"+yunxuchakan+"'  where zuoyeid=" + zuoyeid;
        conn.Open();
        xiugai = comm.ExecuteNonQuery();
        conn.Close();
        return xiugai;       
    }
    public static DataTable GetZuoyeTimuIdAndFenzhi(int zuoyeid)//作业的题目
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT questionid,fenzhi FROM [tb_teacherzuoyetimu]  WHERE [zuoyeid] =" + zuoyeid + " order by zuoyetimuid";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    /// <summary>
    /// 获取作业题目详情，按题型排序
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <returns></returns>
    public static DataTable GetTeacherZuoyeTimuOrderByTixing(int zuoyeid)//教师作业题目按题型排序
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyetimuid,tb_tiku.[type] 题型,tb_tiku.timu as 题目,tb_tiku.answer as 参考答案,tb_tiku.filepath as 相关文件,fenzhi as 分值,tb_timuleixing.tixingid,tb_teacherzuoyetimu.questionid as questionid from tb_teacherzuoyetimu join tb_tiku on tb_tiku.questionid=tb_teacherzuoyetimu.questionid join tb_timuleixing on tb_tiku.[type]=tb_timuleixing.mingcheng where tb_teacherzuoyetimu.zuoyeid=@zuoyeid order by tb_timuleixing.tixingid,zuoyetimuid";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        //comm.CommandText = "SELECT tb_teacherzuoyetimu.zuoyetimuid,tb_teacherzuoyetimu.tihao as 题号,tb_tiku.type as 题型,tb_teacherzuoyetimu.tb_tiku.questionid FROM [tb_teacherzuoyetimu] inner join tb_tiku on tb_tiku.questionid=tb_teacherzuoyetimu.questionid WHERE (tb_teacherzuoyetimu.[zuoyeid] =" + zuoyeid + ") order by tb_teacherzuoyetimu.tihao asc";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }

    public static bool PigaiZuoyeKeguanti(int zuoyeid,int banjiid)//自动批改某作业的客观题并汇总成绩
    {
        bool chenggong = true;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_stuzuoyetimu set defen=fenzhi where (zuoyeid=@zuoyeid) and (answer=(select answer from tb_tiku where questionid=tb_stuzuoyetimu.questionid)) and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid) and charindex((select [type] from tb_tiku where questionid=tb_stuzuoyetimu.[questionid]),'单项选择题多项选择题判断题')>0";
            comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
            comm.Parameters.AddWithValue("@banjiid", banjiid);
            comm.ExecuteNonQuery();
            comm.CommandText = "update tb_stuzuoyetimu set defen=0 where (zuoyeid=@zuoyeid ) and (answer is null or answer<>(select answer from tb_tiku where questionid=tb_stuzuoyetimu.questionid)) and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid) and charindex((select [type] from tb_tiku where questionid=tb_stuzuoyetimu.[questionid]),'单项选择题多项选择题判断题')>0";
            comm.ExecuteNonQuery();
            comm.CommandText = "update tb_studentzuoye set zongfen=(select sum(defen) from tb_stuzuoyetimu where studentusername=tb_studentzuoye.studentusername and zuoyeid=@zuoyeid) where zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
            comm.ExecuteNonQuery();
            st.Commit();
        }
        catch
        {
            st.Rollback();
            chenggong = false;
        }
        finally
        {
            conn.Close();
        }
        return chenggong;
    }
    public static void HuizongStuZuoyeChengji(int zuoyeid, string studentusername)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentzuoye set zongfen=(select sum(defen) from tb_stuzuoyetimu where studentusername='"+studentusername+"' and zuoyeid=" + zuoyeid + ") where zuoyeid=" + zuoyeid +" and studentusername='"+studentusername+"'";
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
    }
    public static bool HuizongZuoyeChengji(int zuoyeid)
    {
        bool chenggong = false;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentzuoye set zongfen=(select sum(defen) from tb_stuzuoyetimu where studentusername=tb_studentzuoye.studentusername  and zuoyeid=" + zuoyeid + ") where zuoyeid=" + zuoyeid;
        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            chenggong = true;
        }
        catch
        {
            chenggong = false;
        }
        finally
        {
            conn.Close();
        }
        return chenggong;
    }

    public static DataTable GetStuZuoye(string stuusername)//获取学生现在能做的作业
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT tb_kecheng.kechengname,teacherzuoye.zuoyename,tb_studentzuoye.zuoyeid,tb_studentzuoye.shangjiaoqixian,tb_studentzuoye.shangjiaoriqi,tb_studentzuoye.zongfen,tb_studentzuoye.pingyu from tb_studentzuoye inner join tb_kecheng on tb_kecheng.kechengid=tb_studentzuoye.kechengid inner join teacherzuoye on teacherzuoye.zuoyeid=tb_studentzuoye.zuoyeid where tb_studentzuoye.studentusername='" + stuusername + "' and teacherzuoye.yunxu='允许' and tb_studentzuoye.shangjiaoqixian>getdate() order by tb_studentzuoye.shangjiaoqixian desc";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetStuZuoyeInfo(string stuusername, int zuoyeid)//获取学生某个作业的信息
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT tb_kecheng.kechengname,teacherzuoye.zuoyename,tb_studentzuoye.shangjiaoqixian,tb_studentzuoye.buzhishijian,tb_studentzuoye.shangjiaoriqi,tb_studentzuoye.zongfen,tb_studentzuoye.pingyu,tb_studentzuoye.shuoming,tb_studentzuoye.yunxuzuoti,tb_studentzuoye.yunxuchakan,tb_studentzuoye.wancheng,tb_studentzuoye.stuzuoyeid,tb_studentzuoye.teacherusername from tb_studentzuoye inner join tb_kecheng on tb_kecheng.kechengid=tb_studentzuoye.kechengid inner join teacherzuoye on teacherzuoye.zuoyeid=tb_studentzuoye.zuoyeid where tb_studentzuoye.studentusername='" + stuusername + "' and tb_studentzuoye.zuoyeid=" + zuoyeid;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    //public static DataTable GetStuZuoyeTimu(string stuusername, int zuoyeid)
    //{
    //    DataTable dt = new DataTable();
    //    SqlConnection conn = new SqlConnection();
    //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    //    SqlCommand comm = conn.CreateCommand();
    //    comm.CommandText = "SELECT tb_stuzuoyetimu.stuzuoyetimuid,tb_stuzuoyetimu.tihao,tb_tiku.type,tb_tiku.timu,tb_tiku.answer as 参考答案,tb_tiku.filepath as 题目文件,tb_stuzuoyetimu.answer as 回答,tb_stuzuoyetimu.filepath as 学生文件,tb_stuzuoyetimu.fenzhi,tb_stuzuoyetimu.defen from tb_stuzuoyetimu inner join tb_tiku on tb_tiku.questionid=tb_stuzuoyetimu.questionid  where tb_stuzuoyetimu.studentusername='" + stuusername + "' and tb_stuzuoyetimu.zuoyeid=" + zuoyeid;
    //    SqlDataAdapter sda = new SqlDataAdapter(comm);
    //    sda.Fill(dt);
    //    return dt;
    //}
    public static DataTable GetStuKechengZuoyeInfo(string stuusername, int kechengid)//获取一个学生某课程的作业
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT  tb_studentzuoye.zuoyeid,teacherzuoye.zuoyename as 作业名称,tb_studentzuoye.shangjiaoqixian as 上交期限,tb_studentzuoye.buzhishijian as 布置时间,tb_studentzuoye.shangjiaoriqi as 上交日期,tb_studentzuoye.yunxuzuoti as 允许做题,tb_studentzuoye.yunxuchakan as 允许查看,tb_studentzuoye.zongfen as 成绩 from tb_studentzuoye inner join teacherzuoye on teacherzuoye.zuoyeid=tb_studentzuoye.zuoyeid where tb_studentzuoye.studentusername='" + stuusername + "' and tb_studentzuoye.kechengid=" + kechengid;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static string GetZuoyeid(string tusername, int kechengid, string zuoyename)
    {
        string zuoyeid="";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT  zuoyeid from teacherzuoye where teacherusername='" + tusername + "' and kechengid=" + kechengid + " and zuoyename='" + zuoyename + "'";
        conn.Open();
        zuoyeid = comm.ExecuteScalar().ToString();
        conn.Close();
        return zuoyeid;
    }
    public static string getZuoye_Kechengid(int zuoyeid)//得到作业的课程id
    {
       string kechengid ="";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT  kechengid from teacherzuoye where zuoyeid= "+zuoyeid.ToString();
        conn.Open();
        kechengid = comm.ExecuteScalar().ToString();
        conn.Close();
        return kechengid;
    }
    public static string getZuoyeName(int zuoyeid)//得到作业名称
    {
        string zuoyename = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT  zuoyename from teacherzuoye where zuoyeid= " + zuoyeid.ToString();
        conn.Open();
        zuoyename = comm.ExecuteScalar().ToString();
        conn.Close();
        return zuoyename;
    }
    public static bool IsTimuInZuoye(string zuoyeid, string questionid)////查询某题目的id在某作业中
    {
        bool isIn = false;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT count(*) from tb_teacherzuoyetimu where zuoyeid= " + zuoyeid +" and questionid="+questionid;
        conn.Open();
        if (((int)comm.ExecuteScalar()) > 0)
        {
            isIn = true;
        }
        conn.Close();
        return isIn;
    }
    public static void UpdateTeacherZuoyeZongfen(string zuoyeid)//更新教师作业总分
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update teacherzuoye set manfen=(select sum(fenzhi) from tb_teacherzuoyetimu where zuoyeid="+zuoyeid+") where zuoyeid="+zuoyeid;
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
    }
    public static bool IsZuoyeUsed(string zuoyeid)//检查作业是否已布置给某个班
    {
        bool isUsed = false;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select count(*) from tb_zuoyebuzhi where zuoyeid="+zuoyeid;
        conn.Open();
        if(((int)comm.ExecuteScalar())>0)
            isUsed=true;
        conn.Close();
        return isUsed;
    }
    /// <summary>
    /// 得到作业的最大题号
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <returns></returns>
    public static int GetMaxTihao(string zuoyeid)
    {
        int tihao = 0;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select count(*) from tb_teacherzuoyetimu where zuoyeid=" + zuoyeid;
        conn.Open();
        tihao = (int)(comm.ExecuteScalar());
        conn.Close();
        return tihao;
    }
    /// <summary>
    /// 得到作业中某题型的最大题号
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <param name="tixing">题型</param>
    /// <returns></returns>
    public static int getMaxTixingTihao(string zuoyeid, string tixing)
    {
        int tixinghao = 0;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select count(*) from tb_teacherzuoyetimu where zuoyeid=" + zuoyeid+" and tixing='"+tixing+"'";
        conn.Open();
        tixinghao = (int)(comm.ExecuteScalar());
        conn.Close();
        return tixinghao;
    }
    /// <summary>
    /// 获得作业布置给哪些班
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <returns></returns>
    public static DataTable GetZuoyeBanji(string zuoyeid)//获得作业布置给哪些班
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyebuzhiid,banjiname as 班级,yunxuzuoti 允许做题,yunxuchakan 允许查看结果,buzhishijian 布置时间,shangjiaoqixian 上交期限 from tb_zuoyebuzhi join tb_banji on tb_banji.banjiid=tb_zuoyebuzhi.banjiid where tb_zuoyebuzhi.zuoyeid=@zuoyeid";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    /// <summary>
    /// 得到作业布置信息
    /// </summary>
    /// <param name="zuoyebuzhiid">作业布置id</param>
    /// <returns></returns>
    public static DataTable GetZuoyeBuzhiInfo(string zuoyebuzhiid)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyebuzhiid,banjiname as 班级,teacherzuoye.zuoyename as 作业名称,yunxuzuoti 允许做题,yunxuchakan 允许查看结果,buzhishijian 布置时间,shangjiaoqixian 上交期限,shuoming 说明,tb_zuoyebuzhi.teacherusername from tb_zuoyebuzhi join tb_banji on tb_banji.banjiid=tb_zuoyebuzhi.banjiid join teacherzuoye on teacherzuoye.zuoyeid=tb_zuoyebuzhi.zuoyeid where tb_zuoyebuzhi.zuoyebuzhiid=@zuoyebuzhiid";
        comm.Parameters.AddWithValue("@zuoyebuzhiid", zuoyebuzhiid);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    /// <summary>
    /// 得到作业布置信息
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <param name="banjiid">班级id</param>
    /// <returns></returns>
    public static DataTable GetZuoyeBuzhiInfo(string zuoyeid,string banjiid)//获取作业布置信息
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyebuzhiid,banjiname as 班级,teacherzuoye.zuoyename as 作业名称,yunxuzuoti 允许做题,yunxuchakan 允许查看结果,buzhishijian 布置时间,shangjiaoqixian 上交期限,shuoming 说明,teacherzuoye.teacherusername from tb_zuoyebuzhi join tb_banji on tb_banji.banjiid=tb_zuoyebuzhi.banjiid join teacherzuoye on teacherzuoye.zuoyeid=tb_zuoyebuzhi.zuoyeid where tb_zuoyebuzhi.zuoyeid=@zuoyeid and tb_zuoyebuzhi.banjiid=@banjiid";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }

    public static int GetTimuCount(string zuoyeid)
    {
        int count = 0;
        string sqltxt = "select count(*) from tb_teacherzuoyetimu where zuoyeid=@zuoyeid";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@zuoyeid", zuoyeid);
        count =(int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt,pa));
        return count;
    }

    public static string GetKechengNameByZuoyebuzhiid(string zuoyebuzhiid)//由作业布置id得到课程名
    {
        string kechengname = string.Empty;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select kechengname from tb_kecheng where kechengid=(select kechengid from teacherzuoye where zuoyeid=(select zuoyeid from tb_zuoyebuzhi where zuoyebuzhiid=@zuoyebuzhiid))";
        comm.Parameters.AddWithValue("@zuoyebuzhiid", zuoyebuzhiid);
        conn.Open();
        kechengname = comm.ExecuteScalar().ToString();
        conn.Close();
        return kechengname;
    }
    /// <summary>
    /// 将作业分发给某班的所有学生
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <param name="banjiid">班级id</param>
    /// <returns></returns>
    //public static void ZuoyeFenfaToBanji(string zuoyeid, string banjiid)//将作业分发给某班的所有学生
    //{
    //    //找到班级学生
    //    //将作业布置信息写入学生作业表
    //    //将作业题目写入学生作业题目表
    //    DataTable stuTb = BanjiInfo.GetStudentUserName(int.Parse(banjiid));//班级学生信息表
    //    string kechengid = ZuoyeInfo.getZuoye_Kechengid(int.Parse(zuoyeid));
    //    DataTable BuzhiTb = ZuoyeInfo.GetZuoyeBuzhiInfo(zuoyeid, banjiid);//作业布置信息
    //    DataTable ZuoyeTimuTb = ZuoyeInfo.GetTeacherZuoyeTimuOrderByTixing(int.Parse(zuoyeid));//作业题目
    //    try
    //    {
    //        foreach (DataRow dr in stuTb.Rows)
    //        {
    //            //将作业布置给学生
    //            ZuoyeInfo.ZuoyeFenfaToStudent(zuoyeid, dr[0].ToString(), BuzhiTb, ZuoyeTimuTb, kechengid);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    /// <summary>
    /// 将作业分发给某个学生
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <param name="studentusername">学生用户名——学号</param>
    /// <param name="BuzhiTb">作业布置信息</param>
    /// <param name="ZuoyeTimuTb">作业题目信息</param>
    /// <param name="kechengid">课程id</param>
    /// <returns></returns>
    public static void ZuoyeFenfaToStudent(string zuoyeid, string studentusername,string kechengid,string banjiid)
    {
        DataTable timudt = ZuoyeInfo.GetZuoyeTimuIdAndFenzhi(int.Parse(zuoyeid));//作业题目信息
        DataTable buzhidt = ZuoyeInfo.GetZuoyeBuzhiInfo(zuoyeid, banjiid);//作业布置信息
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        SqlCommand comm = conn.CreateCommand();
        comm.Transaction = st;
        try
        {
            //布置信息写入学生作业表
            comm.CommandText = "insert into tb_studentzuoye(zuoyeid,studentusername,wancheng,shangjiaoqixian,zongfen,kechengid,buzhishijian,shuoming,yunxuzuoti,yunxuchakan,teacherusername) values(@zuoyeid,@studentusername,'未完成',@shangjiaoqixian,0,@kechengid,@buzhishijian,@shuoming,@yunxuzuoti,@yunxuchakan,@teacherusername)";
            comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
            comm.Parameters.AddWithValue("@studentusername", studentusername);
            comm.Parameters.AddWithValue("@kechengid", kechengid);
            comm.Parameters.AddWithValue("@buzhishijian", buzhidt.Rows[0][5].ToString());//布置时间
            comm.Parameters.AddWithValue("@shangjiaoqixian", buzhidt.Rows[0][6].ToString());//上交期限
            comm.Parameters.AddWithValue("@shuoming", buzhidt.Rows[0][7].ToString());
            comm.Parameters.AddWithValue("@yunxuzuoti",buzhidt.Rows[0][3].ToString());//允许做题
            comm.Parameters.AddWithValue("@yunxuchakan", buzhidt.Rows[0][4].ToString());//允许查看结果
            comm.Parameters.AddWithValue("@teacherusername",buzhidt.Rows[0][8].ToString());//教师
            comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "insert into tb_stuzuoyetimu(zuoyeid,questionid,studentusername,defen,fenzhi) values(@zuoyeid,@questionid,@studentusername,0,@fenzhi)";
            comm.Parameters.AddWithValue("@zuoyeid",zuoyeid);
            comm.Parameters.AddWithValue("@studentusername",studentusername);
            comm.Parameters.Add("@questionid", SqlDbType.Int);
            comm.Parameters.Add("@fenzhi", SqlDbType.Int);
            //comm.Parameters["@zuoyeid"].Value =int.Parse(zuoyeid);
            //comm.Parameters["@studentusername"].Value = studentusername.ToString();
            foreach (DataRow dr in timudt.Rows)
            {
                comm.Parameters["@questionid"].Value = Convert.ToInt32(dr[0]);
                comm.Parameters["@fenzhi"].Value = Convert.ToInt32(dr[1]);
                comm.ExecuteNonQuery();
            }
            st.Commit();
        }
        catch (Exception ex)
        {
            st.Rollback();
            throw ex;
        }
        finally
        {
            if(conn.State==ConnectionState.Open)
                conn.Close();
        }
    }
    public static bool DeleteZuoyeFromBanji(int banjiid, int zuoyeid)//删除已布置的作业，根据作业id和班级id
    {
        bool chenggong = true;
        //DataTable stuDt = BanjiInfo.GetStudentUserName(banjiid);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        //comm.Parameters.Add("@stuusername", SqlDbType.VarChar);
        //string stuusername;
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "delete from tb_stuzuoyetimu where  zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
            comm.ExecuteNonQuery();
            comm.CommandText = "delete from tb_studentzuoye where zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
            comm.ExecuteNonQuery();
            comm.CommandText = "delete from tb_zuoyebuzhi where zuoyeid=@zuoyeid and banjiid=@banjiid";
            comm.ExecuteNonQuery();
            st.Commit();
        }
        catch
        {
            st.Rollback();
            chenggong = false;
        }
        finally
        {
            conn.Close();
        }
        return chenggong;
    }
    public static bool DeleteZuoyeFromBanji(int zuoyebuzhiid)//删除已布置的作业，根据作业布置id
    {
        DataTable dt = GetZuoyeBanjiidAndZuoyeid(zuoyebuzhiid);
        int zuoyeid =(int)(dt.Rows[0][0]);
        int  banjiid= (int)(dt.Rows[0][1]);
        
        return DeleteZuoyeFromBanji(banjiid, zuoyeid);
    }
    /// <summary>
    /// 获取作业中的题型和数量
    /// </summary>
    /// <param name="zuoyeid">作业id</param>
    /// <returns></returns>
    public static DataSet GetZuoyeTixing(int zuoyeid)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = SqlDal.conn;
        string sqlstr = "select [type],count([type]) from tb_tiku where questionid in(select questionid from tb_teacherzuoyetimu where zuoyeid=@zuoyeid) group by [type]";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@zuoyeid", zuoyeid);
        ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, sqlstr);
        return ds;
    }

    public static int getZuoyeBuzhiid(int zuoyeid, int banjiid)//获取作业布置id
    {
        int buzhiid=0;
        SqlConnection conn = SqlDal.conn;
        string sqlstr = "select zuoyebuzhiid from tb_zuoyebuzhi where zuoyeid=@zuoyeid and banjiid=@banjiid";
        SqlParameter[] pa = new SqlParameter[2];
        pa[0] = new SqlParameter("@zuoyeid", zuoyeid);
        pa[1] = new SqlParameter("@banjiid", banjiid);
        buzhiid =(int)( SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlstr, pa));
        return buzhiid;
    }

    public static bool SetZuoyeYunxuZuoti(int zuoyeid, int banjiid, string yunxu)//允许或禁止学生做作业
    {
        bool chenggong = true;
        SqlConnection conn = SqlDal.conn;
        conn.Open();
        SqlCommand comm = conn.CreateCommand();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_zuoyebuzhi set yunxuzuoti=@yunxuzuoti where zuoyeid=@zuoyeid and banjiid=@banjiid";
            comm.Parameters.AddWithValue("@yunxuzuoti", yunxu);
            comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
            comm.Parameters.AddWithValue("@banjiid", banjiid);
            comm.ExecuteNonQuery();
            comm.CommandText = "update tb_studentzuoye set yunxuzuoti=@yunxuzuoti where zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
            comm.ExecuteNonQuery();
            st.Commit();
        }
        catch
        {
            st.Rollback();
            chenggong = false;
        }
        finally
        {
            conn.Close();
        }
        return chenggong;
    }

    public static bool SetZuoyeYunxuChakan(int zuoyeid, int banjiid, string yunxuchakan)//允许或禁止学生查看作业批改结果
    {
        bool chenggong = true;
        SqlConnection conn = SqlDal.conn;
        conn.Open();
        SqlCommand comm = conn.CreateCommand();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_zuoyebuzhi set yunxuchakan=@yunxu where zuoyeid=@zuoyeid and banjiid=@banjiid";
            comm.Parameters.AddWithValue("@yunxu", yunxuchakan);
            comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
            comm.Parameters.AddWithValue("@banjiid", banjiid);
            comm.ExecuteNonQuery();
            comm.CommandText = "update tb_studentzuoye set yunxuchakan=@yunxu where zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
            comm.ExecuteNonQuery();
            st.Commit();
        }
        catch
        {
            st.Rollback();
            chenggong = false;
        }
        finally
        {
            conn.Close();
        }
        return chenggong;
    }
    public static bool SetZuoyeBuzhiInfo(int banjiid, int zuoyeid,string yunxuzuoti,string yunxuchakan,string shangjiaoqixian,string shuoming)//更新作业布置信息
    {
        bool chenggong = true;
        SqlConnection conn = SqlDal.conn;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_zuoyebuzhi set yunxuzuoti=@yunxuzuoti,yunxuchakan=@yunxuchakan,shangjiaoqixian=@shangjiaoqixian,shuoming=@shuoming where zuoyeid=@zuoyeid and banjiid=@banjiid";
            comm.Parameters.AddWithValue("@yunxuzuoti", yunxuzuoti);
            comm.Parameters.AddWithValue("@yunxuchakan", yunxuchakan);
            comm.Parameters.AddWithValue("@shangjiaoqixian", shangjiaoqixian);
            comm.Parameters.AddWithValue("@shuoming", shuoming);
            comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
            comm.Parameters.AddWithValue("@banjiid", banjiid);
            comm.ExecuteNonQuery();
            comm.CommandText = "update tb_studentzuoye set yunxuzuoti=@yunxuzuoti,yunxuchakan=@yunxuchakan,shangjiaoqixian=@shangjiaoqixian,shuoming=@shuoming where zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
            comm.ExecuteNonQuery();
            st.Commit();
        }
        catch(Exception ex)
        {
            st.Rollback();
            chenggong = false;
            throw ex;
        }
        finally
        {
            conn.Close();
        }
        return chenggong;    
    }
    public static DataTable GetZuoyeBanjiidAndZuoyeid(int zuoyebuzhiid)//由作业布置id 获取作业id和班级id
    {
        DataTable dt = new DataTable();
        SqlConnection conn = SqlDal.conn;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select zuoyeid,banjiid from tb_zuoyebuzhi where zuoyebuzhiid=@zuoyebuzhiid";
        comm.Parameters.AddWithValue("@zuoyebuzhiid", zuoyebuzhiid);
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable ZuoyeFenxi(int banjiid, int zuoyeid)//作业分析，最高分，最低分，平均分
    {
        SqlConnection conn = SqlDal.conn;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select max(zongfen) as 最高分,min(zongfen) as 最低分, avg(zongfen) as 平均分 from tb_studentzuoye where zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }

    public static double ZuoyeTimuDefenLv(int banjiid, int zuoyeid, int questionid)//某班某作业的得分率
    {
        double defenlv = 0;
        double zongfen = 1;
        double zdf = 0;
        SqlConnection conn = SqlDal.conn;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select sum(fenzhi),sum(defen) from tb_stuzuoyetimu where questionid=@questionid and zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        comm.Parameters.AddWithValue("@questionid", questionid);
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            zongfen = Convert.ToDouble(sdr[0]);
            zdf = Convert.ToDouble(sdr[1]);
        }
        sdr.Close();
        conn.Close();
        defenlv = zdf / zongfen;
        return defenlv;
    }
    public static int GetZuoyeShangjiaoRenShu(int zuoyeid,int banjiid)
    {
        int shangjiaoshu = 0;
        string sqltxt = "select count(*) from tb_studentzuoye where shangjiaoriqi is not null and zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
        SqlParameter[] pa = new SqlParameter[2];
        pa[0] = new SqlParameter("@zuoyeid", zuoyeid);
        pa[1] = new SqlParameter("@banjiid", banjiid);
        shangjiaoshu = (int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa));
        return shangjiaoshu;
    }
}
