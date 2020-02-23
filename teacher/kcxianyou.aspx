<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="kcxianyou.aspx.cs" Inherits="teacher_kcxianyou" Title="课程列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
       <tr>
            <td style=" height: 19px; text-align: center">
                <span style="font-size: 10pt; font-family: 新宋体">现有课程列表</span></td>
        </tr>
        <tr>
            <td rowspan="5" style="width: 115px; vertical-align: top;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSource1" Width="568px"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="课程名称" HeaderText="课程名称" SortExpression="课程名称" />
                        <asp:BoundField DataField="管理员" HeaderText="管理员" SortExpression="管理员" />
                        <asp:BoundField DataField="创建者" HeaderText="创建者" SortExpression="创建者" />
                        <asp:BoundField DataField="创建时间" HeaderText="创建时间" SortExpression="创建时间"/>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    SelectCommand="SELECT K1.kechengname AS 课程名称, T1.xingming AS 管理员, T2.xingming AS 创建者, K1.createtime AS 创建时间 FROM tb_Kecheng AS K1 INNER JOIN tb_Teacher AS T1 ON K1.guanliyuan = T1.username INNER JOIN tb_Teacher AS T2 ON K1.creater = T2.username" >
                 </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

