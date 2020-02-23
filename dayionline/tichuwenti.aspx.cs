using System;
using System.Text;
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

public partial class onlinedayi_tichuwenti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            TreeView1.Attributes.Add("onclick", "client_OnTreeNodeChecked()");
            if (Session["kechengid"] != null)
            {
                string kechengid = Session["kechengid"].ToString();
                TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
                TreeView1.kechengid = int.Parse(kechengid);

            }
        }
        TextBox1.Text = TreeView1.CheckedNodes.Count.ToString();
        string username="";
        string shenfen="";
        try
        {
            username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            shenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
        }
        catch
        {
        }
        if (username == "")
        {
            Button1.Enabled = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//提问
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string shenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;

        if (Session["kechengid"] != null)
        {
            string biaoti = TextBox2.Text.Trim();
            string neirong = FCKeditor1.Value.Trim();
            string kechengid = Session["kechengid"].ToString();
            //获取问题相关知识点
            ArrayList zhishidianid = new ArrayList();//相关知识点ID列表
            foreach (TreeNode node in TreeView1.CheckedNodes)
            {
                if (!zuxianchecked(node))
                {
                    //xiangguanzhishidian += node.Text + ",";
                    zhishidianid.Add(node.Value);
                }
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            conn.Open();
            SqlTransaction st = conn.BeginTransaction();
            comm.Transaction = st;
            try
            {
                string shijian = DateTime.Now.ToString();
                comm.CommandText = "insert into tb_wenti(kechengid,wenti,username,shenfen,biaoti,shijian) values(" + kechengid + ",'" + neirong + "','" + username + "','" + shenfen + "','" + biaoti + "','" + shijian + "')";
                comm.ExecuteNonQuery();
                comm.CommandText = "select  wentiid from tb_wenti where username='" + username + "' and shijian='" + shijian + "'";
                int wentiid = (int)comm.ExecuteScalar();
                foreach (string s in zhishidianid)
                {
                    comm.CommandText = "insert into tb_wenti_zhishidian(wentiid,zhishidianid) values(" + wentiid + "," + s + ")";
                    comm.ExecuteNonQuery();
                }
                st.Commit();
                Label1.Text = "提问成功！";
            }
            catch (Exception ex)
            {
                st.Rollback();
                Label1.Text = "提问失败！详细：" + ex.Message;
            }
            finally
            {
                if (conn.State.ToString() == "Opened")
                    conn.Close();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), null, "<script>alert('只有登录用户才有权提问！');</script>", false);
        }
    }
    protected bool zuxianchecked(TreeNode mytreenode)//祖先结点中是否有被选择的,如果有，返回true,否则返回false
    {
        bool ischecked = false;
        TreeNode zuxian = mytreenode.Parent;
        while (zuxian != null)
        {
            if (zuxian.Checked)
            {
                ischecked = true;
                break;
            }
            zuxian = zuxian.Parent;
        }
        return (ischecked);
    }
    protected void TreeViewsource_DataBound(object sender, EventArgs e)
    {
        foreach (TreeNode node in TreeView1.Nodes)
        {
            node.SelectAction = TreeNodeSelectAction.None;
            deselect(node);
        }
    }
    protected void deselect(TreeNode node)
    {
        foreach (TreeNode child in node.ChildNodes)
        {
            child.SelectAction = TreeNodeSelectAction.None;
            deselect(child);
        }
    }
}
