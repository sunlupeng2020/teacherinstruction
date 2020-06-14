<%@ Page Title="测试管理" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ceshidefault.aspx.cs" Inherits="teachermanage_ceshidefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 980px">
        <tr>
            <td style="width: 245px">
                选择班级</td>
            <td style="width: 245px">
                选择学生</td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="ListBoxbj" runat="server" AutoPostBack="True" 
                  DataTextField="banjiname" DataValueField="banjiid" Rows="8" Width="242px" 
                    onselectedindexchanged="ListBoxbj_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" 
                    DataTextField="stu" DataValueField="username" Rows="8" Width="242px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:LinkButton ID="LinkButtonqtzice" runat="server" 
                    onclick="LinkButtonqtzice_Click">全体学生自测情况</asp:LinkButton>
            </td>
            <td>
                
            </td>
            <td>
                <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click">学生自测情况统计</asp:LinkButton>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
  
    </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>


