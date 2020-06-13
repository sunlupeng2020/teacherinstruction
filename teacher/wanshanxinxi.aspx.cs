using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class teachermanage_wanshanxinxi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select xingming,xingbie,shenfenzheng,touxiang from tb_teacher where username='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "'";
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            TBxxm.Text = sdr.GetString(0);
            if (!sdr.IsDBNull(1))
            {
                RadioButtonList1.SelectedValue = sdr.GetString(1);
            }
            if (!sdr.IsDBNull(2))
            {
                TBxsfzh.Text = sdr.GetString(2);
            }
            if (!sdr.IsDBNull(3))
            {
                Imagetx01.ImageUrl = sdr.GetString(3);
            }
        }
        sdr.Close();
        conn.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm .CommandText= "update tb_teacher set xingming=@xingming,xingbie=@xingbie,shenfenzheng=@shenfenzheng where username=@username";
        comm.Parameters.AddWithValue("@xingming", TBxxm.Text);
        comm.Parameters.AddWithValue("@xingbie", RadioButtonList1.SelectedValue);
        comm.Parameters.AddWithValue("@shenfenzheng", TBxsfzh.Text);
        comm.Parameters.AddWithValue("@username", ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name);
        conn.Open();
        if (comm.ExecuteNonQuery() > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('个人信息更新成功！');</script>", false);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('个人信息更新失败！');</script>", false);
        }
        conn.Close();
    }
    protected void Buttontouxiang_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            if (FileUpload1.PostedFile.ContentLength > 512000)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('文件太大，请不要超过500KB！');</script>", false);
            }
            else
            {
                //检查文件格式
                string filext = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.') + 1).ToLower();
                if (filext == "bmp" || filext == "jpg" || filext == "gif")
                {
                    string filename = "t" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name.Trim() +"."+filext;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/images/touxiang") + "//" + filename);
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
                    SqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "update tb_teacher set touxiang='~/images/touxiang/" + filename + "' where username='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name + "'";
                    conn.Open();
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        Imagetx01.ImageUrl = "~//images//touxiang" + "//" + filename;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('头像更新成功！');</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('头像更新失败！');</script>", false);
                    }
                    conn.Close();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('头像格式不正确，请选择bmp、jpg或gif图像，更新失败！');</script>", false);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('请选择头像文件！');</script>", false);
        }
    }
}
