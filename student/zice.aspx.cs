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
using System.Xml;
using System.Text;
using System.Collections.Generic;

public partial class studentstudy_zice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1.Text = TreeView1.CheckedNodes.Count.ToString();
        if (!IsPostBack)
        {
            TreeView1.Attributes.Add("onclick", "client_OnTreeNodeChecked()");
            string kechengid = Session["kechengid"].ToString();
            TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
            TreeView1.kechengid = int.Parse(kechengid);
        }
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
    }
    protected void addnodevalue(TreeNode node, List<string> selectandhoudai)//添加后代知识点
    {
        if (node.ChildNodes.Count > 0)
            foreach (TreeNode childnode in node.ChildNodes)
            {
                selectandhoudai.Add(childnode.Value);
                addnodevalue(childnode,selectandhoudai);
            }
    }
    protected void XianshiTimu(string tihaos)//显示测试题目
    {
        tihaos = tihaos.Substring(0, tihaos.Length - 1);
        string[] tihaoshuzu = tihaos.Split(',');
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        comm.CommandText = "select questionid,timu,[type] from tb_tiku  where questionid in (" + tihaos + ") order by questionid,[type] asc";
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.HasRows)
        {
            #region//显示题目
            Literal tmliteral;
            RadioButtonList danxuan;//单选按钮组
            RadioButtonList panduan;//判断按钮组
            CheckBoxList duoxuan;//多选按钮组   
            int tihao = 1;
            while (sdr.Read())
            {
                tmliteral = new Literal();
                tmliteral.ID = "t" + tihao.ToString();
                tmliteral.Text = tihao.ToString() + "(" + sdr[2].ToString().Trim() + ")、" + sdr[1].ToString().Replace("<p>", "").Replace("</p>", "") + "<br/><table><tr><td><font color='Blue'>请选择：</font></td><td>";
                PlaceHolderceshitimu.Controls.Add(tmliteral);
                switch (sdr[2].ToString().Trim())
                {
                    case "判断题":
                        //if (!(ViewState["zhanguangtai"].ToString() == "inceshi" || ViewState["zhanguangtai"].ToString() == "ceshijieshu"))
                        panduan = new RadioButtonList();
                        panduan.ID = "answer" + tihao.ToString().Trim();
                        panduan.Items.Add(new ListItem("正确", "T"));
                        panduan.Items.Add(new ListItem("错误", "F"));
                        panduan.RepeatDirection = RepeatDirection.Horizontal;
                        panduan.Width = Unit.Pixel(150);
                        PlaceHolderceshitimu.Controls.Add(panduan);
                        tmliteral = new Literal();
                        tmliteral.ID = "l" + tihao.ToString();
                        tmliteral.Text = "</td></tr></table><hr/>";
                        PlaceHolderceshitimu.Controls.Add(tmliteral);
                        break;
                    case "单项选择题":
                        danxuan = new RadioButtonList();
                        danxuan.ID = "answer" + tihao.ToString().Trim();
                        danxuan.Items.Add("A");
                        danxuan.Items.Add("B");
                        danxuan.Items.Add("C");
                        danxuan.Items.Add("D");
                        danxuan.RepeatDirection = RepeatDirection.Horizontal;
                        danxuan.Width = Unit.Pixel(300);
                        PlaceHolderceshitimu.Controls.Add(danxuan);
                        tmliteral = new Literal();
                        tmliteral.ID = "l" + sdr[0].ToString();
                        tmliteral.Text = "</td></tr></table><hr/>";
                        PlaceHolderceshitimu.Controls.Add(tmliteral);
                        break;
                    case "多项选择题":
                        duoxuan = new CheckBoxList();
                        duoxuan.ID = "answer" + tihao.ToString().Trim();
                        duoxuan.Items.Add("A");
                        duoxuan.Items.Add("B");
                        duoxuan.Items.Add("C");
                        duoxuan.Items.Add("D");
                        duoxuan.Items.Add("E");
                        duoxuan.RepeatDirection = RepeatDirection.Horizontal;
                        duoxuan.Width = Unit.Pixel(350);
                        PlaceHolderceshitimu.Controls.Add(duoxuan);
                        tmliteral = new Literal();
                        tmliteral.ID = "l" + tihao.ToString();
                        tmliteral.Text = "</td></tr></table><hr/>";
                        PlaceHolderceshitimu.Controls.Add(tmliteral);
                        break;
                }
                tihao++;
            }
            #endregion
        }
        sdr.Close();
        conn.Close();
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
    protected bool XuanzeTimu(StringBuilder tihaoSB)//选择测试题目，两个参数：1、选择的知识点及其下位知识点id,2、题目数量
    {
        HiddenField1.Value="";
        StringBuilder selectnodesSB = new StringBuilder();
        List<string> nodesList = new List<string>();
        int ceshitimushu = int.Parse(RBL_Timushu.SelectedValue);//用户选择的测试题目数
        foreach (TreeNode node in TreeView1.CheckedNodes)
        {
            if (zuxiannode(node) == null)//如果该知识点的祖先知识点没有被选择的，则添加到知识点列表中，否则不添加
            {
                selectnodesSB.Append(node.Text + ",");
                nodesList.Add(node.Value);
                addnodevalue(node, nodesList);
            }
        }
        Labelzhishidian.Text = selectnodesSB.ToString();
        string selectNodesIds = "";
        foreach (string nodeid in nodesList)
            selectNodesIds += nodeid + ",";
        selectNodesIds = selectNodesIds.Substring(0, selectNodesIds.Length - 1);//截去末尾逗号
        Labeltimushu.Text = ceshitimushu.ToString();
        //用数组记录测试知识点id
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandType = CommandType.Text;
        comm.CommandText = "select questionid,timu,answer,type from tb_tiku where zhishidianid in ("+ selectNodesIds+") order by type asc";
        DataTable timutable = new DataTable();
        SqlDataAdapter beixuantimusdr = new SqlDataAdapter(comm);
        beixuantimusdr.Fill(timutable);
        int timushuliang = timutable.Rows.Count;//符合条件的题目数
        int[] timuhaoshuzu=new int[ceshitimushu];//存储题目号的数组
        if (timushuliang < ceshitimushu)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('您要自测"+ceshitimushu.ToString()+"道题，题库中仅有"+timushuliang.ToString()+"道题，数量不足，请选择较大的知识范围或较少的题目数！');</script>", false);
            return false;
        }
        else if(timushuliang==ceshitimushu)//题目数量正好
        {
            timuhaoshuzu = new int[ceshitimushu];
            for (int i = 0; i < ceshitimushu; i++)
                timuhaoshuzu[i] = i;
        }
        else//如果符合条件的题目数多于测试题目数，则随机选择题目
        {
            #region//随机抽取题目
            Random rnd = new Random();
            timuhaoshuzu = new int[ceshitimushu];//记录选择的题目在Timutabl中的编号
            bool[] timuxuze = new bool[timushuliang];
            for (int i = 0; i < timushuliang; i++)//标志所有题目均未被选择
                timuxuze[i] = false;
            int xuanzetihao;
            for (int i = 0; i < ceshitimushu; i++)
            {
                do
                {
                    xuanzetihao = rnd.Next(timushuliang);
                } while (timuxuze[xuanzetihao]);//在已选择的题目中查找该题号是否存在
                timuhaoshuzu[i] = xuanzetihao;
                timuxuze[xuanzetihao] = true;//标志该题已选择
            }
            #endregion
        }
        for (int i = 0; i <ceshitimushu; i++)
        {
            tihaoSB.Append(timutable.Rows[timuhaoshuzu[i]][0].ToString()+",");
        }
        timutable.Dispose();
        HiddenField1.Value =tihaoSB.ToString();
        return true;
    }
    protected void deselect(TreeNode node)
    {
        foreach (TreeNode child in node.ChildNodes)
        {
            child.SelectAction = TreeNodeSelectAction.None;
            deselect(child);
        }
    }
    protected void StartNextButton_Click(object sender, EventArgs e)//第一步，选完知识点后的下一步按钮
    {
        StringBuilder tihaoSB = new StringBuilder();
        if (XuanzeTimu(tihaoSB))
        {
            //显示题目，跳转到下一步
            XianshiTimu(tihaoSB.ToString());
            Wizard1.ActiveStepIndex = 1;
        }
    }
    protected void FinishButton_Click(object sender, EventArgs e)//交卷
    {
        //测试信息写入数据库
        int zongfen = 0;
        StringBuilder sb = new StringBuilder();
        //改卷
        if (JiaoJuan(out zongfen, sb))
        {

            //显示反馈信息、各题目答案
            Labeltmsl.Text = RBL_Timushu.SelectedValue;
            Labelzhshd.Text = Labelzhishidian.Text;
            Labelchj.Text = zongfen.ToString();
            Literaltimuxq.Text = sb.ToString();
            if (zongfen >= 90)
                Labelfkxx.Text += "<br>你太棒了！你是我们的骄傲！";
            else if (zongfen >= 80)
                Labelfkxx.Text += "<br>非常好！你非常出色！百尺竿头，更进一步！";
            else if (zongfen >= 60)
                Labelfkxx.Text += "<br>很好！希望你取得更好的成绩！";
            else
                Labelfkxx.Text += "<br>你再努力一些，就会取得更好的成绩！";
            Wizard1.ActiveStepIndex = 2;
        }
    }
    protected bool JiaoJuan(out int zongfen, StringBuilder sb)//交卷，测试信息写入数据库，改卷
    {
        bool chenggong = false;
        string tihaos = HiddenField1.Value;
        tihaos = tihaos.Substring(0, tihaos.Length - 1);
        string[] tihaoshuzu = tihaos.Split(',');
        string kechengid = Session["kechengid"].ToString();
        string ceshiname;
        CheckBoxList duoxuanCheckList;
        string ceshizhishidian = Labelzhishidian.Text.Trim();
        if (ceshizhishidian.Length <= 100)
        {
            ceshiname = ceshizhishidian;
        }
        else
        {
            ceshiname = ceshizhishidian.Substring(0, 95) + "等知识点";
        }
        int timushuliang = int.Parse(RBL_Timushu.SelectedValue);
        int fenzhi = 100 / timushuliang;//每题分值
        zongfen = 0;//总分
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
         comm.CommandText = "select questionid,timu,type,answer from tb_tiku where questionid in (" + tihaos + ") order by questionid,type asc";
        DataTable tmtb = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(tmtb);
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            comm.CommandText = "insert into tb_zice(kechengid,username,ceshiname,fenshu) values(" + kechengid + ",'" + username + "','" + ceshiname + "',0) select @@identity as ziceid";
            int ceshiid =Convert.ToInt32(comm.ExecuteScalar());//测试Id
            string answer;
            for (int i = 0; i < tmtb.Rows.Count; i++)
            {
                sb.Append((i + 1).ToString() + "(" + tmtb.Rows[i][2].ToString() + ")" + tmtb.Rows[i][1].ToString() + "<br/><font color='red'>本题的参考答案是：" + tmtb.Rows[i][3].ToString() + ";您的回答是：");
                switch (tmtb.Rows[i][2].ToString().Trim())
                {
                    case "单项选择题":
                    case "判断题":
                        answer = ((RadioButtonList)(PlaceHolderceshitimu.FindControl("answer" + (i + 1).ToString()))).SelectedValue.Trim();
                        sb.Append(answer);
                        if (answer == tmtb.Rows[i][3].ToString().Trim())
                        {
                            zongfen += fenzhi;
                            sb.Append(";本题得分：" + fenzhi.ToString() + "分。</font><br/><hr/>");
                        }
                        else
                        {
                            sb.Append(";本题得分：0分。</font><br/><hr/>");
                        }
                        comm.CommandText = "insert into tb_zicetimu(ziceid,questionid,huida,tihao) values(" + ceshiid + "," + tmtb.Rows[i][0].ToString() + ",'" + answer + "'," + (i + 1).ToString() + ")";
                        comm.ExecuteNonQuery();
                        break;
                    case "多项选择题":
                        answer = "";
                        duoxuanCheckList = (CheckBoxList)(PlaceHolderceshitimu.FindControl("answer" + (i + 1).ToString()));
                        for (int j1 = 0; j1 < 5; j1++)
                            if (duoxuanCheckList.Items[j1].Selected)
                                answer += duoxuanCheckList.Items[j1].Value.ToString().Trim();
                        sb.Append(answer);
                        if (answer == tmtb.Rows[i][3].ToString().Trim())
                        {
                            zongfen += fenzhi;
                            sb.Append(";本题得分：" + fenzhi.ToString() + "分。</font><br/><hr/>");

                        }
                        else
                        {
                            sb.Append(";本题得分：0分。</font><br/><hr/>");
                        }
                        comm.CommandText = "insert into tb_zicetimu(ziceid,questionid,huida,tihao) values(" + ceshiid + "," + tmtb.Rows[i][0].ToString() + ",'" + answer + "'," + (i + 1).ToString() + ")";
                        comm.ExecuteNonQuery();
                        break;
                }
            }
            comm.CommandText = "update tb_zice set fenshu=" + zongfen + " where ziceid=" + ceshiid;
            comm.ExecuteNonQuery();
            st.Commit();
            chenggong = true;
        }
        catch (Exception ex)
        {
            st.Rollback();
            Labelfankui.Text = ex.Message;
            chenggong = false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        return chenggong;
    }
    protected void PlaceHolderceshitimu_PreRender(object sender, EventArgs e)
    {
        if (HiddenField1.Value.Trim().Length > 0)
        {
            XianshiTimu(HiddenField1.Value.Trim());
        }
    }
}
