<%@ Page Language="C#" MasterPageFile="TeacherMasterPage.master" AutoEventWireup="true" CodeFile="xuanzekecheng.aspx.cs" Inherits="teachermanage_xuanzkecheng" Title="切换课程" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p style="font-size: large">
        &nbsp;</p>
<p style="font-size: large">
    &nbsp;</p>
<p style="font-size: large">
    &nbsp;</p>
<p style="font-size: large">
    <b>切换课程</b></p>
<p>
    选择课程：<asp:DropDownList ID="ddl_kecheng" runat="server" 
        ondatabound="ddl_kecheng_DataBound">
    </asp:DropDownList>
</p>
<p>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="提交" 
        style="width: 40px" />
</p>
<p>
    &nbsp;</p>
</asp:Content>

