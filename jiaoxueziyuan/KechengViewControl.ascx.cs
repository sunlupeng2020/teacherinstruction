using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

public partial class jiaoxueziyuan_KechengViewControl : System.Web.UI.UserControl
{
    string xuhaobianpai = "①②③④⑤⑥⑦⑧⑨⑩⑾⑿⒀⒁⒂⒃⒄⒅⒆⒇";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string kechengid = Session["kechengid"].ToString();
            TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
            TreeView1.kechengid = int.Parse(kechengid);
        }
    }
    protected void TreeView1_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (TreeView1.Nodes.Count > 0)
            {
                string zhishidianid = TreeView1.Nodes[0].Value.Trim();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
                SqlCommand comm = conn.CreateCommand();
                conn.Open();
                comm.CommandText = "select instruction  from tb_kechengjiegou where kechengjiegouid=" + zhishidianid;
                string zhishidianjieshao = comm.ExecuteScalar().ToString();
                conn.Close();
                if (zhishidianjieshao.Length > 0)
                {
                    Labelzhishidianjieshao.Text = zhishidianjieshao;
                }
                else
                {
                    Labelzhishidianjieshao.Text = "暂无介绍。";
                }
            }
        }
        finally
        {
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            string zhishidianid = TreeView1.SelectedNode.Value;
            hdfslectednodeid.Value = zhishidianid;
            Labelzhishidianjieshao.Text = this.GetZhishidianJieshao(zhishidianid, cbxmode.Checked, "", 1).ToString();
        }
        catch
        {
        }
        finally
        {
        }
    }
    /// <summary>
    /// 显示该知识点的介绍
    /// </summary>
    /// <param name="zhishidianid">知识点ID</param>
    /// <param name="showall">是否显示下级知识点的全部介绍</param>
    /// <param name="cengci">层次数</param>
    /// <param name="shangcengbiaoji">上一层的标记符号</param>
    /// <returns></returns>
    protected StringBuilder GetZhishidianJieshao(string zhishidianid, bool showall,string shangcengbiaoji,int cengci)//是否显示该知识点及下级知识点的全部介绍
    {

        StringBuilder sb1=new StringBuilder();
        SqlConnection conn = new SqlConnection();
        string xuhao="";
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        comm.CommandText = "select jiegouname,instruction,xuhao from tb_kechengjiegou where kechengjiegouid=" + zhishidianid;
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            xuhao = sdr[2].ToString();
            switch (cengci)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    if (shangcengbiaoji == "")
                    {
                        sb1.Append("<h4>" + xuhao + " " + sdr[0].ToString() + "</h4>");
                    }
                    else
                    {
                        sb1.Append("<h4>" + shangcengbiaoji + "." + sdr[2].ToString() + " " + sdr[0].ToString() + "</h4>");
                    }
                    break;
                case 5:
                    sb1.Append("<h4>(" + xuhao + ")" + sdr[0].ToString() + "</h4>");
                    break;
                case 6:
                    sb1.Append("<h4>" + xuhao + ")" + sdr[0].ToString() + "</h4>");
                    break;
                case 7:
                    if (int.Parse(xuhao) < 21)
                        sb1.Append("<h4>" + xuhaobianpai[int.Parse(xuhao) - 1].ToString() + sdr[0].ToString() + "</h4>");
                    else
                        sb1.Append("<h4>[" + xuhao + "]" + sdr[0].ToString() + "</h4>");
                    break;
                default:
                    for (int i = 0; i < cengci - 7; i++)
                        sb1.Append("&nbsp;&nbsp;");
                    sb1.Append("<h4>" + sdr[0].ToString() + "</h4>");
                    break;
            }
            sb1.Append(sdr[1].ToString() + "<br/>");
        }
        sdr.Close();
        conn.Close();
        if (showall)//显示该知识点及下级知识点的全部介绍
        {
            List<string> childid=new List<string>();
            comm.CommandText = "select kechengjiegouid from tb_kechengjiegou where shangwei=" + zhishidianid + " order by xuhao";
            conn.Open();
            sdr = comm.ExecuteReader();
            while (sdr.Read())
            {
                childid.Add(sdr[0].ToString());
            }
            sdr.Close();
            conn.Close();
            foreach (string id in childid)
            {
                if (shangcengbiaoji == "")
                {
                    sb1.Append(this.GetZhishidianJieshao(id, showall, xuhao,cengci+1).ToString());
                }
                else
                {
                    sb1.Append(this.GetZhishidianJieshao(id, showall,shangcengbiaoji+"."+xuhao,cengci+1).ToString());
                }
            }
        }
        return sb1;
    }
}
