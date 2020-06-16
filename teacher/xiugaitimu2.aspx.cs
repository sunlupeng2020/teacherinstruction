using System;
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
using System.Xml;

public partial class teachermanage_timuguanli_xiugaitimu2 : System.Web.UI.Page
{
    string zhishidianid;
    int questionid; 
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        questionid = int.Parse(Request.QueryString["questionid"].ToString());
        if (!IsPostBack)
        {
                myconn.Open();
                mycomm.CommandText = "select timu,answer,tigongzhe,[type],shuoming,kechengid,zhishidianid from tb_tiku where questionid=" + questionid;
                SqlDataReader sdr = mycomm.ExecuteReader();
                if (sdr.Read())
                {
                    string kechengid = sdr.GetInt32(5).ToString();
                    HFzhishidianid.Value = zhishidianid = sdr.GetInt32(6).ToString();//记录知识点ID
                    string answer = "";
                    Labeltixing.Text = sdr.GetString(3).Trim();
                    FCKeditortigan.Value = sdr.GetString(0).Trim();//题目

                    if (sdr.GetValue(4).ToString() !="")//题目解析
                    {
                        FCKeditorshuoming.Value = sdr.GetString(4).Trim();
                    }
                    if (sdr.GetValue(1).ToString() != "")//答案
                    {
                        answer = sdr.GetString(1);
                    }                        
                    switch (sdr.GetString(3).Trim())
                    {
                        case "单项选择题":
                            RadioButtonListdanxuanckda.Visible = true;
                            RadioButtonListdanxuanckda.SelectedValue = answer;
                            CheckBoxListduoxuanckda.Visible = false;
                            RadioButtonListpanduandaan.Visible = false;
                            cankaodaantr.Visible = true;
                            CustomValidatorduoxuan.Enabled = false;
                            break;
                        case "多项选择题":
                            RadioButtonListdanxuanckda.Visible = false;
                            CheckBoxListduoxuanckda.Visible = true;
                            RadioButtonListpanduandaan.Visible = false;
                            cankaodaantr.Visible = true;
                            for (int i = 0; i < 5; i++)
                            {
                                if (answer.IndexOf(CheckBoxListduoxuanckda.Items[i].Value.Trim()) >= 0)
                                {
                                    CheckBoxListduoxuanckda.Items[i].Selected = true;
                                }
                            }
                            CustomValidatorduoxuan.Enabled = true;
                            break;
                        case "判断题":
                            RadioButtonListdanxuanckda.Visible = false;
                            CheckBoxListduoxuanckda.Visible = false;
                            RadioButtonListpanduandaan.Visible = true;
                            RadioButtonListpanduandaan.SelectedValue = answer;
                            cankaodaantr.Visible = true;
                            CustomValidatorduoxuan.Enabled = false;
                            break;
                        default:
                            break;
                    }
                    sdr.Close();
                    UpdateKechengTreeview(kechengid);
                    foreach (TreeNode node in TreeViewsource.Nodes)
                    {
                        treeviewsourcecheck(node, zhishidianid);
                    }
                }
                if (myconn.State.ToString() == "Opened")
                    myconn.Close();
        }
    }
    //在TreeView中查找题目涉及的知识点,找到一个即返回
    protected void treeviewsourcecheck(TreeNode node, string zhishidianid)
    {
        if (zhishidianid ==node.Value)
        {
            Labelzhishdidian.Text = node.Text;
            return;
        }
        else
        {
            foreach (TreeNode childnode in node.ChildNodes)
            {
                treeviewsourcecheck(childnode, zhishidianid);
            }
        }
    }
   
    ////提交后写入题目到数据库
    protected void Buttonsubmit_Click(object sender, EventArgs e)//提交更改
    {
        Labelfankui.Text = "";
        string tixing = Labeltixing.Text.Trim();
        string tigan = FCKeditortigan.Value.Trim();//题干
        tigan = tigan.Replace("'", "''");
        string shuoming = FCKeditorshuoming.Value.Trim();//说明
        string cankaodaan = "";//参考答案
        switch (tixing)
        {
            case "单项选择题":
                //验证各项内容
                cankaodaan = RadioButtonListdanxuanckda.SelectedValue;
                break;
            case "多项选择题":
                for (int i = 0; i < 5; i++)
                    if (CheckBoxListduoxuanckda.Items[i].Selected)
                        cankaodaan += CheckBoxListduoxuanckda.Items[i].Value;
                if (cankaodaan.Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert(请选择多项选择题的答案！');</script>", false);
                     return;
                }
                break;
            case "判断题":
                cankaodaan = RadioButtonListpanduandaan.SelectedValue;
                break;
            default:
                break;
        }
        SqlParameter[] timupa = new SqlParameter[5];
        timupa[0] = new SqlParameter("@questionid", questionid);
        timupa[1] = new SqlParameter("@timu", tigan);
        timupa[2] = new SqlParameter("@answer", cankaodaan);
        timupa[3] = new SqlParameter("@shuoming", shuoming);
        timupa[4] = new SqlParameter("@kaochazhishidian", HFzhishidianid.Value);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.Parameters.AddRange(timupa);
        conn.Open();
        try
        {
            comm.CommandText = "update tb_tiku set timu=@timu,answer=@answer,shuoming=@shuoming,zhishidianid=@kaochazhishidian where questionid=@questionid";
            comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('题目修改成功！');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('题目修改失败！');", true);
            Labelfankui.Text = ex.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void UpdateKechengTreeview(string kechengid)//更新课程结构TreeView
    {
        TreeViewsource.ConnectionString = ConfigurationManager.ConnectionStrings[TreeViewsource.ConnectionStringName].ConnectionString;
        TreeViewsource.kechengid = int.Parse(kechengid);
    }

    protected void TreeViewsource_SelectedNodeChanged(object sender, EventArgs e)
    {
        HFzhishidianid.Value = TreeViewsource.SelectedNode.Value;
        Labelzhishdidian.Text = TreeViewsource.SelectedNode.Text;
    }
}
