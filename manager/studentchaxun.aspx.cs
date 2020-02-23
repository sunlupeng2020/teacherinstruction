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

public partial class manager_studentchaxun : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_xhchaxun_Click(object sender, EventArgs e)
    {
        DataSet ds = StudentInfo.GetStuInfoByXuehao(tbx_xuehao.Text.Trim());
        grv_stu.DataSource = ds;
        grv_stu.DataBind();
    }
    protected void btn_xmchaxun_Click(object sender, EventArgs e)
    {
        DataSet ds = StudentInfo.GetStuInfoByXingming(tbx_xingming.Text.Trim());
        grv_stu.DataSource = ds;
        grv_stu.DataBind();
    }
}
