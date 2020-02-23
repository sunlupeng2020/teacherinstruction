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
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;

public partial class teacher_ceshiqianyi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string shijuanid = Request.QueryString["shijuanid"];
            string sqltxt = "select banjiname from tb_banji where banjiid=(select banjiid from tb_teachershijuan where shijuanid=" + shijuanid + ")";
            lbl_yuanbanji.Text = SqlHelper.ExecuteScalar(SqlDal.conn, CommandType.Text, sqltxt).ToString();
            sqltxt = "select ceshiname,timelength,yunxuzuoti,yunxuchakan,xianzhiip,banjiid,mingtifangshi,ceshizhishidian from tb_teachershijuan where shijuanid=" + shijuanid;
            SqlDataReader sdr = SqlHelper.ExecuteReader(SqlDal.conn, CommandType.Text, sqltxt);
            if (sdr.Read())
            {
                lbl_yuanname.Text = tbx_ceshiname.Text = sdr[0].ToString();
                Ddl_fenzhong.SelectedValue = sdr[1].ToString();
                Rbl_yunxuceshi.SelectedValue = sdr[2].ToString();
                Rbl_yunxuchakan.SelectedValue = sdr[3].ToString();
                Rbl_xianzhiip.SelectedValue = sdr[4].ToString();
                lbl_mingtifangshi.Text = sdr[6].ToString();
                lbl_zhishidian.Text = sdr[7].ToString();
            }
            sdr.Close();
            BindBanji();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)//生成试卷
    {
        //检查测试名称是否可用
        //bool chenggong = true;
        int i,j;
        int shijuanid=0;//新试卷id
        //string sqltxt="";
        string kechengid = Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string ceshiname =tbx_ceshiname.Text.Trim();//测试名称
        string xianzhiip =Rbl_xianzhiip.SelectedValue;//是否限制IP
        string ceshizhishidian = lbl_zhishidian.Text;
        if (CheckCeshiName(int.Parse(kechengid), ceshiname, username))//检查测试名称是否可用
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('您的该课程的该试卷名称已使用，请选择其它名称。');</script>", false);
            return;
        }
        //创建新测试
        string zujuanfangshi = lbl_mingtifangshi.Text.Trim();
        string ceshibanji =ddl_banji.SelectedItem.Text;//班级名称
        string banjiid =ddl_banji.SelectedValue;//班级ID
        string xuantifangshi =lbl_mingtifangshi.Text.Trim();//选题方式，两种+1zhong
        int ceshishichang = int.Parse(Ddl_fenzhong.SelectedValue);//测试时长，分钟
        string shifouyunxu =Rbl_yunxuceshi.SelectedValue;//是否允许做题
        string yunxuchakan = Rbl_yunxuchakan.SelectedValue;//是否允许查看测试结果
        string oldshijuanid=Request.QueryString["shijuanid"];//原试卷id
        //DataTable dt = CeshiInfo.GetCeshiTimu(int.Parse(Request.QueryString["shijuanid"]));//原试卷中的题目
        DataTable stu=BanjiInfo.GetStudent(int.Parse(banjiid)); 
        DataTable tixingshuliang=new DataTable();//题型及数量
        DataTable timu_fenzhi=new DataTable();//题目及分值
        DataTable Bxtimu =GetBeixuanTimu(oldshijuanid);//备选题目
        int bxtimuzongshu = Bxtimu.Rows.Count;//备选题目总数
        int yixuantimushu = 0;//已选题目数
        int kexuantimushu = 0;//可选题目数
        int yingxuantimushu = 0;//应选题目数
        //DataTable tixing=
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        List<int> tihaolist = new List<int>() ;//可选题目在备选题目中的索引
        List<int> xuanzetihaolist=new List<int>();//选择的题号列表
        int xuanzetihao;//选择题号

        Random rand;
        //统计各题型题目数量\分值和出现过的题目，为随机组卷做准备
        if(zujuanfangshi=="全体学生不同")
        {
            tixingshuliang = GetShijuanTixing(oldshijuanid, stu.Rows[0]["username"].ToString());//题型及数量
        }
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        try
        {
            //第一步，写入教师试卷表,得到试卷id
            comm.CommandText = "insert into tb_teachershijuan(teacherusername,ceshiname,kechengid,ceshizhishidian,timelength,manfen,mingtifangshi,yunxuzuoti,ceshibanji,banjiid,yunxuchakan,xianzhiip) values('" + username + "','" + ceshiname + "'," + kechengid + ",'" +lbl_zhishidian.Text + "'," + ceshishichang + ",100,'" + zujuanfangshi + "','" + shifouyunxu + "','" + ceshibanji + "'," + banjiid + ",'" +Rbl_yunxuchakan.SelectedValue + "','" + xianzhiip + "') select @@identity as shijuanid";
            shijuanid = Convert.ToInt32(comm.ExecuteScalar());//
            //第二步，写入教师题目表
            if (zujuanfangshi == "全体学生相同" || zujuanfangshi == "对学生保密")//根据组卷方式选题，全体学生相同和只组一套卷类似，都是只选一套题
            {
                comm.CommandText = "insert into tb_teachershijuantimu(questionid,fenzhi,tihao,shijuanid) select questionid,fenzhi,tihao,"+shijuanid+" from tb_teachershijuantimu where shijuanid="+oldshijuanid;
                comm.ExecuteNonQuery();
                //为学生创建试卷：将试卷信息写入学生试卷表，将题目信息写入学生试卷题目表
                foreach(DataRow dr in stu.Rows)
                {
                    //将试卷信息写入学生试卷表
                    //zujuanxinxiSB.Append("序号：" + stuxuhaoList[i] + ",姓名：" + stuxingmingList[i] + ",学号：" + stuusernameList[i] + ";<br/>");
                    comm.CommandText = "insert into tb_studentkaoshi(shijuanid,studentusername,zongfen,yunxu,kechengid,banjiid) values(" + shijuanid + ",'" +dr["username"].ToString() + "',0,'" + shifouyunxu + "'," + kechengid + "," + banjiid + ")";
                    comm.ExecuteNonQuery();
                    //将题目信息写入学生试卷题目表
                    comm.CommandText = "insert into tb_studentkaoshiti(questionid,fenzhi,timuhao,shijuanid,studentusername) select questionid,fenzhi,tihao,"+shijuanid+",'"+dr["username"].ToString() + "' from tb_teachershijuantimu where shijuanid="+oldshijuanid; 
                    comm.ExecuteNonQuery();
                }
            }
            else//全体学生题目不同,不写入教师试卷题目表，只写学生试卷表和学生题目表
            {
                //写学生试卷表
                foreach(DataRow dr in stu.Rows)
                {
                    int tihao = 1;
                    //将试卷信息写入学生试卷表
                    //zujuanxinxiSB.Append("序号：" + stuxuhaoList[i] + ",姓名：" + stuxingmingList[i] + ",学号：" + stuusernameList[i] + ";<br/>");
                    comm.CommandText = "insert into tb_studentkaoshi(shijuanid,studentusername,zongfen,yunxu,kechengid,banjiid) values(" + shijuanid + ",'" +dr["username"].ToString() + "',0,'" + shifouyunxu + "'," + kechengid + "," + banjiid + ")";
                    comm.ExecuteNonQuery();
                    //为学生随机组卷，根据每种题型的题目数量，随机生成题目，写入学生考试题目表
                    foreach (DataRow dr1 in tixingshuliang.Rows)
                    {
                        tihaolist.Clear();;
                        for( i=0;i< bxtimuzongshu;i++)
                        {
                            if (Bxtimu.Rows[i]["type"].ToString() == dr1["type"].ToString())
                                tihaolist.Add(i);
                        }
                        //随机选题
                        kexuantimushu = tihaolist.Count;
                        xuanzetihaolist.Clear();
                        yixuantimushu = 0;
                        yingxuantimushu = Convert.ToInt32(dr1[1]);
                        rand = new Random();
                        while(yixuantimushu<yingxuantimushu)
                        {
                              xuanzetihao = rand.Next(kexuantimushu);
                              if (!xuanzetihaolist.Contains(xuanzetihao))
                              {
                                      xuanzetihaolist.Add(xuanzetihao);
                                      yixuantimushu++;
                              }
                        }
                    }
                    for (i = 0; i < yingxuantimushu; i++)
                    {
                        comm.CommandText = "insert into tb_studentkaoshiti(studentusername,shijuanid,questionid,fenzhi,timuhao) values('" + dr["username"].ToString() + "'," + shijuanid + "," + Bxtimu.Rows[tihaolist[xuanzetihaolist[i]]]["questionid"].ToString() + "," + Bxtimu.Rows[tihaolist[xuanzetihaolist[i]]]["fenzhi"].ToString() + "," + tihao.ToString() + ")";
                        comm.ExecuteNonQuery();
                        tihao++;
                    }

                }
                //统计各题型题目数量
                //搜索各题型出现过的题目
                //为学生随机组卷
            }
           
            st.Commit();
            lbl_fankui.Text = "试卷迁移成功！";
             ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('试卷迁移成功！');</script>", false);
    }
        catch (Exception ex)
        {
            st.Rollback();
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "<script language='javascript'>alert('试卷迁移失败！');</script>", false);
            lbl_fankui.Text = ex.Message;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        //将测试分发给新班级的所有学生
        //如果是随机命题，即题目是全体学生不同，则搜集所有学生的题目，再随机组卷，包括统计各题型题目数量
        //如果所有学生题目相同，则直接分发即可

    }
    protected void BindBanji()
    {
        string kechengid = Session["kechengid"].ToString();
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        DataSet ds = BanjiInfo.GetTeacherRenkeBanji(username, int.Parse(kechengid));
        ddl_banji.DataSource = ds;
        ddl_banji.DataValueField = "banjiid";
        ddl_banji.DataTextField = "banjiname";
        ddl_banji.DataBind();
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
    protected DataTable GetShijuanTixing(string  shijuanid,string stuusername)//得到试卷有哪些题型及题目数量
    {
        string sqltxt = "select [type],count(*) from tb_tiku where questionid in(select questionid from tb_studentkaoshiti where shijuanid=" + shijuanid+" and studentusername='"+stuusername+"') group by [type]";
        return SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt).Tables[0];
    }
   protected  DataTable GetTixing()
    {
        string sqltxt = "select tixingid,mingcheng from tb_timuliexing order by tixingid";
        return SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt).Tables[0];
    }
   protected DataTable GetBeixuanTimu(string shijuanid)//得到备选题目,从所有学生题目中选择
   {
       string sqltxt = "select distinct tb_studentkaoshiti.questionid,fenzhi,tb_tiku.[type] from tb_studentkaoshiti,tb_tiku  where tb_studentkaoshiti.shijuanid=" + shijuanid + " and tb_tiku.questionid=tb_studentkaoshiti.questionid";
       return SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, sqltxt).Tables[0];
   }
}
