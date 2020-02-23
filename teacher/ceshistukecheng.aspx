<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshistukecheng.aspx.cs" Inherits="teachermanage_ceshistukecheng" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="学生测试信息" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <p>
        班级：<asp:Label ID="Labelbj" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
        姓名：<asp:Label ID="Labelxm" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp; 学号：<asp:Label ID="Labelstuun" runat="server"></asp:Label>
    </p>
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            Width="930px">
            <Columns>
                <asp:BoundField DataField="测试名称" HeaderText="测试名称" SortExpression="测试名称" />
                <asp:BoundField DataField="成绩" HeaderText="成绩" SortExpression="成绩" />
                <asp:BoundField DataField="开始时间" HeaderText="开始时间" SortExpression="开始时间" />
                <asp:BoundField DataField="交卷时间" HeaderText="交卷时间" SortExpression="交卷时间" />
                <asp:BoundField DataField="延长时间" HeaderText="延长时间" SortExpression="延长时间" />
                <asp:BoundField DataField="状态" HeaderText="状态" SortExpression="状态" />
                <asp:BoundField DataField="是否允许测试" HeaderText="是否允许测试" 
                    SortExpression="是否允许测试" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
