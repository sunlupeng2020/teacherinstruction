<%@ Page Title="教师首页" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="teachermanage_Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                    <br />
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" style=" text-align:center">
                                <h4>网站功能概览</h4></td>
                        </tr>
                        <tr>
                            <td style="height: 47px; width: 93px">
                                课程管理</td>
                            <td style="height: 47px">
                                 <a href="createcourse.aspx" title="创建新课程">新建课程</a>&nbsp; &nbsp; <a href="zhishimanage.aspx" title="通过添加课程知识点，创建课程结构">输入课程结构</a>&nbsp;&nbsp;
                                 <a href="zhishidianbianji.aspx" title="修改知识点信息，删除知识点">课程结构编辑</a>&nbsp;&nbsp;<a href="kecheng_update.aspx" title="修改课程介绍、课程图像等">课程信息更新</a>
                                  <a href="sourceview.aspx">浏览课程内容</a></td>
                        </tr>
                        <tr>
                            <td style="height: 44px; width: 93px">
                                教学资源 </td><td><a href="ziyuantianjia.aspx">添加教学资源</a>&nbsp;&nbsp;&nbsp; <a href="ziyuanxiazai.aspx">浏览下载教学资源</a>&nbsp;&nbsp;  <a href="ziyuantongji.aspx" title="查看各课程的资源数目">教学资源统计</a>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 41px; width: 93px">
                                题目管理</td>
                            <td style="height: 41px">
                                        <a href="addtimu.aspx">添加题目</a>&nbsp;&nbsp;
                    <a href="xiugaitimu.aspx" title="按知识点或关键字查询题目，修改题目">查询修改题目</a></td>
                        </tr>
                        <tr>
                            <td style="height: 49px; width: 93px">
                                测试系统</td>
                            <td style="height: 49px">
                                <a href="ceshizujuan.aspx" title="有自动、半自动、手工三种组卷方式">在线组卷</a>&nbsp;&nbsp;
                    <a href="ceshidefault.aspx" title="设置学生在线考试状态等">在线测试</a>&nbsp;&nbsp;
                    <a href="ceshidefault.aspx" title="测试的各种管理">测试操作——测试的各种管理操作</a> &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 42px; width: 93px">
                                作业系统</td>
                            <td style="height: 42px">
                                <a href="zuoyeyuzhi.aspx" title="设置作业题目,需要时布置给学生">创建作业</a>&nbsp;&nbsp; <a href="zuoyemanage.aspx" title="作业批改、分析、汇总等">班级作业管理</a>&nbsp;<a href="myzuoye.aspx">我的作业</a></td>
                        </tr>
                        <tr>
                            <td style="height: 45px; width: 93px">
                                点名系统</td><td><a href="dianming.aspx">在线点名</a> <a href="dianmingtongji.aspx">点名统计</a>&nbsp;&nbsp; 
                                修改考勤状态，查询学生考勤详情。</td>
                        </tr>
                        <tr>
                            <td style="height: 48px; width: 93px">
                                <a href="../dayionline/Default.aspx">答疑系统</a></td>
                            <td style="height: 48px">
                                针对课程知识点提出问题、回答问题，对回答进行评价等。<a href="../dayionline/Default.aspx">进入答疑系统</a></td>
                        </tr>
                        <tr>
                            <td style="height: 48px; width: 93px">
                                个人信息</td>
                            <td style="height: 48px">
                                <a href="wanshanxinxi.aspx">完善个人信息</a>
                    <a href="gerenxinxi.aspx">修改密码</a>
             </td>
                        </tr>
                        <tr>
                            <td style="height: 34px; width: 93px">
                                帮助系统</td>
                            <td style="height: 34px">
             <a href="../help.aspx">使用说明</a>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 64px; width: 93px">
                                </td>
                            <td style="height: 64px">
                                </td>
                        </tr>
                    </table>                
 </asp:Content>

