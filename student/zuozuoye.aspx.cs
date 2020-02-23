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
public partial class studentstudy_zuozuoye : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        //在Paleceholder中显示题目
        if (StudentInfo.WeiWanchengCeshi(username, int.Parse(Session["kechengid"].ToString())))
        {
            Response.Redirect("jiaoshiceshi.aspx");
        }
        if (Request.QueryString["zuoyeid"].Trim().Length > 0)
        {
            string zuoyeid = Request.QueryString["zuoyeid"];
            string yunxu = "";
            DateTime qixian = DateTime.Now;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select yunxuzuoti,shangjiaoqixian from tb_studentzuoye  where zuoyeid=" + zuoyeid + " and studentusername='" + username + "'";
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                yunxu = sdr.GetString(0).Trim();
                qixian = sdr.GetDateTime(1);
            }
            sdr.Close();
            conn.Close();
            if (yunxu == "允许" && qixian.CompareTo(DateTime.Now) > 0)
            {
                xianshiTimu();
            }
            else
            {
                Response.Redirect("zuoye.aspx");
            }
            BindStuZuoyeInfo();
        }
    }
    protected void BindStuZuoyeInfo()
    {
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string zuoyeid = Request.QueryString["zuoyeid"];
        DataTable dt = ZuoyeInfo.GetStuZuoyeInfo(stuusername, int.Parse(zuoyeid));
        FormView1.DataSource = dt;
        FormView1.DataBind();
    }
    protected void xianshiTimu()//在PlaceHolder1中显示题目
    {
        int zuoyeid = int.Parse(Request.QueryString["zuoyeid"]);
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable timutable = StudentInfo.GetStuZuoyeTimu(zuoyeid, stuusername);
        Literal tihaoliteral;//显示题号
        Literal templiteral;
        RadioButtonList danxuanrbl;//显示单项选择
        RadioButtonList panduanrbl;//判断
        CheckBoxList duoxuancbl;//多选
        TextBox tikongtbx;//填空
        FileUpload caozuofuld;//文件上传
        Button btn_uploadfile;//上传文件按钮
        RequiredFieldValidator filevalidator;

        if (timutable.Rows.Count > 0)
        {
            int tihao = 1;
            #region 显示题目
            foreach (DataRow timurow in timutable.Rows)
            {
                tihaoliteral = new Literal();
                tihaoliteral.Text =tihao.ToString()+"("+timurow[2].ToString() + "," + timurow[7].ToString() + "分)、" + timurow[3].ToString() + "<br/>";
                PlaceHolder1.Controls.Add(tihaoliteral);
                switch (timurow[2].ToString().Trim())
                {
                    case "单项选择题":
                        danxuanrbl = new RadioButtonList();
                        danxuanrbl.ID = "timu" + timurow[0].ToString().Trim();
                        danxuanrbl.Items.Add("A");
                        danxuanrbl.Items.Add("B");
                        danxuanrbl.Items.Add("C");
                        danxuanrbl.Items.Add("D");
                        if (timurow[5].ToString().Trim() != "")//设置单选题答案
                        {
                            danxuanrbl.SelectedValue = timurow[5].ToString().Trim();
                        }
                        danxuanrbl.RepeatDirection = RepeatDirection.Horizontal;
                        danxuanrbl.Width = Unit.Pixel(240);
                        templiteral = new Literal();
                        templiteral.Text = "<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;请选择(四选一)：</td><td>";
                        PlaceHolder1.Controls.Add(templiteral);
                        PlaceHolder1.Controls.Add(danxuanrbl);
                        templiteral = new Literal();
                        templiteral.Text = "</td></tr></table>";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                    case "判断题":
                        panduanrbl = new RadioButtonList();
                        panduanrbl.ID = "timu" + timurow[0].ToString().Trim();
                        panduanrbl.Items.Add(new ListItem("正确", "T"));
                        panduanrbl.Items.Add(new ListItem("错误", "F"));
                        if (timurow[5].ToString().Trim() != "")
                        {
                            panduanrbl.SelectedValue = timurow[5].ToString().Trim().ToUpper();
                        }
                        panduanrbl.RepeatDirection = RepeatDirection.Horizontal;
                        panduanrbl.Width = Unit.Pixel(150);
                        templiteral = new Literal();
                        templiteral.Text = "<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;请选择(二选一)：</td><td>";
                        PlaceHolder1.Controls.Add(templiteral);
                        PlaceHolder1.Controls.Add(panduanrbl);
                        templiteral = new Literal();
                        templiteral.Text = "</td></tr></table>";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                    case "多项选择题":
                        duoxuancbl = new CheckBoxList();
                        duoxuancbl.ID = "timu" + timurow[0].ToString().Trim();
                        duoxuancbl.Items.Add("A");
                        duoxuancbl.Items.Add("B");
                        duoxuancbl.Items.Add("C");
                        duoxuancbl.Items.Add("D");
                        duoxuancbl.Items.Add("E");
                        if (timurow[5].ToString().Trim() != "")//设置多选题答案
                        {
                            if (timurow[5].ToString().IndexOf('A') >= 0)
                                duoxuancbl.Items[0].Selected = true;
                            if (timurow[5].ToString().IndexOf('B') >= 0)
                                duoxuancbl.Items[1].Selected = true;
                            if (timurow[5].ToString().IndexOf('C') >= 0)
                                duoxuancbl.Items[2].Selected = true;
                            if (timurow[5].ToString().IndexOf('D') >= 0)
                                duoxuancbl.Items[3].Selected = true;
                            if (timurow[5].ToString().IndexOf('E') >= 0)
                                duoxuancbl.Items[4].Selected = true;
                        }
                        templiteral = new Literal();
                        templiteral.Text = "<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;请选择(可多选)：</td><td>";
                        PlaceHolder1.Controls.Add(templiteral);
                        duoxuancbl.Width = Unit.Pixel(300);
                        duoxuancbl.RepeatDirection = RepeatDirection.Horizontal;
                        PlaceHolder1.Controls.Add(duoxuancbl);
                        templiteral = new Literal();
                        templiteral.Text = "</td></tr></table>";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                    case "操作题":
                        templiteral = new Literal();
                        templiteral.Text = "<font color='red'><b>注意：作业文件要用'提交作业文件'按钮进行提交,各题分别提交,用'提交作业'按钮不能提交作业文件！</b></font><br/>";
                        PlaceHolder1.Controls.Add(templiteral);
                        if (timurow[9].ToString().Length > 0)
                        {
                            HyperLink ziyuanfile = new HyperLink();
                            ziyuanfile.ID = "ziyuanfile" + timurow[0].ToString().Trim();
                            ziyuanfile.Text = "下载本题资源文件";
                            ziyuanfile.NavigateUrl = timurow[10].ToString().Trim();
                            PlaceHolder1.Controls.Add(ziyuanfile);
                        }
                        //显示文件上传控件
                        caozuofuld = new FileUpload();
                        caozuofuld.ID = "timu" + timurow[0].ToString().Trim();
                        caozuofuld.ToolTip = "不要上传程序文件,文件大小不要超过2MB,最好压缩后上传。";
                        templiteral = new Literal();
                        templiteral.Text = "&nbsp;&nbsp;&nbsp;&nbsp;请选择要上传的作业文件：";
                        PlaceHolder1.Controls.Add(templiteral);
                        PlaceHolder1.Controls.Add(caozuofuld);
                        //添加文件验证
                        filevalidator = new RequiredFieldValidator();
                        filevalidator.Text = "请选择作业文件！";
                        filevalidator.ControlToValidate = "timu" + timurow[0].ToString().Trim();
                        filevalidator.ID = "fv" + timurow[0].ToString().Trim();
                        filevalidator.EnableClientScript = true;
                        filevalidator.Display = ValidatorDisplay.Dynamic;
                        filevalidator.ValidationGroup = "g" + timurow[0].ToString().Trim();
                        PlaceHolder1.Controls.Add(filevalidator);
                        //显示上传文件按钮
                        btn_uploadfile = new Button();
                        btn_uploadfile.Text = "提交作业文件";
                        btn_uploadfile.ID = "timu" + timurow[0].ToString().Trim() + "upload";
                        btn_uploadfile.CommandArgument = timurow[0].ToString();
                        //btn_uploadfile.CommandName = "UploadZuoyeFile";
                        btn_uploadfile.Command += new CommandEventHandler(UploadZuoyeFile);
                        btn_uploadfile.CausesValidation = true;
                        btn_uploadfile.ValidationGroup = "g" + timurow[0].ToString().Trim();
                        PlaceHolder1.Controls.Add(btn_uploadfile);
                        templiteral = new Literal();
                        templiteral.Text = "<br/>";
                        PlaceHolder1.Controls.Add(templiteral);
                        //if (timurow[7].ToString().Length > 0)
                        //{
                            HyperLink benrenzuoyefile = new HyperLink();
                            benrenzuoyefile.ID = "zuoyefile" + timurow[0].ToString().Trim();
                            benrenzuoyefile.Text = "下载我的作业文件";
                            benrenzuoyefile.NavigateUrl = timurow[6].ToString().Trim();
                            benrenzuoyefile.Target = "_blank";
                            PlaceHolder1.Controls.Add(benrenzuoyefile);
                        //}
                        templiteral = new Literal();
                        templiteral.Text = "<br/>";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                    case "填空题":
                        tikongtbx = new TextBox();
                        tikongtbx.ID = "timu" + timurow[0].ToString().Trim();
                        tikongtbx.TextMode = TextBoxMode.SingleLine;
                        tikongtbx.MaxLength = 100;
                        if (timurow[5].ToString().Trim() != "")//设置单选题答案
                        {
                            tikongtbx.Text = timurow[5].ToString().Trim();
                        }
                        templiteral = new Literal();
                        templiteral.Text = "&nbsp;&nbsp;&nbsp;&nbsp;请在此框中填写答案：";
                        PlaceHolder1.Controls.Add(templiteral);
                        PlaceHolder1.Controls.Add(tikongtbx);
                        templiteral = new Literal();
                        templiteral.Text = "<br/>";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                    default:
                        tikongtbx = new TextBox();
                        tikongtbx.ID = "timu" + timurow[0].ToString().Trim();
                        tikongtbx.TextMode = TextBoxMode.MultiLine;
                        tikongtbx.Rows = 5;
                        tikongtbx.Columns = 80;
                        tikongtbx.MaxLength = 500;
                        if (timurow[5].ToString().Trim() != "")//设置简答题答案
                        {
                            tikongtbx.Text = timurow[5].ToString().Trim();
                        }
                        templiteral = new Literal();
                        templiteral.Text = "&nbsp;&nbsp;&nbsp;&nbsp;请在此框中填写答案：<br/>";
                        PlaceHolder1.Controls.Add(templiteral);
                        PlaceHolder1.Controls.Add(tikongtbx);
                        templiteral = new Literal();
                        templiteral.Text = "<br/>";
                        PlaceHolder1.Controls.Add(templiteral);
                        break;
                } 
                tihao++;
            }
            #endregion
        }
        else
        {    
            Button1.Enabled = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//交作业
    {
        //保存答案
        int zuoyeid = int.Parse(Request.QueryString["zuoyeid"]);
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataTable timutable = StudentInfo.GetStuZuoyeTimu(zuoyeid,stuusername);
        string daan = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            foreach (DataRow timurow in timutable.Rows)
            {
                switch (timurow[2].ToString().Trim())
                {
                    case "单项选择题":
                        daan = ((RadioButtonList)(PlaceHolder1.FindControl("timu" + timurow[0].ToString().Trim()))).SelectedValue;
                        if (daan != "")
                        {
                            comm.CommandText = "update [tb_stuzuoyetimu] set answer='" + daan + "' where [stuzuoyetimuid]=" + (int)(timurow[0]);
                            comm.ExecuteNonQuery();
                        }
                        break;
                    case "判断题":
                        daan = ((RadioButtonList)(PlaceHolder1.FindControl("timu" + timurow[0].ToString().Trim()))).SelectedValue;
                        if (daan != "")
                        {
                            comm.CommandText = "update [tb_stuzuoyetimu] set answer='" + daan + "' where [stuzuoyetimuid]=" + (int)(timurow[0]);
                            comm.ExecuteNonQuery();
                        }
                        break;
                    case "多项选择题":
                        daan = "";
                        CheckBoxList duoxuancbl = (CheckBoxList)(PlaceHolder1.FindControl("timu" + timurow[0].ToString().Trim()));
                        for (int i = 0; i < 5; i++)
                        {
                            if (duoxuancbl.Items[i].Selected)
                            {
                                daan += duoxuancbl.Items[i].Value.Trim();
                            }
                        }
                        if (daan != "")
                        {
                            comm.CommandText = "update [tb_stuzuoyetimu] set answer='" + daan + "' where [stuzuoyetimuid]=" + (int)(timurow[0]);
                            comm.ExecuteNonQuery();
                        }
                        break;
                    case "操作题":
                        break;
                    case "填空题":
                        daan = ((TextBox)(PlaceHolder1.FindControl("timu" + timurow[0].ToString().Trim()))).Text.Trim();
                        if (daan != "")
                        {
                            comm.CommandText = "update [tb_stuzuoyetimu] set answer='" + daan + "' where [stuzuoyetimuid]=" + (int)(timurow[0]);
                            comm.ExecuteNonQuery();
                        }
                        break;
                    default:
                        daan = ((TextBox)(PlaceHolder1.FindControl("timu" + timurow[0].ToString().Trim()))).Text.Trim();
                        if (daan != "")
                        {
                            comm.CommandText = "update [tb_stuzuoyetimu] set answer='" + daan + "' where [stuzuoyetimuid]=" + (int)(timurow[0]);
                            comm.ExecuteNonQuery();
                        }
                        break;
                }
            }
 
            comm.CommandText = "update [tb_stuzuoyetimu] set defen=fenzhi where  studentusername='" + stuusername + "' and zuoyeid=" + zuoyeid + " and (answer is not null and answer<>'') and answer=(select answer from tb_tiku where questionid=tb_stuzuoyetimu.questionid)";
            comm.ExecuteNonQuery();
            comm.CommandText = "update [tb_stuzuoyetimu] set defen=0 where  studentusername='" + stuusername + "' and  zuoyeid=" + zuoyeid + " and (answer is null or  answer='' or answer<>(select answer from tb_tiku where questionid=tb_stuzuoyetimu.questionid))";
            comm.ExecuteNonQuery();
            comm.CommandText = "update [tb_studentzuoye] set shangjiaoriqi='"+DateTime.Now.ToString()+"',zongfen=(select sum(defen) from tb_stuzuoyetimu where zuoyeid=" + zuoyeid + " and studentusername='" + stuusername + "') where zuoyeid=" + zuoyeid + " and studentusername='" + stuusername + "'";
            comm.ExecuteNonQuery();
            st.Commit();
            FormView1.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('作业上交成功!');</script>", false);
        }
        catch(Exception ex)
        {
            st.Rollback();
            Label2.Text = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('作业上交失败!');</script>", false);
        }
        finally
        {
            conn.Close();
        }
    }
    protected void UploadZuoyeFile(object sender, CommandEventArgs e)//上传单个作业文件
    {
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        int stuzuoyetimuid = int.Parse(e.CommandArgument.ToString());
        string zuoyeid = Request.QueryString["zuoyeid"];
        FileUpload fu = (FileUpload)(PlaceHolder1.FindControl("timu" + stuzuoyetimuid.ToString()));//找到对应的文件上传框
        string kuozhanming = string.Empty;//扩展名
        if (fu.HasFile)//如果有文件
        {
            kuozhanming = fu.FileName.Substring(fu.FileName.LastIndexOf(".") + 1).ToLower();//作业文件扩展名
            string feifakuozhanming = "asp,aspx,php,jsp,js,exe,com,pif";
            if (kuozhanming == "" || feifakuozhanming.IndexOf(kuozhanming) >= 0 || kuozhanming == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('作业文件类型不正确，不能上传任何程序文件，请将作业文件(夹)压缩后上传！');</script>", false);
            }
            else
            {
                if (fu.PostedFile.ContentLength <= 2097152)//作业文件大小2M以下
                {
                    string fankui = string.Empty;
                    bool fileUploadchenggong = true;
                    //获取题目以前上传的文件、题号
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
                    SqlCommand comm = conn.CreateCommand();
                    //上传新文件   
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/stuzuoyefile"));
                    if (!di.Exists)
                        di.Create();
                    string kechengid = Session["kechengid"].ToString();
                    di = new DirectoryInfo(Server.MapPath("~/stuzuoyefile/kecheng" + kechengid));
                    //创建课程文件夹
                    if (!di.Exists)
                        di.Create();
                    //创建作业文件夹
                    di = new DirectoryInfo(Server.MapPath("~/stuzuoyefile/kecheng" + kechengid + "/zuoye" + zuoyeid));
                    if (!di.Exists)
                        di.Create();
                    di = new DirectoryInfo(Server.MapPath("~/stuzuoyefile/kecheng" + kechengid + "/zuoye" + zuoyeid));
                    if (!di.Exists)
                        di.Create();
                    //完成创建目录
                    string filename = "s" + stuusername + "T" + stuzuoyetimuid.ToString() + "." + kuozhanming;
                    try
                    {
                        fu.PostedFile.SaveAs(Server.MapPath("~/stuzuoyefile/kecheng" + kechengid + "/zuoye" + zuoyeid + "/" + filename));
                    }
                    catch
                    {
                        fileUploadchenggong = false;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('作业文件上传失败！');</script>", false);
                    }
                    finally
                    {

                    }
                    if (fileUploadchenggong)
                    {
                        //登记到数据库
                        conn.Open();
                        comm.CommandText = "update [tb_stuzuoyetimu] set filepath='~/stuzuoyefile/kecheng" + kechengid + "/zuoye" + zuoyeid + "/" + filename + "' where [stuzuoyetimuid]=" + stuzuoyetimuid;
                        comm.ExecuteNonQuery();
                        conn.Close();
                        ((HyperLink)(PlaceHolder1.FindControl("zuoyefile" + stuzuoyetimuid.ToString()))).NavigateUrl = "~/stuzuoyefile/kecheng" + kechengid + "/zuoye" + zuoyeid + "/" + filename;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('作业文件上传成功！');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('文件太大,超过2MB,上传失败！');</script>", false);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('请选择上传的文件！');</script>", false);
        }
    }
    protected void SetZuoyeZhuangtai()
    {
        string zuoyeid = Request.QueryString["zuoyeid"];
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        int zuoyetimuzongshu = 10;
        int wanchengtimushu = 5;
        comm.CommandText = "select sum(yiwancheng),max(tihao) from tb_stuzuoyetimu where zuoyeid=" + zuoyeid + " and studentusername='" + stuusername + "'";
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.Read())
        {
            zuoyetimuzongshu = sdr.GetInt32(1);
            wanchengtimushu = sdr.GetInt32(0);
        }
        sdr.Close();
        if (zuoyetimuzongshu == wanchengtimushu)
        {
            comm.CommandText = "update [tb_studentzuoye] set wancheng='已完成',shangjiaoriqi=getdate() where zuoyeid=" + zuoyeid + " and studentusername='" + stuusername + "'";
        }
        else
        {
            comm.CommandText = "update [tb_studentzuoye] set wancheng='未完成',shangjiaoriqi=getdate() where zuoyeid=" + zuoyeid + " and studentusername='" + stuusername + "'";
        }
        comm.ExecuteNonQuery();
        conn.Close();
    }
    protected void getstudentinfo(out string xingming, out string banji)
    {
        string stuusername = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select xingming,banji from tb_Student where username='" + stuusername + "'";
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        sdr.Read();
            xingming = sdr.GetString(0);
            banji = sdr.GetString(1);
        sdr.Close();
        conn.Close();
    }
    
}
