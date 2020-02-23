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

public partial class play_tva : System.Web.UI.Page
{
    public string ziyuanfile;
    //protected override void Render(HtmlTextWriter writer)
    //{

    //    TextWriter tempWriter = new StringWriter();
    //    base.Render(new HtmlTextWriter(tempWriter));
    //    writer.Write(tempWriter.ToString().Replace("<body", "<body onload=setup();"));
    //} 
    protected void Page_Load(object sender, EventArgs e)
    {
         string sql = "select ziyuanfile from tb_jiaoxueziyuan where jiaoxueziyuanid=" + Request["jiaoxueziyuanid"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = sql;
        conn.Open();
        ziyuanfile = (comm.ExecuteScalar()).ToString();
        ziyuanfile = ziyuanfile.Replace("~", "..");
        conn.Close();
        //string link = "sunlupeng201106011039106093.flv";
        //if (!link.StartsWith("http://"))
        //{
        //    //获取当前的绝对路径
        //    string sss = Request.Url.AbsoluteUri;
        //    //查询play.aspx在字符串中的位置
        //    int idx = sss.IndexOf("play.aspx");
        //    //获取指定字符串
        //    sss = sss.Substring(0, idx);
        //    link = sss + link;
        //}
        //Literal1.Text = operateMethod.GetFlashText(link);
        //videoInfo();
    }
    //protected void videoInfo()
    //{
    //    string link="";
    //    string sql = "select * from tb_jiaoxueziyuan where jiaoxueziyuanid=" + Request["id"];
    //    SqlConnection conn=new SqlConnection();
    //    conn.ConnectionString=ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
    //    SqlCommand comm=conn.CreateCommand();
    //    comm.CommandText=sql;
    //    conn.Open();
    //    SqlDataReader sdr = comm.ExecuteReader();
    //    if (sdr.Read())
    //    {
    //        link = sdr["ziyuanfile"].ToString();
    //    }
    //    sdr.Close();
    //    link = link.Replace("~/", "");
    //    conn.Close();
    //    if (link != "")
    //    {
    //        if (!link.StartsWith("http://"))
    //        {
    //            //获取当前的绝对路径
    //            string sss = Request.Url.AbsoluteUri;
    //            //查询play.aspx在字符串中的位置
    //            int idx = sss.IndexOf("teacher/play.aspx");
    //            //获取指定字符串
    //            sss = sss.Substring(0, idx);
    //            link = sss + link;
    //        }
    //        Literal1.Text = operateMethod.GetFlashText(link);
    //    }
    //}
}
