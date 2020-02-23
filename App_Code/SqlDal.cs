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
using System.Data.SqlTypes;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
///sqlDal 的摘要说明
/// </summary>
public class SqlDal
{
    public static DataSet dsCommon = new DataSet();

    public static string strConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    public static SqlConnection conn =new SqlConnection(strConnectionString);

    public SqlDal()
    {
        //得到数据库连接字符串
        //strConnectionString 
    }
}
