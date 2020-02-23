using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;

public partial class studentstudy_chakanzuoyememo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["zuoyeid"].Trim().Length > 0)
        {
            string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            string zuoyeid = Request.QueryString["zuoyeid"];
            string yunxuchakan = "";
            DateTime qixian = DateTime.Now;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select yunxuchakan,shangjiaoqixian from tb_studentzuoye where zuoyeid=" + zuoyeid + " and studentusername='" + username + "'";
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                yunxuchakan = sdr.GetString(0).Trim();
                qixian = sdr.GetDateTime(1);
            }
            sdr.Close();
            conn.Close();
            if (yunxuchakan == "允许" && qixian.CompareTo(DateTime.Now) <= 0)
            {
                SetStudentZuoyePaiming();
                xianshiTimu();
            }
            else
            {
                Response.Redirect("zuoye.aspx");
            }
            BindStuZuoyeinfo();
        }
    }
    protected void SetStudentZuoyePaiming()//班级人数和学生作业成绩排名
    {
        string zuoyeid = Request.QueryString["zuoyeid"];
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText="select count(studentusername) from tb_studentzuoye where  zuoyeid="+zuoyeid;
        conn.Open();
        Labelrenshu.Text = comm.ExecuteScalar().ToString();
        comm.CommandText = "select count(studentusername) from tb_studentzuoye where zuoyeid=" + zuoyeid + " and zongfen>(select zongfen from tb_studentzuoye where studentusername='" + stuusername + "' and zuoyeid=" + zuoyeid + ")";
        Labelpm.Text = ((int)comm.ExecuteScalar() + 1).ToString();
        conn.Close();
    }
    protected void BindStuZuoyeinfo()
    {
        string zuoyeid = Request.QueryString["zuoyeid"];
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable dt = ZuoyeInfo.GetStuZuoyeInfo(stuusername,int.Parse(zuoyeid));
        FormView1.DataSource = dt;
        FormView1.DataBind();
    }
    protected void xianshiTimu()//在PlaceHolder1中显示题目
    {
        int zuoyeid = int.Parse(Request.QueryString["zuoyeid"]);
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable timutable =StudentInfo.GetStuZuoyeTimu(zuoyeid, stuusername);
        Literal tihaoliteral;//显示题号
        Literal templiteral;
        if (timutable.Rows.Count > 0)
        {
            int tihao = 1;
            foreach (DataRow timurow in timutable.Rows)
            {
                tihaoliteral = new Literal();
                tihaoliteral.Text ="<br/>"+tihao.ToString()  + "、(" +timurow[2].ToString() + ",分值:"+timurow[7]+"分，您得" + timurow[8].ToString() + "分)" + timurow[3].ToString() + "<br/>";
                PlaceHolder1.Controls.Add(tihaoliteral);
                switch (timurow[2].ToString().Trim())
                {
                    case "单项选择题":
                    case "判断题":
                    case "多项选择题":
                    case "填空题":
                        templiteral = new Literal();
                        templiteral.Text = "本题的参考答案是:<font color='red'><b>"+timurow[4].ToString()+"</b></font>；<br/>您的回答是:<font color='blue'><b>"+timurow[5].ToString()+"</b></font>。<br/>";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                    case "操作题":
                        if (timurow[9].ToString().Length > 0)
                        {
                            HyperLink ziyuanfile = new HyperLink();
                            ziyuanfile.ID = "ziyuanfile" + timurow[0].ToString().Trim();
                            ziyuanfile.Text = "下载本题资源文件";
                            ziyuanfile.NavigateUrl = timurow[9].ToString().Trim();
                            PlaceHolder1.Controls.Add(ziyuanfile);
                        }
                        //显示文件上传控件
                        if (timurow[6].ToString().Length > 0)
                        {
                            HyperLink benrenzuoyefile = new HyperLink();
                            benrenzuoyefile.ID = "zuoyefile" + timurow[0].ToString().Trim();
                            benrenzuoyefile.Text = "下载我的作业文件";
                            benrenzuoyefile.NavigateUrl = timurow[6].ToString().Trim();
                            benrenzuoyefile.Target = "_blank";
                            PlaceHolder1.Controls.Add(benrenzuoyefile);
                        }
                        else
                        {
                            HyperLink benrenzuoyefile = new HyperLink();
                            benrenzuoyefile.Text = "<font color='red'>没有找到您的作业文件。</font>";
                            PlaceHolder1.Controls.Add(benrenzuoyefile);
                        }
                        break;
                    default:
                        templiteral = new Literal();
                        templiteral.Text = "本题的参考答案是:<font color='red'><b>" + timurow[4].ToString() + "</b></font>；<br/>您的回答是:<font color='blue'>" + timurow[5].ToString() + "</font>。";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                }
                tihao++;
            }
        }
    }
}
