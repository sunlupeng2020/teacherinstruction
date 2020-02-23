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

public partial class teachermanage_kechengguanli_zhishimanage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindKechengTreeView();
        string kechengid=Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if(!KechengInfo.IsTeacherManageKecheng(username,kechengid))
        {
            BtnAddzhishidian.Enabled=false;
            Lbl_fankui.Text = "您不是本课程的管理员，无权对其知识结构进行编辑修改。";
        }
        else
        {
            BtnAddzhishidian.Enabled=true;
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        //获取选择的知识点的名称和id号
        TextBox1.Text = TreeView1.SelectedNode.Text;
        Txtshangweiid.Text = TreeView1.SelectedNode.Value;
        Txtzhishixuhao.Text = "1";
    }
    protected void BtnAddzhishidian_Click(object sender, EventArgs e)
    {
        string zhishileixing = DropDownListzhishileixing.SelectedValue;//知识类型
        string kechengid = Session["kechengid"].ToString();//课程ID
        string zhishiname=Txtzhishiname.Text.Trim();//知识点名称
        int shangweiid = Int32.Parse(Txtshangweiid.Text);//上位知识点ID
        string instruction =FCKeditor1.Value;//介绍
        int zhishixuhao = Int32.Parse(Txtzhishixuhao.Text);//序号
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        //开始事务
        //输入的知识点要插入到课程结构表中，
        //插入到知识点中
        //插入到知识关联表中
        SqlCommand comm = conn.CreateCommand();
        try
        {
            conn.Open();
            //判断该知识点是否已存在，
            comm.CommandText = "select count(jiegouname) from tb_KechengJiegou where jiegouname='" + zhishiname + "' and shangwei=" + shangweiid;
            if ((int)comm.ExecuteScalar() > 0)
            {
                if (!CheckBoxtishi.Checked)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('该知识点已存在！添加失败！');</script>", false);
                }
                Lbl_fankui.Text = "该知识点已存在！添加失败！";
            }
            else
            {
                comm.CommandText = "insert into tb_KechengJiegou(kechengid,jiegouname,shangwei,instruction,xuhao,zhishileixing) values(" + kechengid + ",'" + zhishiname + "'," + shangweiid + ",'" + instruction + "'," + zhishixuhao + ",'" + zhishileixing+ "')";
                comm.ExecuteNonQuery();
                conn.Close();
                zhishixuhao++;
                Txtzhishixuhao.Text = zhishixuhao.ToString();
                Lbl_fankui.Text = "添加知识点:" + zhishiname + ",成功！点击刷新按钮可看到新的结构。";
                if (!CheckBoxtishi.Checked)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('添加知识点:" + zhishiname + ",成功！点击刷新按钮可看到新的结构。');</script>", false);
                }

            }
            BindKechengTreeView();
        }
        catch (Exception e1)
        {
            Lbl_fankui.Text = e1.Message;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    protected void Btnshuaxintree_Click(object sender, EventArgs e)
    {
        //刷新课程结构树
        BindKechengTreeView();
    }
    protected void BindKechengTreeView()
    {
        TreeView1.Nodes.Clear();
        string kechengid = Session["kechengid"].ToString();
        TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
        TreeView1.kechengid = int.Parse(kechengid);
    }
    protected void DropDownListzhishileixing_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownListzhishileixing.SelectedValue = "5";
        }
    }
}
