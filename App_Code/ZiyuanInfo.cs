using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
///Ziyuan 的摘要说明
/// </summary>
public class ZiyuanInfo
{
    public ZiyuanInfo()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static DataTable GetZiyuan(List<int> zhishidianids, string ziyuanleixing, string meitileixing)//根据知识点获取资源,得到完全公开的资源
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        string tmptbname = "tmptb" + DateTime.Now.Ticks.ToString();
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        comm.CommandText = "create table " + tmptbname + "(id int)";
        comm.ExecuteNonQuery();
        foreach (int zsdid in zhishidianids)
        {
            comm.CommandText = "insert into " + tmptbname + "(id) values(" + zsdid + ")";
            comm.ExecuteNonQuery();
        }////
        conn.Close();
        string sqltxt = sqltxt = "select jiaoxueziyuanid as 资源ID,jiaoxueziyuanname as 资源名称,jiaoxueziyuanleixing as 资源类型,ziyuanmeitileixing as 媒体类型,instruction as 介绍,createtime as 上传时间,ziyuanfile as 资源文件 from tb_Jiaoxueziyuan where (jiaoxueziyuanid in(select jiaoxueziyuanid from tb_ziyuanzhishidian where zhishidianid in(select id from " + tmptbname + "))) and (quanxian='完全公开')";
        if (ziyuanleixing != "全部")
        {
            sqltxt += " and(jiaoxueziyuanleixing='" + ziyuanleixing + "')";
        }
        if (meitileixing != "全部")
            sqltxt += " and(ziyuanmeitileixing='" + meitileixing + "')";
        comm.CommandText = sqltxt;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        try
        {
            conn.Open();
            comm.CommandText = "drop table " + tmptbname;
            comm.ExecuteNonQuery();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        return dt;
    }

    public static DataTable GetZiyuan(List<int> zhishidianids, string username, string usershenfen, string ziyuanleixing, string meitileixing)//根据知识点获取资源
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        string tmptbname = "tmptb" + DateTime.Now.Ticks.ToString();
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        comm.CommandText = "create table " + tmptbname + "(id int)";
        comm.ExecuteNonQuery();
        foreach (int zsdid in zhishidianids)
        {
            comm.CommandText = "insert into " + tmptbname + "(id) values(" + zsdid + ")";
            comm.ExecuteNonQuery();
        }////
        conn.Close();
        string sqltxt = "";
        if (usershenfen == "teacher" || usershenfen == "manager")
        {
            sqltxt = "select jiaoxueziyuanid as 资源ID,jiaoxueziyuanname as 资源名称,jiaoxueziyuanleixing as 资源类型,ziyuanmeitileixing as 媒体类型,instruction as 介绍,createtime as 上传时间,ziyuanfile as 资源文件 from tb_Jiaoxueziyuan where (jiaoxueziyuanid in(select jiaoxueziyuanid from tb_ziyuanzhishidian where zhishidianid in(select id from " + tmptbname + "))) and (quanxian='完全公开' or quanxian='只对教师' or(quanxian='仅限本人' and username='" + username + "'))";
        }
        else
        {
            sqltxt = "select jiaoxueziyuanid as 资源ID,jiaoxueziyuanname as 资源名称,jiaoxueziyuanleixing as 资源类型,ziyuanmeitileixing as 媒体类型,instruction as 介绍,createtime as 上传时间,ziyuanfile as 资源文件 from tb_Jiaoxueziyuan where (jiaoxueziyuanid in(select jiaoxueziyuanid from tb_ziyuanzhishidian where zhishidianid in(select id from " + tmptbname + "))) and (quanxian='完全公开')";
        }
        if (ziyuanleixing != "全部")
        {
            sqltxt += " and(jiaoxueziyuanleixing='" + ziyuanleixing + "')";
        }
        if (meitileixing != "全部")
            sqltxt += " and(ziyuanmeitileixing='" + meitileixing + "')";
        comm.CommandText = sqltxt;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        try
        {
            conn.Open();
            comm.CommandText = "drop table " + tmptbname;
            comm.ExecuteNonQuery();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        return dt;
    }

    public static DataTable GetGuanjianziZiyuan(string guanjz)//根据关键字获取资源，得到完全公开的资源
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        string sqltxt = "select jiaoxueziyuanid as 资源ID,jiaoxueziyuanname as 资源名称,jiaoxueziyuanleixing as 资源类型,ziyuanmeitileixing as 媒体类型,createtime as 上传时间,ziyuanfile as 资源文件 from tb_Jiaoxueziyuan where (jiaoxueziyuanname like '%" + guanjz + "%' or guanjianzi like '%" + guanjz + "%' or instruction like '%" + guanjz + "%') and (quanxian='完全公开')";
        comm.CommandText = sqltxt;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }


    public static DataTable GetGuanjianziZiyuan(string username, string usershenfen, string guanjz)//根据关键字获取资源
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        string sqltxt = "select jiaoxueziyuanid as 资源ID,jiaoxueziyuanname as 资源名称,jiaoxueziyuanleixing as 资源类型,ziyuanmeitileixing as 媒体类型,createtime as 上传时间,ziyuanfile as 资源文件 from tb_Jiaoxueziyuan where (jiaoxueziyuanname like '%" + guanjz + "%' or guanjianzi like '%" + guanjz + "%' or instruction like '%" + guanjz + "%') ";
        if (usershenfen == "teacher" || usershenfen == "manager")
        {
            sqltxt += " and (quanxian='完全公开' or quanxian='只对教师' or(quanxian='仅限本人' and username='" + username + "'))";
        }
        else
        {
            sqltxt += " and (quanxian='完全公开')";
        }
        comm.CommandText = sqltxt;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetZiyunaXiangqing(int jiaoxueziyuanid)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "SELECT [jiaoxueziyuanname],tb_kecheng.kechengname,tb_teacher.xingming,[jiaoxueziyuanleixing], [ziyuanmeitileixing], tb_jiaoxueziyuan.[instruction], [quanxian], tb_jiaoxueziyuan.[createtime], [guanjianzi], [filesize], [xiangguanzhishidian] FROM [tb_Jiaoxueziyuan] inner join tb_kecheng on tb_kecheng.kechengid=tb_jiaoxueziyuan.kechengid inner join  tb_teacher on tb_teacher.username=tb_jiaoxueziyuan.username WHERE ([jiaoxueziyuanid] = " + jiaoxueziyuanid + ")";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
}
