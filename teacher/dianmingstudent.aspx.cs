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
using System.Web.Security;

public partial class teachermanage_dianmingstudent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string kechengid = Session["kechengid"].ToString();
        string banjiid = Request.QueryString["banjiid"];
        string stuun = Request.QueryString["studentusername"];
        Labelkc.Text = KechengInfo.getKechengname(kechengid);
        Labelbj.Text =BanjiInfo.GetBanjiName(int.Parse(banjiid));
        Labelxm.Text =StudentInfo.GetStuXingming(stuun);
        Labelun.Text = stuun;
        BindDianMingStu();
    }
    protected void BindDianMingStu()
    {
        string kechengid = Session["kechengid"].ToString();
        string banjiid = Request.QueryString["banjiid"];
        string stuun = Request.QueryString["studentusername"];
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string sqltxt="SELECT [tb_dianmingstu].[zhuangtai],tb_dianming.dianmingtime FROM [tb_dianmingstu] inner join tb_dianming on [tb_dianmingstu].dianmingid=tb_dianming.dianmingid WHERE ([tb_dianmingstu].[studentusername] = @studentusername) AND ([tb_dianmingstu].dianmingid in (select dianmingid from tb_dianming where [banjiid] = @banjiid and kechengid=@kechengid and teacherusername=@teacherusername)) order by tb_dianming.dianmingtime";
        SqlParameter[] pa=new SqlParameter[4];
        pa[0]=new SqlParameter("@studentusername",stuun);
        pa[1]=new SqlParameter("@banjiid",banjiid);
        pa[2]=new SqlParameter("@kechengid",kechengid);
        pa[3] = new SqlParameter("@teacherusername", username);
        DataSet ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt, pa);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
}
