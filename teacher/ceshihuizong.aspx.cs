using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class teachermanage_ceshihuizong : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string banjiid = Request.QueryString["banjiid"];
            Labelbanji.Text = BanjiInfo.GetBanjiName(int.Parse(banjiid));
            BindCeshiChengjiHuizong();
        }
    }
    protected void BindCeshiChengjiHuizong()
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        string banjiid = Request.QueryString["banjiid"];
        DataTable ds = CeshiInfo.CeshiHuizong(username, kechengid, banjiid);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
}
