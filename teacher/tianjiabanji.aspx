<%@ Page Language="C#" MasterPageFile="~/manager/BanjiXuesheng.master" AutoEventWireup="true" CodeFile="tianjiabanji.aspx.cs" Inherits="manager_tianjiabanji" Title="班级学生管理" %>

<%-- 在此处添加内容控件 --%>
<asp:Content ID="bjxscontent1" runat="server" ContentPlaceHolderID="BjXsContentPlaceHolder">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
                </asp:SqlDataSource>
                <table style="width:572px;">
                    <tr>
                        <td>
                            系部</td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                DataSourceID="SqlDataSource1" DataTextField="xibuname" DataValueField="xibuid" 
                                Width="151px" ondatabound="DropDownList1_DataBound">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            班级名称</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="100" Width="210px" 
                                ToolTip="班级名称要以成绩单上的名称为准"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TextBox1" Display="Dynamic" 
                                ErrorMessage="请输入班级名称！" Width="160px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="提交" 
                                Width="133px" />
                            <br />
                            <asp:Label ID="Labelfankui" runat="server" ForeColor="Red" Width="485px"></asp:Label>
                        </td>
                    </tr>
                </table>
</asp:Content>
