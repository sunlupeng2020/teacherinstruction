<%@ Page Language="C#" MasterPageFile="BanjiXuesheng.master" AutoEventWireup="true" CodeFile="banjirenke.aspx.cs" Inherits="manager_banjirenke" Title="班级课程查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BjXsContentPlaceHolder" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                班级课程情况查询</td>
        </tr>
        <tr>
            <td>
                系部：</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" 
        DataSourceID="SqlDataSourcexibu" DataTextField="xibuname" 
        DataValueField="xibuid" AutoPostBack="True">
    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                  班级：</td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" 
        DataSourceID="SqlDataSourcebanji" DataTextField="banjiname" 
        DataValueField="banjiid" AutoPostBack="True">
    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSourcebanjirenke" 
                    EmptyDataText="未找到该班课程信息。">
        <Columns>
            <asp:BoundField DataField="课程名称" HeaderText="课程名称" SortExpression="课程名称" />
            <asp:BoundField DataField="教师姓名" HeaderText="教师姓名" SortExpression="教师姓名" />
            <asp:BoundField DataField="创建时间" HeaderText="创建时间" SortExpression="创建时间" />
        </Columns>
    </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSourcebanjirenke" runat="server" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        SelectCommand="SELECT tb_kecheng.kechengname 课程名称,tb_teacher.xingming 教师姓名, [begintime] 创建时间 FROM [tb_TeacherRenke] join tb_teacher on tb_teacher.username=tb_teacherrenke.teacherusername join tb_kecheng on tb_kecheng.kechengid=tb_teacherrenke.kechengid WHERE (tb_teacherrenke.[banjiid] = @banjiid)">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList2" Name="banjiid" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourcebanji" runat="server" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        SelectCommand="SELECT [banjiname], [banjiid] FROM [tb_banji] WHERE ([xibuid] = @xibuid) ORDER BY [banjiid] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="xibuid" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourcexibu" runat="server" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
    </asp:SqlDataSource>
</asp:Content>

