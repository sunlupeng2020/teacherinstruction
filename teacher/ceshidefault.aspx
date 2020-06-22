<%@ Page Title="测试管理" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ceshidefault.aspx.cs" Inherits="teachermanage_ceshidefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 980px">
        <tr>
            <td style="width: 245px; text-align: right;">
                选择班级</td>
            <td style="width: 245px">
                 <asp:DropDownList ID="ddlbj" runat="server" AutoPostBack="True" 
                  DataTextField="banjiname" DataValueField="banjiid" Rows="8" Width="242px" 
                     AppendDataBoundItems="True">
                     <asp:ListItem Selected="True" Value="0">--选择班级--</asp:ListItem>
                 </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2"  align="center">
 <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="username" DataSourceID="SqlDataSourcestu" 
        ondatabound="GridView2_DataBound" Width="635px">
        <Columns>
            <asp:TemplateField HeaderText="编号">
            <ItemTemplate>
                <asp:Literal ID="Lbh" runat="server"></asp:Literal>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="username" HeaderText="学号" ReadOnly="True" 
                SortExpression="username" />
            <asp:BoundField DataField="xingming" HeaderText="姓名" 
                SortExpression="xingming" />
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
        SelectCommand="SELECT [username],[xingming], [xingbie] FROM [tb_Student] where username in(select studentusername from tb_banjistudent where banjiid=@banjiid) order by [username]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlbj" Name="banjiid" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

            </td>
        </tr>
    </table>
  
    </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>


