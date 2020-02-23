<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="zuoyepigai.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="批改作业主观题" Inherits="teachermanage_zuoyepigai"%>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
         <tr>
            <td>
                &nbsp;班级：<asp:Label ID="Labelbanji" runat="server"></asp:Label>
                &nbsp; 作业名称：<asp:Label ID="Labelzuoyename" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr><td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                    <table>
                    <tr>
                        <td>
                            选择学生</td>
                        <td>
                            学生作业中的主观题列表</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True"  DataTextField="stu" 
                                DataValueField="username" Rows="20" Height="257px" Width="175px" 
                                ondatabound="ListBox1_DataBound"></asp:ListBox>
                        </td>
                        <td style="width:860px; vertical-align:top;">
                            <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" 
                                AutoGenerateRows="False" CellPadding="4" DataKeyNames="stuzuoyetimuid" 
                                DataSourceID="SqlDataSource1" EmptyDataText="作业中没有主观题。" ForeColor="#333333" 
                                GridLines="None" Height="50px" Width="662px" 
                                onitemupdated="DetailsView1_ItemUpdated" DefaultMode="Edit">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" Width="120px" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <Fields>
                                    <asp:BoundField DataField="题型" HeaderText="题型" ReadOnly="True" />
                                    <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" 
                                        ReadOnly="True" />
                                    <asp:BoundField DataField="参考答案" HeaderText="参考答案" ReadOnly="True"  HtmlEncode="False" />
                                    <asp:HyperLinkField DataNavigateUrlFields="题目文件" HeaderText="题目文件" 
                                        Text="题目文件" />
                                    <asp:BoundField DataField="分值" HeaderText="分值" ReadOnly="True" />
                                    <asp:HyperLinkField DataNavigateUrlFields="学生文件" HeaderText="学生文件" 
                                        Text="学生文件" />
                                    <asp:BoundField DataField="回答" HeaderText="回答" HtmlEncode="False" 
                                        ReadOnly="True" />
                                    <asp:TemplateField HeaderText="得分">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddl_defen" runat="server" 
                                                ondatabinding="ddl_defen_DataBinding" SelectedValue='<%# Bind("defen") %>'>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("defen") %>'></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("defen") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField EditText="编辑得分" ShowEditButton="True" />
                                </Fields>
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                                    HorizontalAlign="Right" Width="100px" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:DetailsView>
                            <br />
                            评语：<asp:DropDownList ID="DropDownList1" runat="server" 
                                DataSourceID="SqlDataSource2" DataTextField="pingyu" DataValueField="pingyu" 
                                Height="20px" Width="504px" AutoPostBack="True" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:TextBox ID="TextBoxpingyu" runat="server" Rows="3" TextMode="MultiLine" 
                                Width="558px"></asp:TextBox>
                            <br />
                            <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="提交评语" />
                            <br />
                            <asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                SelectCommand="SELECT DISTINCT [pingyu] FROM [tb_studentzuoye]">
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                DeleteCommand="DELETE FROM [tb_stuzuoyetimu] WHERE [stuzuoyetimuid] = @stuzuoyetimuid" 
                                InsertCommand="INSERT INTO [tb_stuzuoyetimu] ([answer], [filepath], [questionid], [defen], [fenzhi]) VALUES (@tihao, @answer, @filepath, @questionid, @defen, @fenzhi)" 
                                SelectCommand="SELECT [stuzuoyetimuid], tb_tiku.type as 题型,tb_tiku.timu as 题目,tb_tiku.answer as 参考答案,tb_tiku.filepath as 题目文件,tb_stuzuoyetimu.filepath as 学生文件, tb_stuzuoyetimu.[answer] as 回答, [defen], tb_stuzuoyetimu.[fenzhi] as 分值 FROM [tb_stuzuoyetimu] inner join tb_tiku on tb_tiku.questionid=tb_stuzuoyetimu.questionid WHERE (([studentusername] = @studentusername) AND ([zuoyeid] = @zuoyeid) and (tb_tiku.type&lt;&gt;'单项选择题' and tb_tiku.type&lt;&gt;'多项选择题' and tb_tiku.type&lt;&gt;'判断题'))" 
                                UpdateCommand="UPDATE [tb_stuzuoyetimu] SET [defen] = @defen WHERE [stuzuoyetimuid] = @stuzuoyetimuid">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ListBox1" Name="studentusername" 
                                        PropertyName="SelectedValue" Type="String" />
                                    <asp:QueryStringParameter Name="zuoyeid" QueryStringField="zuoyeid" 
                                        Type="Int32" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="stuzuoyetimuid" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="defen" Type="Int32" />
                                    <asp:Parameter Name="stuzuoyetimuid" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="answer" Type="String" />
                                    <asp:Parameter Name="filepath" Type="String" />
                                    <asp:Parameter Name="questionid" Type="Int32" />
                                    <asp:Parameter Name="defen" Type="Int32" />
                                    <asp:Parameter Name="fenzhi" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr> 
                    </table>                 
                     </ContentTemplate>
                   </asp:UpdatePanel>
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
