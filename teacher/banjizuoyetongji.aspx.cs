using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

public partial class teachermanage_banjizuoyetongji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindZuoyeTonji();
    }
    protected void BindZuoyeTonji()
    {
        string kechengid = Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string banjiid = Request.QueryString["banjiid"];
        DataTable dt = ZuoyeInfo.StuZuoyeHuiZong(int.Parse(kechengid),int.Parse(banjiid));
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
}
