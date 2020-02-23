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
using System.Data.SqlClient;

public partial class teachermanage_zuoye_editbuzhi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox1.Text = ((Label)(FormView1.FindControl("Label4"))).Text;//作业上交期限文本框
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//提交作业布置信息
    {
        lbl_fankui.Text = "";
        string shangjiaoqixian = TextBox1.Text;
        string yunxu = RadioButtonList1.SelectedValue;
        string yunxuchakan = RadioButtonList2.SelectedValue;
        string shuoming = TextBox2.Text;
        string zuoyebuzhiid = Request.QueryString["zuoyebuzhiid"];
        string zuoyeid=string.Empty;
        string banjiid=string.Empty;
        DataTable dt = ZuoyeInfo.GetZuoyeBanjiidAndZuoyeid(int.Parse(zuoyebuzhiid));
        if (dt.Rows.Count > 0)
        {
            zuoyeid = dt.Rows[0][0].ToString();
            banjiid = dt.Rows[0][1].ToString();
            try
            {
                ZuoyeInfo.SetZuoyeBuzhiInfo(int.Parse(banjiid), int.Parse(zuoyeid), yunxu, yunxuchakan, shangjiaoqixian, shuoming);
                lbl_fankui.Text = "作业布置信息更新成功！";
                FormView1.DataBind();
            }
            catch (Exception ex)
            {
                lbl_fankui.Text = "作业布置信息更新失败！" + ex.Message;
            }
        }
        else
        {
            lbl_fankui.Text = "未找到作业布置信息！";
        }
    }
}
