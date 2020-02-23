using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

public partial class kechengguanli_sykcshunxu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string kechengid = Session["kechengid"].ToString();
        if (!IsPostBack)
        {
            UpdateKechengTreeview(kechengid);
        }
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (!KechengInfo.IsTeacherManageKecheng(username, kechengid))
        {
            Button_shanchu.Enabled = false;
            Label1.Text = "您不是本课程的管理员，无权对其知识结构进行编辑修改。";
            DetailsView1.Enabled = false;
        }
        else
        {
            Button_shanchu.Enabled =true;
            DetailsView1.Enabled = true;
        }
    }
    //显示初始课程结构
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        Lbl_zhishiname.Text = TreeView1.SelectedNode.Text;
        Lbl_zhishiid.Text = TreeView1.SelectedNode.Value;
        TreeNode node = TreeView1.SelectedNode;
        if (node.ChildNodes.Count > 0 || node.Depth <= 0)
        {
            Button_shanchu.Enabled = false;
        }
        else
        {
            if (KechengInfo.IsTeacherManageKecheng(username, kechengid))
            {
                Button_shanchu.Enabled = true;
            }
            else
            {
                Button_shanchu.Enabled = false;
            }
        }
        int nodedepth = node.Depth;//求结点深度
        //回溯该知识点的上位知识点
        Lbl_dangqianweizhi.Text = "";
        StringBuilder sb = new StringBuilder();
        sb.Append(TreeView1.SelectedNode.Text);
        while (node.Parent != null)
        {
            node = node.Parent;
            sb.Insert(0, node.Text + "&gt;");
        }
        Lbl_dangqianweizhi.Text = sb.ToString();
    }
    protected void UpdateKechengTreeview(string kechengid)//更新课程结构TreeView
    {
        TreeView1.Nodes.Clear();
        TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
        TreeView1.kechengid = int.Parse(kechengid);
    }
    protected void Button12_Click1(object sender, EventArgs e)//刷新课程结构
    {
        string kechengid = Session["kechengid"].ToString();
        UpdateKechengTreeview(kechengid);
        TreeView1.ExpandDepth = 2;
    }
    protected void BindZhishidianxinxi()
    {
        Lbl_zhishiname.Text = TreeView1.Nodes[0].Text;
        Lbl_dangqianweizhi.Text = TreeView1.Nodes[0].Text;
    }
   protected void Button1_Click1(object sender, EventArgs e)//删除知识点
    {
        if (Lbl_zhishiname.Text.Length <= 0)
        {
            Label1.Text = "请选择要删除的知识点！";
        }
        else
        {
            int zhishidianid = Int32.Parse(Lbl_zhishiid.Text);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select kechengjiegouid from tb_kechengjiegou where shangwei=" + zhishidianid;
            SqlDataReader sdr1 = comm.ExecuteReader();
            if (sdr1.Read())
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert(该知识点的子知识点不为空，不能删除！');</script>", false);
                sdr1.Close();
            }
            else///如果该知识点无下位知识点
            {
                sdr1.Close();
                //把对应该知识点的题目设为对应其上位知识点
                comm.CommandText = "select shangwei from tb_kechengjiegou where kechengjiegouid=" + zhishidianid;
                int shangweiid = Convert.ToInt32(comm.ExecuteScalar());
                //开始事务
                if (shangweiid == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('课程结构的根知识点不能删除！');</script>", false);
                }
                else
                {
                    SqlTransaction st = conn.BeginTransaction();
                    comm.Transaction = st;
                    try
                    {
                        //更新题目对应的知识点
                        comm.CommandText = "update tb_timuzhishidian set kechengjiegouid=" + shangweiid + " where kechengjiegouid=" + zhishidianid;
                        comm.ExecuteNonQuery();
                        //更新教学资源对应的知识点
                        comm.CommandText = "update tb_ziyuanzhishidian set zhishidianid=" + shangweiid + " where zhishidianid=" + zhishidianid;
                        comm.ExecuteNonQuery();
                        //更新问题对应的知识点
                        comm.CommandText = "update tb_wenti_zhishidian set zhishidianid=" + shangweiid + " where zhishidianid=" + zhishidianid;
                        comm.ExecuteNonQuery();
                        ////更新组卷测试对应的知识点
                        //comm.CommandText = "update tb_zujuanzhishidian set  zhishidianid=" + shangweiid + " where zhishidianid=" + zhishidianid;
                        //comm.ExecuteNonQuery();
                        //更新自测对应的知识点
                        //comm.CommandText = "update tb_zicezhishidian set  zhishidianid=" + shangweiid + " where zhishidianid=" + zhishidianid;
                        //comm.ExecuteNonQuery();
                        //更新作业对应的知识点
                        //comm.CommandText = "update tb_zuoyezhishidian set  zhishidianid=" + shangweiid + " where zhishidianid=" + zhishidianid;
                        //comm.ExecuteNonQuery();
                        //删除知识点
                        comm.CommandText = "delete from tb_kechengjiegou  where kechengjiegouid=" + zhishidianid;
                        comm.ExecuteNonQuery();
                        st.Commit();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('知识点已删除!对应题目和教学资源已归于其上位知识点!');</script>", false);
                    }
                    catch (Exception ex)
                    {
                        st.Rollback();
                        Label1.Text = ex.Message + "知识点删除失败！";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        Button_shanchu.Enabled = false;
    }
}
