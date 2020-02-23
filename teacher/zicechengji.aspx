<%@ Page Language="C#" MasterPageFile="TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zicechengji.aspx.cs" Inherits="teachermanage_zuzhiceshi_zicechengji" EnableTheming="true"  Title="学生自测成绩查询" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td  class="biaotijishuoming">
            <table>
            <tr>
            <td id="wangyezuocebiaoti" style="text-align: left; vertical-align:top; width: 120px;">
                <span style="font-family: 微软雅黑;font-size: 14px; color:White;"><strong>
            学生自测成绩</strong></span>
            </td>
                <td style="height: 20px; text-align: center; color:Blue;">
            </td>
            </tr>
            </table> 
    </td>
        </tr>
        <tr>
            <td style="height: 29px">
                课程：<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
                    DataTextField="kecheng" DataValueField="kecheng">
                </asp:DropDownList>
                班级：<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2"
                    DataTextField="banji" DataValueField="banji">
                </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    SelectCommand="SELECT [banji], [kecheng] FROM [tb_TeacherRenke] WHERE ([kecheng] = @kecheng) ORDER BY [renkeid] DESC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="kecheng" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    SelectCommand="SELECT DISTINCT [kecheng] FROM [tb_TeacherRenke] WHERE ([teacherusername] = @teacherusername)">
                    <SelectParameters>
                        <asp:SessionParameter   Name="teacherusername" SessionField="username"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="height: 329px" valign="top">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Width="971px">
                    <Columns>
                        <asp:BoundField DataField="xuehao" HeaderText="成绩单编号" SortExpression="xuehao" />
                        <asp:BoundField DataField="xingming" HeaderText="姓名" SortExpression="xingming" />
                        <asp:BoundField DataField="xingbie" HeaderText="性别" SortExpression="xingbie" />
                        <asp:BoundField DataField="username" HeaderText="学生用户名" SortExpression="username" />
                        <asp:BoundField DataField="ceshiname" HeaderText="测试名称" SortExpression="ceshiname" />
                        <asp:BoundField DataField="zhishidianname" HeaderText="测试知识点" SortExpression="zhishidianname" />
                        <asp:BoundField DataField="ceshishijian" HeaderText="测试时间" SortExpression="ceshishijian" />
                        <asp:BoundField DataField="fenshu" HeaderText="得分" SortExpression="fenshu" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    SelectCommand="SELECT  [tb_ceshi].[ceshiname],  [tb_ceshi].[zhishidianname],  [tb_ceshi].[ceshishijian], [fenshu],  [tb_ceshi].[username],tb_student.xuehao,tb_student.xingming,tb_student.xingbie FROM [tb_ceshi],tb_student WHERE (tb_ceshi.[kechengname] = @kechengname) and tb_ceshi.usershenfen='student' and tb_student.username=tb_ceshi.username ORDER BY tb_student.xuehao">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="kechengname" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

