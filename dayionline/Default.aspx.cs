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
using System.Text;
using System.Collections.Generic;

public partial class onlinedayi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TreeView1.Attributes.Add("onclick", "client_OnTreeNodeChecked()");
            BindKechengWenti();
        }
        TextBox1.Text = TreeView1.CheckedNodes.Count.ToString();
        string url = HttpContext.Current.Request.Url.AbsolutePath;
    }
    protected void BindKechengWenti()
    {
        string kechengid = Session["kechengid"].ToString();
        UpdateKechengTreeview(kechengid);
        HFshuju.Value = kechengid;
        HFchaxfs.Value = "kecheng";
        DataTable wttable= WentiInfo.GetWenti(int.Parse(kechengid));
        ViewState["wentitable"]=wttable;
        GridView1.DataSource = wttable;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)//搜索知识点相关问题
    {
        HFchaxfs.Value = "zhishidian";//知识点对应的问题
        //从TreeView1中获取知识点id
        List<int> zhishidianidList = new List<int>();
        foreach (TreeNode node in TreeView1.CheckedNodes)
        {
            zhishidianidList.Add(int.Parse(node.Value));
            addchildren(node, zhishidianidList);
        }
        StringBuilder sb = new StringBuilder();
        foreach (int s in zhishidianidList)
        {
            sb.Append(s.ToString() + ",");
        }
        HFshuju.Value = sb.ToString();
        DataTable wttable=WentiInfo.GetWenti(zhishidianidList);
        ViewState["wentitable"]=wttable;
        GridView1.DataSource = wttable;
        if (GridView1.PageCount > 0)
            GridView1.PageIndex = 0;
        GridView1.DataBind();
    }
    protected void addchildren(TreeNode node,List<int> list)//添加节点的子孙结点值到ArrayList
    {
        foreach (TreeNode child in node.ChildNodes)
        {
            if(!list.Contains(int.Parse(child.Value)))
            {
                list.Add(int.Parse(child.Value));
            }
            addchildren(child, list);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)//按关键字搜索问题
    {
        HFchaxfs.Value = "guanjianzi";
        string guanjianzi=TextBox2.Text.Trim();
        HFshuju.Value = guanjianzi;
        DataTable wttable=WentiInfo.GetWenti(guanjianzi);
        ViewState["wentitable"]=wttable;
        GridView1.DataSource = wttable;
        if (GridView1.PageCount > 0)
            GridView1.PageIndex = 0;
        //GridView1.AutoGenerateColumns = true;
        GridView1.DataBind();
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        DataTable wttable = new DataTable();
        string kechengid = Session["kechengid"].ToString();
        TreeNode node = TreeView1.SelectedNode;
        if (node == TreeView1.Nodes[0])
        {
            HFchaxfs.Value = "kecheng";
            wttable = WentiInfo.GetWenti(int.Parse(kechengid));
            ViewState["wentitable"] = wttable;
            GridView1.DataSource = wttable;
            GridView1.DataBind();
        }
        else
        {
            HFchaxfs.Value = "zhishidian";
            //从TreeView1中获取知识点id
            List<int> zhishidianidList = new List<int>();
            zhishidianidList.Add(int.Parse(node.Value.Trim()));
            addchildren(node, zhishidianidList);
            StringBuilder sb = new StringBuilder();
            foreach (int s in zhishidianidList)
            {
                sb.Append(s.ToString() + ",");
            }
            HFshuju.Value = sb.ToString();
            wttable = WentiInfo.GetWenti(zhishidianidList);
            ViewState["wentitable"] = wttable;
            GridView1.DataSource = wttable;
            GridView1.DataBind();
        }
    }
    protected void UpdateKechengTreeview(string kechengid)//更新课程结构TreeView
    {
        TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
        TreeView1.kechengid = int.Parse(kechengid);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)//删除问题
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string usershenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
        if (username!= null &&usershenfen!= null)
        {
            try
            {
                string wentiid = ((LinkButton)sender).CommandArgument;
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "select guanliyuan from tb_kecheng where kechengid=(select kechengid from tb_wenti where wentiid=" + wentiid + ")";
                conn.Open();
                string guanliyuan = comm.ExecuteScalar().ToString();
                conn.Close();
                if (guanliyuan == username && usershenfen == "teacher")
                {
                    conn.Open();
                    SqlTransaction st = conn.BeginTransaction();
                    comm.Transaction = st;
                    try
                    {
                        comm.CommandText = "delete from tb_huidapingjia where huidaid in(select huidaid from tb_huida where wentiid=" + wentiid + ")";
                        comm.ExecuteNonQuery();
                        comm.CommandText = "delete from tb_huida where wentiid=" + wentiid;
                        comm.ExecuteNonQuery();
                        comm.CommandText = "delete from tb_wenti where wentiid=" + wentiid;
                        comm.ExecuteNonQuery();
                        comm.CommandText = "delete from tb_wenti_zhishidian where wentiid=" + wentiid;
                        comm.ExecuteNonQuery();
                        st.Commit();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('问题删除成功!');</script>", false);
                        ((LinkButton)sender).Enabled = false;
                        BindWenti();
                    }
                    catch
                    {
                        st.Rollback();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('问题删除失败!');</script>", false);

                    }
                    finally
                    {
                        conn.Close();
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('只有课程管理员才有权删除问题!');</script>", false);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('问题删除失败!');</script>", false);

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('您无权删除此问题!');</script>", false);
        }
    }
    protected void BindWenti()
    {
        string ckfs = HFchaxfs.Value.Trim();
        DataTable wttable = new DataTable();
        switch (ckfs)
        {
            case "kecheng":
            default:
                int kechengid = int.Parse(HFshuju.Value.Trim());
                wttable=WentiInfo.GetWenti(kechengid);
                ViewState["wentitable"]=wttable;
                GridView1.DataSource = wttable;
                break;
            case "zhishidian":
                string[] zsdids = HFshuju.Value.Trim().Split(',');
                List<int> zsdList = new List<int>();
                foreach (string s in zsdids)
                {
                    zsdList.Add(int.Parse(s));
                }
                wttable = WentiInfo.GetWenti(zsdList);
                ViewState["wentitable"] = wttable;
                GridView1.DataSource = wttable;
                break;
            case "guanjianzi":
                wttable = WentiInfo.GetWenti(HFshuju.Value.Trim());
                ViewState["wentitable"] = wttable;
                GridView1.DataSource = wttable;
                break;
        }
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.DataSource = (DataTable)(ViewState["wentitable"]);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
}
