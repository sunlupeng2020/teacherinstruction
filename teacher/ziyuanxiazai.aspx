<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ziyuanxiazai.aspx.cs" Inherits="teachermanage_jiaoxueziyuan_ziyuanxiazai" Title="检索下载教学资源" EnableTheming="true"  %>
<%@ Register src="../jiaoxueziyuan/UCZiyuanXiaZai.ascx" tagname="UCZiyuanXiaZai" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
    <uc1:UCZiyuanXiaZai ID="UCZiyuanXiaZai1" runat="server" />
</asp:Content>