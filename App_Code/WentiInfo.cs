using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
/// <summary>
///WentiInfo 的摘要说明
/// </summary>
public class WentiInfo
{
	public WentiInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static DataTable GetWenti(List<int> zhishidianids)//按知识点查询问题
    {
        DataTable dt = new DataTable();
        string tablename = "wt" + DateTime.Now.Ticks.ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "Create table " + tablename + "(id int)";
        conn.Open();
        comm.ExecuteNonQuery();
        //把知识点id插入到表中
        foreach (int s in zhishidianids)
        {
            comm.CommandText = "insert into " + tablename + "(id) values(" + s + ")";
            comm.ExecuteNonQuery();
        }
        //查询知识点相关问题
        conn.Close();
        //问题显示
        comm.CommandText = "select wentiid as 问题ID号,biaoti as 问题标题,shijian as 提问时间,(select count(wentiid) from tb_huida where wentiid=tb_wenti.wentiid) as 回答数  from tb_wenti where (wentiid in(select wentiid from tb_wenti_zhishidian where zhishidianid in(select id from " + tablename + ")))";
        DataTable wentidt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        conn.Open();
        comm.CommandText = "drop table " + tablename;
        comm.ExecuteNonQuery();
        conn.Close();
        return dt;
    }
    public static DataTable GetWenti(string guanjianzi)//按关键字查询问题
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //问题显示
        comm.CommandText = "select wentiid as 问题ID号,biaoti as 问题标题,shijian as 提问时间,(select count(wentiid) from tb_huida where wentiid=tb_wenti.wentiid) as 回答数  from tb_wenti where (wenti like '%" + guanjianzi + "%' or biaoti like '%" + guanjianzi + "%')";
        DataTable wentidt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetWenti(int kechengid)//按课程ID号查询问题
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //问题显示
        comm.CommandText = "select wentiid as 问题ID号,biaoti as 问题标题,shijian as 提问时间,(select count(wentiid) from tb_huida where wentiid=tb_wenti.wentiid) as 回答数  from tb_wenti where kechengid=" + kechengid;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
    public static DataTable GetWenti(string username, string usershenfen)//按用户搜索问题
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //问题显示
        comm.CommandText = "select wentiid as 问题ID号,biaoti as 问题标题,shijian as 提问时间,(select count(wentiid) from tb_huida where wentiid=tb_wenti.wentiid) as 回答数  from tb_wenti where username='"+username+"' and shenfen='"+usershenfen+"'";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        return dt;
    }
}
