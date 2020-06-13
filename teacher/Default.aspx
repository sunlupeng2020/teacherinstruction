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
                            <td style="height: 41px; width: 93px">
                                题目管理</td>
                            <td style="height: 41px">
                                        <a href="addtimu.aspx">添加题目</a>&nbsp;&nbsp;
                    <a href="xiugaitimu.aspx" title="按知识点或关键字查询题目，修改题目">查询修改题目</a></td>
                        </tr>
                        <tr>
                            <td style="height: 64px; width: 93px">
                                </td>
                            <td style="height: 64px">
                                </td>
                        </tr>
                    </table>                
 </asp:Content>

