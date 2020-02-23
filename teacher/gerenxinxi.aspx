<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="gerenxinxi.aspx.cs" Inherits="teachermanage_gerenxinxi" Title="修改登录密码" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 442px">
        <tr>
            <td colspan="2">
            <table>
            <tr>
            <td id="wangyezuocebiaoti" style="text-align: left; vertical-align:top; width: 120px;">
                <span style="font-family: 微软雅黑;font-size: 14px; color:White;"><strong>
                修改登录密码</strong></span>
            </td>
                <td style="height: 20px; text-align: center; color:Blue;">
                </td>
            </tr>
            </table> 
             </td>
            <td colspan="1" style="width: 129px">
            </td>
        </tr>
        <tr>
            <td style="width: 88px; text-align: right">
                旧密码：</td>
            <td style="width: 87px; text-align: left">
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox></td>
            <td style="width: 129px; text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    ErrorMessage="请输入旧密码！"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 88px; text-align: right" class="alignrighttd">
                新密码：</td>
            <td style="width: 87px; text-align: left">
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox></td>
            <td style="width: 129px; text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                    ErrorMessage="请输入新密码！"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 88px; height: 17px; text-align: right" class="alignrighttd">
                重复新密码：</td>
            <td style="width: 87px; height: 17px; text-align: left">
                <asp:TextBox ID="TextBox3" runat="server" TextMode="Password"></asp:TextBox></td>
            <td style="width: 129px; height: 17px; text-align: left">
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2"
                    ControlToValidate="TextBox3" ErrorMessage="两次输入的新密码必须一致！" Width="135px" 
                    Display="Dynamic"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"
                    ErrorMessage="请再次输入新密码！" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="3" class="aligncentertd">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" Width="66px" /></td>
        </tr>
        <tr>
            <td colspan="3" class="aligncentertd">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Width="209px"></asp:Label></td>
        </tr>
    </table>
</asp:Content>