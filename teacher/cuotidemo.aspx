<%@ Page Title="" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="cuotidemo.aspx.cs" Inherits="cuotidemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:GridView ID="GridView1" runat="server" AllowPaging="True"  Width="1000px"
        AllowSorting="True" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" onrowdatabound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField></asp:TemplateField>
            <asp:BoundField DataField="timu" HeaderText="题目" SortExpression="timu" 
                HtmlEncode="False" />
            <asp:BoundField DataField="answer" HeaderText="参考答案" 
                SortExpression="answer" />
            <asp:BoundField DataField="huida" HeaderText="回答" SortExpression="huida" />
            <asp:BoundField DataField="type" HeaderText="题型" SortExpression="type" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        SelectCommand="SELECT [timu], [answer], [huida], [type] FROM [view_zicetimu] WHERE ([questionid] = @questionid and huida<>answer) ORDER BY [questionid], [huida]">
        <SelectParameters>
            <asp:QueryStringParameter Name="questionid" QueryStringField="questionid" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>

