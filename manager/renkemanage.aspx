<%@ Page Language="C#" MasterPageFile="~/manager/MasterPageManager.master" AutoEventWireup="true" CodeFile="renkemanage.aspx.cs" Inherits="manager_任课管理" Title="任课管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 100%">
        <tr>
            <td  style="width: 200px">
            <div id="leftmenu">
            <ul>
                <li><asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                    onclick="LinkButton3_Click">添加任课信息</asp:LinkButton></li>
                <li><asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                    onclick="LinkButton4_Click">教师任课信息</asp:LinkButton> </li>
                <li> <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" 
                    onclick="LinkButton5_Click">班级课程信息</asp:LinkButton></li>
                <li><asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                    onclick="LinkButton1_Click">课程任教信息</asp:LinkButton></li>
            </ul>
            </div>
            </td>
            <td style="width: 760px">
                 <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
               <h4>添加任课信息</h4><br />
        系部：<asp:DropDownList ID="ddl_xibu" runat="server" 
            DataSourceID="SqlDataSourcexibu" DataTextField="xibuname" 
            DataValueField="xibuid" AutoPostBack="True" 
            ondatabound="ddl_xibu_DataBound">
        </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                   ControlToValidate="ddl_xibu" ErrorMessage="请选择系部！" ValidationGroup="view1"></asp:RequiredFieldValidator>
               <br />
        班级：<asp:DropDownList ID="ddl_banji" runat="server" 
            DataSourceID="SqlDataSourcebanji" DataTextField="banjiname" 
            DataValueField="banjiid">
        </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ControlToValidate="ddl_banji" ErrorMessage="请选择班级！" ValidationGroup="view1"></asp:RequiredFieldValidator>
               <br />
        课程：<asp:DropDownList ID="ddl_kecheng" runat="server" 
            DataSourceID="SqlDataSourcekecheng" DataTextField="kechengname" 
            DataValueField="kechengid">
        </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                   ControlToValidate="ddl_kecheng" ErrorMessage="请选择课程！" ValidationGroup="view1"></asp:RequiredFieldValidator>
               <br />
        教师：<asp:DropDownList ID="ddl_teacher" runat="server" 
            DataSourceID="SqlDataSourceteacher" DataTextField="xingming" 
            DataValueField="username">
        </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                   ControlToValidate="ddl_teacher" ErrorMessage="请选择教师！" ValidationGroup="view1"></asp:RequiredFieldValidator>
        <asp:SqlDataSource ID="SqlDataSourceteacher" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT [username], [xingming] FROM [tb_Teacher]">
        </asp:SqlDataSource>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加任课信息" 
                   ValidationGroup="view1" />
            <asp:SqlDataSource ID="SqlDataSourcekecheng" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [kechengid], [kechengname] FROM [tb_Kecheng]">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcebanji" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [banjiid], [banjiname] FROM [tb_banji] WHERE ([xibuid] = @xibuid) ORDER BY [banjiid] DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_xibu" Name="xibuid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcexibu" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
            </asp:SqlDataSource>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <h4>按教师查询任课信息</h4><br />
            系部：<asp:DropDownList ID="ddl_v2xibu" runat="server" 
                DataSourceID="SqlDataSourcexibu" DataTextField="xibuname" 
                DataValueField="xibuid" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="ddl_v2xibu" ErrorMessage="请选择系部！"></asp:RequiredFieldValidator>
            <br />
            教师：<asp:DropDownList ID="ddl_v2teacher" runat="server" 
                DataSourceID="SqlDataSourcev2teacher" DataTextField="xingming" 
                DataValueField="username" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="ddl_v2teacher" Display="Dynamic" ErrorMessage="请选择教师！"></asp:RequiredFieldValidator>
            <asp:Button ID="Button2" runat="server" Text="显示任课信息" onclick="Button2_Click" 
                ValidationGroup="view2" />
            <br />
            <asp:GridView ID="grv_v2renke" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="renkeid" DataSourceID="SqlDataSourceteacherrenke" 
                ondatabound="grv_v2renke_DataBound" 
                onrowdeleting="grv_v2renke_RowDeleting" Width="408px" 
                EmptyDataText="没有该教师的任课信息。">
                <Columns>
                    <asp:BoundField DataField="renkeid" HeaderText="renkeid" InsertVisible="False" 
                        ReadOnly="True" SortExpression="renkeid" Visible="False" />
                    <asp:BoundField DataField="kechengname" HeaderText="课程名称" />
                    <asp:BoundField DataField="banjiname" HeaderText="班级名称" />
                    <asp:BoundField DataField="begintime" HeaderText="创建时间" 
                        SortExpression="begintime" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceteacherrenke" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                
                SelectCommand="SELECT [renkeid], [begintime],tb_banji.banjiname,tb_kecheng.kechengname FROM [tb_TeacherRenke] join tb_banji on tb_teacherrenke.banjiid=tb_banji.banjiid join tb_kecheng on tb_kecheng.kechengid=tb_teacherrenke.kechengid WHERE (tb_teacherrenke.[teacherusername] = @teacherusername)" 
                DeleteCommand="DELETE FROM [tb_TeacherRenke] WHERE [renkeid] = @renkeid" 
                InsertCommand="INSERT INTO [tb_TeacherRenke] ([begintime], [banjiid], [kechengid]) VALUES (@begintime, @banjiid, @kechengid)" 
                UpdateCommand="UPDATE [tb_TeacherRenke] SET [begintime] = @begintime, [banjiid] = @banjiid, [kechengid] = @kechengid WHERE [renkeid] = @renkeid">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_v2teacher" Name="teacherusername" 
                        PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="renkeid" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter DbType="Date" Name="begintime" />
                    <asp:Parameter Name="banjiid" Type="Int32" />
                    <asp:Parameter Name="kechengid" Type="Int32" />
                    <asp:Parameter Name="renkeid" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter DbType="Date" Name="begintime" />
                    <asp:Parameter Name="banjiid" Type="Int32" />
                    <asp:Parameter Name="kechengid" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcev2teacher" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [username], [xingming] FROM [tb_Teacher] WHERE ([xibuid] = @xibuid)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_v2xibu" Name="xibuid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>
        <asp:View ID="View3" runat="server">
          <h4>按班级查询任课信息</h4>系部：<asp:DropDownList ID="ddl_v3xibu" runat="server" 
                AutoPostBack="True" DataSourceID="SqlDataSourcexibu" DataTextField="xibuname" 
                DataValueField="xibuid">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="ddl_v3xibu" ErrorMessage="请选择系部！" ValidationGroup="view3"></asp:RequiredFieldValidator>
            <br />
            班级：<asp:DropDownList ID="ddl_v3banji" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSourcev3banji" DataTextField="banjiname" 
                DataValueField="banjiid">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ControlToValidate="ddl_v3banji" Display="Dynamic" ErrorMessage="请选择班级！" 
                ValidationGroup="view3"></asp:RequiredFieldValidator>
            <asp:Button ID="Btnv3_banjirenke" runat="server" 
                onclick="Btnv3_banjirenke_Click" Text="查看任课信息" ValidationGroup="view3" />
            <br />
            <asp:GridView ID="grv_v3renke" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="renkeid" DataSourceID="SqlDataSourcev3banjirenke" 
                ondatabound="grv_v3renke_DataBound" onrowdeleting="grv_v3renke_RowDeleting" 
                Width="394px" EmptyDataText="没有该班级的任课信息。">
                <Columns>
                    <asp:BoundField DataField="renkeid" HeaderText="renkeid" InsertVisible="False" 
                        ReadOnly="True" SortExpression="renkeid" Visible="False" />
                    <asp:BoundField DataField="xingming" HeaderText="任课教师" 
                        SortExpression="xingming" />
                    <asp:BoundField DataField="kechengname" HeaderText="课程名称" 
                        SortExpression="kechengname" />
                    <asp:BoundField DataField="begintime" HeaderText="创建时间" 
                        SortExpression="begintime" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourcev3banjirenke" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                DeleteCommand="DELETE FROM [tb_TeacherRenke] WHERE [renkeid] = @renkeid" 
                InsertCommand="INSERT INTO [tb_TeacherRenke] ([teacherusername], [begintime], [kechengid]) VALUES (@teacherusername, @begintime, @kechengid)" 
                SelectCommand="SELECT [renkeid], tb_teacher.xingming,tb_kecheng.kechengname,[begintime] FROM [tb_TeacherRenke] join tb_teacher on tb_teacherrenke.teacherusername=tb_teacher.username join tb_kecheng on tb_kecheng.kechengid=tb_teacherrenke.kechengid WHERE (tb_teacherrenke.[banjiid] = @banjiid) ORDER BY [begintime] DESC" 
                UpdateCommand="UPDATE [tb_TeacherRenke] SET [teacherusername] = @teacherusername, [begintime] = @begintime, [kechengid] = @kechengid WHERE [renkeid] = @renkeid">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_v3banji" Name="banjiid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="renkeid" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="teacherusername" Type="String" />
                    <asp:Parameter DbType="Date" Name="begintime" />
                    <asp:Parameter Name="kechengid" Type="Int32" />
                    <asp:Parameter Name="renkeid" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="teacherusername" Type="String" />
                    <asp:Parameter DbType="Date" Name="begintime" />
                    <asp:Parameter Name="kechengid" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcev3banji" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [banjiid], [banjiname] FROM [tb_banji] WHERE ([xibuid] = @xibuid) ORDER BY [createtime] DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_v3xibu" Name="xibuid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
        </asp:View>
        <asp:View ID="View4" runat="server">
            <h4>
                按课程查询任课信息</h4>
            <br />
            选择课程：<asp:DropDownList ID="ddl_v4kecheng" runat="server" 
                DataSourceID="SqlDataSourcekecheng" DataTextField="kechengname" 
                DataValueField="kechengid" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                Display="Dynamic" ErrorMessage="请选择课程!" ValidationGroup="view4" 
                ControlToValidate="ddl_v4kecheng"></asp:RequiredFieldValidator>
            <asp:Button ID="btn_v4renke" runat="server" onclick="btn_v4renke_Click" 
                Text="查看任课信息" ValidationGroup="view4" />
            <br />
            <asp:GridView ID="grv_v4renke" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="renkeid" DataSourceID="SqlDataSourcev4renke" 
                EmptyDataText="没有该课程的任课信息。">
                <Columns>
                    <asp:BoundField DataField="renkeid" HeaderText="renkeid" InsertVisible="False" 
                        ReadOnly="True" SortExpression="renkeid" Visible="False" />
                    <asp:BoundField DataField="xingming" HeaderText="任课教师" 
                        SortExpression="xingming" />
                    <asp:BoundField DataField="banjiname" HeaderText="班级" 
                        SortExpression="banjiname" />
                    <asp:BoundField DataField="begintime" HeaderText="创建时间" 
                        SortExpression="begintime" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourcev4renke" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                
                SelectCommand="SELECT [renkeid], tb_teacher.xingming,tb_banji.banjiname, [begintime] FROM [tb_TeacherRenke] join tb_teacher on tb_teacher.username=tb_teacherrenke.teacherusername join tb_banji on tb_banji.banjiid=tb_teacherrenke.banjiid WHERE (tb_teacherrenke.[kechengid] = @kechengid) ORDER BY [renkeid] DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_v4kecheng" Name="kechengid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>
    </asp:MultiView>

            </td>
            </tr>
            <tr>
            <td colspan="2">
                        <asp:Label ID="lbl_fankui" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

