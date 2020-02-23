<%@ Page Language="C#" MasterPageFile="YoukeMasterPage.master" AutoEventWireup="true" CodeFile="sourceview.aspx.cs" Inherits="youke_sourceview" Title="基于知识树的多课程网络教学平台_游客_课程学习" %>
<%@ Register src="../jiaoxueziyuan/KechengViewControl.ascx" tagname="KechengViewControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <div class="kechengtreeview">
    <cc2:MyTreeView  ID="TreeView1" runat="server" Width="227px"  
            OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ToolTip="选择知识点检索相关教学资源。" 
            ExpandDepth="1" ShowLines="True">
    </cc2:MyTreeView>

</div>
<div style="width:735px;float:right;overflow:auto;height:500px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; text-align: left;">
    <br />
    选择的知识点：<asp:Label ID="Labelzhishidianname" runat="server" Width="370px" ></asp:Label><br />
    <br />
    知识点介绍：<br />
    <br />
    <asp:Label ID="Labelzhishidianjieshao" runat="server" Width="733px" ForeColor="MediumBlue"></asp:Label><br />
</div>--%>
<uc1:KechengViewControl 
        ID="KechengViewControl1" runat="server" />
&nbsp;
</asp:Content>