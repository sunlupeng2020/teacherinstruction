<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="zhaomima.aspx.cs" Inherits="zhaomima" %>
<%@ Register src="foot01.ascx" tagname="foot01" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>找回密码</title>
</head>
<body>
<form id="form1" runat="server">
    <div class="header">
<dl class="header">
<dt>欢迎您！</dt>
    <dd><a href="login.aspx" id="user_login" runat="server">[登录]</a></dd>
</dl>
</div>
<div class="header_w">
<div class="logo"><img src="images/logo02.gif" alt=""/></div>
</div>
<div class="clear_div h_one" id="maindiv">
     <br /><br /><br /><br /><br /><br />
     <h3>请联系系统管理员找回用户名、密码！联系方式见页面底部。</h3>
     <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>
<uc1:foot01 ID="foot011" runat="server" />
</form> 
</body>
</html>