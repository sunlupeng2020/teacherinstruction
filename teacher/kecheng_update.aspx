<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="kecheng_update.aspx.cs" Inherits="teachermanage_kecheng_update" Title="课程信息更新" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                 <table class="fill_table">
                    <tr>
                        <td>
                            课程名称：</td>
                        <td style="width: 300px; text-align: left" >
                            <asp:TextBox ID="TextBoxkcname" runat="server" MaxLength="50" Width="290px"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TextBoxkcname" ErrorMessage="请输入课程名称！" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 286px; text-align: center">
                            课程图像</td>
                    </tr>
                    <tr>
                        <td>
                            课程介绍：</td>
                        <td style="width: 300px; height: 114px; text-align:left;" >
                                        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="300px">
                </FCKeditorV2:FCKeditor>
                            <asp:TextBox ID="TextBox1" runat="server" Height="195px" TextMode="MultiLine" 
                                Width="325px" MaxLength="250"></asp:TextBox></td>
                        <td style="width: 286px; height: 114px">
                            <asp:Image ID="Image1" runat="server" Height="210px" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td >
                            课程图像：</td>
                        <td style="width: 300px; height: 31px; text-align:left;">
                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="图片大小要小于1MB，以150*210像素为宜,只允许使用jpg,gif,bmp,png图片。" />
                            <br />
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" 
                                Text="只允许使用jpg,gif,bmp,png图片。"></asp:Label>
                            <br />
                            </td>
                        <td style="width: 286px; height: 31px">
                            </td>
                    </tr>
                    <tr>
                        <td >
                            课程管理员：</td>
                        <td style="width: 300px; height: 31px; text-align:left;" >
                            <asp:DropDownList ID="DropDownList2" runat="server" 
                                DataSourceID="SqlDataSourceteacher" DataTextField="xingming" 
                                DataValueField="username">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceteacher" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                SelectCommand="SELECT [username], [xingming] FROM [tb_Teacher]">
                            </asp:SqlDataSource>
                        </td>
                        <td style="width: 286px; height: 31px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center; width:auto;">
                            <asp:Button ID="Button1" runat="server" Text="提交更新" Width="119px" 
                                OnClick="Button1_Click" />
                        </td>
                    </tr>
                </table>
</asp:Content>

