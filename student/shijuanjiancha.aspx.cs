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

public partial class student_shijuanjiancha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        lbl_xuehao.Text = username;
        lbl_shijuanname.Text =CeshiInfo.GetCeshiName(shijuanid);
        lbl_xingming.Text = StudentInfo.GetStuXingming(username);
        Jianchashijuan(username, shijuanid);

    }
    protected void Jianchashijuan(string username,int shijuanid)
    {
        int timuzs,wancheng;
        timuzs=wancheng=0;
        StringBuilder tmxx = new StringBuilder(); 
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select tb_tiku.type,tb_studentkaoshiti.answer,tb_studentkaoshiti.timuhao,tb_studentkaoshiti.filepath from tb_studentkaoshiti inner join tb_tiku on tb_studentkaoshiti.questionid=tb_tiku.questionid  where tb_studentkaoshiti.studentusername='" + username + "' and tb_studentkaoshiti.shijuanid=" + shijuanid + "  order by tb_studentkaoshiti.timuhao asc";//按题号排序
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        while (sdr.Read())
        {
            timuzs++;
            //显示题目、分值等
            tmxx.Append("第" + sdr[2].ToString() + "题，题型：" + sdr[0].ToString());
            if (sdr.GetString(0).Trim() == "操作题")
            {
                if (!sdr.IsDBNull(3) && sdr.GetString(3).Trim().Length > 0)
                {
                    tmxx.Append(",已回答,提交了文件。<br/>");
                    wancheng++;
                }
                else
                {
                    tmxx.Append(",<font color='red'>未回答，需要提交文件</font>。<br/>");
                }
            }
            else
            {
                if (!sdr.IsDBNull(1) && sdr[1].ToString().Trim() != "")
                {
                    tmxx.Append(",已回答。<br/>");
                    wancheng++;
                }
                else
                {
                    tmxx.Append(",<font color='red'>未回答</font>。<br/>");
                }
            }
        }
        sdr.Close();
        conn.Close();
        tmxx.Append("<br/><font color='red'>共有 "+timuzs.ToString()+"道题，已完成 "+wancheng.ToString()+"道，还有 "+(timuzs-wancheng).ToString()+"道未完成。</font><br/>");
        Label1.Text = tmxx.ToString();
    }
}
