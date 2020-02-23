<%@ Page Language="C#" AutoEventWireup="true" CodeFile="canjiaceshi.aspx.cs" Inherits="studentstudy_canjiaceshi" EnableTheming="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>在线考试</title>
<object id="locator" classid="CLSID:76A64158-CB41-11D1-8B02-00600806D9B6 VIEWASTEXT"></object>
<object id="foo" classid="CLSID:75718C9A-F029-11d1-A1AC-00C04FB6C223"></object>
    <style type="text/css">
    #daojishidiv{width:150px;height:50px; background-color:#EEEEEE; text-align: center;  right:0px;top:300px; position:absolute;}
    #wangyezuocebiaoti{background:url(../images/wangyebiaoti2.gif) no-repeat;text-align: left; vertical-align:top; width: 120px;}
        .style1
        {
            width: 950px;
            height: 19px;
        }
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 120px;
        }
    </style>
<script language="javascript" type="text/javascript">
function divMove()
    {
         daojishidiv.style.top=document.documentElement.scrollTop+document.documentElement.clientHeight-daojishidiv.offsetHeight-400;
    }
</script>
    <script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="../js/global.js"></script>
<script type="text/javascript" src="../js/jQselect.js"></script>
<script type="text/javascript">
function setCookie()
{
    var exp=new Date();
    exp.setTime(exp.getTime()+60000);
    document.cookie="zhuangtai="+escape("kaoshi")+";expires="+exp.toGMTString()+";path=/";
}
function Myautorun()
{
    var zhuangtai=getCookie("zhuangtai");
    if(zhuangtai==null)
    {
        setCookie();
        setInterval("setCookie()",60000);
    }
    else if(zhuangtai.toString().indexOf("study")>=0)
    {
        alert("检测到您打开了学习页面，不能进行考试，请关闭学习页面一分钟后再进行考试！\n提醒：答疑系统的所有页面，游客页面、作业页面等均为学习页面。");
        //window.history.go(-2);
        document.getElementById("ceshitable").style.display="none";
    }
}
function getCookie(name)//读取cookie
{
    var arr = document.cookie.match(new RegExp("(^|)" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}
</script>
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
<body onload="window.setInterval(divMove,150);Myautorun();">
   <form id="form1" runat="server">
    <div class="header">
<dl class="header">
<dt>欢迎您！<asp:Literal ID="Literalxingming" runat="server"></asp:Literal>同学.<span>&nbsp;</span></dt>
<dd><div class="select">&nbsp;
</div>
    <span>[<a href="../tuichu.aspx">退出</a>]</span></dd>
</dl>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
              SelectMethod="GetKecheng" TypeName="KechengInfo"></asp:ObjectDataSource>
</div>
<!--end头文件-->
<div class="header_w">
<div class="logo"><img src="../images/logo_w.png" alt=""/></div>
<!--end标志-->
</div>
<dl class="site_th"><dd>当前位置：<asp:SiteMapPath ID="SiteMapPath1" runat="server" 
        PathSeparator="&gt;&gt;">
    </asp:SiteMapPath>
</dd></dl>
<div class="clear_div2 h_center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
        <table>
            <tr>
               <td>
                 测试名称：<asp:Literal ID="Literal1" runat="server"></asp:Literal>&nbsp;
                    课程：<asp:Label ID="Labelkecheng" runat="server"></asp:Label>&nbsp; 测试时长：<asp:Label ID="Labelshichang" runat="server"></asp:Label> 分钟&nbsp;  共<asp:Label ID="lbl_timushuliang" runat="server" Text="Label"></asp:Label> 
                   题&nbsp;&nbsp;&nbsp;
      
                   <br />
                   姓名：<asp:Label ID="Labelxingming" runat="server"></asp:Label>
&nbsp;班级：<asp:Label ID="Labelbanji" runat="server"></asp:Label>&nbsp;学号：<asp:Label ID="Labelusername" runat="server"></asp:Label>
            </td>
                
            </tr>
            <tr>
                <td style="height: 33px; text-align: left">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <asp:UpdateProgress id="UpdateProgress1" runat="server"><ProgressTemplate>
<IMG src="../images/jd002.gif"  alt="请稍候..." style="position:absolute;left:50%;top:50%;"/>
</ProgressTemplate>
</asp:UpdateProgress>
                          <table style="width: 960px">
                                <tr>
                                    <td align="center" style="text-align:center" class="style1">
                                        &nbsp;延长时间：<asp:Label ID="Labelyanhang" runat="server"></asp:Label>
                                        分钟&nbsp;&nbsp;&nbsp;&nbsp; 开始时间：<asp:Label ID="Labelkaishishijian" runat="server"></asp:Label>
                                        &nbsp;&nbsp; 状态:<asp:Label ID="Labelzhuangtai" runat="server"></asp:Label>&nbsp;&nbsp; 交卷时间：<asp:Label ID="Labeljiaojuanshijian" runat="server"></asp:Label><hr />
                                        <div  id="daojishidiv" runat="server">
                        <font style="color: #ff0000">自动交卷倒计时</font><br/>
                             <asp:Label ID="Label1" runat="server"
                        Width="56px">0</asp:Label>分钟&nbsp;</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 950px; text-align: left">
                                        <asp:Label ID="Label_fankui" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Panel runat="server" ID="Panel_TimuInfo">
                                        第 <asp:Label ID="lbl_tihao" runat="server">1</asp:Label>
                                        题<br />
                                        题型：<asp:Label ID="lbl_tixing" runat="server"></asp:Label>
                                        &nbsp;&nbsp; 分值：<asp:Label ID="lbl_fenzhi" runat="server"></asp:Label>
                                        <br />
                                        题目：<asp:Label ID="lbl_timu" runat="server" Text=""></asp:Label><br />
                                    </asp:Panel>
                                        <asp:Panel ID="Panel_danxuan" runat="server" Height="55px" Visible="False">
                                            <table class="style2">
                                                <tr>
                                                    <td class="style3">
                                                        请选择（四选一）：</td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rbl_xuanzedaan" runat="server" Height="27px" 
                                                            RepeatDirection="Horizontal" Width="227px" EnableViewState="False">
                                                            <asp:ListItem>A</asp:ListItem>
                                                            <asp:ListItem>B</asp:ListItem>
                                                            <asp:ListItem>C</asp:ListItem>
                                                            <asp:ListItem>D</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panelduoxuan" runat="server" Height="50px" Visible="False">
                                            <table class="style2">
                                                <tr>
                                                    <td  class="style3">
                                                        请选择(可多选)：</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cbx_duoxuandaan" runat="server" Height="34px" 
                                                            RepeatDirection="Horizontal" Width="287px" EnableViewState="False">
                                                            <asp:ListItem>A</asp:ListItem>
                                                            <asp:ListItem>B</asp:ListItem>
                                                            <asp:ListItem>C</asp:ListItem>
                                                            <asp:ListItem Value="D"></asp:ListItem>
                                                            <asp:ListItem>E</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panelpanduan" runat="server" Height="50px" Visible="false">
                                        <table class="style2">
                                            <tr>
                                                <td class="style3">
                                                    请选择(二选一)：</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rbl_panduandaan" runat="server" Height="16px" 
                                                        RepeatDirection="Horizontal" Width="157px" EnableViewState="False">
                                                        <asp:ListItem Value="T">正确</asp:ListItem>
                                                        <asp:ListItem Value="F">错误</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel_tiankong" runat="server" Visible="False" Height="80px">
                                            <table class="style2">
                                                <tr>
                                                    <td class="style3">
                                                        请在此填入答案：</td>
                                                    <td>
                                                        <asp:TextBox ID="tbx_tiankongdaan" runat="server" Width="173px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Paneljianda" runat="server" Height="180px">
                                            <table class="style2">
                                                <tr>
                                                    <td class="style3">
                                                        请在此填入答案：</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="tbx_jiandadaan" runat="server" Height="150px" 
                                                            TextMode="MultiLine" Width="945px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="Panelcaozuo" Visible="false" Height="100px">
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">下载本题资源文件</asp:HyperLink><br />
                                      <font color="red">请注意：按题目要求完成后，点击'浏览'选择文件,点'上传文件'按钮提交文件。需要逐题上传。其它按钮没有上传文件功能。不能上传程序文件,文件大小不能超过2MB。</font>
                                        <br />
                                        <asp:HyperLink ID="Hylk_yhwenjian" runat ="server" Target="_blank">下载我的文件</asp:HyperLink><br />
                   
                                        <asp:FileUpload ID="fileupload1" runat="server" /><asp:Button ID="Button2" onclick="btn_UPFile_Click"
                                            runat="server" Text="上传文件" />
                                        </asp:Panel>
&nbsp;<asp:Timer ID="Timerzdjiao" runat="server" Enabled="False" 
                                            ontick="Timerzdjiao_Tick">
                                        </asp:Timer>
                                        <asp:HiddenField ID="HFkechengid" runat="server" />
                                         
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 950px">
                                        <asp:Button ID="btn_pretimu" runat="server" OnClick="Button1_Click" Text="上一题" />&nbsp; &nbsp; <asp:Button ID="btn_nexttimu" runat="server" OnClick="Button2_Click" Text="下一题" />
                                        &nbsp; &nbsp;转到第<asp:DropDownList ID="ddl_tihaoliebiao" runat="server">
                                        </asp:DropDownList>题&nbsp;<asp:Button ID="btn_timutiaozhuan" runat="server" 
                                            onclick="btn_timutiaozhuan_Click" Text="跳转" />
&nbsp;  &nbsp;
                                         <asp:Button ID="btn_jianchashijuan" runat="server" Text="试卷检查" 
                       onclick="btn_jianchashijuan_Click" />  &nbsp;  &nbsp;
                                         <asp:Button ID="Buttonjiaojuan" runat="server" Text="交卷" OnClick="Buttonjiaojuan_Click" Width="131px" OnClientClick="return confirm('交卷之后不能再答题!你确定要交卷吗？');" />
                                    </td>
                                </tr>
                            </table><asp:Timer ID="Timerdjszdbc" runat="server" ontick="Timerdjszdbc_Tick" 
                                Interval="180000">
                                        </asp:Timer>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button2" />
                        </Triggers>
                   </asp:UpdatePanel>

                </td>
            </tr>
        </table>
        </div>
    </form>
    <div class="clear_div2 footer">
<dl class="clear_div2 footer">
<dt>郑州师范学院信息科学与技术学院&nbsp;&nbsp;设计开发</dt>
<dd><span>联系人：孙老师</span><span>QQ:326641288</span><span>电话(集团号):617068</span><span>电子邮箱：slp2060@163.com</span>
<p>豫ICP备14002493</p></dd>
</dl>
</div>
</body>
</html>
