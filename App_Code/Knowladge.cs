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
    public abstract List<int> GetTimuID();//得到该知识点对应的题目的ID 
    public abstract List<int> GetTimuIDs();//得到该知识点及其子知识点对应的题目ID号    
}
public class ConcreteKnowladge : Knowladge
{
    private List<Knowladge> children;
    public ConcreteKnowladge(int id) : base(id) 
    {
        children = new List<Knowladge>();
        //从库中查找其子知识点
        SqlDataReader sdr = SqlHelper.ExecuteReader(SqlDal.strConnectionString, CommandType.Text, "select kechengjiegouid,jiegouname from tb_kechengjiegou where shangwei=" + this.Id.ToString());
        while (sdr.Read())
        {
            children.Add(new ConcreteKnowladge((int)(sdr[0]), sdr[1].ToString()));
        }
        sdr.Close();
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
    
    public override List<int> GetTimuID()
    {
        List<int> timuid = new List<int>();
        SqlDataReader sdr= SqlHelper.ExecuteReader(SqlDal.strConnectionString,CommandType.Text,"select questionid from tb_tiku where zhishidianid=" + this.Id.ToString());
        while(sdr.Read())
        {
            timuid.Add((int)(sdr[0]));
        }
        sdr.Close();
        return timuid;
    }
    /// <summary>
    /// 得到知识点及其所有下级知识点对应的题目id，
    /// </summary>
    /// <returns></returns>
    public override List<int> GetTimuIDs()
    {
        List<int> TimuID = this.GetTimuID();
        List<int> TimuIDs = new List<int>();
        TimuIDs.AddRange(TimuID);
        foreach(ConcreteKnowladge k in this.children)
        {
            TimuIDs.AddRange(k.GetTimuIDs());
        }
        return TimuIDs;
    }
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
            List<int> timuids = GetTimuIDs();
            string tds ="";
            foreach (int id in timuids)
                tds += id.ToString()+",";
            if (tds.Length > 0)
            {
                tds = tds.Substring(0, tds.Length - 1); 
                dt = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, "select [questionid],[timu],[answer],[tigongzhe],[type],[shuoming] from tb_tiku where [questionid] in (" + tds + ")").Tables[0];
            } 
        }
        else
        {
            dt = SqlHelper.ExecuteDataset(SqlDal.conn, CommandType.Text, "select [questionid],[timu],[answer],[tigongzhe],[type],[shuoming]  from tb_tiku  where [questionid] =" + this.Id.ToString()).Tables[0];
        }
        return dt;
    }
}