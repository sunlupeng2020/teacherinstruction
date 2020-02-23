<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshiqtzice.aspx.cs" Inherits="teachermanage_ceshiqtzice" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="某班学生自测信息" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        班级：<asp:Label ID="Labelbj" runat="server" Text="Label"></asp:Label>
        <br />
    </div>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="username" DataSourceID="SqlDataSourcestu" 
        ondatabound="GridView2_DataBound">
        <Columns>
            <asp:BoundField DataField="xuhao" HeaderText="编号" SortExpression="xuhao" />
            <asp:BoundField DataField="xingming" HeaderText="姓名" 
                SortExpression="xingming" />
            <asp:BoundField DataField="username" HeaderText="学号" ReadOnly="True" 
                SortExpression="username" />
            <asp:BoundField DataField="xingbie" HeaderText="性别" SortExpression="xingbie" />
            <asp:TemplateField HeaderText="自测次数">
                <ItemTemplate>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最高分">
                <ItemTemplate>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最低分">
                <ItemTemplate>
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="平均分">
                <ItemTemplate>
                    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="详情">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" 
                        CommandArgument='<%# Eval("username") %>' onclick="LinkButton1_Click">自测详情</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourcestu" runat="server" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        SelectCommand="SELECT tb_banjistudent.xuhao,[tb_Student].[username], [tb_Student].[xingming], [tb_Student].[xingbie] FROM [tb_Student] inner join tb_banjistudent on tb_banjistudent.studentusername=tb_student.username where tb_banjistudent.banjiid=@banjiid">
        <SelectParameters>
            <asp:QueryStringParameter Name="banjiid" QueryStringField="banjiid" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
