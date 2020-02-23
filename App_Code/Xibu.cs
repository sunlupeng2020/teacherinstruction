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
using System.Data.SqlClient;

/// <summary>
///Xibu 的摘要说明
/// </summary>
public class Xibu
{
	public Xibu()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string GetXibuXhqianzhui(int xibuid)//获取系部学号前缀，用于一些特殊的系部，特殊的学号，避免学号冲突
    {
        string qianzhui = string.Empty;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select xhqz from tb_xibu where  xibuid=@xibuid";
        comm.Parameters.AddWithValue("@xibuid", xibuid);
        conn.Open();
        qianzhui = comm.ExecuteScalar().ToString();
        conn.Close();
        return qianzhui;
    }
}
