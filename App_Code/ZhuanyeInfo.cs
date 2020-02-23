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
using System.Collections.Generic;

/// <summary>
///ZhuanInfo 的摘要说明
/// </summary>
public class ZhuanyeInfo
{
	public ZhuanyeInfo()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static int GetMinZhuanyeId()
    {
        int zhuanyeid = 0;
        string sqltxt = "select top 1 zhuanyeid from tb_zhuanye order by zhuanyeid asc";
        zhuanyeid=(int)(SqlHelper.ExecuteScalar(SqlDal.strConnectionString, CommandType.Text, sqltxt));
        return zhuanyeid;
    }
    public static DataSet GetZhuanyeDataSet()
    {
        DataSet ds = new DataSet();
        string sqltxt = "select zhuanyeid,zhuanyename from tb_zhuanye order by zhuanyeid asc";
        ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt);
        return ds;
    }
    public static Dictionary<string, int> ZhuanyeDict()
    {
        DataSet ds = new DataSet();
        string sqltxt = "select zhuanyeid,zhuanyename from tb_zhuanye order by zhuanyeid asc";
        ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt);
        Dictionary<string, int> zhuanye = new Dictionary<string, int>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            zhuanye.Add(dr[1].ToString(), (int)(dr[0]));
        }
        return zhuanye;
    }
}
