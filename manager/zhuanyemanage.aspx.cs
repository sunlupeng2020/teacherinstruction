﻿using System;
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

public partial class teachermanage_studentmanage_zhuanyemanage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        bool kejian = true;
        string zhuanye = TextBox1.Text.Trim();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString;
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select zhuanyeid from tb_zhuanye where zhuanyename='" + zhuanye+"'";
        conn.Open();
        SqlDataReader sdr = cmd.ExecuteReader();
        if (sdr.Read())
        {
            Label1.Text = "该专业已存在，请使用其他专业名称。";
            kejian = false;
        }
        sdr.Close();
        if(kejian)
        {
            cmd.CommandText = "insert into tb_zhuanye(zhuanyename) values('" + zhuanye + "')";
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    Label1.Text = "添加专业成功！";
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Label1.Text = "添加专业失败！原因：" + ex.Message;
            }
        }
        if(conn.State==ConnectionState.Open)
            conn.Close();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string zhuanyeid = e.Keys[0].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString);
        SqlCommand comm = conn.CreateCommand();
        comm.CommandText = "select count(studentid) from tb_student where zhuanyeid='" +zhuanyeid+ "'";
        conn.Open();
        int zhuanyeshu = (int)comm.ExecuteScalar();
        conn.Close();
        if (zhuanyeshu > 0)
        {
            e.Cancel = true;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('该专业有学生存在，不能删除!');", true);
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string zhuanyid = e.Keys[0].ToString();
        string zhuanyenewname = e.NewValues[0].ToString();
        string zhuanyeoldname = e.OldValues[0].ToString();
        if (zhuanyenewname == zhuanyeoldname)
        {
            e.Cancel = true;
        }
        else
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["kecheng2012ConnectionString"].ConnectionString);
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "select count(zhuanyeid) from tb_zhuanye where zhuanyename='" + zhuanyenewname + "' and  zhuanyeid<>" + zhuanyid;
            conn.Open();
            int n = (int)comm.ExecuteScalar();
            conn.Close();
            if (n > 0)
            {
                e.Cancel = true;
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "", "alert('该系部该专业名称已存在，更新失败!');", true);
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
}
