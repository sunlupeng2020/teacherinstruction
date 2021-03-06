﻿using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class teachermanage_timuguanli_xiugaitimu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateKechengTreeview(Session["kechengid"].ToString());
        }
    }
    protected void UpdateKechengTreeview(string kechengid)//更新课程结构TreeView
    {
        TreeViewsource.ConnectionString = ConfigurationManager.ConnectionStrings[TreeViewsource.ConnectionStringName].ConnectionString;
        TreeViewsource.kechengid = int.Parse(kechengid);
        CheckBox1.Checked = false;
    }
    /// <summary>
    /// 显示题目时的分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string cxleixing = HFleixing.Value.Trim();
        if (cxleixing == "zhishidian")
        {
            int zhishidianid = int.Parse(HFzhishidianid.Value);
            bool xiaji = Boolean.Parse(HFxiajitimu.Value);
            ConcreteKnowladge ck = new ConcreteKnowladge(zhishidianid);
            GridView1.DataSource = ck.GetTimu(xiaji);
        }
        else
        {
            string keyword = HFkeyword.Value.Trim();
        
            GridView1.DataSource = GuanjianziTimu(keyword,Session["kechengid"].ToString());
        }
        
        int pagecount = GridView1.PageCount;
        int pageindex = e.NewPageIndex;
        int hangshu = GridView1.Rows.Count;
        if (hangshu != 0)
        {
            if (pageindex > pagecount - 1)
                GridView1.PageIndex = pagecount - 1;
            else
                GridView1.PageIndex = pageindex;
        }
        GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)//按关键字搜题目显示
    {
        HFleixing.Value = "guanjianzi";
        string keyword = TextBox1.Text.Trim();
        HFkeyword.Value = keyword;
        string kechengid = Session["kechengid"].ToString();//课程ID
        HFzhishidianid.Value = kechengid;
        GridView1.DataSource = GuanjianziTimu(keyword, kechengid);
        if (GridView1.PageCount > 0)
        {
            GridView1.PageIndex = 0;
        }
        GridView1.DataBind();
    }

    protected DataTable GuanjianziTimu(string keyword, string kechengid)//按关键字搜题目
    {
        DataTable timutable = new DataTable();
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        string sqltext = "select questionid,timu,[type],answer,tigongzhe  from  tb_tiku  where timu like '%" + keyword + "%'";
        mycomm.CommandText = sqltext;
        SqlDataAdapter da = new SqlDataAdapter(mycomm);
        da.Fill(timutable);
        return timutable;
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (TreeViewsource.CheckedNodes.Count > 0)
            args.IsValid = true;
        else
            args.IsValid = false;
    }
    protected bool TimuYishiyong(string questionid)//查询某题目是否已使用
    {
        bool yishiyong = false;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();

        //学生自测题目
        comm.CommandText = "select count(questionid) from tb_zicetimu where questionid='" + questionid + "'";
        if (((int)comm.ExecuteScalar()) > 0)
        {
            yishiyong = true;
        }
        conn.Close();
        return yishiyong;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)//删除题目
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name; ;
        string tigongzhe = "";
        string guanliyuan = "";
        string questionid = ((LinkButton)sender).CommandArgument;
        if (TimuYishiyong(questionid))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", " <script   language= 'javascript'>alert('题目已被使用，不能删除，请尝试修改题目!');</script> ", false);
        }
        else
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select tb_tiku.tigongzhe,tb_kecheng.guanliyuan from tb_tiku inner join tb_kecheng on tb_kecheng.kechengid=tb_tiku.kechengid where tb_tiku.questionid=" + questionid;
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                tigongzhe = sdr.GetString(0);
                guanliyuan = sdr.GetString(1);
            }
            sdr.Close();
            conn.Close();
            if (username == guanliyuan || username == tigongzhe)
            {
                try
                {
                    conn.Open();
                    SqlTransaction st = conn.BeginTransaction();
                    comm.Transaction = st;
                    comm.CommandText = "delete from tb_tiku where questionid=" + questionid;
                    comm.ExecuteNonQuery();
                    comm.CommandText = "delete from tb_timuzhishidian where questionid=" + questionid;
                    comm.ExecuteNonQuery();
                    st.Commit();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script   language= 'javascript'> alert('题目删除成功！');</script>", false);
                    //删除题目后，删除按钮不可用
                    ((LinkButton)sender).Enabled = false;
                    //删除题目后，修改按钮不可用
                    ((LinkButton)(((LinkButton)sender).Parent.FindControl("LinkButton3"))).Enabled = false;
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script   language= 'javascript'> alert('题目删除失败！');</script>", false);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script   language= 'javascript'> alert('只有题目提供者和课程管理员才有权修改题目！');</script>", false);
            }
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)//修改题目
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string tigongzhe = "";
        string guanliyuan = "";
        string questionid = ((LinkButton)sender).CommandArgument;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select tb_tiku.tigongzhe,tb_kecheng.guanliyuan from tb_tiku inner join tb_kecheng on tb_kecheng.kechengid=tb_tiku.kechengid where tb_tiku.questionid=" + questionid;
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            tigongzhe = sdr.GetString(0);
            guanliyuan = sdr.GetString(1);
        }
        sdr.Close();
        conn.Close();
        if (username == guanliyuan || username == tigongzhe)
        {
            string urlx = "xiugaitimu2.aspx?questionid=" + questionid;
            string URL = " <script   language= 'javascript'> window.open( '" + urlx + "','_blank');</script> ";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", URL, false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script   language= 'javascript'> alert('只有题目提供者和课程管理员才有权修改题目！');</script>", false);
        }
    }
    /// <summary>
    /// 单击左侧知识树的某知识点，显示相关题目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeViewsource_SelectedNodeChanged(object sender, EventArgs e)
    {
        GridviewBoundCuoti();
    }
    /// <summary>
    /// 在Gridview上绑定相关知识点的题目
    /// </summary>
    protected void GridviewBoundCuoti()
    {
        HFleixing.Value = "zhishidian";
        HFxiajitimu.Value = CheckBox1.Checked.ToString();
        //得到所选知识点的id号
        string zhishidianid = TreeViewsource.SelectedNode.Value;
        HFzhishidianid.Value = zhishidianid;
        ConcreteKnowladge ck = new ConcreteKnowladge(int.Parse(zhishidianid));
        GridView1.DataSource = ck.GetTimu(CheckBox1.Checked);
        GridView1.DataBind();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (TreeViewsource.SelectedNode != null)
        {
            GridviewBoundCuoti();
        }
    }
}
