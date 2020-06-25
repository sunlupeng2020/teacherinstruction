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
using System.IO;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;

public partial class teachermanage_kecheng_update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string kechengid = Session["kechengid"].ToString();
        string tusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        if (!KechengInfo.IsTeacherManageKecheng(tusername, kechengid))
        {
            Button1.Enabled = false;
            Label1.Text = "您不是本课程的管理员，无权更新课程信息。";
        }
        else
        {
            Button1.Enabled = true;
        }
        if (!IsPostBack)
        {
            BindKechengInfo();
        }
    }
    //显示课程信息
    protected void BindKechengInfo()
    {
        string kechengid = Session["kechengid"].ToString();
        TextBoxkcname.Text = KechengInfo.getKechengname(kechengid);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select instruction,image,guanliyuan from tb_kecheng where kechengid=" + kechengid;
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            if (!sdr.IsDBNull(0))
            {
                FCKeditor1.Value = sdr.GetString(0);
                //TextBox1.Text = sdr.GetString(0);
            }
            if (!sdr.IsDBNull(1))
            {
                Image1.ImageUrl = sdr.GetString(1);
            }
            DropDownList2.SelectedValue = sdr.GetString(2);
        }
        sdr.Close();
        conn.Close();
    }
    //更新课程信息
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label2.Visible = false;
        bool imagesaved = false;
        string kechengid = Session["kechengid"].ToString();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //检查课程名称是否重复
        string kechengname = TextBoxkcname.Text.Trim().Replace(" ", "").Replace("　", "");
        comm.CommandText = "select count(kechengid) from tb_kecheng where kechengname='" + kechengname + "' and kechengid<>" + kechengid;
        conn.Open();
        if (((int)(comm.ExecuteScalar())) == 0)
        {
            string oldfile = Image1.ImageUrl;
            string filesavepath = Image1.ImageUrl;
            if (FileUpload1.HasFile)
            {
                string kuozhanming = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf(".") + 1);
                if (kuozhanming == "bmp" || kuozhanming == "jpg" || kuozhanming == "gif" || kuozhanming == "png")
                {
                    if (FileUpload1.PostedFile.ContentLength > 1048576)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('图片太大，未能保存，请选择小于1MB的图片！');</script>", false);
                    }
                    else
                    {
                        string filename = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + DateTime.Now.Ticks.ToString();
                        string fileextension = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf("."));
                        filesavepath = "~/images/" + filename + fileextension;
                        FileUpload1.PostedFile.SaveAs(Server.MapPath(filesavepath));
                        imagesaved = true;
                        if (oldfile != "~/images/NullBookImage148.gif")
                        {
                            try
                            {
                                File.Delete(Server.MapPath(oldfile));
                            }
                            catch
                            {
                            }
                        }
                    }    
                }
                else
                {
                    Label2.Visible = true;
                }
            }
            try
            {
                if (imagesaved)
                {
                    comm.CommandText = "update tb_kecheng set instruction='" + FCKeditor1.Value + "',image='" + filesavepath + "',guanliyuan='" + DropDownList2.SelectedValue + "', kechengname='" + kechengname + "' where kechengid='" + kechengid + "'";
                }
                else
                {
                    comm.CommandText = "update tb_kecheng set instruction='" + FCKeditor1.Value + "',guanliyuan='" + DropDownList2.SelectedValue + "', kechengname='" + kechengname + "' where kechengid='" + kechengid + "'";
                }
                comm.ExecuteNonQuery();
                //comm.CommandText = "update tb_kechengjiegou set instruction='" + TextBox1.Text + "',jiegouname='"+kechengname+"' where kechengid='" + kechengid + "' and shangwei=0";
                comm.CommandText = "update tb_kechengjiegou set instruction='" + FCKeditor1.Value + "',jiegouname='" + kechengname + "' where kechengid='" + kechengid + "' and shangwei=0";
                comm.ExecuteNonQuery();
                BindKechengInfo();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('课程信息更新成功！');</script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('课程信息更新失败！');</script>", false);
                Label1.Text = ex.Message;
            }
            finally
            {
                conn.Close();
                BindKechengInfo();
            }
        }
        if (conn.State == ConnectionState.Open)
            conn.Close();
    }
}
