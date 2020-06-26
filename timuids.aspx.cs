using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class timuids : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ConcreteKnowladge ck1 = new ConcreteKnowladge(85, "ASP.NET概述");
        Label1.Text = "a:" + ck1.ZhishidianIds;
    }
}