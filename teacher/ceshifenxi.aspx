<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshifenxi.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="试卷分析" Inherits="teachermanage_ceshifenxi" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="width:1024px; text-align:center;">
        班级：<asp:Label ID="Labelbj" runat="server"></asp:Label>&nbsp;&nbsp; 测试名称：<asp:Label ID="Labelceshiname" runat="server"></asp:Label>
    &nbsp;&nbsp; 命题方式：<asp:Label ID="Labelmtfs" runat="server"></asp:Label>
                    <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT max([zongfen]) as 最高分,min([zongfen]) as 最低分,avg([zongfen]) as 平均分 FROM [tb_studentkaoshi] WHERE ([shijuanid] = @shijuanid)">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
                    <asp:Repeater ID="Repeater1" runat="server" 
            DataSourceID="SqlDataSource8">
                        <HeaderTemplate>
                            <table>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    平均分:</td>
                                <td>
                                    <%#Eval("平均分") %> 分，</td>
                                <td>
                                    最高分:</td>
                                <td>
                                    <%#Eval("最高分") %> 分，</td>
                                <td>
                                    最低分</td>
                                <td>
                                    <%#Eval("最低分") %> 分。</td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="questionid" DataSourceID="SqlDataSource5" 
            Width="981px" onrowdatabound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="questionid" HeaderText="题目ID" InsertVisible="False" 
                                ReadOnly="True" SortExpression="questionid">
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="type" HeaderText="题型" SortExpression="type">
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="timu" HeaderText="题目" HtmlEncode="False" 
                                SortExpression="timu">
                            <ItemStyle Width="400px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="answer" HeaderText="参考答案" SortExpression="answer" >
                                <ItemStyle Width="240px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="人次">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="得分率">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="相关知识点">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
    
    </div>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        SelectCommand="SELECT [questionid],[timu], [answer], [type] FROM [tb_tiku] where questionid in(select distinct questionid from tb_studentkaoshiti where shijuanid=@shijuanid)">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" />
                        </SelectParameters>
                    </asp:SqlDataSource>
</asp:Content>