using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class teachermanage_zuoye_addtimu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["zuoyeid"] != null)
            {
                //绑定课程知识树
                int zuoyeid = int.Parse(Request.QueryString["zuoyeid"]);
                string kechengid = ZuoyeInfo.getZuoye_Kechengid(zuoyeid);
                TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
                TreeView1.kechengid = int.Parse(kechengid);
                Hylk_buzhi.NavigateUrl = "zuoye_id_buzhi01.aspx?zuoyeid=" + zuoyeid.ToString();
            }
            else
            {
                Response.Redirect("zuoyeyuzhi.aspx");
            }
        }
    }
    protected void grvw_timu_RowDataBound(object sender, GridViewRowEventArgs e)//显示符合条件的题目的GridView,显示行号
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();//显示行号
            string zuoyeid = Request.QueryString["zuoyeid"];
            //设置添加和删除按钮的可用状态
            int rowindex = e.Row.RowIndex; 
            //然后根据行号，来得到主键
            string questionid=grvw_timu.DataKeys[rowindex].Value.ToString();
            Button tianjiabtn =(Button)(e.Row.FindControl("btn_add"));//添加按钮
            Button shanchubtn = (Button)(e.Row.FindControl("btn_del"));//删除按钮
            if (ZuoyeInfo.IsTimuInZuoye(zuoyeid, questionid))
            {
                tianjiabtn.Enabled = false;
                shanchubtn.Enabled = true;
            }
            else
            {
                tianjiabtn.Enabled = true;
                shanchubtn.Enabled = false;
            }
        }
    }
    protected void AddTimuToZuoye(object sender, CommandEventArgs e)//将题目添加到作业,按题目id,questionid
    {
        lbl_fankui.Text = "";
        bool chenggong = true;
        string zuoyeid = Request.QueryString["zuoyeid"];
        string questionid = e.CommandArgument.ToString();
        string fenzhi = ((TextBox)(((GridViewRow)(((Button)sender).NamingContainer)).FindControl("TextBox1"))).Text.Trim();
        string tixing = ((GridViewRow)(((Button)sender).NamingContainer)).Cells[1].Text.Trim();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        try
        {
            comm.CommandText = "insert into tb_teacherzuoyetimu(zuoyeid,questionid,fenzhi) values(@zuoyeid,@questionid,@fenzhi)";
            comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
            comm.Parameters.AddWithValue("@questionid", questionid);
            comm.Parameters.AddWithValue("@fenzhi", fenzhi);
            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            chenggong = false;
            lbl_fankui.Text = ex.Message;
        }
        finally
        {
            conn.Close();
        }
        if (chenggong)
        {
            ZuoyeInfo.UpdateTeacherZuoyeZongfen(zuoyeid);
            FormView1.DataBind();
            Button shanchubtn = (Button)(((GridViewRow)(((Button)sender).NamingContainer)).FindControl("btn_del"));
            shanchubtn.Enabled = true;
            ((Button)sender).Enabled = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('添加失败!');</script>", false);
        }
    }
    protected void DelTimuFromZuoye(object sender, CommandEventArgs e)//从作业中删除题目,按题目questionid,zuoyeid
    {
        string questionid = e.CommandArgument.ToString();
        string zuoyeid = Request.QueryString["zuoyeid"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "delete from tb_teacherzuoyetimu where zuoyeid=" + zuoyeid+" and questionid="+questionid;
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
        ZuoyeInfo.UpdateTeacherZuoyeZongfen(Request.QueryString["zuoyeid"]);
        FormView1.DataBind();
        Button tianjiabtn = (Button)(((Button)sender).NamingContainer.FindControl("btn_add"));
        tianjiabtn.Enabled = true;
        ((Button)sender).Enabled = false;
    }
    protected void BindTimu()//绑定知识点题目
    {
        string tixing = DropDownList1.SelectedValue.Trim();
        if (TreeView1.CheckedNodes.Count > 0)
        {
            try
            {
                List<string> zhishidianids = TreeView1.CheckedNodesAndChildrenIds;//选择的知识点及其子孙知识点的id
                DataTable dt = TimuInfo.GetTimuOnZhishidian(zhishidianids, tixing);
                grvw_timu.DataSource = dt;
                ViewState["timutable"] = dt;
                grvw_timu.DataBind();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('获取题目失败！');</script>", false);
            }
        }
        else
        {
            int zuoyeid = int.Parse(Request.QueryString["zuoyeid"]);
            string kechengid = ZuoyeInfo.getZuoye_Kechengid(zuoyeid);
            DataTable dt = TimuInfo.GetTimuOnKecheng(kechengid,tixing);
            grvw_timu.DataSource = dt;
            ViewState["timutable"] = dt;
            grvw_timu.DataBind();
        }
    }
     protected void grvw_timu_PageIndexChanging(object sender, GridViewPageEventArgs e)//符合条件的题目，分页
    {
        if (ViewState["timutable"] != null)
        {
            grvw_timu.PageIndex = e.NewPageIndex;
            grvw_timu.DataSource = (DataTable)(ViewState["timutable"]);
            grvw_timu.DataBind();
        }
        else
        {
            BindTimu();
        }
    }
    protected void btn_searchtimu_Click(object sender, EventArgs e)
    {
        BindTimu();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTimu();
    }
}
