<%@ Page Title="我的提问" Language="C#" MasterPageFile="~/dayionline/MasterPageDayi.master" AutoEventWireup="true" CodeFile="Mywenti.aspx.cs" Inherits="dayionline_Mywenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <hr />
    <asp:GridView id="GridView1" 
            runat="server" ForeColor="#333333" Width="728px" AutoGenerateColumns="False" 
            EmptyDataText="没有数据。" GridLines="None" CellPadding="4" 
            DataKeyNames="问题ID号" AllowPaging="True" 
    onpageindexchanging="GridView1_PageIndexChanging" 
    onrowdatabound="GridView1_RowDataBound">
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

<RowStyle BackColor="#EFF3FB"></RowStyle>
<Columns>
    <asp:BoundField />
<asp:BoundField DataField="问题ID号" HeaderText="问题号" Visible="False"></asp:BoundField>
<asp:HyperLinkField DataNavigateUrlFields="问题ID号" DataNavigateUrlFormatString="liulanhuida.aspx?wentiid={0}" DataTextField="问题标题" HeaderText="问题标题"></asp:HyperLinkField>
<asp:BoundField DataField="提问时间" HeaderText="提问时间"></asp:BoundField>
<asp:BoundField DataField="回答数" HeaderText="回答数"></asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#2461BF"></EditRowStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> 
</asp:Content>

