using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;
using System.Data;
using System.Configuration;

public partial class UCZiyuanXiaZai : System.Web.UI.UserControl
{
    private string kechengid;
    private string username;
    private string usershenfen;

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Usershenfen
    {
        get { return usershenfen; }
        set { usershenfen = value; }
    }

    public string KechengId
    {
        get
        { return kechengid; }
        set
        {
            kechengid = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindKechengTreeView();
        }
    }
    protected DataSet myds = new DataSet();//目标课程结构数据集
    protected DataSet ziyuands;
    protected void nodesandchildnodes(TreeNode node, List<int> zhishidianids)
    {
        if (node.ChildNodes.Count > 0)
        {
            foreach (TreeNode childnode in node.ChildNodes)
            {
                zhishidianids.Add(int.Parse(childnode.Value));
                nodesandchildnodes(childnode, zhishidianids);
            }
        }
    }
    protected void GetZhisshidianids(List<int> zhishidianids)
    {
        foreach (TreeNode node in TreeView1.CheckedNodes)
        {
            zhishidianids.Add(int.Parse(node.Value));
            nodesandchildnodes(node, zhishidianids);
        }
    }
    protected void BindKechengTreeView()
    {
        string kechengid = Session["kechengid"].ToString();
        TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
        TreeView1.kechengid = int.Parse(kechengid);
    }
    protected void databind()
    {
        GridView1.DataSource = ziyuands;
        GridView1.Columns.Clear();
        if (ziyuands.Tables[0].Rows.Count > 0)
        {
            HyperLinkField bf1 = new HyperLinkField();
            bf1.HeaderText = "资源名称";
            bf1.DataTextField = "资源名称";
            string[] pram = { "资源文件" };
            bf1.DataNavigateUrlFields = pram;
            bf1.DataNavigateUrlFormatString = "{0}";
            BoundField bf2 = new BoundField();
            bf2.ReadOnly = true;
            bf2.HeaderText = "资源类型";
            bf2.DataField = "资源类型";
            BoundField bf3 = new BoundField();
            bf3.ReadOnly = true;
            bf3.HeaderText = "文件类型";
            bf3.DataField = "文件类型";
            BoundField bf4 = new BoundField();
            bf4.ReadOnly = true;
            bf4.HeaderText = "介绍";
            bf4.DataField = "介绍";
            BoundField bf5 = new BoundField();
            bf5.ReadOnly = true;
            bf5.HeaderText = "上传时间";
            bf5.DataField = "上传时间";
            BoundField bf6 = new BoundField();
            bf6.ReadOnly = true;
            bf6.HeaderText = "上传者";
            bf6.DataField = "上传者";
            HyperLinkField bf8 = new HyperLinkField();
            bf8.HeaderText = "播放";
            string[] param = { "资源ID" };
            bf8.DataNavigateUrlFields = param;
            bf8.DataNavigateUrlFormatString = "play.aspx?id={0}";
            bf8.Target = "_blank";
            bf8.Text = "播放";
            GridView1.Columns.Add(bf1);
            GridView1.Columns.Add(bf2);
            GridView1.Columns.Add(bf3);
            GridView1.Columns.Add(bf4);
            GridView1.Columns.Add(bf6);
            GridView1.Columns.Add(bf5);
            GridView1.Columns.Add(bf8);
            GridView1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//按关键字检索教学资源
    {
        HFziyuanlx.Value = "guanjinazi";
        string guanjianzi = TextBox1.Text.Trim();
        if (username!= null && usershenfen != null)
        {
            GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(username, usershenfen, guanjianzi);
        }
        else
        {
            GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(guanjianzi);
        }
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string meitileixing = e.Row.Cells[3].Text;
            if (meitileixing == "视频" || meitileixing == "音频")
                ((HyperLink)(e.Row.Cells[6].Controls[0])).Enabled = true;
            else
                ((HyperLink)(e.Row.Cells[6].Controls[0])).Enabled = false;

        }
    }
    protected void Buttonzsdss_Click(object sender, EventArgs e)//按知识点检索教学资源
    {
        if (TreeView1.CheckedNodes.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('请在左侧知识树中勾选知识点！');", true);
            return;
        }
        HFziyuanlx.Value = "zhishidian";
        List<int> zhishidianids = new List<int>();
        GetZhisshidianids(zhishidianids);
        if (username != null && usershenfen!= null)//对登录用户
        {
            string ziyuanleixing = DropDownListziyuanleixing.SelectedValue;
            string meitileixing = DropDownListmeitileixing.SelectedValue;
            DataTable dt = ZiyuanInfo.GetZiyuan(zhishidianids, username, usershenfen, ziyuanleixing, meitileixing);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else//对游客
        {
            string ziyuanleixing = DropDownListziyuanleixing.SelectedValue;
            string meitileixing = DropDownListmeitileixing.SelectedValue;
            DataTable dt = ZiyuanInfo.GetZiyuan(zhishidianids,ziyuanleixing, meitileixing);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void TreeView1_DataBound(object sender, EventArgs e)
    {
        if (TreeView1.Nodes.Count > 0)
        {
            TreeView1.Nodes[0].Checked = true;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (TreeView1.CheckedNodes.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javscript'>alert('请在左侧知识树中勾选知识点！');</script>", false);
            return;
        }
        switch (HFziyuanlx.Value.Trim())
        {
            case"guanjianzi":
                string guanjianzi = TextBox1.Text.Trim();
                if (username != null)
                {
                    GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(username,usershenfen, guanjianzi);
                }
                else
                {
                    GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(guanjianzi);
                }
                GridView1.DataBind();
                GridView1.PageIndex = e.NewPageIndex;
                break;
            case"zhishidian":
                List<int> zhishidianids = new List<int>();
                GetZhisshidianids(zhishidianids);
                string ziyuanleixing = DropDownListziyuanleixing.SelectedValue;
                string meitileixing = DropDownListmeitileixing.SelectedValue;
                if (username != null && usershenfen != null)
                {
                    GridView1.DataSource = ZiyuanInfo.GetZiyuan(zhishidianids, username, usershenfen, ziyuanleixing, meitileixing);
                }
                else
                {
                    GridView1.DataSource = ZiyuanInfo.GetZiyuan(zhishidianids, ziyuanleixing, meitileixing);
                }
                GridView1.DataBind();
                GridView1.PageIndex = e.NewPageIndex;
                break;
            default:
                break;
        }
    }
}
