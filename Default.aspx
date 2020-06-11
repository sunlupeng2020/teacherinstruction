<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register src="foot01.ascx" tagname="foot01" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta name="description" content="基于知识树的多课程网络教学平台为广大师生提供一个在线教学、学习、交流的平台，提供教学资源上传下载，电子作业，在线自测与考试，在线考勤，等功能，教学资源、习题、答疑问题等与知识点相关联，可任选知识点查询相关内容。" />
<meta name="keywords" content="知识树,多课程,教学平台,教学资源,教学资源下载,在线学习,在线自测,网上作业,电子作业,在线考试,在线测试,知识点关联,在线答疑,在线考勤,选择知识点自测,自动改卷,作业管理,考试管理" />
    <title>基于知识树的多课程网络教学平台_首页</title>
<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="js/global.js"></script>
<script type="text/javascript" src="js/flash.js"></script>
<script type="text/javascript" src="js/MSClass.js"></script>
<%--<script type="text/javascript">
new Marquee("textdiv1",2,1,610,135,20,0,0);
</script>--%>
<!--[if lt IE 10]>
<script type="text/javascript" src="pie.js"></script>
<![endif]-->
<script language="javascript" type="text/javascript">
$(function () {
if (window.PIE) {
$('.rounded').each(function () {
PIE.attach(this);
});
}
});
</script>
</head>
<body id="nav_btn01 -11" >
    <form id="form1" runat="server">
<div class="header">
<dl class="header">
<dt id="userinfo" runat="server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal></dt>
<dd id="user_login" runat="server">[<a href="login.aspx">登录</a>]</dd>
<dd id="user_logout" runat="server">[<a href="tuichu.aspx">退出</a>]</dd>
</dl>
</div>
<!--end头文件-->
<div class="header_c1">
<div class="logo"><img src="images/logo.gif" alt=""/></div>
<!--end标志-->
</div>
<!--end头文件中-->
<div class="nav th">
<ul class="nav last_list">
<li id="nav_hover01"><a href="teacher/default.aspx"><span>教师首页</span></a></li>
<li id="nav_hover02"><a href="student/default.aspx"><span>学生首页</span></a></li>
<li id="nav_hover03"><a href="manager/default.aspx"><span>管理员首页</span></a></li>
</ul>
</div>
<!--end导航-->
<div class="banner"><img src="images/flash/guanggao.gif" alt="" width="1000" height="315"/></div>
<!--end图片-->
<div class="clear_div h_one">
<div class="orange_border h_ke">
<dl class="orange_th"><dd class="th">主要课程</dd></dl>
<div class="h_ke_c" <%--id="textdiv1"--%>>
<marquee onmouseover="this.stop()" onmouseout="this.start()" scrollamount=1 scrolldelay=10  behavior="alternate">
    <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlDataSource5" 
        RepeatDirection="Horizontal">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" 
                CommandArgument='<%# Eval("kechengid") %>' Height="135px" width="90px"
                ImageUrl='<%# Eval("image") %>' AlternateText='<%# Eval("kechengname") %>' 
                onclick="ImageButton1_Click" BorderWidth="8px" BorderStyle="Solid" BorderColor="White" />
        </ItemTemplate>
    </asp:DataList></marquee>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        
        SelectCommand="SELECT [kechengid], [image], [kechengname] FROM [tb_Kecheng] where image<>'~/images/nullbookimage148.gif'">
    </asp:SqlDataSource>
</div>
<!--end滚动-->
</div>
<!--end主要课程-->
<div class="orange_border h_news">
<dl class="orange_th"><dd class="th">作业动态</dd></dl>
&nbsp;</div>
<!--end作业动态-->
</div>
<!--end一行-->
<div class="clear_div h_one">
<div class="orange_border h_ke h_zhi">
<dl class="orange_th"><dd class="th">现有课程及资源统计</dd></dl>
</div>
<!--end主要课程-->
<div class="orange_border h_news">
<dl class="orange_th"><dd class="th">测试动态</dd></dl>
&nbsp;</div>
<!--end测试动态-->
</div>
<!--end一行-->

<!--end文件底-->
    <uc1:foot01 ID="foot011" runat="server" />
    </form>
</body>
</html>
