<%@ Page Language="C#" MasterPageFile="TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ziyuantongji.aspx.cs" Inherits="teachermanage_jiaoxueziyuan_ziyuantongji" Title="教学资源统计" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
       SelectCommand="SELECT [tb_Kecheng].[kechengname],tb_kecheng.creater,[tb_Kecheng].[createtime], (select count(questionid)  as 题目数  from tb_tiku  where kechengid=[tb_Kecheng].kechengid),(select count(jiaoxueziyuanid) as 资源数 from tb_jiaoxueziyuan  where  kechengid=[tb_Kecheng].kechengid)  FROM [tb_Kecheng]">
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="kechengname" DataSourceID="SqlDataSource1"  Width="700px">
        <Columns>
            <asp:BoundField DataField="kechengname" HeaderText="课程名称" ReadOnly="True" SortExpression="kechengname" />
            <asp:BoundField DataField="creater" HeaderText="创建者" SortExpression="creater" />
            <asp:BoundField DataField="Column1" HeaderText="题目数量" SortExpression="Column1" />
            <asp:BoundField DataField="Column2" HeaderText="教学资源数量" SortExpression="Column2" />
            <asp:BoundField DataField="createtime" HeaderText="创建时间"  SortExpression="createtime" />
        </Columns>
    </asp:GridView>
</asp:Content>

