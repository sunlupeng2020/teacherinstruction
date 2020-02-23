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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

/// <summary>
///TimuInfo 的摘要说明
/// </summary>
public class TimuInfo
{
	public TimuInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static DataTable GetTimuOnZhishidian(List<string> ZhishidianList,string tixing)//从知识点列表得到相关题目
    {
        DataTable dt = new DataTable();
        string tablename = "tb_" + DateTime.Now.Ticks.ToString();//临时数据表名
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        try
        {
            myconn.Open();
            mycomm.CommandText = "create table " + tablename + "(jiegouid int)";
            mycomm.ExecuteNonQuery();
            foreach (string str in ZhishidianList)
            {
                mycomm.CommandText = "insert into " + tablename + "(jiegouid) values(" + str + ")";
                mycomm.ExecuteNonQuery();
            }
            myconn.Close();
            switch (tixing)
            {
                case "全部题型":
                    mycomm.CommandText = "select tb_tiku.questionid,tb_tiku.timu as 题目,tb_tiku.[type] as 题型,tb_tiku.answer,tb_tiku.tigongzhe,tb_tiku.filepath  from tb_tiku where tb_tiku.questionid in ( select questionid from tb_timuzhishidian where questionid not in ( select questionid from tb_timuzhishidian where kechengjiegouid not in (select jiegouid from " + tablename + "))) order by tb_tiku.questionid";
                    break;
                default:
                    mycomm.CommandText = "select tb_tiku.questionid,tb_tiku.timu as 题目,tb_tiku.[type] as 题型,tb_tiku.answer,tb_tiku.tigongzhe,tb_tiku.filepath from  tb_tiku  where tb_tiku.[type]='" + tixing + "' and   tb_tiku.questionid in(select questionid from tb_timuzhishidian where questionid not in(select questionid from tb_timuzhishidian where kechengjiegouid not in (select jiegouid from " + tablename + "))) order by tb_tiku.questionid";
                    break;
            }
            SqlDataAdapter myda = new SqlDataAdapter(mycomm);
            myda.Fill(dt);
            try
            {
                myconn.Open();
                mycomm.CommandText = "drop table " + tablename;
                mycomm.ExecuteNonQuery();
            }
            finally
            {
                if (myconn.State == ConnectionState.Open)
                    myconn.Close();
            }
        }
        catch (Exception e1)
        {
            throw e1;
        }
        finally
        {
            if (myconn.State.ToString() == "Opened")
                myconn.Close();
        }
        return dt;

    }
    /// <summary>
    /// 检索课程题型题目
    /// </summary>
    /// <param name="kechengid">课程id号</param>
    /// <param name="tixing">题型</param>
    /// <returns></returns>
    public static DataTable GetTimuOnKecheng(string kechengid, string tixing)//得到课程、题型相关题目
    {
        DataTable dt = new DataTable();
        string tablename = "tb_" + DateTime.Now.Ticks.ToString();//临时数据表名
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        try
        {
            switch (tixing)
            {
                case "全部题型":
                    mycomm.CommandText = "select questionid,timu as 题目,[type] as 题型,answer,tigongzhe,filepath  from tb_tiku where kechengid="+kechengid+" order by tb_tiku.questionid";
                    break;
                default:
                    mycomm.CommandText = "select questionid,timu as 题目,[type] as 题型,answer,tigongzhe,filepath  from tb_tiku where kechengid=" + kechengid + " and [type]='"+tixing+"' order by tb_tiku.questionid";
                    break;
            }
            SqlDataAdapter myda = new SqlDataAdapter(mycomm);
            myda.Fill(dt);
        }
        catch (Exception e1)
        {
            throw e1;
        }
        finally
        {
            if (myconn.State.ToString() == "Opened")
                myconn.Close();
        }
        return dt;

    }
    public static string TimuZhishidian(int questionid)
    {
        StringBuilder s = new StringBuilder();
        SqlConnection conn = SqlDal.conn;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select jiegouname from tb_kechengjiegou where kechengjiegouid in(select kechengjiegouid from tb_timuzhishidian where questionid=@questionid)";
        comm.Parameters.AddWithValue("@questionid", questionid);
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        while (sdr.Read())
        {
            s.Append(sdr.GetString(0)+" ");
        }
        sdr.Close();
        conn.Close();
        return (s.ToString());
    }
}
