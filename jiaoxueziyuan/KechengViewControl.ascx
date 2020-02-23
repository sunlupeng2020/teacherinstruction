<%@ Control Language="C#" AutoEventWireup="true" CodeFile="KechengViewControl.ascx.cs" Inherits="jiaoxueziyuan_KechengViewControl" %>
<div class="kechengtreeview">
    <asp:CheckBox ID="cbxmode" runat="server" Text="显示所选知识点下级知识点的全部内容" 
        oncheckedchanged="TreeView1_SelectedNodeChanged" AutoPostBack="True" 
        Checked="True" /><asp:HiddenField
        ID="hdfslectednodeid" runat="server" />
    <cc2:MyTreeView  ID="TreeView1" runat="server" Width="227px"  
            OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ToolTip="选择知识点检索相关教学资源。" 
            ExpandDepth="1" ShowLines="True">
    </cc2:MyTreeView>
</div>
<div style="width:750px;float:right;
    height:500px; border-right: #cccccc 1px solid; overflow:auto; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; text-align: left;">
    <asp:Label ID="Labelzhishidianjieshao" runat="server" Width="732px" ForeColor="#075099"></asp:Label><br />
</div>