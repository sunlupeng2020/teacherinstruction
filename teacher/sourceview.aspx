<%@ Page Language="C#" MasterPageFile="TeacherMasterPage.master" AutoEventWireup="true" CodeFile="sourceview.aspx.cs" Inherits="kechengview" Title="知识点浏览" %>
<%@ Register src="../jiaoxueziyuan/KechengViewControl.ascx" tagname="KechengViewControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:KechengViewControl  ID="KechengViewControl1" runat="server" />
</asp:Content>