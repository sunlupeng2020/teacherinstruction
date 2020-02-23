<%@ Page Language="C#" AutoEventWireup="true" EnableTheming="true" MasterPageFile="~/teacher/TeacherMasterPage.master" CodeFile="zuoyefenxi.aspx.cs" Inherits="teachermanage_zuoyemanage_zuoyefenxi" Title="作业分析"%>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
 <table border="0" cellpadding="0" cellspacing="0" style="width: 980px; height: 100%">
        <tr>
            <td>
                班级：<asp:Label ID="Labelbanji" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp; 作业名称：<asp:Label ID="Labelzuoyename" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;完成人数：<asp:Label ID="Labelsjrs" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 32px; text-align: center">
                <asp:FormView ID="FormView1" runat="server"
                    Height="25px" Width="572px">
                <ItemTemplate>
                <table><tr><td>平均分:</td><td><%#Eval("平均分") %>分，</td><td>最高分:</td><td><%#Eval("最高分") %>分，</td><td>
                    最低分</td><td><%#Eval("最低分") %>分。</td></tr></table>
                </ItemTemplate>
                </asp:FormView>
            </td>
        </tr>
        <tr>
            <td style="height: 32px; text-align: left">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="questionid"
                    OnRowDataBound="GridView1_RowDataBound" Width="981px" 
                    ondatabound="GridView1_DataBound">
                    <Columns>
                        <asp:BoundField >
                            <ItemStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" SortExpression="题目">
                            <ItemStyle Width="360px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="题型" HeaderText="题型" SortExpression="题型">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="参考答案" HeaderText="参考答案" SortExpression="参考答案" 
                            HtmlEncode="False" >
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="分值" HeaderText="分值" SortExpression="分值" >
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="得分率" >
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="相关知识点" >
                            <ItemStyle Width="160px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px; text-align: left">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>