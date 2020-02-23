<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControl.ascx.cs" Inherits="WebUserControl" %>
<div style="z-index: 101; width: 980px;
    height: 90px; border-bottom:#ff9966 1px double">
    <asp:ImageMap ID="ImageMap1" runat="server" Height="90px" Width="980px" ImageUrl="~/images/bannernew2012.gif"> 
     <asp:RectangleHotSpot AlternateText="学习进步曲线" Bottom="90" Left="685" Right="810" NavigateUrl="~/HTMLPage.htm" Target="_blank" />
        <asp:RectangleHotSpot AlternateText="学习遗忘曲线" Bottom="90" Left="810" Right="980" NavigateUrl="~/HTMLPage2.htm" Target="_blank" />
    </asp:ImageMap></div>