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
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    DeleteCommand="DELETE FROM [tb_Teacher] WHERE  [teacherid] = @original_teacherid"
                    InsertCommand="INSERT INTO [tb_Teacher] ([username], [xingming],  [xingbie]) VALUES (@username, @xingming,@xingbie)"
                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [username], [xingming], [xingbie], [password],[keyong] FROM [tb_Teacher] order by [xingming]"
                    
                    
                    UpdateCommand="UPDATE [tb_Teacher] SET [keyong] = @keyong WHERE [username] = @original_username">
                    <DeleteParameters>
                        <asp:Parameter Name="original_teacherid" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="keyong" />
                        <asp:Parameter Name="original_username" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="username" Type="String" />
                        <asp:Parameter Name="xingming" Type="String" />
                        <asp:Parameter Name="xingbie" Type="String" />
                    </InsertParameters>
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

