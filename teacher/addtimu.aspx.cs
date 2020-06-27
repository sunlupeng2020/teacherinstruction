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
using System.Collections.Generic;
public partial class teachermanage_timuguanli_addtimu2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBoxzhishidian.Text = TreeViewsource.CheckedNodes.Count.ToString();
        if (!IsPostBack)
        {
            string kechengid = Session["kechengid"].ToString();
            TreeViewsource.ConnectionString = ConfigurationManager.ConnectionStrings[TreeViewsource.ConnectionStringName].ConnectionString;
            TreeViewsource.kechengid = int.Parse(kechengid);
        }
    }
    protected void DropDownList1_OnDataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (DropDownListtixing.Items.Count > 0)
            {
                DropDownListtixing.SelectedValue = "1";
                timutable.Visible = true;
                RadioButtonListdanxuanckda.Visible = true;
                CheckBoxListduoxuanckda.Visible = false;
                RadioButtonListpanduandaan.Visible = false;
                CustomValidator1.Enabled = false;
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)//题型选择后，各控件的状态
    {
        string tixing = DropDownListtixing.SelectedValue;
        switch (tixing)
        {
            case "单项选择题"://单项选择题
                timutable.Visible = true;
                RadioButtonListdanxuanckda.Visible = true;
                CheckBoxListduoxuanckda.Visible = false;
                RadioButtonListpanduandaan.Visible = false;
                cankaodaantr.Visible = true;
                CustomValidator1.Enabled = false;
                break;
            case "多项选择题"://多项选择题
                timutable.Visible = true;
                RadioButtonListdanxuanckda.Visible = false;
                CheckBoxListduoxuanckda.Visible = true;
                RadioButtonListpanduandaan.Visible = false;
                cankaodaantr.Visible = true;
                CustomValidator1.Enabled = true;
                break;
            case "判断题"://判断题
                timutable.Visible = true;
                RadioButtonListdanxuanckda.Visible = false;
                CheckBoxListduoxuanckda.Visible = false;
                RadioButtonListpanduandaan.Visible = true;
                cankaodaantr.Visible = true;
                CustomValidator1.Enabled = false;
                break;
            default://其它题型
                timutable.Visible = true;
                RadioButtonListdanxuanckda.Visible = false;
                CheckBoxListduoxuanckda.Visible = false;
                RadioButtonListpanduandaan.Visible = false;
                cankaodaantr.Visible = true;
                CustomValidator1.Enabled = false;
                break;
        }
    }
    //题目写入数据库
    protected void Buttonsubmit_Click(object sender, EventArgs e)
    {
        string kechengid = Session["kechengid"].ToString();
        string tigan = FCKeditortigan.Value.Replace("<p>", "").Replace("</p>", "");//题干
        if (tigan.Length <= 1)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('请输入题目！');</script>", false);
            return;
        }
        string tigongzhe = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;//题目提供者用户名
        string shuoming = FCKeditorshuoming.Value.Replace("<p>", "").Replace("</p>", "");//说明
        if (shuoming.Trim().Length <= 0)
            shuoming = "没有说明。";
        string tixing = DropDownListtixing.SelectedValue.Trim();//题型
        string cankaodaan="";//参考答案
        string zhishidianid = TreeViewsource.SelectedValue;//题目对应的知识点ID
        //List<string> zhishidianidlist =TreeViewsource.CheckedNodesExceptChildren;//知识点ID列表
        switch (tixing)
        {
            case "单项选择题"://单项选择题
                cankaodaan = RadioButtonListdanxuanckda.SelectedValue;
                break;
            case "多项选择题"://多项选择题
                cankaodaan = "";
                for (int i = 0; i < 5; i++)
                    if (CheckBoxListduoxuanckda.Items[i].Selected)
                        cankaodaan += CheckBoxListduoxuanckda.Items[i].Value;
                if (cankaodaan.Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('请选择多项选择题的答案！');</script>", false);
                    return;
                }
                 break;
            case "判断题"://
                cankaodaan = RadioButtonListpanduandaan.SelectedValue;
                break;
            default:
                break;
        }
        SqlParameter[] timupa = new SqlParameter[7];
        timupa[0] = new SqlParameter("@kechengid", kechengid);
        timupa[1] = new SqlParameter("@timu", tigan);
        timupa[2] = new SqlParameter("@answer", cankaodaan);
        timupa[3] = new SqlParameter("@type", tixing);
        timupa[4] = new SqlParameter("@shuoming", shuoming);
        timupa[5] = new SqlParameter("@tigongzhe", tigongzhe);
        timupa[6] = new SqlParameter("@zhishidianid", zhishidianid);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "insert into tb_tiku(kechengid,timu,answer,type,shuoming,tigongzhe,zhishidianid) values(@kechengid,@timu,@answer,@type,@shuoming,@tigongzhe,@zhishidianid)";
        comm.Parameters.AddRange(timupa); 
        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            Lbl_fankui.Text = "添加题目成功！";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('添加题目成功!');</script>", false);
        }
        catch(Exception  e1)
        {
            Lbl_fankui.Text = "添加题目失败！请检查题目、答案或解析中内容，尽量简洁。原因:"+e1.Message;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('添加题目失败!');</script>", false);
        }
        finally
        {
            comm.Parameters.Clear();
            if (conn.State.ToString() == "Opened")
                conn.Close();
        }
    }
    protected void TreeViewsource_DataBound(object sender, EventArgs e)
    {
        foreach (TreeNode node in TreeViewsource.Nodes)
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
    protected void TreeViewsource_SelectedNodeChanged(object sender, EventArgs e)
    {
        TextBoxzhishidian.Text = TreeViewsource.SelectedNode.Value;
        Labelzhishidian.Text = TreeViewsource.SelectedNode.Text;
    }
}
