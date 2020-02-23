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


public partial class teachermanage_zuoye_edittimu : System.Web.UI.Page
{
    bool zuoyeyibuzhi = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["zuoyeid"] == null)
        {
            Response.Redirect("zuoyeyuzhi.aspx");
        }
        else
        {
            zuoyeyibuzhi = !ZuoyeInfo.IsZuoyeUsed(Request.QueryString["zuoyeid"]);
        }
        if (!IsPostBack)
        {
            BindZuoyeTimu();
        }
    }
    protected void DeleteTimuFromZuoye(object sender, CommandEventArgs e)//从作业中删除题目
    {
        string zuoyetimuid = e.CommandArgument.ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "delete from tb_teacherzuoyetimu where zuoyetimuid=" + zuoyetimuid;
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
        BindZuoyeTimu();
        ZuoyeInfo.UpdateTeacherZuoyeZongfen(Request.QueryString["zuoyeid"]);
        FormView1.DataBind();
    }
    protected void grvw_zuoyetimu_RowDataBound(object sender, GridViewRowEventArgs e)//作业题目GridView,自动编号
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();//显示行号
            LinkButton editLbt=(LinkButton)(e.Row.FindControl("LinkButton1"));
            if (editLbt != null)
            {
                editLbt.Enabled = zuoyeyibuzhi;//是否可以编辑题目分值
            }
            Button shanchubtn = (Button)(e.Row.FindControl("Button2"));//删除题目按钮
            shanchubtn.Enabled = zuoyeyibuzhi;//设置是否可以删除题目
            shanchubtn.Attributes.Add("onclick", "return confirm('你确定要删除该题目吗？')");
        }
    }
    protected void grvw_zuoyetimu_RowUpdating(object sender, GridViewUpdateEventArgs e)//更新题目分值
    {
        string fenzhi = ((TextBox)(grvw_zuoyetimu.Rows[grvw_zuoyetimu.EditIndex].FindControl("TextBox1"))).Text.Trim();
        string zuoyetimuid = grvw_zuoyetimu.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_teacherzuoyetimu set fenzhi=" + fenzhi + " where zuoyetimuid=" + zuoyetimuid;
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
        grvw_zuoyetimu.EditIndex = -1;
        BindZuoyeTimu();
        ZuoyeInfo.UpdateTeacherZuoyeZongfen(Request.QueryString["zuoyeid"]);
        FormView1.DataBind();
    }
    protected void grvw_zuoyetimu_RowEditing(object sender, GridViewEditEventArgs e)//编辑
    {
        grvw_zuoyetimu.EditIndex = e.NewEditIndex;
        BindZuoyeTimu();
    }
    protected void grvw_zuoyetimu_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)//取消编辑
    {
        grvw_zuoyetimu.EditIndex = -1;
        BindZuoyeTimu();
    }
    protected void BindZuoyeTimu()
    {
        DataTable tmtable = ZuoyeInfo.GetTeacherZuoyeTimuOrderByTixing(int.Parse(Request.QueryString["zuoyeid"]));
        grvw_zuoyetimu.DataSource = tmtable;
        grvw_zuoyetimu.DataBind();
    }
    protected void grvw_zuoyetimu_DataBound(object sender, EventArgs e)
    {
        if (grvw_zuoyetimu.Rows.Count > 0)
        {
            HyperLink1.Visible = true;
            HyperLink1.NavigateUrl = "zuoye_id_buzhi01.aspx?zuoyeid=" + Request.QueryString["zuoyeid"];
        }
        else
        {
            HyperLink1.Visible = false;
        }
    }
}
