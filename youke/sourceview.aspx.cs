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

public partial class youke_sourceview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    string kechengid = Session["kechengid"].ToString();
        //    TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
        //    TreeView1.kechengid = int.Parse(kechengid);
        //}
    }
    //protected void TreeView1_DataBound(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TreeView1.Nodes.Count > 0)
    //        {
    //            string zhishidianid = TreeView1.Nodes[0].Value.Trim();
    //            SqlConnection conn = new SqlConnection();
    //            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    //            SqlCommand comm = conn.CreateCommand();
    //            conn.Open();
    //            comm.CommandText = "select instruction  from tb_kechengjiegou where kechengjiegouid=" + zhishidianid;
    //            string zhishidianjieshao = comm.ExecuteScalar().ToString();
    //            conn.Close();
    //            if (zhishidianjieshao.Length > 0)
    //            {
    //                Labelzhishidianjieshao.Text = zhishidianjieshao;
    //            }
    //            else
    //            {
    //                Labelzhishidianjieshao.Text = "暂无介绍。";
    //            }
    //       }
    //    }
    //    finally
    //    {
    //    }
    //}
    //protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string zhishidianid = TreeView1.SelectedNode.Value;
    //        Labelzhishidianname.Text = TreeView1.SelectedNode.Text;
    //        SqlConnection conn = new SqlConnection();
    //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    //        SqlCommand comm = conn.CreateCommand();
    //        conn.Open();
    //        comm.CommandText = "select instruction from tb_kechengjiegou where kechengjiegouid=" + zhishidianid;
    //        string zhishidianjieshao = comm.ExecuteScalar().ToString();
    //        conn.Close();
    //        if (zhishidianjieshao.Length > 0)
    //        {
    //            Labelzhishidianjieshao.Text = zhishidianjieshao;
    //        }
    //        else
    //        {
    //            Labelzhishidianjieshao.Text = "暂无介绍。";
    //        }
    //        zhishidianid += ",";
    //    }
    //    finally
    //    {
    //    }
    //}
}
