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

public partial class OnlineStudy : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
    {
        //string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        //if (StudentInfo.WeiWanchengCeshi(username,int.Parse(Session["kechengid"].ToString())))
        //{
        //    Response.Redirect("jiaoshiceshi.aspx");
        //}
        //if (!IsPostBack)
        //{
        //    string kechengid = Session["kechengid"].ToString();
        //    BindKechengTreeview(kechengid);
        //}
    }
    //protected void BindKechengTreeview(string kechengid)//更新课程结构TreeView
    //{
    //    TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
    //    TreeView1.kechengid = int.Parse(kechengid);
    //}
    //protected void TreeView1_DataBound(object sender, EventArgs e)
    //{
    //    if (TreeView1.Nodes.Count > 0)
    //    {
    //        string zhishidianid = TreeView1.Nodes[0].Value.Trim();

    //        SqlConnection conn = new SqlConnection();
    //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    //        SqlCommand comm = conn.CreateCommand();
    //        conn.Open();
    //        comm.CommandText = "select instruction  from tb_kechengjiegou where kechengjiegouid=" + zhishidianid;
    //        string zhishidianjieshao = (string)(comm.ExecuteScalar());
    //        conn.Close();
    //        if (zhishidianjieshao.Length > 0)
    //        {
    //            Labelzhishidianjieshao.Text = zhishidianjieshao;
    //        }
    //        else
    //        {
    //            Labelzhishidianjieshao.Text = "暂无介绍。";
    //        }
    //    }
    //}
    //protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    string zhishidianid =TreeView1.SelectedNode.Value;
    //    Labelzhishidianname.Text = TreeView1.SelectedNode.Text;
    //    SqlConnection conn = new SqlConnection();
    //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    //    SqlCommand comm = conn.CreateCommand();
    //    conn.Open();
    //    comm.CommandText = "select instruction from tb_kechengjiegou where kechengjiegouid=" + zhishidianid;
    //    string zhishidianjieshao = (string)(comm.ExecuteScalar());
    //    conn.Close();
    //    if (zhishidianjieshao.Length > 0)
    //    {
    //        Labelzhishidianjieshao.Text = zhishidianjieshao;
    //    }
    //    else
    //    {
    //        Labelzhishidianjieshao.Text = "暂无介绍。";
    //    }
    //}
}
