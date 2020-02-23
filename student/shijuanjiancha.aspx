<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shijuanjiancha.aspx.cs" Inherits="student_shijuanjiancha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试卷检查</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1">
            <tr>
                <td>
                    试卷检查____题目详情</td>
            </tr>
            <tr>
                <td>
                    试卷名称：<asp:Label ID="lbl_shijuanname" runat="server"></asp:Label>
&nbsp; 姓名:<asp:Label ID="lbl_xingming" runat="server"></asp:Label>
                &nbsp;&nbsp; 学号：<asp:Label ID="lbl_xuehao" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
