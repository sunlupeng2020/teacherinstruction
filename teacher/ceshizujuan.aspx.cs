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
using System.Xml;
using System.Collections.Generic;
using System.Text;

public partial class teachermanage_ceshizujuan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setTextboxValue();
        if (!IsPostBack)
        {
            string kechengid = Session["kechengid"].ToString();
            TreeView1.ConnectionString = ConfigurationManager.ConnectionStrings[TreeView1.ConnectionStringName].ConnectionString;
            TreeView1.kechengid = int.Parse(kechengid);
            BindBanji();
        }
    }
    protected void BindBanji()
    {
        string kechengid = Session["kechengid"].ToString();
        string username=((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataSet ds = BanjiInfo.GetTeacherRenkeBanji(username, int.Parse(kechengid));
        DropDownList_banji.DataSource = ds;
        DropDownList_banji.DataBind();
    }
    protected void setTextboxValue()//设置选中的知识点数量
    {
        TextBoxzhishidianshu.Text = TreeView1.CheckedNodes.Count.ToString();
    }
    protected void StartNextButton_Click(object sender, EventArgs e)//验证是否有班级、题目，如果有，则跳转到下一步
    {
        //查询相关知识点、课程有无任课班级、题目
        bool banjicunzai = false;
        bool timucunzai = false;
        string kechengid = Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string kechengname = KechengInfo.getKechengname(kechengid);
        //查询是否有班级
        DataSet ds = BanjiInfo.GetTeacherRenkeBanji(username, int.Parse(kechengid));
        if (ds.Tables[0].Rows.Count > 0)
            banjicunzai = true;
        if (!banjicunzai)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(String), "", "<script language='javascript'>alert('没有找到您任教课程" +kechengname + "的班级，请选择其他班级!');</script>", false);
            return;
        }
        //查询是否有题目
        //显示每种题型的数量，选择数量，分值
        //获取选择的知识点及其下位知识点
        List<string> nodeandchilren = TreeView1.CheckedNodesAndChildrenIds;// 选择的知识点及其子孙知识点id
        StringBuilder nodeandchildrenSB = new StringBuilder();
        //从题库中查询符合条件的题目
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //建立临时表，记录选择的知识点id号 
        string temptablename = "#temptable" + username + DateTime.Now.Ticks.ToString();
        comm.CommandText = "create table " +temptablename + "(id int)";
        conn.Open();
        comm.ExecuteNonQuery();
        foreach (string nodeid in nodeandchilren)
        {
            nodeandchildrenSB.Append(nodeid + ",");
            comm.CommandText = "insert into " +temptablename + "(id) values(" + int.Parse(nodeid) + ")";
            comm.ExecuteNonQuery();
        }
        //查询符合条件的第一题
        comm.CommandText = "select top 1 questionid from tb_tiku where questionid in( select questionid from tb_timuzhishidian where questionid not in ( select questionid from tb_timuzhishidian where kechengjiegouid not in (select id from " +temptablename + ")))";
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.HasRows)
        {
            timucunzai = true;
        }
        sdr.Close();
        conn.Close();
        HiddenFieldnodeandchildren.Value = nodeandchildrenSB.ToString();//把选择的知识点及其下位知识点保存在hiddenfield中,以便以后使用
        if (!timucunzai)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(String), "", "<script language='javascript'>alert('没有找到您选择的知识点的题目，请选择其他知识点!');</script>", false);
        }
        if (banjicunzai && timucunzai)
        {
            Wizard1.ActiveStepIndex = 1;
        }
    }
    protected bool CheckCeshiName(int kechengid, string ceshiname, string username)//检查一个用户某课程某测试名称是否存在
    {
        bool yicunzai = false;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select shijuanid from tb_teachershijuan where ceshiname='" + ceshiname + "' and teacherusername='" + username + "' and kechengid=" + kechengid;
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.HasRows)
        {
            yicunzai = true;
        }
        sdr.Close();
        conn.Close();
        return yicunzai;
    }
    protected void StepNextButton_Click(object sender, EventArgs e)//检查测试名称是否可用,收集组卷信息，跳转到下一步,绑定试题
    {
        //检查测试名称是否可用
        string kechengid = Session["kechengid"].ToString();
        string username=((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string ceshiname = TextBoxceshimingcheng.Text.Trim();//测试名称
        string xianzhiip = (chbxianzhiip.Checked ? "是" : "否");//是否限制IP
        if (CheckCeshiName(int.Parse(kechengid),ceshiname,username))//检查测试名称是否可用
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('您的该课程的该试卷名称已使用，请选择其它名称。');</script>", false);
            return;
        }
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //检查所选班级有无学生，如果没有学生，则不能组卷
        string banjiid = DropDownList_banji.SelectedValue;//测试班级ID
        comm.CommandText = "select count(studentusername) from tb_banjistudent where banjiid=" + banjiid;
        conn.Open();
        int banjistudentshuliang = (int)(comm.ExecuteScalar());
        conn.Close();
        if (banjistudentshuliang <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('所选班级没有学生，不能组卷！请前往添加学生。');</script>", false);
            return;
        }
        //收集组卷信息，跳转到下一步
        StringBuilder tixingxuanzexinxi = new StringBuilder();
        string tishi = "";
        if (RadioButtonListxuantifangshi.SelectedValue == "0")
            tishi = "应该选择";
        else
            tishi = "应至少选择";
        StringBuilder tishiSB = new StringBuilder();
        string[] tixingshuzu = HiddenFieldtixings.Value.Split(',');//要选择哪些题型
        DropDownList tixuzeshuDropdownList;//显示某题型有多少道题的
        List<string> tixings = new List<string>();//选择了哪些题型
        foreach (string tixing in tixingshuzu)
        {
            if (tixing.Length > 0)
            {
                tixuzeshuDropdownList = (DropDownList)(PlaceHolder1.FindControl("tixing" + tixing));
                    if (tixuzeshuDropdownList != null&&tixuzeshuDropdownList.SelectedIndex!=0)
                    {
                        tixings.Add(tixing);
                        tishiSB.Append(tixing+"有"+(tixuzeshuDropdownList.Items.Count-1).ToString()+tishi+tixuzeshuDropdownList.SelectedValue+"道.<br/>");
                    }
            }
        }
        Literalxuantitishi.Text=tishiSB.ToString();
        //绑定备选题目GridView
        List<string> nodeandchildren =TreeView1.CheckedNodesAndChildrenIds;//选中的知识点及其子孙知识点
        DataTable dt= GetTimuDataTable(nodeandchildren,tixings);
        GridView1.DataSource=dt;
        GridView1.DataBind();//绑定题目gridview
        if (RadioButtonListxuantifangshi.SelectedIndex != 2)//手工组卷和半自动组卷
        {
            Wizard1.ActiveStepIndex = 2;
        }
        else//全自动组卷
        {
            #region 准备工作，获取组卷数据
            List<string> nodeandchilren = TreeView1.CheckedNodesAndChildrenIds;//选择的知识点及其子孙知识点id
            StringBuilder timufenzhixinxi = new StringBuilder();
            int ceshishichang = int.Parse(DropDownList4.SelectedValue);//测试时间长度
            string yunxuzuoti = RadioButtonList9.SelectedValue;//是否允许学生做题
            string ceshibanji = DropDownList_banji.SelectedItem.Text;//测试班级
            string mingtifangshi = RadioButtonListzujuanfangshi.SelectedValue;//命题方式，统一命题，每个学生的题目不同，对学生保密
            string yunxuchakan = RadioButtonList10.SelectedValue;
            int manfen = 100;
            #endregion
            bool zujuanchenggong = true;
            int xuanzeshu = 0;
            int tifenzhi = 0;
            List<string> tixingXuanzeList = new List<string>();
            List<int> tixingshuliangList = new List<int>();
            List<int> tixingfenzhiList = new List<int>();
            List<int> studentxuhaoList = new List<int>();
            List<string> studentxingmingList = new List<string>();
            DropDownList tifenzhiDropdownList;
            comm.CommandText = "select mingcheng from tb_timuleixing order by tixingid";
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            while (sdr.Read())
            {
                tixuzeshuDropdownList = (DropDownList)(PlaceHolder1.FindControl("tixing" + sdr.GetString(0)));
                if (tixuzeshuDropdownList != null)
                {
                    xuanzeshu = int.Parse(tixuzeshuDropdownList.SelectedValue);
                    if (xuanzeshu > 0)
                    {
                        timufenzhixinxi.Append(sdr.GetString(0) + ":" + tixuzeshuDropdownList.SelectedValue + "道，每题");
                        tixingXuanzeList.Add(sdr.GetString(0));//选择了哪些题型
                        tifenzhiDropdownList = (DropDownList)(PlaceHolder1.FindControl("timufenzhi" + sdr.GetString(0)));
                        tixingshuliangList.Add(xuanzeshu);//每种题型数量
                        tifenzhi = int.Parse(tifenzhiDropdownList.SelectedValue);
                        timufenzhixinxi.Append(tifenzhi.ToString() + "分;");
                        tixingfenzhiList.Add(tifenzhi);//每种题型分值
                    }
                }
            }
            sdr.Close();
            conn.Close();
            List<int>[] beixuanTihaoList = new List<int>[tixingXuanzeList.Count];//各种题型备选的questionid列表
            List<int>[] xuanzhongTihaoList = new List<int>[tixingXuanzeList.Count];//各种题型选中的questionid列表
            for (int i = 0; i < tixingXuanzeList.Count; i++)
            {
                beixuanTihaoList[i] = new List<int>();
                xuanzhongTihaoList[i] = new List<int>();
            }
            string temptablename = "temp_table" + username + DateTime.Now.Ticks.ToString();
            comm.CommandText = "create table " +temptablename + "(id int)";
            conn.Open();
            comm.ExecuteNonQuery();
            foreach (string nodeid in nodeandchilren)
            {
                comm.CommandText = "insert into " +temptablename + "(id) values(" + int.Parse(nodeid) + ")";
                comm.ExecuteNonQuery();
            }
            string sqlstr = "select questionid,type from tb_tiku where(";
            for (int i = 0; i < tixingXuanzeList.Count; i++)//添加题型条件
            {
                if (i == 0)
                {
                    sqlstr += "type='" + tixingXuanzeList[i] + "'";
                }
                else
                {
                    sqlstr += " or type='" + tixingXuanzeList[i] + "'";
                }
            }
            sqlstr += ")";
            sqlstr += " and questionid in ( select questionid from tb_timuzhishidian where questionid not in ( select questionid from tb_timuzhishidian where kechengjiegouid not in (select id from " +temptablename + "))) order by type";
            comm.CommandText = sqlstr;
            sdr = comm.ExecuteReader();
            int tixinghao;
            int tihao;
            while (sdr.Read())
            {
                tixinghao = tixingXuanzeList.IndexOf(sdr.GetString(1));//得到题型在列表中的编号
                beixuanTihaoList[tixinghao].Add(sdr.GetInt32(0));
            }
            sdr.Close();
            //随机选则题目，为统一组卷，全体学生相同或对学生保密的情况
            try
            {
                comm.CommandText = "drop table " + temptablename;
                comm.ExecuteNonQuery();
            }
            finally
            {
                if(conn.State==ConnectionState.Open)
                    conn.Close();
            }
            Random r = new Random();
            for (int i = 0; i < tixingXuanzeList.Count; i++)
            {
                tixinghao = 0;
                while (tixinghao < tixingshuliangList[i])
                {
                    tihao = r.Next(beixuanTihaoList[i].Count);
                    if (!xuanzhongTihaoList[i].Contains(tihao))
                    {
                        xuanzhongTihaoList[i].Add(tihao);
                        tixinghao++;
                    }
                }
            }
            //随机选择题目，全体学生不同的情况
            List<string> stuusernameList = new List<string>();//学生用户名列表
            comm.CommandText = "select tb_banjistudent.xuhao,tb_banjistudent.studentusername,tb_student.xingming from tb_banjistudent,tb_student where tb_banjistudent.banjiid=" + banjiid + " and tb_banjistudent.studentusername=tb_student.username order by tb_banjistudent.xuhao";
            conn.Open();
            sdr = comm.ExecuteReader();
            while (sdr.Read())
            {
                studentxuhaoList.Add(sdr.GetInt32(0));
                stuusernameList.Add(sdr.GetString(1));
                studentxingmingList.Add(sdr.GetString(2));
            }
            sdr.Close();
            conn.Close();
            List<int>[,] stusuijiXuantiList = new List<int>[stuusernameList.Count, tixingshuliangList.Count];//记录题型i随机选择的题目在题目ID列表中的下标
            for (int i = 0; i < stuusernameList.Count; i++)
                for (int j = 0; j < tixingshuliangList.Count; j++)
                {
                    stusuijiXuantiList[i, j] = new List<int>();
                }
            //开始随机选题
            for (int i = 0; i < stuusernameList.Count; i++)
                for (int j = 0; j < tixingshuliangList.Count; j++)
                {
                    int yixuantimushu = 0;
                    while (yixuantimushu < tixingshuliangList[j])
                    {
                        tihao = r.Next(beixuanTihaoList[j].Count);
                        if (!stusuijiXuantiList[i, j].Contains(tihao))
                        {
                            stusuijiXuantiList[i, j].Add(tihao);
                            yixuantimushu++;
                        }
                    }
                }
            //写入数据库
            int shijuanid = 0;
            StringBuilder studentzujuanxinxi = new StringBuilder();
            conn.Open();
            SqlTransaction st = conn.BeginTransaction();
            comm.Transaction = st;
            try
            {
                //第一步：写入教师试卷表,得到试卷ｉｄ
                comm.CommandText = "insert into tb_teachershijuan(teacherusername,ceshiname,createtime,timelength,manfen,mingtifangshi,kechengid,ceshibanji,banjiid,yunxuzuoti,yunxuchakan,xianzhiip) values('" + username + "','" + ceshiname + "','" + DateTime.Now.ToString() + "'," + ceshishichang + "," + manfen + ",'" + mingtifangshi + "'," + kechengid + ",'" + ceshibanji + "'," + banjiid + ",'" + yunxuzuoti + "','"+yunxuchakan+"','"+xianzhiip+"') select @@identity as shijuanid";
                shijuanid =Convert.ToInt32(comm.ExecuteScalar());
                ////写入组卷知识点
                //第二步：写入教师试卷题目表
                if (mingtifangshi == "全体学生相同" || mingtifangshi == "对学生保密")
                {
                    tihao = 1;
                    for (int i = 0; i < tixingXuanzeList.Count; i++)
                        for (int j = 0; j < xuanzhongTihaoList[i].Count; j++)
                        {
                            comm.CommandText = "insert into tb_teachershijuantimu(shijuanid,questionid,fenzhi,tihao) values(" + shijuanid + "," + beixuanTihaoList[i][xuanzhongTihaoList[i][j]]+ "," + tixingfenzhiList[i] + "," + tihao + ")";
                            comm.ExecuteNonQuery();
                            tihao++;
                        }
                }
                //第三步：写入学生试卷表
                if (mingtifangshi == "全体学生相同" || mingtifangshi == "全体学生不同")
                {
                    for (int i = 0; i < stuusernameList.Count; i++)
                    {
                        comm.CommandText = "insert into tb_studentkaoshi(shijuanid,studentusername,zongfen,yunxu,kechengid,banjiid,jiaojuan) values(" + shijuanid + ",'" + stuusernameList[i] + "',0,'" + yunxuzuoti + "'," + kechengid + "," + banjiid + ",'未开始')";
                        comm.ExecuteNonQuery();
                        studentzujuanxinxi.Append(studentxuhaoList[i].ToString() + ",姓名：" + studentxingmingList[i] + ",学号：" + stuusernameList[i] + ";</br>");
                    }
                }
                else
                {
                    studentzujuanxinxi.Append("组卷类型为只组一套卷，因此没有学生试卷，此试卷对学生报密。");
                }
                //第四步：写入学生试题表
                if (mingtifangshi == "全体学生相同")
                {
                    foreach (string stuusername in stuusernameList)
                    {
                        tihao = 1;
                        for (int i = 0; i < tixingXuanzeList.Count; i++)
                            for (int j = 0; j < xuanzhongTihaoList[i].Count; j++)
                            {
                                comm.CommandText = "insert into tb_studentkaoshiti(studentusername,shijuanid,questionid,fenzhi,timuhao) values('" + stuusername + "'," + shijuanid + "," + beixuanTihaoList[i][xuanzhongTihaoList[i][j]]+ "," + tixingfenzhiList[i] + "," + tihao + ")";
                                comm.ExecuteNonQuery();
                                tihao++;
                            }
                    }
                }
                else if (mingtifangshi == "全体学生不同")
                {
                    for (int k = 0; k < stuusernameList.Count; k++)
                    {
                        tihao = 1;
                        for (int i = 0; i < tixingXuanzeList.Count; i++)
                            for (int j = 0; j < tixingshuliangList[i]; j++)
                            {
                                comm.CommandText = "insert into tb_studentkaoshiti(studentusername,shijuanid,questionid,fenzhi,timuhao) values('" + stuusernameList[k] + "'," + shijuanid + "," + beixuanTihaoList[i][stusuijiXuantiList[k, i][j]] + "," + tixingfenzhiList[i] + "," + tihao + ")";
                                comm.ExecuteNonQuery();
                                tihao++;
                            }
                    }
                }
                st.Commit();
                zujuanchenggong = true;
            }
            catch (Exception ex)
            {
                st.Rollback();
                zujuanchenggong = false;
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('组卷失败！原因：" + ex.Message + ".');</script>", false);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            if (zujuanchenggong)
            {
                Wizard1.ActiveStepIndex = 3;
                Labelceshimingcheng.Text =TextBoxceshimingcheng.Text;
                Labelkecheng.Text = KechengInfo.getKechengname(kechengid);
                Labelzujuanfangshi.Text = RadioButtonListzujuanfangshi.SelectedValue;
                Labelzhishidian.Text = TreeView1.CheckedNodesText;
                Labelyunxu.Text =RadioButtonList9.SelectedValue;//是否允许做题
                Labelshichang.Text =DropDownList4.SelectedValue;//时长
                Labelbanji.Text = DropDownList_banji.SelectedItem.Text;//班级
                Labeltixing.Text = HiddenFieldtixings.Value;
                Literalstudentxiangxi.Text = studentzujuanxinxi.ToString();
                Labelxianzhiip.Text = (chbxianzhiip.Checked ? "限制IP,学生考试时IP地址必须唯一" : "不限制IP");
                Labelyunxuchakan.Text = yunxuchakan;
           }

        }//end of全自动组卷
    }
    protected void JisuanZongfen(object sender, EventArgs e)//计算总分的同时显示种题型应选择多少道题,选择了哪些题型
    {
        StringBuilder tixings = new StringBuilder();
        DropDownList tixuzeshuDropdownList;
        DropDownList fenzhiDropdownList;
        int zongfen = 0;        
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select mingcheng from tb_timuleixing order by tixingid";
        conn.Open();
        SqlDataReader sdr = comm.ExecuteReader();
        while (sdr.Read())
        {
            tixuzeshuDropdownList = (DropDownList)(PlaceHolder1.FindControl("tixing" + sdr.GetString(0).Trim()));
            if (tixuzeshuDropdownList != null&&tixuzeshuDropdownList.SelectedIndex>0)
            {
                tixings.Append(sdr.GetString(0)+",");
                fenzhiDropdownList = (DropDownList)(PlaceHolder1.FindControl("timufenzhi" + sdr.GetString(0).Trim()));
                if (fenzhiDropdownList != null)
                {
                    zongfen += int.Parse(tixuzeshuDropdownList.SelectedValue) * int.Parse(fenzhiDropdownList.SelectedValue);
                }
            }
        }
        sdr.Close();
        conn.Close();
        TextBoxzongfen.Text = zongfen.ToString();
        HiddenFieldtixings.Value = tixings.ToString();//选择了哪些题型
    }
    protected void PlaceHolder1_DataBinding(object sender, EventArgs e)//显示题目数量，显示题目数量、分值供选择
    {
        //获取选择的知识点及其下位知识点
        List<string> nodeandchilren = TreeView1.CheckedNodesAndChildrenIds; //选中的知识点及其子孙知识点的id
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        //从题库中查询符合条件的题目
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        //建立临时表，记录选择的知识点id号 
        string temptablename = "temp_table" + username + DateTime.Now.Ticks.ToString();
        comm.CommandText = "create table " +temptablename + "(id int)";
        conn.Open();
        comm.ExecuteNonQuery();
        foreach (string nodeid in nodeandchilren)
        {
            comm.CommandText = "insert into " +temptablename + "(id) values(" + int.Parse(nodeid) + ")";
            comm.ExecuteNonQuery();
        }
        //查询符合条件的题目和题型
        Literal tixingliteral;
        DropDownList timuxuanzeshuliangdropdownlist;
        DropDownList timufenzhi;
        int timuzongshu = 0;//符合条件的题目总数
        comm.CommandText = "select type,count(type) from tb_tiku where questionid in( select questionid from tb_timuzhishidian where questionid not in ( select questionid from tb_timuzhishidian where kechengjiegouid not in (select id from " +temptablename + ")))  group by type";
        //显示每种题型的数量和选项
        SqlDataReader sdr = comm.ExecuteReader();
        if (sdr.HasRows)
        {
            while (sdr.Read())
            {
                if (sdr.GetInt32(1) > 0)
                {
                    timuzongshu += sdr.GetInt32(1);
                    tixingliteral = new Literal();
                    tixingliteral.Text = sdr.GetString(0) + "共有" + sdr.GetInt32(1).ToString() + "道,需要选择";
                    PlaceHolder1.Controls.Add(tixingliteral);
                    timuxuanzeshuliangdropdownlist = new DropDownList();
                    timuxuanzeshuliangdropdownlist.ID = "tixing" + sdr.GetString(0).Trim();
                    for (int i = 0; i <= sdr.GetInt32(1); i++)
                        timuxuanzeshuliangdropdownlist.Items.Add(i.ToString());
                    timuxuanzeshuliangdropdownlist.AutoPostBack = false;// true;
                    PlaceHolder1.Controls.Add(timuxuanzeshuliangdropdownlist);
                    tixingliteral = new Literal();
                    tixingliteral.Text = "道，每题";
                    PlaceHolder1.Controls.Add(tixingliteral);
                    timufenzhi = new DropDownList();
                    timufenzhi.ID = "timufenzhi" + sdr.GetString(0).Trim();
                    for (int i = 1; i <= 100; i++)
                    {
                        timufenzhi.Items.Add(i.ToString());
                    }
                    timufenzhi.AutoPostBack = false;// true;
                    PlaceHolder1.Controls.Add(timufenzhi);
                    tixingliteral = new Literal();
                    tixingliteral.Text = "分。<br>";
                    PlaceHolder1.Controls.Add(tixingliteral);
                }
            }
        }
        sdr.Close();
        try
        {
            comm.CommandText = "drop table " + temptablename;
            comm.ExecuteNonQuery();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    protected void FinishButton_Click(object sender, EventArgs e)//开始选题,手工组卷和半自动组卷
    {
        //收集用户选择的信息
        string kechengid = Session["kechengid"].ToString();
        string kechengname = KechengInfo.getKechengname(kechengid);
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string ceshiname = TextBoxceshimingcheng.Text.Trim();//测试名称
        string xianzhiip = (chbxianzhiip.Checked ? "是" : "否");//是否限制IP
        if (CheckCeshiName(int.Parse(kechengid), ceshiname, ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name))//如果该课程该试卷名称已存在，提示，返回
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('您的该课程的该试卷名称已使用，请选择其它名称。');</script>", false);
            return;
        }
        string ceshibanji = DropDownList_banji.SelectedItem.Text;//班级名称
        string banjiid = DropDownList_banji.SelectedValue;//班级ID
        string zujuanfangshi = RadioButtonListzujuanfangshi.SelectedValue.Trim();//组卷方式，三种
        string xuantifangshi = RadioButtonListxuantifangshi.SelectedValue.Trim();//选题方式，两种+1zhong
        int ceshishichang = int.Parse(DropDownList4.SelectedValue);//测试时长，分钟
        string shifouyunxu = RadioButtonList9.SelectedValue;//是否允许做题
        //获取测试题型
        List<string> tixingsList = new List<string>();//要选择哪些题型
        string[] nodeidsshuzu = HiddenFieldnodeandchildren.Value.Split(',');
        string[] tixingshuzu = HiddenFieldtixings.Value.Split(',');
        foreach (string tixing in tixingshuzu)
        {
            if (tixing.Length > 0)
                tixingsList.Add(tixing);
        }
        //检查选择的题目数是否符合要求
        List<int>[] xuanzetihaoList = new List<int>[tixingsList.Count];//使用List记录每种题型选择的题号,questionid
        for (int i = 0; i < tixingshuzu.Length - 1; i++)
        {
            xuanzetihaoList[i] = new List<int>();
        }
        int manfen = 100;//计划的满分分值
        
        int[,] timushufenzhi = new int[tixingsList.Count, 4];//题库中现有的题目数，要选择的题目数，分值，实际选择的题目数
        //统计每种题型计划选择的题目数和预计分值
        for (int i = 0; i < tixingsList.Count; i++)
        {
            timushufenzhi[i, 0] = ((DropDownList)(PlaceHolder1.FindControl("tixing" + tixingsList[i]))).Items.Count - 1;
            timushufenzhi[i, 1] = int.Parse(((DropDownList)(PlaceHolder1.FindControl("tixing" + tixingsList[i]))).SelectedValue);
            timushufenzhi[i, 2] = int.Parse(((DropDownList)(PlaceHolder1.FindControl("timufenzhi" + tixingsList[i]))).SelectedValue);
        }
        #region 获取选择的题号、题目数量
        //获取选择的题号、题目数量
        string xuanzetihaoliebiao = Labelhidden.Value;//选择的题号questionid,中间用两个逗号分割
        string[] xuanzetihao;//选择的题号数组,总的，所有的
        xuanzetihao = xuanzetihaoliebiao.Split(',');//选择的题号数组，字符串，因为题号间用,,分割，所以该数组中有很多空字符串
        int tixingIndex;
        string timutixing;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        for (int i = 0; i < xuanzetihao.Length; i++)//统计选择了多少道题
        {
            if (xuanzetihao[i].Length > 0)
            {
                //得到该题的题型
                comm.CommandText = "select type from tb_tiku where questionid=" + xuanzetihao[i];
                timutixing = comm.ExecuteScalar().ToString();
                tixingIndex = tixingsList.IndexOf(timutixing);
                //统计每种题型选择了多少道题
                timushufenzhi[tixingIndex, 3]++;
                //将选中的题目的questionid添加到对应列表中
                xuanzetihaoList[tixingIndex].Add(int.Parse(xuanzetihao[i]));//每种题型选择的questionid
            }
        }
        conn.Close();
        #endregion
        #region 判断是否按计划选够了题目
        //判断是否按计划选够了题目
        string xuantixinxi = "";
        bool xuangoutimu = true;//已选够了题目
        if (xuantifangshi == "0")//按所选题目组卷，选题数量应等于计划数量
        {
            for (int i = 0; i <tixingsList.Count; i++)
            {
                if (timushufenzhi[i, 3] != timushufenzhi[i, 1])
                {
                    xuantixinxi +=tixingsList[i] + "计划选择" + timushufenzhi[i, 1].ToString() + "道，实际选择" + timushufenzhi[i, 3].ToString() + "道。";
                    if (timushufenzhi[i, 3] > timushufenzhi[i, 1])
                    {
                        xuantixinxi += "多选了" + (timushufenzhi[i, 3] - timushufenzhi[i, 1]).ToString() + "道，请去掉" + (timushufenzhi[i, 3] - timushufenzhi[i, 1]).ToString() + "道。";
                    }
                    else
                    {
                        xuantixinxi += "少选了" + (timushufenzhi[i, 1] - timushufenzhi[i, 3]).ToString() + "道，请再选择" + (timushufenzhi[i, 1] - timushufenzhi[i, 3]).ToString() + "道。";

                    }
                    xuangoutimu = false;
                }
            }
        }
        else//随机组卷，选题数量应大于等于计划数量
        {
            for (int i = 0; i <tixingsList.Count; i++)
            {
                if (timushufenzhi[i, 3] < timushufenzhi[i, 1])
                {
                    xuantixinxi += tixingsList[i] + "计划选择" + timushufenzhi[i, 1].ToString() + "道，实际选择" + timushufenzhi[i, 3].ToString() + "道。数量不足，请至少再选择" + (timushufenzhi[i, 1] - timushufenzhi[i, 3]).ToString() + "道。";
                    xuangoutimu = false;
                }
            }
        }
        if (!xuangoutimu)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('" + xuantixinxi + "');</script>", false);
            return;
        }
        #endregion
        #region 随机选题
        int xuanzedetihao;
        comm.CommandText = "select count(studentusername) from tb_banjistudent where banjiid=" + banjiid;
        conn.Open();
        int studentRenshu = (int)(comm.ExecuteScalar()); //查询学生数量
        conn.Close();
        List<int>[,] stusuijiXuantiList = new List<int>[studentRenshu,tixingsList.Count];//记录为学生随机选择的题目,记录题型i随机选择的题目在题目ID列表中的下标
        List<int>[] teasuijiXuantiList = new List<int>[tixingsList.Count];//记录为教师随机选择的题目编号
        Random rand = new Random();
        int yixuantimushu = 0;
        if (xuantifangshi == "1")//随机选题//为每个学生随机选择的题目
        {
            if (zujuanfangshi == "全体学生不同")//为每个学生随机选择的题目
            {
                for (int i = 0; i < studentRenshu; i++)
                    for (int j = 0; j <tixingsList.Count; j++)
                    {
                        stusuijiXuantiList[i, j] = new List<int>();
                        yixuantimushu = 0;
                        while (yixuantimushu < timushufenzhi[j, 1])
                        {
                            xuanzedetihao = rand.Next(timushufenzhi[j, 3]);
                            if (!stusuijiXuantiList[i, j].Contains(xuanzedetihao))
                            {
                                stusuijiXuantiList[i, j].Add(xuanzedetihao);
                                yixuantimushu++;
                            }
                        }
                    }
            }
            else//只组一套卷或全体学生相同
            {
                for (int i = 0; i < teasuijiXuantiList.Length; i++)
                    teasuijiXuantiList[i] = new List<int>();//记录题型i随机选择的题目在题目ID列表中的下标

                for (int i = 0; i < tixingshuzu.Length - 1; i++)
                {
                   yixuantimushu = 0;
                    while (yixuantimushu < timushufenzhi[i, 1])
                    {
                        xuanzedetihao = rand.Next(timushufenzhi[i, 3]);
                        if (!teasuijiXuantiList[i].Contains(xuanzedetihao))
                        {
                            teasuijiXuantiList[i].Add(xuanzedetihao);
                            yixuantimushu++;
                        }
                    }
                }
            }
        }
        #endregion
        //生成试卷，写入数据库

        int shijuanid;//试卷ID号
        int tihao;
        //查看该试卷题目是否可用
        //开始事务
        string zujuanxinxi = "";
        bool chenggong = false;
        List<string> stuusernameList = new List<string>();
        List<int> stuxuhaoList = new List<int>();
        List<string> stuxingmingList = new List<string>();
        StringBuilder zujuanxinxiSB = new StringBuilder();
        conn.Open();
        comm.CommandText = "select tb_banjistudent.xuhao,tb_banjistudent.studentusername,tb_student.xingming from tb_banjistudent,tb_student where tb_banjistudent.banjiid=" + banjiid+" and tb_banjistudent.studentusername=tb_student.username";
        SqlDataReader sdr = comm.ExecuteReader();
        while (sdr.Read())
        {
            stuxuhaoList.Add(sdr.GetInt32(0));
            stuusernameList.Add(sdr.GetString(1));
            stuxingmingList.Add(sdr.GetString(2));
        }
        sdr.Close();
        //conn.Close();
        //将试卷、试题写入数据库
        //conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            //第一步，写入教师试卷表,得到试卷id
            comm.CommandText = "insert into tb_teachershijuan(teacherusername,ceshiname,kechengid,ceshizhishidian,timelength,manfen,mingtifangshi,yunxuzuoti,ceshibanji,banjiid,yunxuchakan,xianzhiip) values('" + username + "','" + ceshiname + "'," + kechengid + ",'" +HiddenFieldzhishidianname.Value + "'," +ceshishichang + "," + manfen + ",'" + zujuanfangshi + "','" +shifouyunxu+ "','" + ceshibanji + "'," + banjiid + ",'"+RadioButtonList10.SelectedValue+"','"+xianzhiip+"') select @@identity as shijuanid";
            shijuanid =Convert.ToInt32(comm.ExecuteScalar());//
            //第二步，写入教师题目表
            if (zujuanfangshi == "全体学生相同" || zujuanfangshi == "对学生保密")//根据组卷方式选题，全体学生相同和只组一套卷类似，都是只选一套题
            {
                if (xuantifangshi == "0")//严格按照所选题目组卷
                {
                    tihao = 1;
                    for (int i = 0; i <tixingsList.Count; i++)
                        for (int j = 0; j < xuanzetihaoList[i].Count; j++)
                        {
                            comm.CommandText = "insert into tb_teachershijuantimu(shijuanid,questionid,fenzhi,tihao) values(" + shijuanid + "," + xuanzetihaoList[i][j] + "," + timushufenzhi[i, 2] + "," + tihao + ")";
                            comm.ExecuteNonQuery();
                            tihao++;
                        }
                }
                else//随机选题方式
                {
                    tihao = 1;
                    for (int i = 0; i < tixingsList.Count; i++)
                        for (int j = 0; j <timushufenzhi[i,1]; j++)
                        {
                            //关于题目的questionid,选择的题目ID在xuzetihaoList[]中，每个数组元素是一个ArrayList,
                            //每个ArrayList记录了一种题型选择的questionid
                            //随机选题时，在teasuijiXuantiList中记录了选中的题目在对应ArrayList中的位置
                            //因此，要获取选中的题目的questionid,使用：(int)(xuzetihaoList[i][(int)(teasuijiXuantiList[i][j])])
                            comm.CommandText = "insert into tb_teachershijuantimu(shijuanid,questionid,fenzhi,tihao) values(" + shijuanid + "," + xuanzetihaoList[i][teasuijiXuantiList[i][j]] + "," + timushufenzhi[i, 2] + "," + tihao + ")";
                            comm.ExecuteNonQuery();
                            tihao++;
                        }
                }
            }
            //第三步：写入学生试卷表
            if (zujuanfangshi == "全体学生相同" || zujuanfangshi == "全体学生不同")//有学生的试卷
            {
                for ( int i=0;i<stuusernameList.Count;i++)
                {
                    zujuanxinxiSB.Append("序号："+stuxuhaoList[i]+",姓名："+stuxingmingList[i]+",学号："+stuusernameList[i]+";<br/>");
                    comm.CommandText = "insert into tb_studentkaoshi(shijuanid,studentusername,zongfen,yunxu,kechengid,banjiid) values(" + shijuanid + ",'" +stuusernameList[i] + "',0,'" +shifouyunxu + "'," + kechengid + "," + banjiid + ")";
                    comm.ExecuteNonQuery();
                }
            }
            else
            {
                zujuanxinxiSB.Append("组卷方式为只组一套卷，对学生保密，用于生成纸质试卷，因此没有为学生生成试卷。");
            }
            //第四步：试题写入学生试题表
            if (zujuanfangshi == "全体学生相同")
            {
                if (xuantifangshi == "0")//严格组卷
                {
                    foreach (string suname in stuusernameList)
                    {
                        tihao = 1;
                        for (int i = 0; i < tixingsList.Count; i++)
                            for (int j = 0; j <timushufenzhi[i,1]; j++)
                            {
                                comm.CommandText = "insert into tb_studentkaoshiti(studentusername,shijuanid,questionid,fenzhi,timuhao) values('" + suname + "'," + shijuanid + "," + xuanzetihaoList[i][j] + "," + timushufenzhi[i, 2] + "," + tihao + ")";
                                comm.ExecuteNonQuery();
                                tihao++;
                            }
                    }
                }
                else//随机选题方式
                {
                    foreach (string suname in stuusernameList)
                    {
                        tihao = 1;
                        for (int i = 0; i <tixingsList.Count; i++)
                            for (int j = 0; j <timushufenzhi[i,1]; j++)
                            {
                                comm.CommandText = "insert into tb_studentkaoshiti(studentusername,shijuanid,questionid,fenzhi,timuhao) values('" + suname + "'," + shijuanid + "," + xuanzetihaoList[i][teasuijiXuantiList[i][j]] + "," + timushufenzhi[i, 2] + "," + tihao + ")";
                                comm.ExecuteNonQuery();
                                tihao++;
                            }
                    }

                }
            }
            else if (zujuanfangshi == "全体学生不同")
            {
                if (xuantifangshi == "1")//随机选题
                {
                    for (int i = 0; i < stuusernameList.Count; i++)
                    {
                        tihao = 1;
                        for (int j = 0; j <tixingsList.Count; j++)
                        {
                            for (int k = 0; k <timushufenzhi[j,1]; k++)
                            {
                                comm.CommandText = "insert into tb_studentkaoshiti(studentusername,shijuanid,questionid,fenzhi,timuhao) values('" + stuusernameList[i] + "'," + shijuanid + "," + xuanzetihaoList[j][stusuijiXuantiList[i, j][k]] + "," + timushufenzhi[j, 2] + "," + tihao + ")";
                                comm.ExecuteNonQuery();
                                tihao++;
                            }
                        }
                    }
                }
            }
            st.Commit();
            chenggong = true;
        }
        catch (Exception ex)
        {
            st.Rollback();
            zujuanxinxi = ex.Message;
            chenggong = false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        if (chenggong)//组卷成功，显示汇总信息，跳转到完成/跳转到Completestep,显示组卷信息
        {
            Labelkecheng.Text = kechengname;
            Labelbanji.Text = DropDownList_banji.SelectedItem.Text;
            Labelshichang.Text = ceshishichang.ToString();
            Labelceshimingcheng.Text = ceshiname;
            Labeltixing.Text = HiddenFieldtixings.Value;
            Labelyunxu.Text = shifouyunxu;
            Labelzujuanfangshi.Text = zujuanfangshi;
            Literalstudentxiangxi.Text = zujuanxinxiSB.ToString();
            Labelzhishidian.Text =TreeView1.CheckedNodesText;
            Labelyunxuchakan.Text = RadioButtonList10.SelectedValue;
            Labelxianzhiip.Text = (chbxianzhiip.Checked ? "限制IP,学生考试时IP地址必须唯一" : "不限制IP");
            Wizard1.ActiveStepIndex = 3;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('" + zujuanxinxi + "');</script>", false);
        }
    }
    protected void RadioButtonListxuantifanshi_SelectedIndexChanged(object sender, EventArgs e)//根据选题方式设置组卷方式哪些不可用
    {
        switch (RadioButtonListxuantifangshi.SelectedIndex)
        {
            case 0:
                RadioButtonListzujuanfangshi.Items[1].Enabled = false;
                RadioButtonListzujuanfangshi.SelectedIndex = 0;
                break;
            default:
                RadioButtonListzujuanfangshi.Items[1].Enabled = true;
                RadioButtonListzujuanfangshi.SelectedIndex = 0;
                break;
        }
    }
    protected DataTable GetTimuDataTable(List<string> nodeandchildren, List<string> tixings)//根据知识点和题型搜题目
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        conn.Open();
        ////建立临时表，表中包含所选择的知识点id
        string temptablename = "temp_table" + username + DateTime.Now.Ticks.ToString();
        comm.CommandText = "create table " +temptablename + "(id int)";
        comm.ExecuteNonQuery();
        foreach (string nodeid in nodeandchildren)
        {
            comm.CommandText = "insert into " +temptablename + "(id) values(" +nodeid + ")";
            comm.ExecuteNonQuery();
        }
        conn.Close();
        string timusql = "select questionid as 题号,type as 题型,timu as 题目,answer as 答案,filepath as 资源文件 from tb_tiku where";
        for (int i = 0; i < tixings.Count; i++)//添加题型条件
        {
            if (i == 0)
            {
                timusql += "(type='" +tixings[i] + "'";
            }
            else
            {
                timusql += " or type='" + tixings[i] + "'";
            }
        }
        timusql += ")";
        timusql += " and questionid in (select questionid from tb_timuzhishidian where questionid not in ( select questionid from tb_timuzhishidian where kechengjiegouid not in (select id from " +temptablename + "))) order by type,questionid";
        comm.CommandText = timusql;
        SqlDataAdapter sda = new SqlDataAdapter(comm);
        sda.Fill(dt);
        try
        {
            conn.Open();
            comm.CommandText = "drop table " + temptablename;
            comm.ExecuteNonQuery();
        }
        finally
        {
            if(conn.State==ConnectionState.Open)
                conn.Close();
        }
        return dt;
    }
    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)//绑定数据行时，设置Checkbox的值和选中状态
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlInputCheckBox hicbox = new HtmlInputCheckBox();
            hicbox = (HtmlInputCheckBox)(e.Row.Cells[0].FindControl("Checkbox2"));
            if (Labelhidden.Value.IndexOf("," + ((Label)(e.Row.Cells[1].FindControl("Label1"))).Text + ",") >= 0)//.Value
            {
                hicbox.Checked = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)//分页
    {
        GridView1.PageIndex = e.NewPageIndex;
        string str = HiddenFieldnodeandchildren.Value;
        string[] s1 = str.Split(',');
        List<string> nodess = new List<string>();
        foreach (string s in s1)
        {
            if (s.Length > 0)
            {
                nodess.Add(s);
            }
        }
        List<string> tixings = new List<string>();
        str = HiddenFieldtixings.Value;
        s1 = str.Split(',');
        foreach (string s in s1)
        {
            if (s.Length > 0)
            {
                tixings.Add(s);
            }
        }
        GridView1.DataSource = GetTimuDataTable(nodess, tixings);
        GridView1.DataBind();
    }
}
