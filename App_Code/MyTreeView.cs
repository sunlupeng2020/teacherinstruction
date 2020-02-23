using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

/// <summary>
///MyTreeView 的摘要说明
/// </summary>
namespace MyControls
{
    public class MyTreeView : TreeView
    {
        public MyTreeView()
        {
            myds = new DataSet();
        }
        //
        //TODO: 在此处添加构造函数逻辑
        //
        private DataSet myds;
        private string cntstringname = "kecheng2012ConnectionString";
        private string connectionstring;
        private string checkednodeids="";
        public string ConnectionStringName
        {
            get
            {
                return this.cntstringname;
            }
            set
            {
                this.cntstringname = value;
            }
        }
        public string ConnectionString
        {
            set
            {
                this.connectionstring = value;
            }
        }
        public string CheckedNodesText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (TreeNode node in this.CheckedNodes)
                {
                    sb.Append(node.Text.Trim() + ",");
                }
                return sb.ToString();
            }
        }
        public int kechengid
        {
            set
            {
                
                CreateKechengTreeView(value);
            }
        }
        public string CheckedNodesIds//得到选中的结点的id号
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (TreeNode node in this.CheckedNodes)
                {
                    sb.Append(node.Value + ",");
                }
                return sb.ToString();
            }
            set//设置结点的选中状态
            {
                if (value != "" && value != string.Empty)
                {
                    this.checkednodeids = value;
                    List<string> list = new List<string>();
                    string[] abc = value.Split(',');
                    foreach (string s in abc)
                    {
                        if (s.Length > 0)
                            list.Add(s);
                    }
                    TreeNode node = this.Nodes[0];
                    SetNodeChecked(node, list);
                }
            }
        }
        protected void SetNodeChecked(TreeNode node, List<string> checkednodes)//设置结点的选中状态
        {
            if (checkednodes.Contains(node.Value))
                node.Checked = true;
            foreach (TreeNode cnode in node.ChildNodes)//设置子结点的选中状态
            {
                SetNodeChecked(cnode, checkednodes);
            }
        }
        /// <summary>
        /// 获取选中结点及其子孙结点的id号,根据选中知识点查询资源、题目、问题时使用
        /// </summary>
        public List<string> CheckedNodesAndChildrenIds//获取选中结点及其子孙结点的id号,根据选中知识点查询资源、题目、问题时使用
        {
            get
            {
                List<string> ln = new List<string>();
                foreach (TreeNode tn in this.CheckedNodes)
                {
                    if (!ln.Contains(tn.Value))//防止重复
                        ln.Add(tn.Value);
                    AddAllChildrenToList(tn, ln);
                }
                return ln;
            }
        }
        protected void AddAllChildrenToList(TreeNode node, List<string> list)//对已选中的结点，无条件添加子孙结点到列表
        {
            foreach (TreeNode cnode in node.ChildNodes)
            {
                if (!list.Contains(cnode.Value))
                    list.Add(cnode.Value);
                AddAllChildrenToList(cnode, list);
            }
        }
        /// <summary>
        /// 获取选中的知识点值，不包括子知识点，即如果某知识点及其子孙知识点同时被选中的，只登记该知识点
        /// </summary>
        public List<string> CheckedNodesExceptChildren
        {
            get
            {
                List<string> list = new List<string>();
                foreach (TreeNode tn in this.CheckedNodes)
                {
                    if (ZuxianNode(tn) == null)//如果祖先结点中没有被选中的，则添加
                    {
                        list.Add(tn.Value);
                    }
                }
                return list;
            }
        }
        protected void AddChildrenToList(TreeNode node, List<string> list)
        {
            if (node.Checked)
            {
                if (!list.Contains(node.Value))
                    list.Add(node.Value);
                AddAllChildrenToList(node, list);
            }
        }


        protected void CreateKechengTreeView(int kechengid)//创建课程知识结构树
        {
            myds = new DataSet();
            string sqltxt = "select jiegouname,kechengjiegouid,xuhao,shangwei from tb_KechengJiegou where kechengid=" + kechengid + " order by xuhao";
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = sqltxt;
            SqlDataAdapter sda = new SqlDataAdapter(comm);
            sda.Fill(myds);
            DataRow[] mydr = myds.Tables[0].Select("shangwei=0");
            TreeNode mytreenode = new TreeNode();
            mytreenode.Text = mydr[0][0].ToString();
            mytreenode.Value = mydr[0][1].ToString();
            this.Nodes.Add(mytreenode);
            CreateChildNodes(mytreenode, myds);
        }

        private void CreateChildNodes(TreeNode mytreenode, DataSet myds)//生成某知识点的下位知识结构
        {
            DataRow[] mydr = myds.Tables[0].Select("shangwei=" + mytreenode.Value, "xuhao asc");
            TreeNode mynode;
            foreach (DataRow row in mydr)
            {
                mynode = new TreeNode();
                mynode.Text = row[0].ToString();
                mynode.Value = row[1].ToString();
                mytreenode.ChildNodes.Add(mynode);
                CreateChildNodes(mynode, myds);
            }
        }
        protected TreeNode ZuxianNode(TreeNode mytreenode)//查找祖先结点中哪一个被选择，添加题目、资源、问题,登记知识点时使用
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


    }
}
