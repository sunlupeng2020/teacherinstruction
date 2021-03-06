﻿<%@ Page Language="C#" MasterPageFile="MasterPageManager.master" AutoEventWireup="true" CodeFile="zhuanyemanage.aspx.cs" Inherits="teachermanage_studentmanage_zhuanyemanage" Title="专业管理" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 101%">
        <tr>
            <td style="width: 150px; vertical-align:top;">
            <div id="leftmenu">
            <ul>
                <li><asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                                            onclick="LinkButton4_Click">添加新专业</asp:LinkButton></li>
                <li><asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" 
                    onclick="LinkButton5_Click">全部专业列表</asp:LinkButton></li>
            </ul>
</div>
            </td> 
            <td>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server"> 
        <table align="center" style="width: 62%">
                <tr>
            <td style="height: 40px" colspan="2">
                            <h4>添加新专业</h4></td>
        </tr>
                    <tr>
                        <td align="right" class="alignrighttd" style="width: 106px">
                            专业名称：</td>
                        <td align="left" class="alignlefttd" style="width: 333px">
                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Width="226px"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="请输入专业名称！" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td class="aligncentertd" colspan="2">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Width="356px"></asp:Label>
                        </td>
                    </tr>
                </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
         <table border="0" cellpadding="0" cellspacing="0" style="width: 35%; height: 100%">
                    <tr>
                        <td style="height: 34px">
                            <h4>全部专业列表</h4></td>
                    </tr>
                    <tr>
                        <td class="aligncentertd" style="text-align: center;">
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                                AutoGenerateColumns="False" DataKeyNames="zhuanyeid" 
                                DataSourceID="SqlDataSource2" EmptyDataText="没有可显示的数据记录。" 
                                HorizontalAlign="Center"
                                onrowdeleting="GridView1_RowDeleting" onrowupdating="GridView1_RowUpdating" 
                                Width="349px">
                                <Columns>
                                    <asp:TemplateField HeaderText="专业名称" SortExpression="zhuanyename">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" 
                                                Text='<%# Bind("zhuanyename") %>'></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="请输入专业名称！"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("zhuanyename") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作">
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
                                                CommandName="Delete" OnClientClick="return confirm('你确定要删除该专业吗？');" Text="删除"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                DeleteCommand="DELETE FROM [tb_zhuanye] WHERE [zhuanyeid] = @original_zhuanyeid" 
                                InsertCommand="INSERT INTO [tb_zhuanye] ([zhuanyename]) VALUES (@zhuanyename)" 
                                OldValuesParameterFormatString="original_{0}" 
                                ProviderName="<%$ ConnectionStrings:kecheng2012ConnectionString.ProviderName %>" 
                                SelectCommand="SELECT [zhuanyename], [zhuanyeid] FROM [tb_zhuanye]" 
                                UpdateCommand="UPDATE [tb_zhuanye] SET [zhuanyename] = @zhuanyename WHERE [zhuanyeid] = @original_zhuanyeid">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_zhuanyeid" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="zhuanyename" Type="String" />
                                    <asp:Parameter Name="original_zhuanyeid" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="zhuanyename" Type="String" />
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

