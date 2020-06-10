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
    }
    protected string Get_kechengguanliyuan(string kechengid)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select guanliyuan from tb_kecheng where kechengid=" + kechengid;
        conn.Open();
        string guanliyuan = comm.ExecuteScalar().ToString().Trim();
        conn.Close();
        return guanliyuan;

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string cxleixing = HFleixing.Value.Trim();
        if (cxleixing == "zhishidian")
        {
            List<string> zhishidianids = new List<string>();
            string[] zsdsz = HFdata.Value.Split(',');
            foreach (string zsd in zsdsz)
            {
                if (zsd.Length > 0)
                    zhishidianids.Add(zsd);
            }
            string tixing = HFtixing.Value.Trim();
            GridView1.DataSource = gridview1_huoqushuju(zhishidianids, tixing);
        }
        else if (cxleixing == "guanjianzi")
        {
            string keyword = HFdata.Value.Trim();
            string kechengid = HFkechengid.Value.Trim();
            string tixing = HFtixing.Value.Trim();
            bool xdkc = bool.Parse(HFxdkc.Value.Trim());
            bool xdtx = bool.Parse(HFxdtx.Value.Trim());
            GridView1.DataSource = GuanjianziTimu(keyword, kechengid, tixing, xdtx, xdkc);
        }
        else
        {
            string kechengid = HFkechengid.Value.Trim();
            string tixing = HFtixing.Value.Trim();
            GridView1.DataSource = GetKechengTimu(kechengid, tixing);
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
    protected void AddNodeChildValueToList(List<string> zhishidianids, TreeNode node)
    {
        foreach (TreeNode childnode in node.ChildNodes)
        {
            if (zhishidianids.Contains(childnode.Value) == false)
                zhishidianids.Add(childnode.Value);
            AddNodeChildValueToList(zhishidianids, childnode);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//显示相关题目按钮
    {
        //获取选择了哪些知识点
        if (Page.IsValid == true)
        {
            HFleixing.Value = "zhishidian";
            //获取选择的知识点及其子知识点id号
            List<string> zhishidianids =TreeViewsource.CheckedNodesAndChildrenIds;
            StringBuilder sb = new StringBuilder();
            foreach (string s in zhishidianids)
                sb.Append(s + ",");
            HFdata.Value = sb.ToString();
            //Gridview获取数据
            string tixing = DropDownList2.SelectedValue;
            HFtixing.Value = tixing;
            GridView1.DataSource = gridview1_huoqushuju(zhishidianids, tixing);
            if (GridView1.PageCount > 0)
            {
                GridView1.PageIndex = 0;
            }
            GridView1.DataBind();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)//按关键字搜题目显示
    {
        HFleixing.Value = "guanjianzi";
        string keyword = TextBox1.Text.Trim();
        HFdata.Value = keyword;
        string kechengid = Session["kechengid"].ToString();//课程ID
        HFkechengid.Value = kechengid;
        string tixing = DropDownList2.SelectedValue;//题型名称
        HFtixing.Value = tixing;
        bool xdkc = CheckBox1.Checked;
        bool xdtx = CheckBox2.Checked;
        HFxdtx.Value = xdtx.ToString();
        HFxdkc.Value = xdkc.ToString();
        GridView1.DataSource = GuanjianziTimu(keyword, kechengid, tixing, xdtx, xdkc);
        if (GridView1.PageCount > 0)
        {
            GridView1.PageIndex = 0;
        }
        GridView1.DataBind();

    }
    protected void Button3_Click(object sender, EventArgs e)//显示课程题目
    {
        string kechengid = Session["kechengid"].ToString();
        HFleixing.Value = "kecheng";
        HFkechengid.Value = kechengid;
        string tixing = DropDownList2.SelectedValue;
        HFtixing.Value = tixing;
        GridView1.DataSource = GetKechengTimu(kechengid, tixing);
        if (GridView1.PageCount > 0)
        {
            GridView1.PageIndex = 0;
        }
        GridView1.DataBind();
    }
    protected DataTable GuanjianziTimu(string keyword, string kechengid, string tixing, bool xdtx, bool xdkc)//按关键字搜题目
    {
        DataTable timutable = new DataTable();
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        string sqltext = "select tb_tiku.questionid,tb_tiku.timu as 题目,tb_tiku.[type],tb_tiku.answer,tb_tiku.tigongzhe,tb_tiku.filepath,tb_teacher.xingming,tb_tiku.kechengid  from tb_tiku  left join tb_teacher on tb_teacher.username=tb_tiku.tigongzhe where timu like '%" + keyword + "%'";
        if (xdkc)//限定课程
            sqltext += " and kechengid=" + kechengid;
        if (xdtx && tixing != "全部题型")//如果限定了题型，又不是全部题型
            sqltext += " and type='" + tixing + "'";
        mycomm.CommandText = sqltext;
        SqlDataAdapter da = new SqlDataAdapter(mycomm);
        da.Fill(timutable);
        return timutable;
    }
    protected DataTable GetKechengTimu(string kechengid, string tixing)//课程题目
    {
        DataTable timutable = new DataTable();
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        string sqltext = "select tb_tiku.questionid,tb_tiku.timu as 题目,tb_tiku.[type],tb_tiku.answer,tb_tiku.tigongzhe,tb_tiku.filepath,tb_teacher.xingming,tb_tiku.kechengid  from tb_tiku  left join tb_teacher on tb_teacher.username=tb_tiku.tigongzhe where tb_tiku.kechengid='" + kechengid + "'";
        if (tixing != "全部题型")//如果限定了题型，又不是全部题型
            sqltext += " and type='" + tixing + "'";
        mycomm.CommandText = sqltext;
        SqlDataAdapter da = new SqlDataAdapter(mycomm);
        da.Fill(timutable);
        return timutable;
    }
    protected DataTable gridview1_huoqushuju(List<string> zhishidianids, string tixing)//根据题型、知识点号搜相关题目
    {
        //得到所有课程结构id
        DataTable timutable = new DataTable();
        string tablename = "tb_" + DateTime.Now.Ticks.ToString();//临时数据表名
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        try
        {
            myconn.Open();
            mycomm.CommandText = "create table " + tablename + "(jiegouid int)";
            mycomm.ExecuteNonQuery();
            foreach (string str in zhishidianids)
            {
                mycomm.CommandText = "insert into " + tablename + "(jiegouid) values(" + str + ")";
                mycomm.ExecuteNonQuery();
            }
            myconn.Close();
            switch (tixing)
            {
                case "全部题型":
                    mycomm.CommandText = "select tb_tiku.questionid,tb_tiku.timu as 题目,tb_tiku.[type],tb_tiku.answer,tb_tiku.tigongzhe,tb_tiku.filepath,tb_teacher.xingming,tb_tiku.kechengid  from tb_tiku left join tb_teacher on tb_teacher.username=tb_tiku.tigongzhe where  tb_tiku.questionid in ( select questionid from tb_timuzhishidian where questionid not in ( select questionid from tb_timuzhishidian where kechengjiegouid not in (select jiegouid from " + tablename + "))) order by tb_tiku.questionid";
                    break;
                default:
                    mycomm.CommandText = "select tb_tiku.questionid,tb_tiku.timu as 题目,tb_tiku.[type],tb_tiku.answer,tb_tiku.tigongzhe,tb_tiku.filepath,tb_teacher.xingming,tb_tiku.kechengid  from  tb_tiku  left join tb_teacher on tb_teacher.username=tb_tiku.tigongzhe where tb_tiku.[type]='" + tixing + "' and   tb_tiku.questionid in(select questionid from tb_timuzhishidian where questionid not in(select questionid from tb_timuzhishidian where kechengjiegouid not in (select jiegouid from " + tablename + "))) order by tb_tiku.questionid";
                    break;
            }
            SqlDataAdapter myda = new SqlDataAdapter(mycomm);
            myda.Fill(timutable);
            GridView1.DataSource = timutable;
            GridView1.DataBind();
            try
            {
                myconn.Open();
                mycomm.CommandText = "drop table " + tablename;
                mycomm.ExecuteNonQuery();
            }
            finally
            {
                if (myconn.State == ConnectionState.Open)
                    myconn.Close();
            }
        }
        catch (Exception e1)
        {
            Labelfankui.Text += e1.Message;
        }
        finally
        {
            if (myconn.State.ToString() == "Opened")
                myconn.Close();
        }
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
        try
        {
            conn.Open();
            while (true)
            {
                //教师作业题目
                comm.CommandText = "select count(questionid) from tb_teacherzuoyetimu where questionid='" + questionid + "'";
                if (((int)comm.ExecuteScalar()) > 0)
                {
                    yishiyong = true;
                    break;
                }
                //学生作业题目
                comm.CommandText = "select count(questionid) from tb_stuzuoyetimu where questionid='" + questionid + "'";
                if (((int)comm.ExecuteScalar()) > 0)
                {
                    yishiyong = true;
                    break;
                }
                //教师测试题目
                comm.CommandText = "select count(questionid) from tb_teachershijuantimu where questionid='" + questionid + "'";
                if (((int)comm.ExecuteScalar()) > 0)
                {
                    yishiyong = true;
                    break;
                }
                //学生测试题目
                comm.CommandText = "select count(questionid) from tb_studentkaoshiti where questionid='" + questionid + "'";
                if (((int)comm.ExecuteScalar()) > 0)
                {
                    yishiyong = true;
                    break;
                }
                //学生自测题目
                comm.CommandText = "select count(questionid) from tb_zicetimu where questionid='" + questionid + "'";
                if (((int)comm.ExecuteScalar()) > 0)
                {
                    yishiyong = true;
                }
                break;
            }
        }
        catch (Exception ex)
        {
            Labelfankui.Text += ex.Message;
        }
        finally
        {
            conn.Close();
        }
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
}