using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

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
    private string name;

    protected string Name
    {
        get { return name; }
    }

	public Knowladge(int id)
	{
        this.id = id;
        this.name =SqlHelper.ExecuteScalar(SqlDal.strConnectionString, CommandType.Text, "select jiegouname from tb_kechengjiegou where kechengjiegouid="+id.ToString()).ToString();
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
    private List<Knowladge> children = new List<Knowladge>();
    public ConcreteKnowladge(int id) : base(id) 
    { }
    public ConcreteKnowladge(int id,string name)
        : base(id, name)
    {
        children = new List<Knowladge>();
        //从库中查找其子知识点
        SqlDataReader sdr = SqlHelper.ExecuteReader(SqlDal.strConnectionString, CommandType.Text, "select kechengjiegouid,jiegouname from tb_kechengjiegou where shangwei=" + this.Id.ToString());
        while (sdr.Read())
        {
            this.Add(new ConcreteKnowladge((int)(sdr[0]),sdr[1].ToString()));
        }
        sdr.Close();
    }
    public override void Add(Knowladge k)
    {
        children.Add(k);
    }
    public override void Remove(Knowladge k)
    {
        children.Remove(k);
    }
    public override List<int> GetTimuID()
    {
        List<int> timuid = new List<int>();
        SqlDataReader sdr= SqlHelper.ExecuteReader(SqlDal.strConnectionString,CommandType.Text,"select questionid from shuati_tiku where zhishidianid=" + this.Id.ToString());
        while(sdr.Read())
        {
            timuid.Add((int)(sdr[0]));
        }
        sdr.Close();
        return timuid;
    }
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
}