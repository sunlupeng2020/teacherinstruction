<%@ Page Language="C#" MasterPageFile="~/manager/MasterPageManager.master" AutoEventWireup="true" CodeFile="gerenxinxi.aspx.cs" Inherits="manager_gerenxinxi" Title="修改密码" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 533px" align="center">
        <tr>
            <td colspan="2">
                <h4>修改个人密码</h4></td>
            <td colspan="1" style="width: 145px">
            </td>
        </tr>
        <tr>
            <td style="width: 105px; text-align: right" class="alignrighttd">
                旧密码：</td>
            <td style="width: 178px; text-align: left" class="alignlefttd">
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox></td>
            <td style="width: 145px; text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    ErrorMessage="请输入旧密码！" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 105px; text-align: right" class="alignrighttd">
                新密码：</td>
            <td style="width: 178px; text-align: left" class="alignlefttd">
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" MaxLength="25"></asp:TextBox></td>
            <td style="width: 145px; text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                    ErrorMessage="请输入新密码！" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 105px; height: 17px; text-align: right" class="alignrighttd">
                重复新密码：</td>
            <td style="width: 178px; height: 17px; text-align: left" class="alignlefttd">
                <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" MaxLength="25"></asp:TextBox></td>
            <td style="width: 145px; height: 17px; text-align: left">
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2"
                    ControlToValidate="TextBox3" ErrorMessage="两次输入的新密码必须一致！" Width="180px" 
                    Display="Dynamic" Height="42px"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"
                    ErrorMessage="请再次输入新密码！"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" Width="66px" /></td>
            <td colspan="1" style="width: 145px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Width="237px"></asp:Label></td>
            <td colspan="1" style="width: 145px">
            </td>
        </tr>
    </table>
</asp:Content>

