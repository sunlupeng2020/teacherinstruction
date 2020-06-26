<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基于知识树的多课程网络教学平台_用户登录</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <meta name="description" content="基于知识树的多课程网络教学平台为广大师生提供一个在线教学、学习、交流的平台，提供教学资源上传下载，电子作业，在线自测与考试，在线考勤，等功能，教学资源、习题、答疑问题等与知识点相关联，可任选知识点查询相关内容。" />
    <meta name="keywords" content="知识树,多课程,教学平台,教学资源,教学资源下载,在线学习,在线自测,网上作业,电子作业,在线考试,在线测试,知识点关联,在线答疑,在线考勤,选择知识点自测,自动改卷,作业管理,考试管理" />
</head>
<body>
    <form id="form1" runat="server">
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>    <br/>
    <br/>    <br/>
    <br/>
    <table width="620" border="0" align="center" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td width="620">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <br/>
                    <table width="570" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="570" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="570" height="80" align="center" valign="top">
                                            <img src="images/zicelogo_2.gif" alt="用户登录" width="570" height="80" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table border="0" cellspacing="0" cellpadding="0" style="width: 543px">
                                    <tr>
                                        <td width="123" height="120">
                                            <img alt="" src="images/login_p_img05.gif" /></td>
                                        <td align="center">
                                            <table cellspacing="0" cellpadding="0" border="0" 
                                                style="height: 92px; width: 372px">
                                                <tr>
                                                    <td width="80" height="25" valign="middle" class="alignrighttd">用户名：</td>
                                                    <td width="*" align="left" valign="middle">                            
                                                        <asp:TextBox ID="TextBoxusername" runat="server" Font-Size="Small" 
                                                            Width="172px" ToolTip="学生的用户名是完整学号" Height="22px" ></asp:TextBox>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" width="80" height="12" class="alignrighttd">密&nbsp;&nbsp;码：</td>
                                                    <td valign="middle" align="left">
<asp:TextBox ID="TextBoxpassword" runat="server" Font-Size="Small" TextMode="Password"
                                Width="173px" ToolTip="学生的密码是完整学号" TabIndex="1" Height="23px" style="margin-left: 0px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" height="13" width="80" class="alignrighttd">身&nbsp;&nbsp;份：</td>
                                                    <td valign="middle" height="13" align="left">
                                                    <asp:RadioButtonList ID="RadioButtonListshenfen" runat="server" 
                                Font-Size="12px" RepeatDirection="Horizontal" EnableTheming="false"
                                Width="189px" TabIndex="3">
                                <asp:ListItem Value="teacher">教师</asp:ListItem>
                                <asp:ListItem Selected="True" Value="student">学生</asp:ListItem>
                                <asp:ListItem Value="manager">管理员</asp:ListItem>
                            </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="bottom" height="13" colspan="2">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph" />
                            <asp:Label ID="Labelerror" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxusername"
                                ErrorMessage="请输入用户名。" Display="None" SetFocusOnError="True">请输入用户名。</asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxpassword"
                                ErrorMessage="请输入密码。" Display="None" SetFocusOnError="True">请输入密码。</asp:RequiredFieldValidator>                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                 <asp:Button ID="Buttonloginin" runat="server" Text="登录" Font-Size="Small" 
                                OnClick="Buttonloginin_Click" Width="134px" TabIndex="4" Height="40px" 
                                    CssClass="fill_btn" />
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <table class="style1">
                                <tr>
                                    <td>
                                        <a href="zhaomima.aspx">忘记密码？</a></td>
                                    <td>
                                        <a href="Default.aspx">网站首页</a></td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                        <tr style="background:url(images/lg_ft.gif) repeat-x; height:54px;">
                            <td>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
        </tbody>
    </table>
    <br />
    <br />
    </form>
</body>
</html>
