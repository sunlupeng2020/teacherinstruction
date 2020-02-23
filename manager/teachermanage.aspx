<%@ Page Language="C#" MasterPageFile="~/manager/MasterPageManager.master" AutoEventWireup="true" CodeFile="teachermanage.aspx.cs" Inherits="manager_teachermanage" Title="教师管理" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
        
            <td style="width: 150px; vertical-align:top;">
                <div id="leftmenu">
            <ul>
                <li><asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                    onclick="LinkButton3_Click">添加教师</asp:LinkButton></li>
                <li> <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                    onclick="LinkButton2_Click">教师用户列表</asp:LinkButton> </li>
                <li><asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" 
                    onclick="LinkButton5_Click">教师任课查询</asp:LinkButton></li>
            </ul>
          </div>
            </td>
            <td style="width: 500px">
             <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
        <tr>
            <td>
                <table style="width:402px" align="center">
                    <tr>
                        <td colspan="2">
                            <h4>添加教师用户</h4></td>
                    </tr>
                    <tr>
                        <td style="width: 100px" class="alignrighttd">
                            姓名：</td>
                        <td style="width: 258px" class="alignlefttd">
                            <asp:TextBox ID="TextBoxname" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ErrorMessage="请输入姓名。" ControlToValidate="TextBoxname" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px" class="alignrighttd">
                            性别：</td>
                        <td style="width: 258px" class="alignlefttd">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Selected="True">男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px" class="alignrighttd">
                            系部：</td>
                        <td style="width: 258px" class="alignlefttd">
                            <asp:DropDownList ID="DropDownList2" runat="server" 
                                DataSourceID="SqlDataSource2" DataTextField="xibuname" 
                                DataValueField="xibuid" 
                                ondatabound="DropDownList2_DataBound">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px" class="alignrighttd">
                            用户名：</td>
                        <td style="width: 258px" class="alignlefttd">
                            <asp:TextBox ID="TextBoxusername" runat="server" MaxLength="25" 
                                ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TextBoxusername" Display="Dynamic" ErrorMessage="请输入用户名。"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px" class="alignrighttd">
                            密码：</td>
                        <td style="width: 258px" class="alignlefttd">
                            <asp:TextBox ID="TextBoxpwd1" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="TextBoxpwd1" Display="Dynamic" ErrorMessage="请输入密码。"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="提交" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Labelfanki" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
        <table>
        <tr>
            <td>
                <h4>教师用户列表</h4></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="username" 
                    DataSourceID="SqlDataSource1" Width="699px" 
                    ondatabound="GridView1_DataBound">
                    <Columns>
                        <asp:BoundField DataField="username" HeaderText="用户名" 
                            SortExpression="username" ReadOnly="True" />
                        <asp:BoundField DataField="xingming" HeaderText="姓名"
                            SortExpression="xingming" ReadOnly="True" />
                        <asp:BoundField DataField="xingbie" HeaderText="性别" SortExpression="xingbie" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="xibuname" HeaderText="系部" 
                            SortExpression="xibuname" ReadOnly="True" />
                        <asp:BoundField DataField="password" HeaderText="密码" 
                            SortExpression="password" ReadOnly="True" />
                        <asp:TemplateField HeaderText="可用" SortExpression="keyong">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("keyong") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList5" runat="server" 
                                    SelectedValue='<%# Bind("keyong") %>'>
                                    <asp:ListItem>是</asp:ListItem>
                                    <asp:ListItem>否</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="修改状态" ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" DeleteCommand="DELETE FROM [tb_Teacher] WHERE [username] = @original_username AND [teacherid] = @original_teacherid AND [xingming] = @original_xingming AND [xibu] = @original_xibu AND [xingbie] = @original_xingbie"
                    InsertCommand="INSERT INTO [tb_Teacher] ([username], [xingming], [xibu], [xingbie]) VALUES (@username, @xingming, @xibu, @xingbie)"
                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [username], [xingming], [xingbie], [xibuname],[password],[keyong] FROM [tb_Teacher] join tb_xibu on tb_teacher.xibuid=tb_xibu.xibuid order by [xingming]"
                    
                    UpdateCommand="UPDATE [tb_Teacher] SET [keyong] = @keyong WHERE [username] = @original_username">
                    <DeleteParameters>
                        <asp:Parameter Name="original_username" Type="String" />
                        <asp:Parameter Name="original_teacherid" Type="Int32" />
                        <asp:Parameter Name="original_xingming" Type="String" />
                        <asp:Parameter Name="original_xibu" Type="String" />
                        <asp:Parameter Name="original_xingbie" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="keyong" />
                        <asp:Parameter Name="original_username" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="username" Type="String" />
                        <asp:Parameter Name="xingming" Type="String" />
                        <asp:Parameter Name="xibu" Type="String" />
                        <asp:Parameter Name="xingbie" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <h4>教师任课查询</h4></td>
                </tr>
                <tr>
                    <td>
                        系部：</td>
                    <td>
                        <asp:DropDownList ID="ddl_v3xibu" runat="server" 
                            DataSourceID="SqlDataSource2" DataTextField="xibuname" 
                            DataValueField="xibuid" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        教师：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList4" runat="server" 
                            DataSourceID="SqlDataSource3" DataTextField="xingming" 
                            DataValueField="username" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                            Text="显示教师任课信息" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSourceteacherrenke" EmptyDataText="未找到该教师的任课信息。">
                            <Columns>
                                <asp:BoundField DataField="课程名称" HeaderText="课程名称" SortExpression="课程名称" />
                                <asp:BoundField DataField="班级名称" HeaderText="班级名称" SortExpression="班级名称" />
                                <asp:BoundField DataField="创建时间" HeaderText="创建时间" SortExpression="创建时间" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSourceteacherrenke" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                            SelectCommand="SELECT tb_kecheng.kechengname 课程名称,tb_banji.banjiname 班级名称,[tb_TeacherRenke].[begintime] 创建时间 FROM [tb_TeacherRenke] join tb_kecheng on tb_kecheng.kechengid=[tb_TeacherRenke].kechengid join tb_banji on tb_banji.banjiid=[tb_TeacherRenke].banjiid WHERE ([tb_TeacherRenke].[teacherusername] = @teacherusername) order by [tb_TeacherRenke].[begintime] desc">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList4" Name="teacherusername" 
                                    PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                            SelectCommand="SELECT [username], [xingming] FROM [tb_Teacher] WHERE ([xibuid] = @xibuid)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddl_v3xibu" Name="xibuid" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
            </td>
        </tr>
    </table>   
</asp:Content>

