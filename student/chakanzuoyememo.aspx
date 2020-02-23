<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chakanzuoyememo.aspx.cs" Inherits="studentstudy_chakanzuoyememo" MasterPageFile="~/student/StudentMasterPage.master" Title="作业详情" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="content1">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 19px;
        }
        .style3
        {
            font-size: large;
            font-weight: bold;
            color: #0000FF;
        }
    </style>
   <div>
        <asp:FormView ID="FormView1" runat="server">
            <ItemTemplate>
                <table class="style1">
                    <tr>
                        <td>
                            作业名称：</td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("zuoyename") %>'></asp:Label>
                        </td>
                        <td>
                            上交时间：</td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("shangjiaoriqi") %>'></asp:Label>
                        </td>
                        <td>
                            成绩：</td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("zongfen") %>'></asp:Label>分
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            评语：</td>
                        <td class="style2" colspan="5">
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("pingyu") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <hr />
    
        <table class="style1">
            <tr>
                <td class="style3">
                    本次作业统计信息：</td>
                <td>
                     <asp:FormView ID="FormView2" runat="server" 
            DataSourceID="SqlDataSourcezuoyetongji">
            <ItemTemplate>
                <table class="style1">
                    <tr>
                        <td>
                            最高分：</td>
                        <td>
                            <asp:Label ID="zuigaofenLabel" runat="server" Text='<%# Bind("zuigaofen") %>' />
                        </td>
                        <td>
                            最低分：</td>
                        <td>
                            <asp:Label ID="zuidifenLabel" runat="server" Text='<%# Bind("zuidifen") %>' />
                        </td>
                        <td>
                            平均分：</td>
                        <td>
                            <asp:Label ID="pingjunfenLabel" runat="server" 
                                Text='<%# Bind("pingjunfen") %>' />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView></td>
                <td>
        班级人数：<asp:Label ID="Labelrenshu" runat="server"></asp:Label>
        人，本作业成绩排名：第<asp:Label ID="Labelpm" runat="server"></asp:Label>
        名。</td>
            </tr>
        </table>
    
      
        <asp:SqlDataSource ID="SqlDataSourcezuoyetongji" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT max([zongfen]) as zuigaofen,min(zongfen) as zuidifen,avg(zongfen)as pingjunfen FROM [tb_studentzuoye] WHERE ([zuoyeid] = @zuoyeid)">
            <SelectParameters>
                <asp:QueryStringParameter Name="zuoyeid" QueryStringField="zuoyeid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <hr />
        作业题目详情：<br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>