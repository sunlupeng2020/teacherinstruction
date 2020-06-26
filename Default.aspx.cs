using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.User.Identity.Name != null)
            {
                string username = HttpContext.Current.User.Identity.Name;

                if (username != string.Empty)
                {
                    Literal1.Text = "欢迎您！用户：" + username;
                    user_login.Visible = false;
                    user_logout.Visible = true;
                }
                else
                {
                    Literal1.Text = "欢迎您！";
                    user_login.Visible = true;
                    user_logout.Visible = false;
                }
            }
        }
        catch
        {
            Literal1.Text = "欢迎您！";
            user_login.Visible = true;
            user_logout.Visible = false;
        }
    }
}
