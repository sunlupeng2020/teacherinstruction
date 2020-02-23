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
    int zhishidianshu;
    int questionid; 
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        questionid = int.Parse(Request.QueryString["questionid"].ToString());
        if (!IsPostBack)
        {
            TreeViewsource.Attributes.Add("onclick", "client_OnTreeNodeChecked()");
                myconn.Open();
                mycomm.CommandText = "select timu,answer,nandu,tigongzhe,[type],shuoming,leibie,filepath,kechengid from tb_tiku where questionid=" + questionid;
                SqlDataReader sdr = mycomm.ExecuteReader();
                if (sdr.Read())
                {
                    string kechengid = sdr.GetInt32(8).ToString();
                    string answer = "";
                    Labeltixing.Text = sdr.GetString(4).Trim();
                    FCKeditortigan.Value = sdr.GetString(0).Trim();//题目
                    //难度
                    DropDownListnandu.SelectedValue = sdr.GetByte(2).ToString().Trim();
                    if (sdr.GetValue(5).ToString() !="")//题目解析
                    {
                        FCKeditorshuoming.Value = sdr.GetString(5).Trim();
                    }
                    if(sdr.GetString(6).ToString()!="")//类别，考试题？练习题
                        RadioButtonListkaoshiorlianxi.SelectedValue = sdr.GetString(6).Trim();
                    if (sdr.GetValue(1).ToString() != "")//答案
                    {
                        answer = sdr.GetString(1);
                    }                        
                    switch (sdr.GetString(4).Trim())
                    {
                        case "单项选择题":
                            FCKeditorcankaodaan.Visible = false;
                            RadioButtonListdanxuanckda.Visible = true;
                            RadioButtonListdanxuanckda.SelectedValue = answer;
                            CheckBoxListduoxuanckda.Visible = false;
                            RadioButtonListpanduandaan.Visible = false;
                            filetr.Visible = false;
                            cankaodaantr.Visible = true;
                            RequiredFieldValidatorcankaodaan.Enabled = false;
                            RequiredFieldValidatordanxuandaan.Enabled = true;
                            RequiredFieldValidatorpanduandaan.Enabled = false;
                            CustomValidatorduoxuan.Enabled = false;
                            break;
                        case "多项选择题":
                            FCKeditorcankaodaan.Visible = false;
                            RadioButtonListdanxuanckda.Visible = false;
                            CheckBoxListduoxuanckda.Visible = true;
                            RadioButtonListpanduandaan.Visible = false;
                            filetr.Visible = false;
                            cankaodaantr.Visible = true;
                            for (int i = 0; i < 5; i++)
                            {
                                if (answer.IndexOf(CheckBoxListduoxuanckda.Items[i].Value.Trim()) >= 0)
                                {
                                    CheckBoxListduoxuanckda.Items[i].Selected = true;
                                }
                            }
                            RequiredFieldValidatorcankaodaan.Enabled = false;
                            RequiredFieldValidatordanxuandaan.Enabled = false;
                            RequiredFieldValidatorpanduandaan.Enabled = false;
                            CustomValidatorduoxuan.Enabled = true;
                            break;
                        case "判断题":
                            FCKeditorcankaodaan.Visible = false;
                            RadioButtonListdanxuanckda.Visible = false;
                            CheckBoxListduoxuanckda.Visible = false;
                            RadioButtonListpanduandaan.Visible = true;
                            RadioButtonListpanduandaan.SelectedValue = answer;
                            filetr.Visible = false;
                            cankaodaantr.Visible = true;
                            RequiredFieldValidatorcankaodaan.Enabled = false;
                            RequiredFieldValidatordanxuandaan.Enabled = false;
                            RequiredFieldValidatorpanduandaan.Enabled = true;
                            CustomValidatorduoxuan.Enabled = false;
                            break;
                        case "操作题":
                            FCKeditorcankaodaan.Visible = false;
                            RadioButtonListdanxuanckda.Visible = false;
                            CheckBoxListduoxuanckda.Visible = false;
                            RadioButtonListpanduandaan.Visible = false;
                            filetr.Visible = true;
                            if (sdr.GetValue(7).ToString() != "")
                            {
                                HyperLink1.Visible = true;
                                HyperLink1.NavigateUrl = sdr.GetString(7).Trim();
                            }
                            else
                            {
                                HyperLink1.Visible = false;
                            }
                            cankaodaantr.Visible = false;
                            RequiredFieldValidatorcankaodaan.Enabled = false;
                            RequiredFieldValidatordanxuandaan.Enabled = false;
                            RequiredFieldValidatorpanduandaan.Enabled = false;
                            CustomValidatorduoxuan.Enabled = false;
                            break;
                        default:
                            FCKeditorcankaodaan.Visible = true;
                            FCKeditorcankaodaan.Value = answer;
                            RadioButtonListdanxuanckda.Visible = false;
                            CheckBoxListduoxuanckda.Visible = false;
                            RadioButtonListpanduandaan.Visible = false;
                            filetr.Visible = false;
                            cankaodaantr.Visible = true;
                            RequiredFieldValidatorcankaodaan.Enabled = true;
                            RequiredFieldValidatordanxuandaan.Enabled = false;
                            RequiredFieldValidatorpanduandaan.Enabled = false;
                            CustomValidatorduoxuan.Enabled = false;
                            break;
                    }
                     sdr.Close();
                    UpdateKechengTreeview(kechengid);
                    Labelkechengname.Text = TreeViewsource.Nodes[0].Text.Trim();

                    zhishidianshu = 0;
                    ArrayList zhishidian = new ArrayList();
                    if (myconn.State.ToString() == "Closed")
                        myconn.Open();
                    mycomm.CommandText = "select kechengjiegouid from tb_timuzhishidian where questionid=" + questionid;
                    SqlDataReader dr = mycomm.ExecuteReader();
                    while (dr.Read())
                    {
                        zhishidian.Add(dr.GetInt32(0).ToString());
                    }
                    dr.Close();
                    dr.Dispose();
                    myconn.Close();
                    foreach (TreeNode node in TreeViewsource.Nodes)
                    {
                        treeviewsourcecheck(node, zhishidian);
                    }
                    TextBoxzhishidian.Text = zhishidianshu.ToString();
                    if (myconn.State.ToString() == "Opened")
                        myconn.Close();
                }
                myconn.Close();
        }
    }
    //对题目涉及的知识点，在TreeView中进行勾选操作，
    protected void treeviewsourcecheck(TreeNode node, ArrayList zhishidianid)
    {
        string nodevalue = node.Value;
        if (zhishidianid.Contains(nodevalue))
        {
            node.Checked = true;
            zhishidianshu++;
            foreach (TreeNode childnode in node.ChildNodes)
            {
                treeviewsourcecheck(childnode, zhishidianid);
            }
        }
        else
        {
            node.Checked = false;
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
        int nandu = Convert.ToInt32(DropDownListnandu.SelectedValue);//难度
        string leibie = RadioButtonListkaoshiorlianxi.SelectedValue.ToString();//考试题？练习题？
        string cankaodaan = "";//参考答案
        string zhishidianid = "";
        //获取选中的知识ID号，如果某结点的祖先结点中有被选择的，则该结点的id号不计入
        foreach (TreeNode selectNode in TreeViewsource.CheckedNodes)
        {
            if (!zuxianchecked(selectNode))
            {
                //zhishidianid += TreeViewsource.CheckedNodes[i].Value + ",";
                zhishidianid += selectNode.Value + ",";
            }
        }
        string fileextendname = "";
        string filename = "";
        string savepath = "";
        if (FileUpload1.HasFile)
        {
            fileextendname = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.') + 1);
            filename = "caozuoti" + questionid.ToString() + "." + fileextendname;
            savepath = "~/timufile/" + filename;
            FileUpload1.PostedFile.SaveAs(Server.MapPath(savepath));
        }
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
            case "操作题":
                break;
            default:
                cankaodaan = FCKeditorcankaodaan.Value;
                break;
        }
        SqlParameter[] timupa = new SqlParameter[9];
        timupa[0] = new SqlParameter("@questionid", questionid);
        timupa[1] = new SqlParameter("@timu", tigan);
        timupa[2] = new SqlParameter("@answer", cankaodaan);
        timupa[3] = new SqlParameter("@nandu", nandu);
        timupa[4] = new SqlParameter("@type", tixing);
        timupa[5] = new SqlParameter("@shuoming", shuoming);
        timupa[6] = new SqlParameter("@leibie", leibie);
        timupa[7] = new SqlParameter("@kaochazhishidian", zhishidianid);
        timupa[8] = new SqlParameter("@filepath", savepath);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.Parameters.AddRange(timupa);
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "update tb_tiku set timu=@timu,answer=@answer,nandu=@nandu,shuoming=@shuoming,leibie=@leibie,kaochazhishidian=@kaochazhishidian,filepath=@filepath where questionid=@questionid";
            comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "delete from tb_timuzhishidian where questionid=" + questionid;
            comm.ExecuteNonQuery();
            string[] zhishidianidshuzu = zhishidianid.Split(',');
            foreach (string s in zhishidianidshuzu)
            {
                if (s.Length > 0)
                {
                    comm.CommandText = "insert into tb_timuzhishidian(questionid,kechengjiegouid) values(" + questionid + "," + s + ")";
                    comm.ExecuteNonQuery();
                }
            }
            st.Commit();
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('题目修改成功！');", true);
        }
        catch (Exception ex)
        {
            st.Rollback();
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('题目修改失败！');", true);
            Labelfankui.Text = ex.Message;
        }
        finally
        {
            conn.Close();
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
        return(ischecked);
    }
    protected TreeNode zuxiannode(TreeNode mytreenode)//查找祖先结点中哪一个被选择
    {
        TreeNode dangqiannode = mytreenode;
        TreeNode zuxian = mytreenode.Parent;
        //上溯
        while ((zuxian != null) && (zuxian.Checked == false))
        {
            dangqiannode = zuxian;
            zuxian = dangqiannode.Parent;
        }
        return zuxian;
    }

    protected void UpdateKechengTreeview(string kechengid)//更新课程结构TreeView
    {
        TreeViewsource.ConnectionString = ConfigurationManager.ConnectionStrings[TreeViewsource.ConnectionStringName].ConnectionString;
        TreeViewsource.kechengid = int.Parse(kechengid);
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
}
