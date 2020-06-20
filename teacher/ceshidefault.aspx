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
                    onselectedindexchanged="ListBoxbj_SelectedIndexChanged" 
                     AppendDataBoundItems="True">
                     <asp:ListItem Selected="True" Value="0">--选择班级--</asp:ListItem>
                 </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2">

                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>

            </td>
        </tr>
    </table>
  
    </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>


