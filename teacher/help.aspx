<%@ Page Language="C#" MasterPageFile="TeacherMasterPage.master" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="teachermanage_help" EnableTheming="true"  Title="使用说明" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 136%">
        <tr>
            <td colspan="2" style="height: 29px; text-align: center">
            </td>
        </tr>
        <tr>
            <td style="width: 446px; text-align: center; height: 24px;">
                <strong><span style="font-size: 14pt; color: #3300ff">
                网站功能说明 </span></strong>
            </td>
            <td style="height: 24px; width: 492px; text-align: center;" valign="top">
                <span style="font-size: 14pt; color: #3300ff"><strong>
                网站使用流程 </strong></span>
            </td>
        </tr>
        <tr>
        <td style="width: 446px; text-align: left">
         <span style="color: #ff6600"><strong>网站功能：</strong></span>教师使用本网站，可以组织测试、布置和批改电子作业、教学资源的添加、下载、在线答疑等。<br />
                            <span style="color: #ff6600"><strong>使用流程：</strong></span>老师使用本网站，一般要先进行<b><a href="createcourse.aspx">创建课程</a></b>、<span
                                style="color: #ff6600; font-weight: bold;"><a href="zhishimanage.aspx">创建课程结构</a></span>等步骤。<br />
                            <span style="color: #ff6600; font-weight: bold;"><a href="createcourse.aspx">
            创建课程</a></span>如果本网站中没有您任教的课程，需要进行本操作,课程的创建者默认为课程的管理员，系统管理员可以修改课程管理员。<br />
                            <span style="color: #ff6600; font-weight: bold;"><a href="zhishimanage.aspx">
            创建课程结构</a></span>题目和教学资源要与课程的知识点相对应，有了课程结构，才能按知识点<b><a
                                href="addtimu.aspx">添加题目</a></b>、<b><a href="ziyuantianjia.aspx">添加教学资源</a></b>、进行测试等。方法是课程管理--&gt;<b><a
                                href="zhishimanage.aspx">输入结构</a></b>。课程知识结构可以逐渐完善。<br />
                            <span style="color: #ff6600"><a href="addtimu.aspx">添加题目</a></span>有了题目，才可以实现在线测试、电子作业等功能。<br />
                                        如果没有您任教的班级，请联系管理员添加班级、添加学生信息。<br />
                            如果没有任课信息，请联系管理员添加本人任课信息。<br />
                            如果课程结构、题库、任课信息等都具备，您就可以使用本网站的所有功能了！具体功能请查看网站菜单。
        
        
        </td>
            <td style="height: 197px; width: 492px;" valign="top">
                               <asp:ImageMap ID="ImageMap1" runat="server" 
                                   ImageUrl="~/images/teacherbangzhu.gif">
                                   <asp:RectangleHotSpot AlternateText="如果没有本人任教课程，请创建课程" Bottom="34" Left="158" 
                                       NavigateUrl="~/teacher/createcourse.aspx" Right="216" Top="17" />
                                   <asp:RectangleHotSpot AlternateText="如果没有本人任教班级，请联系管理员添加班级" Bottom="35" Left="323" 
                                       NavigateUrl="" Right="382" Top="17" />
                                   <asp:RectangleHotSpot AlternateText="输入课程的章、节结构信息" Bottom="99" Left="71" 
                                       NavigateUrl="~/teacher/zhishimanage.aspx" Right="156" Top="85" />
                                   <asp:RectangleHotSpot AlternateText="如果没有任课信息，请联系管理员添加本人任课信息，包括课程和班级" Bottom="102" Left="240" 
                                       NavigateUrl="" Right="299" Top="85" />
                                   <asp:RectangleHotSpot AlternateText="往班级中添加学生，以便布置作业、组织测试等，此项工作由管理员来做" Bottom="108" 
                                       Left="403" NavigateUrl="" Right="458" Top="88" />
                                   <asp:RectangleHotSpot AlternateText="上传教学资源，包括课件、教案等" Bottom="175" Left="148" 
                                       NavigateUrl="~/teacher/ziyuantianjia.aspx" Right="206" Top="160" />
                                   <asp:RectangleHotSpot AlternateText="有了题目，才能布置作业、组织测试" Bottom="191" Left="151" 
                                       NavigateUrl="~/teacher/addtimu.aspx" Right="207" Top="176" />
                                   <asp:RectangleHotSpot AlternateText="创建一个作业，然后布置给学生" Bottom="267" Left="239" 
                                       NavigateUrl="~/teacher/zuoyeyuzhi.aspx" Right="299" Top="253" />
                                   <asp:RectangleHotSpot AlternateText="在线组卷，有手工、半自动、自动三种方式" Bottom="287" 
                                       Left="241" NavigateUrl="~/teacher/ceshizujuan.aspx" Right="298" Top="270" />
                               </asp:ImageMap>
            </td>
        </tr>
    </table>
</asp:Content>

