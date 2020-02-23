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

public partial class teachermanage_zuoyepigai : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Labelbanji.Text = BanjiInfo.GetBanjiName(int.Parse(Request.QueryString["banjiid"]));
            Labelzuoyename.Text = ZuoyeInfo.getZuoyeName(int.Parse(Request.QueryString["zuoyeid"]));
            DataTable studt = BanjiInfo.GetStudentNameAndUsername(int.Parse(Request.QueryString["banjiid"]));
            ListBox1.DataSource = studt;
            ListBox1.DataBind();
        }
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        int fenzhi = int.Parse(((Label)(e.Row.Cells[8].FindControl("Label1"))).Text);
    //        DropDownList defenDrop = (DropDownList)(e.Row.Cells[9].FindControl("DropDownListtimudefen"));
    //        for (int i = 0; i < fenzhi; i++)
    //        {
    //            defenDrop.Items.Add(i.ToString());
    //        }
    //    }
    //}

    //protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)//某题重新给分后，自动重算总分
    //{
    //    int zuoyeid = int.Parse(Request.QueryString["zuoyeid"]);
    //    string studentusername = ListBox1.SelectedValue;
    //    ZuoyeInfo.HuizongStuZuoyeChengji(zuoyeid, studentusername);
        
    //}
    protected void Button4_Click(object sender, EventArgs e)
    {
        Label4.Text = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        try
        {
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "update tb_studentzuoye set pingyu='" + TextBoxpingyu.Text + "' where studentusername='" + ListBox1.SelectedValue + "' and zuoyeid=" + Request.QueryString["zuoyeid"];
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            Label4.Text = "评语提交成功！";
        }
        catch
        {
            Label4.Text = "评语提交失败！";
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBoxpingyu.Text += DropDownList1.SelectedValue;
    }
    protected void DetailsView1_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        int zuoyeid = int.Parse(Request.QueryString["zuoyeid"]);
        string studentusername = ListBox1.SelectedValue;
        ZuoyeInfo.HuizongStuZuoyeChengji(zuoyeid, studentusername);
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DetailsView1.DataBind();
    }
    protected void ListBox1_DataBound(object sender, EventArgs e)
    {
        if (ListBox1.Items.Count > 0)
        {
            ListBox1.SelectedIndex = 0;
            DetailsView1.DataBind();
        }
    }
    protected void ddl_defen_DataBinding(object sender, EventArgs e)
    {
        Label4.Text = "";
        try
        {
            int fenzhi = int.Parse(DetailsView1.Rows[4].Cells[1].Text.Trim());
            DropDownList defendrop = (DropDownList)(DetailsView1.FindControl("ddl_defen"));
            if (defendrop != null)
            {
                if (defendrop.Items.Count == 0)
                {
                    for (int i = fenzhi; i >=0 ; i--)
                    {
                        defendrop.Items.Add(i.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Label4.Text = ex.Message;
        }
    }
}
