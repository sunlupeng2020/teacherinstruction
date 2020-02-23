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

public partial class teachermanage_dianmingtongji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBanji();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//统计点名信息
    {
        if ( DropDownList2.Items.Count > 0)
        {
            string kechengid = Session["kechengid"].ToString();
            string banjiid = DropDownList2.SelectedValue;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select count(dianmingid) from tb_dianming where kechengid=" + kechengid + " and banjiid=" + banjiid + " and teacherusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "'";
            conn.Open();
            Label1.Text = cmd.ExecuteScalar().ToString();
            conn.Close();
            GridView1.DataBind();
        }

    }
    protected void BindBanji()
    {
        int kechengid = int.Parse(Session["kechengid"].ToString());
        string tu = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DropDownList2.DataSource = BanjiInfo.GetTeacherRenkeBanji(tu, kechengid);
        DropDownList2.DataBind();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        string stuunm;
        HyperLink h1;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        conn.Open();
        string kechengid = Session["kechengid"].ToString();
        if (GridView1.Rows.Count > 0)
        {
            BindDianmingcishu();
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                stuunm = gvr.Cells[1].Text.Trim();
                cmd.CommandText = "select  count(studianmingid) from tb_dianmingstu where studentusername='" + stuunm + "' and zhuangtai='在岗' and dianmingid in(select dianmingid from tb_dianming where teacherusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "' and kechengid=" + kechengid + " and banjiid=" + DropDownList2.SelectedValue + ")";
                gvr.Cells[3].Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "select  count(studianmingid) from tb_dianmingstu where studentusername='" + stuunm + "' and zhuangtai='请假' and dianmingid in(select dianmingid from tb_dianming where teacherusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "' and kechengid=" + kechengid + " and banjiid=" + DropDownList2.SelectedValue + ")";
                gvr.Cells[4].Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "select  count(studianmingid) from tb_dianmingstu where studentusername='" + stuunm + "' and zhuangtai='迟到' and dianmingid in(select dianmingid from tb_dianming where teacherusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "' and kechengid=" + kechengid + " and banjiid=" + DropDownList2.SelectedValue + ")";
                gvr.Cells[5].Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "select  count(studianmingid) from tb_dianmingstu where studentusername='" + stuunm + "' and zhuangtai='早退' and dianmingid in(select dianmingid from tb_dianming where teacherusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "' and kechengid=" + kechengid + " and banjiid=" + DropDownList2.SelectedValue + ")";
                gvr.Cells[6].Text = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "select  count(studianmingid) from tb_dianmingstu where studentusername='" + stuunm + "' and zhuangtai='旷课' and dianmingid in(select dianmingid from tb_dianming where teacherusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "' and kechengid=" + kechengid + " and banjiid=" + DropDownList2.SelectedValue + ")";
                gvr.Cells[7].Text = cmd.ExecuteScalar().ToString();
                h1 = (HyperLink)(gvr.Cells[8].FindControl("HyperLink1"));
                h1.NavigateUrl = "dianmingstudent.aspx?kechengid=" + kechengid + "&banjiid=" + DropDownList2.SelectedValue + "&studentusername=" + stuunm;
            }
        }
        conn.Close();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex > 0)
        {
            GridView1.DataBind();
        }
    }
    protected void BindDianmingcishu()//绑定点名次数
    {
        string kechengid = Session["kechengid"].ToString();
        string banjiid = DropDownList2.SelectedValue;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select count(dianmingid) from tb_dianming where kechengid=" + kechengid + " and banjiid=" + banjiid + " and teacherusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "'";
        conn.Open();
        Label1.Text = cmd.ExecuteScalar().ToString();
        conn.Close();
    }
    protected void DropDownList2_DataBound(object sender, EventArgs e)
    {
        if (DropDownList2.Items.Count > 0)
            GridView1.DataBind();
    }
}
