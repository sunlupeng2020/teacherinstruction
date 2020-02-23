using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class teachermanage_ceshistuzice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string banjiid = Request.QueryString["banjiid"];
            string stuun = Request.QueryString["studentusername"];
            Labelbj.Text = BanjiInfo.GetBanjiName(int.Parse(banjiid));
            Labelxm.Text = StudentInfo.GetStuXingming(stuun);
            Labelstuun.Text = stuun;
        }
    }
}
