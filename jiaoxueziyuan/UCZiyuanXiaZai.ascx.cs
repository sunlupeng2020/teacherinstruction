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
using System.IO;

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
            BindZiyuanGridView();
        }
    }
    protected void BindZiyuanGridView()
    {
        switch (HFziyuanlx.Value.Trim())
        {
            case "guanjianzi":
                string guanjianzi = TextBox1.Text.Trim();
                if (username != null)
                {
                    GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(username, usershenfen, guanjianzi);
                }
                else
                {
                    GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(guanjianzi);
                }
                break;
            case "zhishidian":
                if (TreeView1.CheckedNodes.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('请在左侧知识树中勾选知识点！');", true);
                    return;
                }
                HFziyuanlx.Value = "zhishidian";
                List<int> zhishidianids = new List<int>();
                GetZhisshidianids(zhishidianids);
                if (username != null && usershenfen != null)//对登录用户
                {
                    string ziyuanleixing = DropDownListziyuanleixing.SelectedValue;
                    string meitileixing = DropDownListmeitileixing.SelectedValue;
                    DataTable dt = ZiyuanInfo.GetZiyuan(zhishidianids, username, usershenfen, ziyuanleixing, meitileixing);
                    GridView1.DataSource = dt;

                }
                else//对游客
                {
                    string ziyuanleixing = DropDownListziyuanleixing.SelectedValue;
                    string meitileixing = DropDownListmeitileixing.SelectedValue;
                    DataTable dt = ZiyuanInfo.GetZiyuan(zhishidianids, ziyuanleixing, meitileixing);
                    GridView1.DataSource = dt;
                }
                break;          
            default:
                GridView1.DataSource = ZiyuanInfo.GetKechengziyuan(kechengid, username, usershenfen);
                break;

        }
        GridView1.DataBind();
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
    protected void Button1_Click(object sender, EventArgs e)//按关键字检索教学资源
    {
        HFziyuanlx.Value = "guanjinazi";
        BindZiyuanGridView();
    }
    protected void Buttonzsdss_Click(object sender, EventArgs e)//按知识点检索教学资源
    {
        if (TreeView1.CheckedNodes.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('请在左侧知识树中勾选知识点！');", true);
            return;
        }
        HFziyuanlx.Value = "zhishidian";
        BindZiyuanGridView();
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
        //switch (HFziyuanlx.Value.Trim())
        //{
        //    case "guanjianzi":
        //        string guanjianzi = TextBox1.Text.Trim();
        //        if (username != null)
        //        {
        //            GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(username, usershenfen, guanjianzi);
        //        }
        //        else
        //        {
        //            GridView1.DataSource = ZiyuanInfo.GetGuanjianziZiyuan(guanjianzi);
        //        }
        //        GridView1.DataBind();
        //        GridView1.PageIndex = e.NewPageIndex;
        //        break;
        //    case "zhishidian":
        //        if (TreeView1.CheckedNodes.Count == 0)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javscript'>alert('请在左侧知识树中勾选知识点！');</script>", false);
        //            return;
        //        }
        //        List<int> zhishidianids = new List<int>();
        //        GetZhisshidianids(zhishidianids);
        //        string ziyuanleixing = DropDownListziyuanleixing.SelectedValue;
        //        string meitileixing = DropDownListmeitileixing.SelectedValue;
        //        if (username != null && usershenfen != null)
        //        {
        //            GridView1.DataSource = ZiyuanInfo.GetZiyuan(zhishidianids, username, usershenfen, ziyuanleixing, meitileixing);
        //        }
        //        else
        //        {
        //            GridView1.DataSource = ZiyuanInfo.GetZiyuan(zhishidianids, ziyuanleixing, meitileixing);
        //        }
        //        GridView1.DataBind();
        //        GridView1.PageIndex = e.NewPageIndex;
        //        break;
        //    default:
        //        break;
        //}
        GridView1.PageIndex = e.NewPageIndex;
        BindZiyuanGridView();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string ziyuanid = ((LinkButton)sender).CommandArgument;
        DataTable dt = ZiyuanInfo.GetZiyunaXiangqing(int.Parse(ziyuanid));
        if (dt.Rows.Count > 0)
        {
            string filename = dt.Rows[0]["ziyuanfile"].ToString();
            if (ZiyuanInfo.deleteZiyuan(ziyuanid))
            {
                //删除资源文件
                FileInfo f1 = new FileInfo(Server.MapPath(filename));
                if (f1.Exists)
                {
                    f1.Delete();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('资源删除成功！')", true);
                    ((LinkButton)sender).Enabled = false;
                    BindZiyuanGridView();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('资源删除失败！')", true);
            }
        }
    }
    protected bool IsKechengManager()
    {
        bool y = false;
        if (usershenfen == "teacher")
            y=KechengInfo.IsTeacherManageKecheng(this.username, this.kechengid);
        return  y; 
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (username=="youke")
            {
                ((HyperLink)(e.Row.Cells[3].FindControl("HyperLink2"))).Visible = false;
            }
            string jiaoxueziyuanid = GridView1.DataKeys[e.Row.RowIndex][0].ToString();
            if (IsKechengManager() || ZiyuanInfo.IsZiyuanTransfer(username, jiaoxueziyuanid))
            {
                ((LinkButton)(e.Row.Cells[5].FindControl("LinkButton1"))).Visible = true;
                ((HyperLink)(e.Row.Cells[5].FindControl("HyperLink1"))).Visible = true;
            }
            else
            {
                ((LinkButton)(e.Row.Cells[5].FindControl("LinkButton1"))).Visible = false;
                ((HyperLink)(e.Row.Cells[5].FindControl("HyperLink1"))).Visible = false;
            }
        }
    }
}
