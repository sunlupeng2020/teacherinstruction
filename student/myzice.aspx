<%@ Page Language="C#" MasterPageFile="~/student/StudentMasterPage.master" AutoEventWireup="true" CodeFile="myzice.aspx.cs" Inherits="student_myzice" Title="我的自测" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView id="GridView1" runat="server" ForeColor="#333333" Width="980px" 
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataKeyNames="测试ID" 
        CellPadding="4" EmptyDataText="没有您查询的测试数据。" AutoGenerateColumns="False" 
        AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdatabound="GridView1_RowDataBound" PageSize="5" 
>
<FooterStyle BackColor="#507CD1" Font-Bold="True"></FooterStyle>

<RowStyle BackColor="#EFF3FB"></RowStyle>
<Columns>
    <asp:BoundField />
    <asp:TemplateField HeaderText="测试ID" Visible="False">
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%# Bind("测试ID") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("测试ID") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>
<asp:BoundField DataField="测试名称" HeaderText="测试名称"></asp:BoundField>
<asp:BoundField DataField="测试时间" HeaderText="测试时间"></asp:BoundField>
    <asp:TemplateField HeaderText="成绩">
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%# Bind("得分") %>'></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("得分") %>'></asp:TextBox>
        </EditItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField ShowHeader="False">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                CommandArgument='<%# Eval("测试ID") %>' CommandName="Select" Text="查看该测试题目"></asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>

<PagerStyle HorizontalAlign="Center" BackColor="#2461BF"></PagerStyle>

<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#2461BF"></EditRowStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> 
        <asp:GridView id="GridView2" runat="server" ForeColor="#333333" Width="980px" 
            CellPadding="4" EmptyDataText="选择一个测试，才会显示题目信息。" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="题号" HeaderText="题号">
                    <ItemStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="题型" HeaderText="题型">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False">
                    <ItemStyle Width="600px" />
                </asp:BoundField>
                <asp:BoundField DataField="参考答案" HeaderText="参考答案">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="回答" HeaderText="回答">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
            </Columns>
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

<RowStyle BackColor="#EFF3FB"></RowStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#2461BF"></EditRowStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView>
</asp:Content>

