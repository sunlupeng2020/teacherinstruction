<%@ Page Language="C#" MasterPageFile="~/manager/MasterPageManager.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="manager_Default" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 389px;">
        <tr>
            <td style=" text-align:center">
                 <h3>功能概览</h3></td>
        </tr>
        <tr>
            <td>
                <a href="zhuanyemanage.aspx">专业管理</a>：专业的增删改查。</td>
        </tr>
        <tr>
            <td>
                <a href="teachermanage.aspx">教师管理</a>：添加、修改教师信息。</td>
        </tr>
    </table>
</asp:Content>

