<%@ Page Language="C#" MasterPageFile="StudentMasterPage.master" AutoEventWireup="true" CodeFile="sourceview.aspx.cs" Inherits="OnlineStudy" Title="课程学习" %>
<%@ Register src="../jiaoxueziyuan/KechengViewControl.ascx" tagname="KechengViewControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <uc1:KechengViewControl  ID="KechengViewControl1" runat="server" />
</asp:Content>

