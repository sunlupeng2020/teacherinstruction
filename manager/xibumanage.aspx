<%@ Page Language="C#" MasterPageFile="MasterPageManager.master" AutoEventWireup="true" CodeFile="xibumanage.aspx.cs" Inherits="manager_xibumanage" Title="系部管理" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px; ">
        <tr>
            <td style="width:900px;" align="center">
                            <h4>添加新系部</h4></td>
        </tr>
        <tr>
            <td style="width:900px; text-align:center;"  align="center">
               <table style="width: 474px" align="center">
                     <tr>
                          <td class="aligncentertd">
                                        系部名称：<asp:TextBox ID="TextBox1" runat="server" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                            Display="Dynamic" ErrorMessage="请输入系部名称。" ValidationGroup="group2" Width="161px"></asp:RequiredFieldValidator></td>
                                </tr>
                     <tr>
<%--                          <td class="aligncentertd">
                                        学号前缀：<asp:TextBox ID="Tbx_qianzhui" runat="server" MaxLength="20"></asp:TextBox>
                          </td>--%>
                                </tr>
                                <tr>
                                    <td style="text-align: center" class="aligncentertd">
                                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" ValidationGroup="group2"
                                            Width="113px" /></td>
                                </tr>
                                </table></td>
        </tr>
        <tr>
            <td>
                   <asp:Label ID="Label1" runat="server" ForeColor="Red" Width="343px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:900px;" align="center">
                            <h4>编辑删除系部</h4></td>
        </tr>
        <tr>
            <td  style="width:900px; text-align:center;" align="center" 
                class="aligncentertd">
                           <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CellPadding="4" DataSourceID="SqlDataSource1" EmptyDataText="没有可显示的数据记录。" ForeColor="#333333"
                                GridLines="None" DataKeyNames="xibuid" Width="406px" 
                                HorizontalAlign="Center" onrowdeleting="GridView1_RowDeleting" 
                               onrowupdating="GridView1_RowUpdating">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="系部名称" SortExpression="xibuname">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" 
                                                Text='<%# Bind("xibuname") %>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="请输入系部名称！"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("xibuname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                             
                                    <asp:TemplateField HeaderText="编辑、删除" ShowHeader="False">
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
                                                CommandName="Delete" Text="删除" OnClientClick="return confirm('你确定要删除该系部吗？');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" DeleteCommand="DELETE FROM [tb_Xibu] WHERE [xibuid] = @original_xibuid"
                    InsertCommand="INSERT INTO [tb_Xibu] ([xibuname]) VALUES (@xibuname)"
                    OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:kecheng2012ConnectionString.ProviderName %>"
                    SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]"
                    
                    UpdateCommand="UPDATE [tb_Xibu] SET [xibuname] = @xibuname WHERE [xibuid] = @original_xibuid">
                    <DeleteParameters>
                        <asp:Parameter Name="original_xibuid" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="xibuname" Type="String" />
                        <asp:Parameter Name="original_xibuid" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="xibuname" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>
              </td>
        </tr>
    </table>
</asp:Content>

