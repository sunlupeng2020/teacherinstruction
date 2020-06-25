using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;

/// <summary>
///知识点类，包括课程、章、节、概念等
///可以包含若干子知识点
/// </summary>
public abstract class Knowladge
{
    private int id;

    protected int Id
    {
        get { return id; }
        set { id = value; }
    }
    protected string name;

    public string Name
    {
        get { return name; }
        set { this.name = value; }
    }

	public Knowladge(int id)
	{
        this.id = id;
	}
    public Knowladge(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
    public abstract void Add(Knowladge k);//增加子知识点、
    public abstract void Remove(Knowladge k);//移除  
}
public class ConcreteKnowladge : Knowladge
{
    private List<Knowladge> children;
    public string ZhishidianIds//知识点及其子知识点的id构成的字符串
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            this.GetZhishidianIds(sb);
            string zhishidianids = sb.ToString();
            return zhishidianids.Substring(0, zhishidianids.Length - 1);
        }
    }
    public ConcreteKnowladge(int id) : base(id) 
    {
    }
    /// <summary>
    /// 建立知识点的结构，把子孙知识点添加上
    /// </summary>
    public void CreateStruct()
    {
        this.children = new List<Knowladge>();
        //从库中查找其子知识点
        SqlDataReader sdr = SqlHelper.ExecuteReader(SqlDal.strConnectionString, CommandType.Text, "select kechengjiegouid,jiegouname from tb_kechengjiegou where shangwei=" + this.Id.ToString());
        while (sdr.Read())
        {
            children.Add(new ConcreteKnowladge((int)(sdr[0]), sdr[1].ToString()));
        }
        sdr.Close();
        foreach (ConcreteKnowladge ck in this.children)
        {
            ck.CreateStruct();
        }
    }
    /// <summary>
    /// 得到知识点及其子知识点的id构成的字符串
    /// </summary>
    /// <param name="sb">一个StringBuilder</param>
    /// <returns></returns>
    private void GetZhishidianIds(StringBuilder sb)
    {
        sb.Append(this.Id.ToString()+",");
        if (this.children == null)
        {
            this.CreateStruct();
        }
        foreach (ConcreteKnowladge ck in this.children)
        {
            ck.GetZhishidianIds(sb);
        }
    }
    public ConcreteKnowladge(int id,string name)
        : this(id)
    {
        this.name = name;
    }

    public override void Add(Knowladge k)
    {
        children.Add(k);
    }
    public override void Remove(Knowladge k)
    {
        children.Remove(k);
    }
    /// <summary>
    /// 得到该知识点对应的题目id，不包括下位知识点
    /// </summary>
    /// <returns></returns>
    
    //public List<int> GetTimuID()
    //{
    //    List<int> timuid = new List<int>();
    //    SqlDataReader sdr= SqlHelper.ExecuteReader(SqlDal.strConnectionString,CommandType.Text,"select questionid from tb_tiku where zhishidianid=" + this.Id.ToString());
    //    while(sdr.Read())
    //    {
    //        timuid.Add((int)(sdr[0]));
    //    }
    //    sdr.Close();
    //    return timuid;
    //}
    /// <summary>
    /// 得到知识点及其所有下级知识点对应的题目id，
    /// </summary>
    /// <returns></returns>
    //public override List<int> GetTimuIDs()
    //{
    //    List<int> TimuID = this.GetTimuID();
    //    List<int> TimuIDs = new List<int>();
    //    TimuIDs.AddRange(TimuID);
    //    foreach(ConcreteKnowladge k in this.children)
    //    {
    //        TimuIDs.AddRange(k.GetTimuIDs());
    //    }
    //    return TimuIDs;
    //}
    /// <summary>
    /// 得到知识点对应的题目
    /// </summary>
    /// <param name="xiaji">是否包括其所有下级知识点的题目</param>
    /// <returns></returns>
    public DataTable GetTimu(bool xiaji)
    {
        DataTable dt = new DataTable();
        if (xiaji)
        {
            dt = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, "select [questionid],[timu],[answer],[tigongzhe],[type],[shuoming] from tb_tiku where [zhishidianid] in (" + this.ZhishidianIds + ")").Tables[0];
        }
        else
        {
            dt = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, "select [questionid],[timu],[answer],[tigongzhe],[type],[shuoming]  from tb_tiku  where [zhishidianid] =" + this.Id.ToString()).Tables[0];
        }
        return dt;
    }
}