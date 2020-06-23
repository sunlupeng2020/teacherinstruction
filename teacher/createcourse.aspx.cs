using System;
using System.Data;
using System.Data.OleDb;
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
using System.IO;

public partial class createactrualcourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void BtnCreateCourse_Click(object sender, EventArgs e)//创建课程
    {
        string kechengname = TxtKechengName.Text.Trim().Replace(" ", "").Replace("　","");
        string kechengshuoming =FCKeditor1.Value.Trim();

        SqlConnection myconn = new SqlConnection();
        myconn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand mycomm = myconn.CreateCommand();
        mycomm.CommandText = "select count(kechengid) from tb_kecheng where kechengname='" + kechengname + "'";
        myconn.Open();
        if (((int)mycomm.ExecuteScalar()) > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('课程名称已存在，请使用其他课程名称。');</script>", false);
            myconn.Close();
            return;
        }
        if (myconn.State == ConnectionState.Open)
            myconn.Close();
        int kechengid = 0;
        string filesavepath="~/images/nullbookimage148.gif";
        if (FileUpload1.HasFile)
        {
            if (FileUpload1.PostedFile.ContentLength > 1000000)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('图片太大，缩小图片后重新上传。');</script>", false);
                return;
            }
            else
            {
                string filename = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + DateTime.Now.Ticks.ToString();
                string extension = FileUpload1.FileName;
                extension = extension.Substring(extension.LastIndexOf("."));
                filename += extension;
                filesavepath = "~/images/" + filename;
                FileUpload1.SaveAs(Server.MapPath(filesavepath));
            }
        }
        Lbl_fankui.Text = "";
        string teacherusername = "";
        teacherusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        myconn.Open();
        SqlTransaction st = myconn.BeginTransaction();
        mycomm.Transaction = st;
        try
        {
            mycomm.CommandText = "insert into tb_Kecheng(kechengname,instruction,guanliyuan,creater,image) values('" + kechengname + "','" + kechengshuoming + "','" + teacherusername + "','" + teacherusername + "','" + filesavepath + "') select @@identity";
            kechengid =Convert.ToInt32(mycomm.ExecuteScalar());
            //把课程名称作为课程知识点写入知识点表
            mycomm.CommandText = "insert into tb_KechengJiegou(kechengid,jiegouname,instruction,shangwei) values (" + kechengid + ",'" + kechengname + "','" + kechengshuoming + "',0)";
            mycomm.ExecuteNonQuery();
            st.Commit();
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('课程 "+kechengname + "创建成功!');</script>", false);
        }
        catch (Exception e1)
        {
            st.Rollback();
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('课程创建失败!');</script>", false);
            Lbl_fankui.Text = e1.Message;
        }
        finally
        {
            if(myconn.State==ConnectionState.Open)
                myconn.Close();
        }

    }
}
