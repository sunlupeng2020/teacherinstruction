<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zuoyechengji.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="学生作业信息" Inherits="teachermanage_zuoyechengji" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div>
    <p>
       班级：<asp:Label ID="Labelbanji" runat="server"></asp:Label>
        ,作业名称：<asp:Label ID="Labelzuoyename" runat="server"></asp:Label>
        学生情况</p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                onrowdatabound="GridView1_RowDataBound" Width="685px">
            <Columns>
                <asp:BoundField />
                <asp:BoundField DataField="姓名" HeaderText="姓名" />
                <asp:BoundField DataField="学号" HeaderText="学号" />
                <asp:BoundField DataField="成绩" HeaderText="成绩" />
                <asp:BoundField DataField="上交时间" HeaderText="上交时间" />
                <asp:TemplateField HeaderText="评语">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("评语") %>' 
                            ToolTip='<%# Eval("评语") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("评语") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="300px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
    
    </div>
</asp:Content>
