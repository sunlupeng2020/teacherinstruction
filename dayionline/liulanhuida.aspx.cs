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

public partial class onlinedayi_liulanhuida : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username_ol = string.Empty;
        string usershenfen_ol = string.Empty;
        try
        {
            username_ol = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            usershenfen_ol = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
        }
        catch
        {
        }
        if (username_ol == string.Empty)
        {
            wolaihuida.Visible = false;
            huidatable.Visible = false;
        }
        else
        {
            wolaihuida.Visible =true;
            huidatable.Visible = true;
        }

        if (!IsPostBack)
        {
            string username="";
            string shenfen="";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select username,shenfen from tb_wenti where wentiid=" + Request.QueryString["wentiid"];
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                username = sdr.GetString(0).Trim();
                shenfen = sdr.GetString(1).Trim();
            }
            sdr.Close();
            conn.Close();
            Labeltiwenzhe.Text = username;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)//提交回答
    {
        string username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
        string usershenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;

        if (username!= null)
        {
            string huida = FCKeditor1.Value.Trim();
            if (huida != null && huida != "" && huida.Length > 0)
            {
                int wentiid = 0;
                if (Request.QueryString["wentiid"] != null)
                {
                    wentiid = int.Parse(Request.QueryString["wentiid"]);
                }
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "insert into tb_huida(wentiid,huida,username,shenfen) values(" + wentiid + ",'" + huida + "','" + username + "','"+usershenfen+"')";
                try
                {
                    conn.Open();
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        string js = "<script language='javascript'>alert('提交成功，谢谢您的回答！');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", js, false);
                        Label1.Text = "提交成功，谢谢您的回答！";
                        DataList1.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    string js = "<script language='javascript'>alert('提交失败！');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", js, false);
                    Label1.Text = "提交失败！原因：" + ex.Message;
                }
                finally
                {
                    if (conn.State.ToString() == "Opened")
                    {
                        conn.Close();
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), null, "<script>alert('只有登录用户才有权回答问题！');</script>", false);
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)//对回答进行评价
    {
        string username = string.Empty;
        string usershenfen = string.Empty;
        try
        {
            username = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            usershenfen = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
        }
        catch
        {
        }
        if (username !=string.Empty)
        {
            bool isyipingjia = false;//是否已评价该回答
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            //获取回答id号
            string huidausername = "";
            string huidashenfen = "";
            int huidaid = 0;
            if (e.CommandArgument.ToString().Length > 0)
            {
                huidaid = int.Parse(e.CommandArgument.ToString());
                //获取回答者用户名、身份
                comm.CommandText = "select username,shenfen from tb_huida where huidaid='" + huidaid + "'";
                conn.Open();
                SqlDataReader sdr = comm.ExecuteReader();
                if (sdr.Read())
                {
                    huidausername = sdr.GetString(0).Trim();
                    huidashenfen = sdr.GetString(1).Trim();
                }
                sdr.Close();
                conn.Close();
                if (huidashenfen != "" && huidausername != "")
                {
                    if (huidashenfen == usershenfen && huidausername == username)
                    {
                        string js = "<script language='javascript'>alert('不能对自己的回答进行评价！');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", js, false);
                        Label1.Text = "不能对自己的回答进行评价！";
                        return;
                    }
                    //检查用户是否已评价过该回答
                    comm.CommandText = "select huidaid from tb_huidapingjia where username='" + username + "' and usershenfen='" + usershenfen + "' and huidaid=" + huidaid;
                    conn.Open();
                    sdr = comm.ExecuteReader();
                    if (sdr.HasRows)//如果已进行评价，不能再评
                    {
                        isyipingjia = true;
                        //conn.Close();
                    }
                    sdr.Close();
                    conn.Close();
                    if (isyipingjia)
                    {
                        string js = "<script language='javascript'>alert('您已经评价过该回答，不能重复评价！');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", js, false);
                        Label1.Text = "您已经评价过该回答，不能重复评价!";
                        return;
                    }
                    conn.Open();
                    SqlTransaction st = conn.BeginTransaction();
                    comm.Transaction = st;
                    switch (e.CommandName)
                    {
                        case "pingjiahuidahao":
                            try
                            {
                                comm.CommandText = "update tb_huida set pingjiahao=pingjiahao+1 where huidaid=" + huidaid;
                                comm.ExecuteNonQuery();
                                comm.CommandText = "insert into tb_huidapingjia(username,usershenfen,huidaid,pingjia) values('" + username + "','" + usershenfen + "'," + huidaid + ",'好')";
                                comm.ExecuteNonQuery();
                                st.Commit();
                                DataList1.DataBind();
                            }
                            catch (Exception ex)
                            {
                                st.Rollback();
                                string js = "<script language='javascript'>alert('评价失败！');</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "pingjiahao", js, false);
                                Label1.Text = "评价失败！原因：" + ex.Message;
                            }
                            finally
                            {
                                conn.Close();
                            }
                            break;
                        case "pingjiahuidabuhao":
                            try
                            {
                                comm.CommandText = "update tb_huida set pingjiabuhao=pingjiabuhao+1 where huidaid=" + huidaid;
                                comm.ExecuteNonQuery();
                                comm.CommandText = "insert into tb_huidapingjia(username,usershenfen,huidaid,pingjia) values('" + username + "','" + usershenfen + "'," + huidaid + ",'不好')";
                                comm.ExecuteNonQuery();
                                st.Commit();
                                DataList1.DataBind();
                            }
                            catch (Exception ex)
                            {
                                st.Rollback();
                                string js = "<script language='javascript'>alert('评价失败！');</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "pingjiabuhao", js, false);
                                Label1.Text = "评价失败！原因：" + ex.Message;
                            }
                            finally
                            {
                                conn.Close();
                            }
                            break;
                    }
                }
                //if (conn.State.ToString() == "Opened")
                //    conn.Close();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), null, "<script>alert('只有登录用户才有权回答问题！');</script>", false);
        }
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)//设置评价按钮可用状态
    {
        string username_ol = string.Empty;
        string usershenfen_ol = string.Empty;
        try
        {
            username_ol = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
            usershenfen_ol = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
        }
        catch
        {
        }

        if (username_ol !=string.Empty)
        {
            bool isbenrenhuida = false;//是否本人的回答
            bool isyipingjia = false;//是否已评价过
            string shenfen = ((Label)(e.Item.FindControl("shenfenLabel"))).Text.Trim();
            string username = ((Label)(e.Item.FindControl("usernameLabel"))).Text.Trim();
            string huidaid = ((Label)(e.Item.FindControl("Label_huidaid"))).Text.Trim();//回答ID
            if (username ==username_ol && shenfen ==usershenfen_ol)
            {
                isbenrenhuida = true;
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select huidaid from tb_huidapingjia where username='" +username_ol + "' and usershenfen='" +usershenfen_ol + "' and huidaid=" + huidaid;
            conn.Open();
            SqlDataReader sdr = comm.ExecuteReader();
            if (sdr.HasRows)//如果已进行评价，不能再评
            {
                isyipingjia = true;

                //conn.Close();
            }
            sdr.Close();
            conn.Close();
            if (isbenrenhuida || isyipingjia)
            {
                ((LinkButton)(e.Item.FindControl("LinkButton1"))).Enabled = false;
                ((LinkButton)(e.Item.FindControl("LinkButton2"))).Enabled = false;
            }
            else
            {
                ((LinkButton)(e.Item.FindControl("LinkButton1"))).Enabled = true;
                ((LinkButton)(e.Item.FindControl("LinkButton2"))).Enabled = true;
            }
        }
        else
        {
            ((LinkButton)(e.Item.FindControl("LinkButton1"))).Enabled = false;
            ((LinkButton)(e.Item.FindControl("LinkButton2"))).Enabled = false;
        }        
    }
}
