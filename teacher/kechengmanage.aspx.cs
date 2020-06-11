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
using Microsoft.ApplicationBlocks.Data;

public partial class manager_kechengmanage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LinkButton delbtn = (LinkButton)(e.Row.FindControl("LinkButton3"));
                string kechengid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                //delbtn.Attributes.Add(
                DataTable timudt = TimuInfo.GetTimuOnKecheng(kechengid, "全部题型");
                if (timudt.Rows.Count > 0)
                {
                    delbtn.Enabled = false;
                }
            }
            catch
            {
            }
            finally
            {
            }
        }
    }
}
