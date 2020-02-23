<%@ Page Language="C#" MasterPageFile="StudentMasterPage.master" AutoEventWireup="true" CodeFile="ziyuanxiazai.aspx.cs" Inherits="studentstudy_ziyuanxiazai" Title="浏览下载教学资源" EnableTheming="true"  %>
<%@ Register src="../jiaoxueziyuan/UCZiyuanXiaZai.ascx" tagname="UCZiyuanXiaZai" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
    <uc1:UCZiyuanXiaZai ID="UCZiyuanXiaZai1" runat="server" />
</asp:Content>

