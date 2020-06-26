<%@ Page Language="C#" MasterPageFile="BanjiXuesheng.master" AutoEventWireup="true" CodeFile="banjilist.aspx.cs" Inherits="manager_banjilist" Title="班级列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BjXsContentPlaceHolder" Runat="Server">
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<TABLE><TBODY>
        <tr>
            <td colspan="2">
            </td>
    </tr>
        <TR>
            <td style="WIDTH: 340px; vertical-align: middle;" class="alignlefttd">
                班级列表</td>
    </TR>
    <tr>
        <td style="WIDTH: 210px; vertical-align: top;">
            <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource4" DataTextField="banjiname" 
                DataValueField="banjiid" Height="176px" ondatabound="ListBox1_DataBound" 
                onselectedindexchanged="ListBox1_SelectedIndexChanged" Width="200px">
            </asp:ListBox>
            <br />
            <asp:Button ID="Button3" runat="server" CausesValidation="False" 
                onclick="Button3_Click" Text="删除该班级" 
                ToolTip="删除班级,不删除学生。" Width="145px" OnClientClick="return confirm('你确定要删除吗？');" />
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"                 
                SelectCommand="SELECT [banjiid], [banjiname] FROM [tb_banji] WHERE ([teacherusername] = @teacherusername) ORDER BY [banjiid] DESC">
                <SelectParameters>
                    <asp:SessionParameter Name="teacherusername" SessionField="username" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td style="WIDTH: 640px; vertical-align: top;">
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT tb_Student.username, tb_Student.xingming, tb_Student.xingbie,tb_Student.password,tb_banjistudent.banjistudentid FROM tb_Student INNER JOIN tb_banjistudent ON tb_Student.username = tb_banjistudent.studentusername WHERE (tb_banjistudent.banjiid = @banjiid) ORDER BY tb_Student.username" 
                DeleteCommand="DELETE FROM [tb_banjistudent] WHERE [banjistudentid] = @banjistudentid" 
                InsertCommand="INSERT INTO [tb_banjistudent] ([studentusername]) VALUES (@studentusername)" 
                UpdateCommand="UPDATE [tb_Student] SET [xingming]=@xingming, [xingbie]=@xingbie,[password]=@password WHERE [username] = @username">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ListBox1" Name="banjiid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="banjistudentid" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="xingming" Type="String" />
                    <asp:Parameter Name="xingbie" Type="String" />
                    <asp:Parameter Name="username" Type="String" />
                    <asp:Parameter Name="password" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="xuhao" Type="Int32" />
                    <asp:Parameter Name="studentusername" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                DataKeyNames="username,banjistudentid" DataSourceID="SqlDataSource3" 
                ForeColor="#333333" Width="600px" EmptyDataText="没有该班学生信息。" 
                ondatabound="GridView1_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                    <asp:Literal ID="ltxh" runat="server"></asp:Literal>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="username" HeaderText="学号" SortExpression="username" 
                        ReadOnly="True" />
                    <asp:TemplateField HeaderText="姓名" SortExpression="xingming">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("xingming") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("xingming") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TextBox1" ErrorMessage="姓名不能为空!"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="性别" SortExpression="xingbie">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("xingbie") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                RepeatDirection="Horizontal" SelectedValue='<%# Bind("xingbie") %>'>
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:RadioButtonList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="密码">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("password") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("password") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TextBox2" ErrorMessage="密码不能为空！"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Delete" Text="删除" OnClientClick="return confirm('你确定要删除吗？');"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="更新"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Labelfankui" runat="server" __designer:wfdid="w3" 
                ForeColor="Red" Width="485px"></asp:Label>
        </td>
    </tr>
    </TBODY></TABLE>
</contenttemplate>
</asp:UpdatePanel>

</asp:Content>

