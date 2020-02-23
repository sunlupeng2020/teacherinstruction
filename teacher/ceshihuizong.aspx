<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshihuizong.aspx.cs" Inherits="teachermanage_ceshihuizong" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="测试成绩汇总" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        班级：<asp:Label ID="Labelbanji" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
</asp:Content>
