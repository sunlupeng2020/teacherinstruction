<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshipigaizhgt.aspx.cs" Inherits="teachermanage_ceshipigaikgt" Title="手工批改试卷主观题" MasterPageFile="~/teacher/TeacherMasterPage.master" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        width: 168px;
    }
    </style><div>
        <table class="style1">
            <tr>
                <td colspan="2">
                    测试名称：<asp:Label ID="Labelcsmc" runat="server"></asp:Label>
                    班级：<asp:Label ID="Labelbj" runat="server"></asp:Label>
                    命题方式：<asp:Label ID="Labelmtfs" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    学生列表</td>
                <td style="width:900px; vertical-align:text-top;">
                    题目及批改</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSourcestu" DataTextField="stu" 
                        DataValueField="username" Height="334px" Rows="30" Width="157px">
                    </asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSourcestu" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                        
                        SelectCommand="SELECT username+xingming as stu,username FROM tb_student where username in (select studentusername  from [tb_studentkaoshi] WHERE [shijuanid] = @shijuanid) ORDER BY [username]">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                SelectCommand="SELECT [shitiid], [timuhao] as 题号,tb_tiku.[type] as 题型,tb_tiku.timu as 题目,tb_tiku.answer as 参考答案,tb_tiku.filepath as 题目文件,tb_studentkaoshiti.filepath as 学生文件, tb_studentkaoshiti.[answer] as 回答, [defen], tb_studentkaoshiti.[fenzhi] as 分值 FROM [tb_studentkaoshiti] inner join tb_tiku on tb_tiku.questionid=tb_studentkaoshiti.questionid WHERE (([studentusername] = @studentusername) AND ([shijuanid] = @shijuanid) and (tb_tiku.type&lt;&gt;'单项选择题' and tb_tiku.type&lt;&gt;'多项选择题' and tb_tiku.type&lt;&gt;'判断题'))" 
                                UpdateCommand="UPDATE [tb_studentkaoshiti] SET [defen] = @defen WHERE [shitiid] = @shitiid">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ListBox1" Name="studentusername" 
                                        PropertyName="SelectedValue" Type="String" />
                                    <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" 
                                        Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="defen" Type="Int32" />
                                    <asp:Parameter Name="shitiid" Type="Int32" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                            <asp:DetailsView ID="DetailsView1" runat="server" DataSourceID="SqlDataSource1" 
                                Height="50px" Width="655px" AllowPaging="True" AutoGenerateRows="False" 
                                CellPadding="4" DataKeyNames="shitiid" ForeColor="#333333" GridLines="None" 
                                onitemupdated="DetailsView1_ItemUpdated">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <Fields>
                                <asp:BoundField DataField="shitiid" HeaderText="shitiid" 
                                        InsertVisible="False" ReadOnly="True" SortExpression="shitiid" 
                                        Visible="False" />
                                    <asp:BoundField DataField="题号" HeaderText="题号" ReadOnly="True" 
                                        SortExpression="题号" />
                                    <asp:BoundField DataField="题型" HeaderText="题型" ReadOnly="True" 
                                        SortExpression="题型" />
                                    <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" 
                                        ReadOnly="True" SortExpression="题目" />
                                    <asp:HyperLinkField DataNavigateUrlFields="题目文件" HeaderText="题目文件" 
                                        Text="题目文件" />
                                    <asp:BoundField DataField="参考答案" HeaderText="参考答案" HtmlEncode="False" 
                                        ReadOnly="True" SortExpression="参考答案" />
                                    <asp:BoundField DataField="回答" HeaderText="回答" HtmlEncode="False" 
                                        ReadOnly="True" SortExpression="回答" />
                                    <asp:HyperLinkField DataNavigateUrlFields="学生文件" HeaderText="学生文件" 
                                        Text="学生文件" />
                                    <asp:BoundField DataField="分值" HeaderText="分值" ReadOnly="True" 
                                        SortExpression="分值" />
                                    <asp:TemplateField HeaderText="得分" SortExpression="得分">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("defen") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddl_defen" runat="server" 
                                                ondatabinding="ddl_defen_DataBinding" SelectedValue='<%# Bind("defen") %>'>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField EditText="编辑得分" ShowEditButton="True" />
                                </Fields>
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                                    Width="120px" Wrap="False" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:DetailsView>
                            <asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label>
                            <br />
                            </td>
            </tr>
        </table>
    
    </div>
</asp:Content>
