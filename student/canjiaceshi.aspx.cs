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
using System.Threading;
using System.Timers;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
public partial class studentstudy_canjiaceshi : System.Web.UI.Page
{
    /*测试的几种状态：
     * 1、保存过答案，但由于某种原因又重新进行考试；
     * 2、第一次进行考试，答案全是空的
     * 3、刚刚保存了答案；保持题目原来的答案即可
     * 4、已交卷。不再显示题目及答案。
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid=0;

        if (Request.QueryString["shijuanid"] != null)
        {
            shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        }
        else
        {
            Response.Redirect("jiaoshiceshi.aspx");
        }
        if (CeshiInfo.IsIpXianzhi(shijuanid))//如果该考试限制IP地址
        {
            SetStukaoshiIp(username, shijuanid);
            switch (CheckIp(username, shijuanid))
            {
                case 1://学生考试机的IP与数据库中登记的IP不一致，需要到原来考试的机器上进行考试
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('考试过程中不允许换机器！请到您初次进行考试的电脑进行考试，或与教师联系。');", true);
                    SetTimerStop();
                    return;
                case 2:
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('检测到您所在的电脑已有同学进行本次考试！请换台电脑进行考试。');", true);
                    SetTimerStop();
                    return;
            }
        }
        if (!IsPostBack)
        {
            XianshiTimu(int.Parse(lbl_tihao.Text));
            GetCeshiJibenInfo();
            GetStudentCeshiInfo(username, shijuanid);
        }
    }
    //读取开始时间、交卷时间，是否已交卷、是否允许做题、延长时间、学生试卷在数据库中的id号stks_id等
    protected void GetStudentCeshiInfo(string username, int shijuanid)
    {
        int timeyanchang = 0, ceshishichang = 0, shengyumiaoshu;
        string zhuangtai = "", jiaojuanshijian = "", kaishishijian, yunxuzuoti = "", yunxuceshi = "";
        kaishishijian = DateTime.Now.ToString();
        SqlDataReader reader;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select tb_studentkaoshi.kaishi_shijian,tb_studentkaoshi.jiaojuanshijian,tb_studentkaoshi.jiaojuan,tb_studentkaoshi.timeyanchang,tb_studentkaoshi.stks_id,tb_teachershijuan.yunxuzuoti,tb_studentkaoshi.yunxu,tb_teachershijuan.timelength from tb_studentkaoshi,tb_teachershijuan where tb_studentkaoshi.shijuanid=" + shijuanid + " and tb_studentkaoshi.studentusername='" + username + "' and tb_teachershijuan.shijuanid=" + shijuanid;
        conn.Open();
        reader = comm.ExecuteReader();
        if (reader.Read())
        {
            zhuangtai = reader.GetString(2);
            if (!reader.IsDBNull(1))
            {
                jiaojuanshijian = reader.GetDateTime(1).ToString();
            }
            if (!reader.IsDBNull(0))
            {
                kaishishijian = reader.GetDateTime(0).ToString();
            }
            timeyanchang = reader.GetInt32(3);
            yunxuzuoti = reader.GetString(5);
            yunxuceshi = reader.GetString(6);
            ceshishichang = reader.GetInt32(7);
        }
        reader.Close();
        conn.Close();
        Labelyanhang.Text = timeyanchang.ToString();
        Labelkaishishijian.Text = kaishishijian;
        Labelzhuangtai.Text = zhuangtai;
        Labeljiaojuanshijian.Text = jiaojuanshijian;
        if (yunxuzuoti == "禁止")//如果教师已禁止该测试
        {
            Label_fankui.Text = "教师已禁止进行该测试,请与教师联系！";
            SetTimerStop();
        }
        else//如果教师允许测试
        {
            if (zhuangtai == "已交卷")
            {
                Label_fankui.Text = "你已于" + jiaojuanshijian + "交卷。待教师允许查看测试详情时，可查看测试结果。";
                SetTimerStop();
            }
            else//如果未交卷
            {
                if (yunxuceshi == "禁止")//教师如果已禁目该学生进行该测试
                {
                    Label_fankui.Text = "教师已禁止你进行测试，请与教师联系！";
                    SetTimerStop();
                }
                else//如果教师允许该学生进行测试
                {
                    //计算测试时间长度（分钟）
                    if (zhuangtai == "未开始" || zhuangtai.Trim().Length == 0)
                    {
                        SetKaishi(username, shijuanid);
                        Labelkaishishijian.Text = DateTime.Now.ToString();
                        kaishishijian = DateTime.Now.ToString();
                        Labelzhuangtai.Text = "未交卷";
                    }
                    int kaoshifenzhong = ceshishichang + timeyanchang + 1;//多给1分钟，以弥补时间误差
                    shengyumiaoshu = kaoshifenzhong * 60 - (int)((TimeSpan)(DateTime.Now - Convert.ToDateTime(kaishishijian))).TotalSeconds;
                    //显示剩余时间
                    if (shengyumiaoshu <= 0)
                    {
                        SetJiaojuan(username,shijuanid.ToString());
                        Label_fankui.Text = "您的本次测试已超时。待教师允许查看测试详情时，可查看测试结果。";
                        Gaijuan(username,shijuanid.ToString());
                        SetTimerStop();
                        Label1.Text = "0";
                    }
                    else
                    {
                        if (shengyumiaoshu < 180)
                        {
                            Label1.Text = "0";
                            Timerdjszdbc.Enabled = false;
                            Timerzdjiao.Enabled = true;
                            Timerzdjiao.Interval = shengyumiaoshu * 1000;
                        }
                        else
                        {
                            Label1.Text = (shengyumiaoshu / 60).ToString();
                            Timerdjszdbc.Enabled = true;
                            Timerzdjiao.Enabled = false;
                        }
                    }
                }
            }
        }
    }
    //读取测试名称、测试时间长度等
    protected void GetCeshiJibenInfo()// 设置课程名,测试名，姓名，学号
    {
        int timushuliang = 0;
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        Labelusername.Text = username;
        Literalxingming.Text = StudentInfo.GetStuXingming(username);
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString))
        {
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select tb_Teachershijuan.ceshiname,tb_kecheng.kechengname,tb_student.xingming,tb_banji.banjiname,tb_studentkaoshi.kechengid,tb_Teachershijuan.timelength from tb_studentkaoshi inner join tb_Teachershijuan on tb_studentkaoshi.shijuanid=tb_Teachershijuan.shijuanid inner join tb_kecheng on tb_kecheng.kechengid=tb_studentkaoshi.kechengid inner join tb_student on tb_student.username=tb_studentkaoshi.studentusername inner join tb_banji on tb_banji.banjiid=tb_studentkaoshi.banjiid where tb_studentkaoshi.shijuanid=" + shijuanid + " and tb_studentkaoshi.studentusername='" + username + "'";
            SqlDataReader reader;
            conn.Open();
            reader = comm.ExecuteReader();
            if (reader.Read())
            {
                Labelkecheng.Text = reader.GetString(1).Trim();//课程名称
                Literal1.Text = reader.GetString(0);//测试名称
                Labelxingming.Text = reader.GetString(2);//姓名
                Labelbanji.Text = reader.GetString(3);//班级
                HFkechengid.Value = reader.GetInt32(4).ToString();//课程ID
                Labelshichang.Text = reader.GetInt32(5).ToString();//时间长度（分钟）
            }
            reader.Close();
            comm.CommandText = "select count(*) from tb_studentkaoshiti where studentusername='" + username + "' and shijuanid=" + shijuanid;
            timushuliang =(int) comm.ExecuteScalar();
            lbl_timushuliang.Text =timushuliang.ToString();
        }
        if (ddl_tihaoliebiao.Items.Count > 0)
            ddl_tihaoliebiao.Items.Clear();
        for (int i = 1; i <= timushuliang; i++)
        {
            ddl_tihaoliebiao.Items.Add(i.ToString());
        }
    }
    //显示测试信息
    //设置开始时间为现在的时间

    protected void SetKaishi(string username, int shijuanid)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "update tb_studentkaoshi set kaishi_shijian='" + DateTime.Now.ToString() + "',jiaojuan='未交卷' where shijuanid=" + shijuanid + " and studentusername='" + username + "'";
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
    }
    
    //停止倒计时、定时保存、定时交卷功能,隐藏倒计时面板
    protected void SetTimerStop()
    {
        Timerzdjiao.Enabled = false;
        Timerdjszdbc.Enabled = false;
        Buttonjiaojuan.Enabled = false;
        btn_nexttimu.Enabled = false;
        btn_pretimu.Enabled = false;
        Panel_danxuan.Visible = false;//单选题
        Panel_tiankong.Visible = false;//填空题
        Panelcaozuo.Visible = false;//操作题
        Paneljianda.Visible = false;//简答题
        Panelpanduan.Visible = false;//判断题
        Panelduoxuan.Visible = false;//多选题       
        Panel_TimuInfo.Visible = false;
        Label_fankui.Visible = true;
        btn_timutiaozhuan.Enabled = false;
        btn_jianchashijuan.Enabled = false;
    }
    protected void XianshiTimu(int tihao)//显示题目
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select tb_tiku.timu,tb_tiku.type,tb_studentkaoshiti.answer,tb_studentkaoshiti.timuhao,tb_studentkaoshiti.fenzhi,tb_studentkaoshiti.shitiid,tb_tiku.filepath,tb_studentkaoshiti.filepath from tb_studentkaoshiti inner join tb_tiku on tb_studentkaoshiti.questionid=tb_tiku.questionid  where tb_studentkaoshiti.studentusername='" + username + "' and tb_studentkaoshiti.shijuanid=" + shijuanid + "  and tb_studentkaoshiti.timuhao=" + tihao;//按题号排序
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            int shitiid = sdr.GetInt32(5);
            //显示题目、分值等
            //lbl_tihao.Text = tihao.ToString();
            lbl_tixing.Text = sdr.GetString(1).ToString();
            lbl_fenzhi.Text =sdr.GetInt32(4).ToString() + "分";
            lbl_timu.Text = sdr.GetString(0).Trim().Replace("<p>", "").Replace("</p>", "");
            switch (sdr.GetString(1).Trim())
            {
                case "单项选择题":
                    Panel_danxuan.Visible = true;
                    Panel_tiankong.Visible = false;
                    Panelcaozuo.Visible = false;
                    Paneljianda.Visible = false;
                    Panelpanduan.Visible = false;
                    Panelduoxuan.Visible = false;
                    if (!sdr.IsDBNull(2))
                    {
                        if (sdr.GetString(2).Trim().Length > 0)
                            rbl_xuanzedaan.SelectedValue = sdr.GetString(2).Trim();
                        else
                            rbl_xuanzedaan.ClearSelection();
                    }
                    else
                    {
                        rbl_xuanzedaan.ClearSelection();
                    }

                    break;
                case "判断题":
                    Panel_danxuan.Visible = false;
                    Panel_tiankong.Visible = false;
                    Panelcaozuo.Visible = false;
                    Paneljianda.Visible = false;
                    Panelpanduan.Visible = true;
                    Panelduoxuan.Visible = false;
                    if (!sdr.IsDBNull(2))
                    {
                        if (sdr.GetString(2).Trim().Length > 0)
                        {
                            rbl_panduandaan.SelectedValue = sdr.GetString(2).Trim();
                        }
                        else
                        {
                            rbl_panduandaan.ClearSelection();
                        }
                    }
                    else
                    {
                        rbl_panduandaan.ClearSelection();
                    }
                    break;
                case "多项选择题":
                    Panel_danxuan.Visible = false;
                    Panel_tiankong.Visible = false;
                    Panelcaozuo.Visible = false;
                    Paneljianda.Visible = false;
                    Panelpanduan.Visible = false;
                    Panelduoxuan.Visible = true;
                    if (!sdr.IsDBNull(2))
                    {
                        if (sdr.GetString(2).Trim().Length > 0)
                        {
                            for (int j = 0; j < 5; j++)
                                if (sdr.GetString(2).Trim().IndexOf(cbx_duoxuandaan.Items[j].Value) >= 0)
                                    cbx_duoxuandaan.Items[j].Selected = true;
                        }
                        else
                        {
                            cbx_duoxuandaan.ClearSelection();
                        }
                    }
                    else
                    {
                        cbx_duoxuandaan.ClearSelection();
                    }

                    break;
                case "填空题":
                    Panel_danxuan.Visible = false;
                    Panel_tiankong.Visible = true;
                    Panelcaozuo.Visible = false;
                    Paneljianda.Visible = false;
                    Panelpanduan.Visible = false;
                    Panelduoxuan.Visible = false;
                    if (!sdr.IsDBNull(2))
                    {
                        if (sdr.GetString(2).Trim().Length > 0)
                        {
                            tbx_tiankongdaan.Text = sdr.GetString(2).Trim();
                        }
                        else
                        {
                            tbx_tiankongdaan.Text = "";
                        }
                    }
                    else
                    {
                        tbx_tiankongdaan.Text = "";
                    }
                    break;
                case "操作题":
                    Panel_danxuan.Visible = false;
                    Panel_tiankong.Visible = false;
                    Panelcaozuo.Visible = true;
                    Paneljianda.Visible = false;
                    Panelpanduan.Visible = false;
                    Panelduoxuan.Visible = false;
                    if (!sdr.IsDBNull(6))
                    {
                        if (sdr.GetString(6).Trim().Length > 0)
                        {
                            HyperLink1.NavigateUrl = sdr.GetString(6).Trim();
                            HyperLink1.Visible = true;
                        }
                        else
                        {
                            HyperLink1.Visible = false;
                        }
                    }
                    else
                    {
                        HyperLink1.Visible = false;
                    }
                    if (!sdr.IsDBNull(7))
                    {
                        if (sdr.GetString(7).Trim().Length > 0)
                        {
                            Hylk_yhwenjian.NavigateUrl = sdr.GetString(7).Trim();
                            Hylk_yhwenjian.Visible = true;
                        }
                        else
                        {
                            Hylk_yhwenjian.Visible = false;
                        }
                    }
                    else
                    {
                        Hylk_yhwenjian.Visible = false;
                    }
                    break;
                default:
                    Panel_danxuan.Visible = false;
                    Panel_tiankong.Visible = false;
                    Panelcaozuo.Visible = false;
                    Paneljianda.Visible = true;
                    Panelpanduan.Visible = false;
                    Panelduoxuan.Visible = false;
                    if (!sdr.IsDBNull(2))
                    {
                        if (sdr.GetString(2).Trim().Length > 0)
                        {
                            tbx_jiandadaan.Text = sdr.GetString(2).Trim();
                        }
                    }
                    else
                    {
                        tbx_jiandadaan.Text = "";
                    }
                    break;
            }
        }
        sdr.Close();
        conn.Close();
    }
    protected void btn_UPFile_Click(object sender, EventArgs e)//上传文件
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengid = HFkechengid.Value;
        string shijuanid=Request.QueryString["shijuanid"];
        int tihao = int.Parse(lbl_tihao.Text);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        string feifakuozhanming = "asp,aspx,php,jsp,js,exe,com,pif";
        if (fileupload1.HasFile)
        {
            string kuozhanming = fileupload1.FileName.Substring(fileupload1.FileName.LastIndexOf('.') + 1);
            if (kuozhanming == "" || feifakuozhanming.IndexOf(kuozhanming) >= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('文件类型不正确，不能上传任何程序文件，请将文件(夹)压缩后上传！');</script>", false);
            }
            else
            {
                if (fileupload1.PostedFile.ContentLength <= 2097152)//文件应小于2M
                {
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/stuceshifile"));
                    if (!di.Exists)
                        di.Create();
                    di = new DirectoryInfo(Server.MapPath("~/stuceshifile/kecheng" + kechengid));
                    //创建课程文件夹
                    if (!di.Exists)
                        di.Create();
                    //创建班级文件夹
                    di = new DirectoryInfo(Server.MapPath("~/stuceshifile/kecheng" + kechengid + "/ceshi" + shijuanid));
                    if (!di.Exists)
                        di.Create();
                    //完成创建目录
                    string filename = "s" + username + "T" +tihao.ToString().Trim() + "." + kuozhanming;
                    fileupload1.SaveAs(Server.MapPath("~/stuceshifile/kecheng" + kechengid + "/ceshi" + shijuanid + "/" + filename));
                    conn.Open();
                    comm.CommandText = "update [tb_studentkaoshiti] set filepath='~/stuceshifile/kecheng" + kechengid + "/ceshi" + shijuanid + "/" + filename + "' where studentusername='"+username+"' and shijuanid="+shijuanid+" and [timuhao]=" +tihao;
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script>alert('文件上传成功！');</script>", false);
                    }
                    conn.Close();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script>alert('文件太大，超过2MB，上传失败！');</script>", false);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script>alert('请选择文件！');</script>", false);
        }
    }
    protected void BaocunDaan(int i)//保存答案
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        string daan = "";
        string tixing = lbl_tixing.Text.Trim();
        switch (tixing)
        {
            case "判断题":
                daan = rbl_panduandaan.SelectedValue;
                break;
            case "单项选择题":
                daan = rbl_xuanzedaan.SelectedValue;
                break;
            case "多项选择题":
                for (int j = 0; j < cbx_duoxuandaan.Items.Count; j++)
                {
                    if (cbx_duoxuandaan.Items[j].Selected)
                        daan += cbx_duoxuandaan.Items[j].Value.Trim();
                }
                break;
            case "操作题":
                //操作题需要提交文件，因此处理方法不同。
                daan = "";
                break;
            case "填空题":
                daan = tbx_tiankongdaan.Text.Trim();
                break;
            default:
                daan = tbx_jiandadaan.Text.Trim();
                break;
        }
        comm.CommandText = "update tb_studentkaoshiti set answer='" + daan + "' where studentusername='" + ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name.Trim() + "' and shijuanid=" + Request.QueryString["shijuanid"] + " and timuhao=" + lbl_tihao.Text;
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
        conn.Dispose();
    }
    protected void Buttonjiaojuan_Click(object sender, EventArgs e)//交卷按钮
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid =int.Parse(Request.QueryString["shijuanid"]); 
        int tihao = int.Parse(lbl_tihao.Text);
        BaocunDaan(tihao);
        Jiaojuan(username,shijuanid.ToString());
        GetStudentCeshiInfo(username,shijuanid);
    }
    //交卷，设置为已交卷
    protected void Jiaojuan(string username,string shijuanid)
    {
        SetJiaojuan(username,shijuanid);
        Gaijuan(username,shijuanid);
        SetTimerStop();
    }
    protected int Gaijuan(string username,string shijuanid)//改卷并登记成绩，返回分数，设置为已交卷
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        //改卷
        int zongfen = 0;
        comm.CommandText = "update tb_studentkaoshiti set defen=fenzhi where (rtrim(answer)=(select rtrim(answer) from tb_tiku where questionid=tb_studentkaoshiti.questionid)) and shijuanid=" + shijuanid + " and studentusername='" + username + "'";
        comm.ExecuteNonQuery();
        comm.CommandText = "update tb_studentkaoshiti set defen=0 where(answer is null or rtrim(answer)='' or rtrim(answer)<>(select rtrim(answer) from tb_tiku where questionid=tb_studentkaoshiti.questionid)) and shijuanid=" + shijuanid + " and studentusername='" + username + "'";
        comm.ExecuteNonQuery();
        comm.CommandText = "select sum(defen) from tb_studentkaoshiti where shijuanid=" + shijuanid + " and studentusername='" + username + "'";
        zongfen = (int)comm.ExecuteScalar();
        comm.CommandText = "update tb_studentkaoshi set zongfen="+zongfen+" where shijuanid=" + shijuanid + " and studentusername='" + username + "'";
        comm.ExecuteNonQuery();
        conn.Close();
        //设置为已交卷,登记成绩
        return zongfen;
    }
    protected void SetJiaojuan(string username,string shijuanid)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString))
        {
            SqlCommand comm = conn.CreateCommand();
            conn.Open();
            comm.CommandText = "update tb_studentkaoshi set jiaojuanshijian=getdate(),jiaojuan='已交卷' where shijuanid=" + shijuanid + " and studentusername='" + username + "'";
            comm.ExecuteNonQuery();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//上一题
    {
        int tihao = int.Parse(lbl_tihao.Text);
        BaocunDaan(tihao);
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        GetStudentCeshiInfo(username,shijuanid);   
        int newtihao = GetTihao(tihao, -1);
        lbl_tihao.Text =newtihao.ToString();
        XianshiTimu(newtihao);
     
    }
    protected void Button2_Click(object sender, EventArgs e)//下一题
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid =int.Parse(Request.QueryString["shijuanid"]);
        GetStudentCeshiInfo(username,shijuanid);   
        int tihao = int.Parse(lbl_tihao.Text);
        BaocunDaan(tihao);
        int newtihao = GetTihao(tihao, 1);
        lbl_tihao.Text = newtihao.ToString();
        XianshiTimu(newtihao);
     }
    protected void Timerzdjiao_Tick(object sender, EventArgs e)//自动交卷
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string shijuanid = Request.QueryString["shijuanid"];
        int tihao = int.Parse(lbl_tihao.Text);
        BaocunDaan(tihao);
        Jiaojuan(username,shijuanid);
        GetStudentCeshiInfo(username,int.Parse(shijuanid));
    }
    protected void Timerdjszdbc_Tick(object sender, EventArgs e)//倒计时和自动保存
    {
        //BaocunDaan();//保存答案
        int tihao = int.Parse(lbl_tihao.Text);
        BaocunDaan(tihao);
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        GetStudentCeshiInfo(username,shijuanid);
    }
    /// <summary>
    /// 检测考试机器的IP地址，同一机器不允许两个学生进行同一试卷的考试，一个试卷不允许在两台机器上考试
    /// </summary>
    /// <param name="stuusername">学生用户名</param>
    /// <param name="shijuanid">试卷id</param>
    /// <returns>
    /// 0_符合要求,可以考试
    /// 1_学生考试机的IP与数据库中登记的IP不一致，需要到原来考试的机器上进行考试
    /// 2_学生考试机已有学生进行过该考试，需要换机器进行考试
    /// </returns>
    protected int CheckIp(string stuusername, int shijuanid)//检测考试机器的IP地址，同一机器不允许两个学生进行同一试卷的考试，一个试卷不允许在两台机器上考试
    {
        string stuip = Request.UserHostAddress;
        //string stuip = Hdfmac.Value;

        string kaoshiip = GetStukaoshiIp(stuusername, shijuanid);//得到数据库中学生考试机的IP
        if (kaoshiip == "")//如果学生该考试的IP为空，检测机器IP在该考试中是否存在
        {
            if (IsKaoshiIpUnique(stuusername,shijuanid, stuip))
            {
                return (0);
            }
            else//机器IP在该考试中存在,已有学生在该机器考试过
            {
                return (2);
            }
        }
        else//学生该考试的IP不为空，
        {
            if (stuip.Trim() == kaoshiip.Trim())//机器IP与数据库中考试的IP一致
                return (0);
            else
                return (1);
        }
    }
    protected string GetStukaoshiIp(string stuusername, int shijuanid)//得到数据库中学生考试机的IP
    {
        string kaoshiip = "";
        string strsql = "select ip from tb_studentkaoshi where studentusername=@stuusername and shijuanid=@shijuanid";
        SqlParameter[] para ={
                                 new SqlParameter("@stuusername",stuusername),
                                 new SqlParameter("@shijuanid",shijuanid)
                             };
        kaoshiip = SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, strsql, para).ToString();
        return kaoshiip;
    }
    protected void SetStukaoshiIp(string username,int shijuanid)//设置学生考试的IP
    {
        string strsql = "update tb_studentkaoshi set ip=@ip where studentusername=@stuusername and shijuanid=@shijuanid";
        SqlParameter[] para ={
                                 new SqlParameter("@ip",Request.UserHostAddress),
                                 new SqlParameter("@stuusername",username),
                                 new SqlParameter("@shijuanid",shijuanid)
                            };
        SqlHelper.ExecuteNonQuery(SqlDal.conn, CommandType.Text, strsql, para);
    }
    protected bool IsKaoshiIpUnique(string stuusername,int shijuanid, string ip)//当学生该考试IP地址为空时，检测学生机IP在该次考试中是否存在
    {
        string strsql = "select count(ip) from tb_studentkaoshi where  (ip=@ip and shijuanid=@shijuanid and studentusername<>@stuusername)";
        SqlParameter[] para ={
                                 new SqlParameter("@ip",ip),
                                 new SqlParameter("@shijuanid",shijuanid),
                                 new SqlParameter("@stuusername",stuusername)
                             };
        int count=(int)(SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, strsql, para));
        return (count == 0);
    }
    protected int GetTihao(int n,int i)//得到题号
    {
        int tihao=n+i;
        int maxtihao = int.Parse(lbl_timushuliang.Text);
        if (tihao > maxtihao)
            tihao = 1;
        else if (tihao <= 0)
            tihao = maxtihao;
        return tihao;
    }
    protected void btn_timutiaozhuan_Click(object sender, EventArgs e)//跳转到某一题
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int shijuanid = int.Parse(Request.QueryString["shijuanid"]);
        GetStudentCeshiInfo(username, shijuanid);
        int tihao = int.Parse(lbl_tihao.Text);
        BaocunDaan(tihao);
        int newtihao = int.Parse(ddl_tihaoliebiao.SelectedValue);
        lbl_tihao.Text = newtihao.ToString();
        XianshiTimu(newtihao);
    }
    protected void btn_jianchashijuan_Click(object sender, EventArgs e)
    {
        string shijuanid = Request.QueryString["shijuanid"];
        ScriptManager.RegisterClientScriptBlock(this,typeof(string),"","<script language='javascript'>window.open('shijuanjiancha.aspx?shijuanid="+shijuanid+"','newwindow','width=400,height=400');</script>",false);
    }
}
