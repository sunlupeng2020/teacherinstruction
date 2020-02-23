using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Collections.Generic;

public partial class teachermanage_jiaoxueziyuan_ziyuanxiazai : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        UCZiyuanXiaZai1.KechengId = Session["kechengid"].ToString();
        UCZiyuanXiaZai1.Username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        UCZiyuanXiaZai1.Usershenfen = "teacher";
    }
}
