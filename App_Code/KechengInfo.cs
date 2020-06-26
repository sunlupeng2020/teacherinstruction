using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
///KechengInfo 的摘要说明
/// </summary>
public class KechengInfo
{
	public KechengInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
    
	}
    public static int GetKechengCount()
    {
        int count = 0;
        string sqltxt = "select count(*) from tb_kecheng";
        count =Convert.ToInt32(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt));
        return count;
    }
    public static bool IsTeacherManageKecheng(string username, string kechengid)//教师是否管理某课程
    {
        bool ismg = true;
        string sqltxt = "select count(*) from tb_kecheng where guanliyuan=@username and kechengid=@kechengid";
        SqlParameter[] pa=new SqlParameter[2];
        pa[0]=new SqlParameter("@username",username);
        pa[1]=new SqlParameter("@kechengid",kechengid);
        if (((int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt, pa))) <= 0)
            ismg = false;
        return ismg;
    }
    public static DataSet GetTeacherManageKecheng(string username)//获取教师管理的课程
    {
        DataSet ds = new DataSet();
        string sqlstr = "SELECT [kechengid], [kechengname] FROM [tb_Kecheng] WHERE ([guanliyuan] = @tusername) ORDER BY [createtime] DESC";
        SqlParameter[] pa = new SqlParameter[1];
        pa[0] = new SqlParameter("@tusername", username);
        ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqlstr, pa);
        return ds;
    }
    public static DataSet GetKecheng()
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select kechengname,kechengid from tb_kecheng";
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(ds);
        return ds;
    }
    public static int MinKechengid()
    {
        int minkechengid = 0;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select min(kechengid) from tb_kecheng";
        conn.Open();
        try
        {
            minkechengid = Convert.ToInt32(comm.ExecuteScalar());
        }
        finally
        {
            conn.Close();
        }
        return minkechengid;
    }


    public static string getKechengname(string kechengid)//由课程id得到课程名称
    {
        string kechengname = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select kechengname from tb_kecheng where  kechengid=@kechengid";
        comm.Parameters.AddWithValue("@kechengid", kechengid);
        conn.Open();
        kechengname = comm.ExecuteScalar().ToString();
        conn.Close();
        return kechengname;
    }
    public static void CreateKechengJiegouXml(string kechengid,string path)
    {
        string kechengname = getKechengname(kechengid);
        string sqltxt = "select kechengjiegouid,jiegouname,shangwei,xuhao from tb_kechengjiegou where kechengid=" + kechengid + "";
        DataTable dt = (SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt)).Tables[0];
        //创建课程结构XML
        XmlWriterSettings setting = new XmlWriterSettings();
        setting.Indent = true;
        setting.IndentChars = "\t";
        using (XmlWriter writer = XmlWriter.Create(path, setting))
        {
            writer.WriteStartDocument(false);
            writer.WriteComment(kechengname + "课程结构");
            DataRow[] dr = dt.Select("shangwei=0", "xuhao asc");
            writer.WriteStartElement("知识点");
            writer.WriteAttributeString("id", dr[0][0].ToString());
            writer.WriteAttributeString("name", dr[0][1].ToString());
            writer.WriteAttributeString("shangwei", dr[0][2].ToString());
            writer.WriteAttributeString("xuhao", dr[0][3].ToString());
            writeChildnode(dt, (int)(dr[0][0]), writer);
            writer.WriteEndElement();
            writer.Flush();
        }
    }
    protected static void writeChildnode(DataTable dt, int shangwei, XmlWriter writer)
    {
        DataRow[] dr = dt.Select("shangwei=" + shangwei, "xuhao asc");
        if (dr.Length > 0)
        {
            foreach (DataRow drr in dr)
            {
                writer.WriteStartElement("知识点");
                writer.WriteAttributeString("id", drr[0].ToString());
                writer.WriteAttributeString("name", drr[1].ToString());
                writer.WriteAttributeString("shangwei", drr[2].ToString());
                writer.WriteAttributeString("xuhao", drr[3].ToString());
                writeChildnode(dt, (int)(drr[0]), writer);
                writer.WriteEndElement();
            }
        }
    }

}
