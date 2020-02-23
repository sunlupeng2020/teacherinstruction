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

public partial class youke_ziyuanxiazai : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UCZiyuanXiaZai1.Username = "youke";
        UCZiyuanXiaZai1.Usershenfen = "youke";
        UCZiyuanXiaZai1.KechengId= ((HiddenField)(this.Master.FindControl("hdf_kechengid"))).Value;
    }
}
