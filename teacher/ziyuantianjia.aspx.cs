using System;
using System.IO;
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
using System.Collections.Generic;

public partial class jiaoxueziyuan_ziyuantianjia : System.Web.UI.Page
{
    string[] videoExtension = new string[] { "flv", "avi", "wmv"};//可接受的视频格式
    string[] flashExtension = new string[] { "swf","gif" };
    string[] audioExtension = new string[] { "mp3"};
    string[] wordExtension = new string[] { "doc", "rtf", "docx","txt","pdf","xml" };
    string[] rarExtension = new string[] { "rar", "zip" };
    string[] imageExtension = new string[] { "bmp", "jpg", "gif", "png", "psd", "tif" };
    string[] pptExtension = new string[] { "ppt","pptx" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindKechengTreeView();
            TreeView1.Attributes.Add("onclick", "client_OnTreeNodeChecked()");
            
        }
        TextBoxxiangguanzhishiid.Text = TreeView1.CheckedNodes.Count.ToString();
    }
    protected void BindKechengTreeView()
    {
        string kechengid = Session["kechengid"].ToString();
        TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
        TreeView1.kechengid = int.Parse(kechengid);
    }
    protected void Buttonaddziyuan_Click(object sender, EventArgs e)//添加资源
    {
        //获取相关数据
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = Session["kechengid"].ToString();
        string ziyuanname = TextBoxziyuanname.Text;//资源名
        string guanjianzi = TextBoxguanjianzi.Text;//关键字
        string instruction = TextBoxinstruction.Text;//介绍
        string ziyuanleixing = DropDownListziyuanleixing.Text;//资源类型
        string meitileixing = DropDownListmeitileixing.Text;//媒体类型
        string quanxian = RadioButtonList1.SelectedValue;//开放程度
        List<string> xianguanzhishidianid =TreeView1.CheckedNodesExceptChildren;//相关知识点id
         //判断文件大小
        bool fileSaved = false;//文件是否保存成功
        bool fileCanSave = true;//文件是否能够保存
        int filesize = 0;
        string upFileName="";
        string upExtension="";
        string saveName = "";
        string serverpath;
        if (FileUpload1.HasFile)
        {
            filesize = FileUpload1.PostedFile.ContentLength;
            //hasFile = true;
            upFileName = FileUpload1.FileName;
            upExtension = upFileName.Substring(upFileName.LastIndexOf(".") + 1);
            if (FileUpload1.PostedFile.ContentLength > 4194304)
            {
                ScriptManager.RegisterClientScriptBlock(this,typeof(string),"","<script>alert('文件太大，不能超过4MB！');</script>",false);
                fileCanSave = false;
            }
            string allowedextention;
            if (!CheckFileExtention(meitileixing, upExtension, out allowedextention))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "",  "<script>alert('文件格式错误！"+allowedextention+"');</script>", false);
                fileCanSave=false;
            }
        }
        string playFile = "";
        if (fileCanSave)
        {
            switch (meitileixing)
            {
                case "视频"://将上传的视频文件转换为flv格式并保存
                    string upFilePath = Server.MapPath("../upfile/") + upFileName;
                    //将文件保存到指定路径
                    //转换后的文件名
                    saveName = "zy" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    //转换后保存的路径和文件名
                    serverpath = Server.MapPath("~/ziyuanfile/kecheng") + kechengid;
                    CreateMulu(serverpath);//创建文件夹
                    playFile = "~/ziyuanfile/kecheng" + kechengid + "/" + saveName + ".flv";
                    try
                    {
                        if (upExtension == "flv")
                        {
                            //filesize = FileUpload1.PostedFile.ContentLength;
                            FileUpload1.SaveAs(Server.MapPath(playFile));
                            fileSaved = true;

                        }
                        else
                        {
                            FileUpload1.SaveAs(upFilePath);
                            //调用公共类中的changeVedioType方法转换视频格式
                            if (operateMethod.changeVideoType(upFileName, Server.MapPath(playFile)))
                            {
                                File.Delete(upFilePath);
                                fileSaved = true;
                            }
                            else
                            {
                                Lbl_fankui.Text = "文件转换发生错误，上传失败！";
                                File.Delete(upFilePath);
                                fileSaved = false;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Lbl_fankui.Text = ex.Message;
                        fileSaved = false;
                    }
                    break;

                default:
                    saveName = "zy"+ DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    serverpath = Server.MapPath("~/ziyuanfile/kecheng") + kechengid;
                    CreateMulu(serverpath);//创建文件夹
                    playFile = "~/ziyuanfile/kecheng" + kechengid + "/" + saveName + "." + upExtension;
                    //将文件保存到指定路径
                    FileUpload1.SaveAs(Server.MapPath(playFile));
                    fileSaved = true;
                    break;
            }
        }
        if (fileSaved)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand comm = conn.CreateCommand();
            SqlTransaction st = conn.BeginTransaction();
            comm.Transaction = st;
            try
            {
                //插入资源，得到id号
                string datenow = DateTime.Now.ToString();
                comm.CommandText = "insert into tb_Jiaoxueziyuan(jiaoxueziyuanname,kechengid,username,jiaoxueziyuanleixing,ziyuanmeitileixing,ziyuanfile,instruction,quanxian,guanjianzi,createtime,filesize) values('" + ziyuanname + "'," + kechengid + ",'" + username + "','" + ziyuanleixing + "','" + meitileixing + "','" + playFile + "','" + instruction + "','" + quanxian + "','" + guanjianzi + "','"+datenow+"',"+filesize+") select @@identity as ziyuanid";
                //comm.ExecuteNonQuery();
                //comm.CommandText = "select jiaoxueziyuanid from tb_jiaoxueziyuan where jiaoxueziyuanname='" + ziyuanname + "' and username='" + username + "' and createtime='"+datenow+"'";
                int jiaoxueziyuanid = Convert.ToInt32(comm.ExecuteScalar());
                foreach(string zhishidianid in xianguanzhishidianid)
                {
                    comm.CommandText = "insert into tb_ziyuanzhishidian(jiaoxueziyuanid,zhishidianid) values(" + jiaoxueziyuanid + "," + int.Parse(zhishidianid) + ")";
                    comm.ExecuteNonQuery();
                }
                st.Commit();
                //如果写成事务，更好
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('资源上传成功，非常感谢您的支持！');</script>", false);
            }
            catch (Exception e1)
            {
                st.Rollback();
                Lbl_fankui.Text ="资源上传失败，原因："+ e1.Message;
            }
            finally
            {
                if (conn.State.ToString() == "Opened")
                    conn.Close();
            }
        }
    }
    protected void CreateMulu(string filepath)
    {
        DirectoryInfo di = new DirectoryInfo(filepath);
        if (di.Exists)
        {
            return;
        }
        else
        {
            di.Create();
        }
    }
    protected void TreeViewsource_DataBound(object sender, EventArgs e)
    {
        foreach (TreeNode node in TreeView1.Nodes)
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

    protected bool CheckFileExtention(string meitileixing, string extention,out string isvalidextention)
    {
        bool strReturn = false;
        isvalidextention = "";
        //遍历数组判断当前上传的文件格式是否正确
        switch (meitileixing)
        {
            case "视频"://将上传的视频文件转换为flv格式并保存
                isvalidextention = "视频文件允许的类型：";
                foreach (string s in videoExtension)
                    isvalidextention += s + " ";
                strReturn= CheckMeitiExtention(videoExtension, extention);
                break;
            case "音频":
                isvalidextention = "音频文件允许的类型：";
                foreach (string s in audioExtension)
                    isvalidextention += s + " ";
                strReturn = CheckMeitiExtention(audioExtension, extention);
                break;

            case "flash动画":
                isvalidextention = meitileixing+"允许的类型：";
                foreach (string s in flashExtension)
                    isvalidextention += s + " ";
                strReturn = CheckMeitiExtention(flashExtension, extention);
                break;

            case "文本文档":
                isvalidextention = meitileixing + "允许的类型：";
                foreach (string s in wordExtension)
                    isvalidextention += s + " ";
                strReturn = CheckMeitiExtention(wordExtension, extention);
                break;

            case "图片":
                isvalidextention = meitileixing + "允许的类型：";
                foreach (string s in imageExtension)
                    isvalidextention += s + " ";
                strReturn = CheckMeitiExtention(imageExtension, extention);
                break;

            case "压缩文件":
                isvalidextention = meitileixing + "允许的类型：";
                foreach (string s in rarExtension)
                    isvalidextention += s + " ";
                strReturn = CheckMeitiExtention(rarExtension, extention);
                break;
            case "演示文稿":
                isvalidextention = meitileixing + "允许的类型：";
                foreach (string s in pptExtension)
                    isvalidextention += s + " ";
                strReturn = CheckMeitiExtention(pptExtension, extention);
                break;
        }

        return strReturn;
    }
    protected bool CheckMeitiExtention(string[] validExtention,string extention)
    {
        bool strReturn = false;
        //遍历数组判断当前上传的文件格式是否正确
        foreach (string var in validExtention)
        {
            if (var == extention)
            {
                strReturn = true; break;
            }
        }
        return strReturn;
    }
}
