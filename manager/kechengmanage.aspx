<%@ Page Language="C#" MasterPageFile="~/manager/MasterPageManager.master" AutoEventWireup="true" CodeFile="kechengmanage.aspx.cs" Inherits="manager_kechengmanage" Title="课程管理" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="kechengid" DataSourceID="SqlDataSource1"
                    EmptyDataText="当前系统中没有课程信息。" Width="975px" 
                    onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="kechengid" HeaderText="课程ID" ReadOnly="True" />
                        <asp:BoundField DataField="kechengname" HeaderText="课程名称" ReadOnly="True" SortExpression="kechengname" />
                        <asp:TemplateField HeaderText="管理员" SortExpression="guanliyuan">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("guanliyuan") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" 
                                    DataSourceID="SqlDataSource1" DataTextField="username" 
                                    DataValueField="username" SelectedValue='<%# Bind("guanliyuan") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                    SelectCommand="SELECT [username] FROM [tb_Teacher]"></asp:SqlDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="createtime" HeaderText="创建时间" 
                            SortExpression="createdate" ReadOnly="True" />
                        <asp:BoundField DataField="creater" HeaderText="创建者" SortExpression="creater" 
                            ReadOnly="True" />
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                    CommandName="Update" Text="更新"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" Text="取消"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                    CommandName="Edit" Text="编辑"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                    CommandName="Select" Text="查看任课信息"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                                    CommandName="Delete" Text="删除"
                                    OnClientClick="return confirm('删除课程请谨慎，将删除该课程的所有信息！您确定要删除该课程吗？')" 
                                    ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    DeleteCommand="DELETE FROM [tb_Kecheng] WHERE [kechengid] = @kechengid" 
                    InsertCommand="INSERT INTO [tb_Kecheng] ([kechengname], [instruction], [guanliyuan], [createdate], [creater]) VALUES (@kechengname, @instruction, @guanliyuan, @createdate, @creater)"
                    ProviderName="<%$ ConnectionStrings:kecheng2012ConnectionString.ProviderName %>"
                    SelectCommand="SELECT [kechengid],[kechengname],[guanliyuan],[createtime], [creater] FROM [tb_Kecheng]"
                    UpdateCommand="UPDATE [tb_Kecheng] SET  [guanliyuan] = @guanliyuan where [kechengid] = @kechengid">
                    <DeleteParameters>
                        <asp:Parameter Name="kechengid"/>
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="kechengname" Type="String" />
                        <asp:Parameter Name="instruction" Type="String" />
                        <asp:Parameter Name="guanliyuan" Type="String" />
                        <asp:Parameter Name="createdate" Type="DateTime" />
                        <asp:Parameter Name="creater" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="guanliyuan" Type="String" />
                        <asp:Parameter Name="kechengid" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSourcerenke" EmptyDataText="没有该课程的任课信息.">
                    <Columns>
                        <asp:BoundField DataField="教师姓名" HeaderText="教师姓名" SortExpression="教师姓名" />
                        <asp:BoundField DataField="创建时间" HeaderText="创建时间" SortExpression="创建时间" />
                        <asp:BoundField DataField="banjiname" HeaderText="创建时间" 
                            SortExpression="banjiname" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourcerenke" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT tb_teacher.xingming 教师姓名,[begintime] 创建时间,tb_banji.banjiname FROM [tb_TeacherRenke] join tb_teacher on [tb_TeacherRenke].teacherusername=tb_teacher.username join tb_banji on tb_banji.banjiid=tb_teacherrenke.banjiid WHERE ([kechengid] = @kechengid) order by [tb_TeacherRenke].[begintime]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="kechengid" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

