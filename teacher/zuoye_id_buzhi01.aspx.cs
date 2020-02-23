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
using System.Data.SqlTypes;
using System.Data.Sql;

public partial class teachermanage_zuoye_id_buzhi01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBanji();
        }
    }
    protected void grvw_zuoyetimu_RowDataBound(object sender, GridViewRowEventArgs e)//作业题目GridView,自动编号
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();//显示行号
        }
    }
    protected void BindBanji()//绑定任课班级
    {
        int kechengid;
        string zuoyeid = Request.QueryString["zuoyeid"];
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select kechengid from teacherzuoye where zuoyeid=" + zuoyeid;
        conn.Open();
        kechengid = (int)(comm.ExecuteScalar());
        conn.Close();
        DataSet ds = BanjiInfo.GetTeacherRenkeBanji(((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name, kechengid);
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "banjiname";
        DropDownList1.DataValueField = "banjiid";
        DropDownList1.DataBind();
    }
    protected void btn_buzhizuoye_Click(object sender, EventArgs e)//将作业布置给某个班
    {
        lbl_fankui.Text = "";
        string zuoyeid = Request.QueryString["zuoyeid"];
        if (ZuoyeInfo.GetTimuCount(zuoyeid) <= 0)//判断作业中有无题目
        {
            lbl_fankui.Text = "该作业中没有题目，无法布置该作业。";
            return;
        }
        string banjiid = DropDownList1.SelectedValue.Trim();
        if(BanjiInfo.GetStudentCount(int.Parse(banjiid))<=0)
        {
            lbl_fankui.Text=DropDownList1.SelectedItem.Text+"班没有学生，无法布置作业。";
            return;
        }
        string shangjiaoqixian = TextBox1.Text;
        string yunxu = RadioButtonList1.SelectedValue;
        string yunxuchakan = RadioButtonList2.SelectedValue;
        string kechengid = ZuoyeInfo.getZuoye_Kechengid(int.Parse(zuoyeid));
        DataTable studt = BanjiInfo.GetStudentUserName(int.Parse(banjiid));
        DataTable ZuoyeTimuTb = ZuoyeInfo.GetZuoyeTimuIdAndFenzhi(int.Parse(zuoyeid));
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
         SqlCommand comm = conn.CreateCommand(); 
        conn.Open();
        SqlTransaction st = conn.BeginTransaction();
        comm.Transaction = st;
        comm.CommandText = "insert into tb_zuoyebuzhi(zuoyeid,banjiid,yunxuzuoti,yunxuchakan,buzhishijian,shangjiaoqixian,teacherusername,shuoming,kechengid) values(@zuoyeid,@banjiid,@yunxuzuoti,@yunxuchakan,@buzhishijian,@shangjiaoqixian,@teacherusername,@shuoming,@kechengid)";
        comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
        comm.Parameters.AddWithValue("@banjiid", banjiid);
        comm.Parameters.AddWithValue("@yunxuzuoti", yunxu);
        comm.Parameters.AddWithValue("@yunxuchakan", yunxuchakan);
        comm.Parameters.AddWithValue("@buzhishijian", DateTime.Now);
        comm.Parameters.AddWithValue("@shangjiaoqixian", TextBox1.Text);
        comm.Parameters.AddWithValue("@teacherusername", ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name);
        comm.Parameters.AddWithValue("@shuoming", TextBox2.Text);
        comm.Parameters.AddWithValue("@kechengid", kechengid);
        try
        {
            comm.ExecuteNonQuery();
            //将作业分发给学生,写入学生作业表
            //删除以前布置给学生的该作业
            //comm.CommandText = "delete from tb_studentzuoye where zuoyeid=@zuoyeid and studentusername in(select studentusername from tb_banjistudent where banjiid=@banjiid)";
            //comm.ExecuteNonQuery();
            //删除题目
            //comm.CommandText = "delete from tb_stuzuoyetimu where zuoyeid=@zuoyeid and studentusername in(select studentusername  from tb_banjistudent where banjiid=@banjiid)";
            //comm.ExecuteNonQuery();
            //写入学生作业表
            comm.CommandText = "insert into tb_studentzuoye(zuoyeid,kechengid,studentusername,wancheng,zongfen,buzhishijian,shangjiaoqixian,shuoming,yunxuzuoti,yunxuchakan,teacherusername) values(@zuoyeid,@kechengid,@studentusername,'未完成',0,@buzhishijian,@shangjiaoqixian,@shuoming,@yunxuzuoti,@yunxuchakan,@teacherusername)";
            comm.Parameters.Add("@studentusername", SqlDbType.VarChar);
            foreach (DataRow sdr in studt.Rows)
            {
                comm.Parameters[9].Value = sdr[0].ToString();
                comm.ExecuteNonQuery();
            }
            //将题目写入学生作业题目表
            comm.Parameters.Clear();
            comm.CommandText = "insert into tb_stuzuoyetimu(zuoyeid,studentusername,questionid,defen,fenzhi) values(@zuoyeid,@studentusername,@questionid,0,@fenzhi)";
            comm.Parameters.AddWithValue("@zuoyeid", zuoyeid);
            comm.Parameters.Add("@studentusername", SqlDbType.VarChar);
            comm.Parameters.Add("@questionid", SqlDbType.Int);
            comm.Parameters.Add("@fenzhi", SqlDbType.Int);
            foreach (DataRow sdr in studt.Rows)
            {
                comm.Parameters[1].Value = sdr[0].ToString();
                foreach (DataRow dr in ZuoyeTimuTb.Rows)
                {
                    comm.Parameters[2].Value = Convert.ToInt32(dr[0]);
                    comm.Parameters[3].Value = Convert.ToInt32(dr[1]);
                    comm.ExecuteNonQuery();
                }
            }
            st.Commit();
            lbl_fankui.Text = "将作业布置给" + DropDownList1.SelectedItem.Text + ", 成功！";
        }
        catch
        {
            st.Rollback();
            lbl_fankui.Text = "布置失败！您是否已经给该班布置过该作业？" ;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}
