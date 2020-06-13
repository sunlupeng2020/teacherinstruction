﻿<%@ Page Language="C#" MasterPageFile="BanjiXuesheng.master" AutoEventWireup="true" CodeFile="addstudent.aspx.cs" Inherits="teachermanage_studentmanage_addstudent" Title="添加单个学生" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BjXsContentPlaceHolder" Runat="Server">

    <script language="javascript" type="text/javascript">
function setvalue()
{
   var username=document.getElementById("<%=this.TextBox1.ClientID%>").value;
   var xuehao=username.substring(9);
   document.getElementById("<%=this.TextBoxxuhao.ClientID %>").value=xuehao;
}

</script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
        <tr>
            <td style="height: 193px">
                <table style="width: 844px">
                    <tr>
                        <td style="height: 19px; text-align: center;" colspan="2">
                            添加学生到班级</td>
                    </tr>
                    <tr>
                        <td style="width: 134px; height: 19px; text-align: right;">
                            系部：</td>
                        <td style="width: 250px; height: 19px; text-align: left;">
                            <asp:DropDownList ID="DropDownListxibu" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
                                DataTextField="xibuname" DataValueField="xibuid" 
                                ondatabound="DropDownListxibu_DataBound">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="DropDownListxibu" Display="Dynamic" ErrorMessage="请选择系部！"></asp:RequiredFieldValidator>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                                SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px; text-align: right;">
                            班级：</td>
                        <td style="width: 250px; text-align: left;">
                            <asp:DropDownList ID="DropDownListbanji" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource3"
                                DataTextField="banjiname" DataValueField="banjiid">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="DropDownListbanji" Display="Dynamic" ErrorMessage="请选择班级！"></asp:RequiredFieldValidator>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                                
                                SelectCommand="SELECT [banjiid], [banjiname] FROM [tb_banji] WHERE ([xibuid] = @xibuid)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListxibu" Name="xibuid" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px; text-align: right;">
                            学号：</td>
                        <td style="width: 250px; text-align: left;">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="请输入完整学号。" Width="179px" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px; text-align: right;">
                            姓名：</td>
                        <td style="width: 250px; text-align: left;">
                            <asp:TextBox ID="TextBox2" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                ErrorMessage="请输入学生姓名。" Display="Dynamic" Width="178px"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 134px; text-align: right;">
                            序号：</td>
                        <td style="width: 250px; text-align: left;">
                            <asp:TextBox ID="TextBoxxuhao" runat="server" ToolTip="学生成绩表上学生的序号。"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="TextBoxxuhao" ErrorMessage="序号最多三位，纯数字。" 
                                ValidationExpression="^\d{1,3}$" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="TextBoxxuhao" ErrorMessage="请输入学生序号。" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px; text-align: right;">
                            性别：</td>
                        <td style="width: 250px; text-align: left;">
                            <asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem Selected="True">男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:DropDownList><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px; text-align: right;">
                            专业：</td>
                        <td style="width: 250px; text-align: left;">
                            <asp:DropDownList ID="DropDownListzhuanye" runat="server" 
                                DataSourceID="SqlDataSource2" DataTextField="zhuanyename" 
                                DataValueField="zhuanyeid">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="DropDownListzhuanye" Display="Dynamic" ErrorMessage="请选择专业！"></asp:RequiredFieldValidator>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                SelectCommand="SELECT [zhuanyeid], [zhuanyename] FROM [tb_zhuanye]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                    <td></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="提交" Width="119px" 
                                OnClick="Button1_Click" style="height: 21px" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                Width="663px" Height="16px"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
