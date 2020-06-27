using System;
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
using Microsoft.ApplicationBlocks.Data;

public partial class teacher_cuotitongji : System.Web.UI.Page
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
    /// 显示题目时的分页，
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string cxleixing = HFleixing.Value.Trim();
        if (cxleixing == "zhishidian")//按知识点搜题目
        {
            string zhishidianid = HFzhishidianid.Value;
            bool xiaji = Boolean.Parse(HFxiajitimu.Value);
            ConcreteKnowladge ck = new ConcreteKnowladge(int.Parse(zhishidianid));
            if (xiaji)
                zhishidianid = ck.ZhishidianIds;
            GridView1.DataSource = GetZhishidianCuoti(zhishidianid);
        }
        else//按关键字搜题目
        {
            string keyword = HFkeyword.Value.Trim();
            GridView1.DataSource = GuanjianziTimu(keyword, Session["kechengid"].ToString());
        }
        int pageindex = e.NewPageIndex;
        GridView1.PageIndex = pageindex;
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
    /// <summary>
    /// 按关键字搜题目
    /// </summary>
    /// <param name="keyword">关键字</param>
    /// <param name="kechengid">课程ID号</param>
    /// <returns></returns>
    protected DataSet GuanjianziTimu(string keyword, string kechengid)//按关键字搜题目
    {
        string sqltxt = "select tb_tiku.questionid,tb_tiku.timu,tb_tiku.type,tb_tiku.answer,tb_tiku.shuoming,tb_zicetimu.huida from tb_tiku,tb_zicetimu where tb_tiku.questionid=tb_zicetimu.questionid and tb_tiku.timu like '%" + keyword + "%' and tb_tiku.answer <> tb_zicetimu.huida and tb_tiku.kechengid=" + kechengid;
        DataSet ds = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt);
        return ds;
    }

    /// <summary>
    /// 按知识点搜对应的错题
    /// </summary>
    /// <param name="zhishidianid">一个或多个知识点的id,中间用逗号隔开</param>
    /// <returns></returns>
    protected DataSet GetZhishidianCuoti(string zhishidianid)
    {
        string sqltxt = "select tb_tiku.questionid,tb_tiku.timu,tb_tiku.type,tb_tiku.answer,tb_tiku.shuoming,tb_zicetimu.huida from tb_tiku,tb_zicetimu where tb_tiku.questionid=tb_zicetimu.questionid  and tb_tiku.questionid in(select questionid from tb_tiku where zhishidianid in(" + zhishidianid + ")) and tb_tiku.answer <> tb_zicetimu.huida order by tb_tiku.questionid asc";
        return SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt);
    }


    /// <summary>
    /// 单击左侧知识树的某知识点，显示相关题目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreeViewsource_SelectedNodeChanged(object sender, EventArgs e)
    {
        GridviewBindZhishidianCuoti();
    }
    /// <summary>
    /// 在GridView上绑定知识点对应的错题
    /// </summary>
    protected void GridviewBindZhishidianCuoti()
    {
        //得到所选知识点的id号
        string zhishidianid = TreeViewsource.SelectedNode.Value;
        HFzhishidianid.Value = zhishidianid;
        HFleixing.Value = "zhishidian";
        HFxiajitimu.Value = CheckBox1.Checked.ToString();
        //创建知识点类
        ConcreteKnowladge ck = new ConcreteKnowladge(int.Parse(zhishidianid));
        if (CheckBox1.Checked)
            zhishidianid = ck.ZhishidianIds;
        string sqltxt = "select tb_tiku.questionid,tb_tiku.timu,tb_tiku.type,tb_tiku.answer,tb_tiku.shuoming,tb_zicetimu.huida from tb_tiku,tb_zicetimu where tb_tiku.questionid=tb_zicetimu.questionid  and tb_tiku.questionid in(select questionid from tb_tiku where zhishidianid in(" + zhishidianid + ")) and tb_tiku.answer <> tb_zicetimu.huida";
        GridView1.DataSource = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt);
        GridView1.DataBind();
    }

    /// <summary>
    /// 在题目前显示编号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        int i = 1;
        foreach (GridViewRow row in GridView1.Rows)
        {
            ((Label)(row.Cells[0].FindControl("timubh"))).Text = i.ToString();
            i++;
        }
    }

    //是否显示下级知识点对应的错题状态变化时
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (TreeViewsource.SelectedNode != null)
        {
            GridviewBindZhishidianCuoti();
        }
    }
}