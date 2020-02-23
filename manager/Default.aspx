<%@ Page Language="C#" MasterPageFile="~/manager/MasterPageManager.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="manager_Default" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 389px;">
        <tr>
            <td style=" text-align:center">
                 <h3>功能概览</h3></td>
        </tr>
        <tr>
            <td>
                <a href="glymanage.aspx">管理员管理</a>：查询系统管理员、系部管理员，将教师设置为某系部管理员，删除管理员。</td>
        </tr>
        <tr>
            <td>
                <a href="xibumanage.aspx">系部管理</a>：系部的增删改查。</td>
        </tr>
        <tr>
            <td>
                <a href="zhuanyemanage.aspx">专业管理</a>：专业的增删改查。</td>
        </tr>
        <tr>
            <td>
                <a href="teachermanage.aspx">教师管理</a>：添加、修改教师信息，查询教师任课信息。</td>
        </tr>
        <tr>
            <td>
                <a href="kechengmanage.aspx">课程管理</a>：查询各课程的创建者、管理员，修改课程管理员。</td>
        </tr>
        <tr>
            <td>
                班级学生管理：添加班级、学生，从Excel表整批导入学生到班级,修改学生序号等。</td>
        </tr>
        <tr>
            <td>
                <a href="kechengmanage.aspx">任课管理</a>：添加教师的任课信息，按教师、班级或课程查询任课信息。</td>
        </tr>
    </table>
</asp:Content>

