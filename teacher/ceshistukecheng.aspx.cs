using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

public partial class teachermanage_ceshistukecheng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string banjiid = Request.QueryString["banjiid"];
            string kechengid = Session["kechengid"].ToString();
            Labelbj.Text = BanjiInfo.GetBanjiName(int.Parse(banjiid));
            string stuusername = Request.QueryString["studentusername"];
            Labelxm.Text = StudentInfo.GetStuXingming(stuusername);
             Labelstuun.Text = stuusername;
             BindStuCeshiInfo();
        }
    }
    protected void BindStuCeshiInfo()
    {
        string sqltxt = "SELECT tb_teachershijuan.ceshiname 测试名称,[tb_studentkaoshi].[zongfen] 成绩, [tb_studentkaoshi].[kaishi_shijian] 开始时间, [tb_studentkaoshi].[jiaojuanshijian] 交卷时间, [timeyanchang] 延长时间,[tb_studentkaoshi].[jiaojuan] 状态, [tb_studentkaoshi].[yunxu] 是否允许测试 FROM [tb_studentkaoshi] inner join tb_teachershijuan on tb_teachershijuan.shijuanid=[tb_studentkaoshi].shijuanid WHERE (([tb_studentkaoshi].[banjiid] = @banjiid) AND ([tb_studentkaoshi].[kechengid] = @kechengid) AND ([tb_studentkaoshi].[studentusername] = @studentusername))";
        SqlParameter[] pa = new SqlParameter[3];
        pa[0] = new SqlParameter("@banjiid", Request.QueryString["banjiid"]);
        pa[1] = new SqlParameter("@kechengid", Session["kechengid"].ToString());
        pa[2]=new SqlParameter("@studentusername",Request.QueryString["studentusername"]);
        DataSet ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
}
