<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshidemo.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="测试详细信息及修改"  Inherits="teachermanage_ceshidemo" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        班级：<asp:Label ID="Labelbj" runat="server"></asp:Label>
        测试名称：<asp:Label ID="Labelcsname" runat="server"></asp:Label>
        <br />
        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
            DataKeyNames="shijuanid" DataSourceID="SqlDataSourceceshi" Height="50px" 
            Width="385px" HorizontalAlign="Center" CellPadding="4" 
         ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <Fields>
                <asp:BoundField DataField="ceshizhishidian" HeaderText="测试知识点" ReadOnly="True" 
                    SortExpression="ceshizhishidian" />
                <asp:BoundField DataField="createtime" HeaderText="创建时间" ReadOnly="True" 
                    SortExpression="createtime" />
                <asp:TemplateField HeaderText="时长（分钟）" SortExpression="timelength">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("timelength") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            SelectedValue='<%# Bind("timelength") %>'>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>60</asp:ListItem>
                            <asp:ListItem>70</asp:ListItem>
                            <asp:ListItem>80</asp:ListItem>
                            <asp:ListItem>90</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem>120</asp:ListItem>
                            <asp:ListItem>150</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("timelength") %>'></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="manfen" HeaderText="满分分值" ReadOnly="True" 
                    SortExpression="manfen" />
                <asp:BoundField DataField="mingtifangshi" HeaderText="命题方式" ReadOnly="True" 
                    SortExpression="mingtifangshi" />
                <asp:TemplateField HeaderText="允许做题？" SortExpression="yunxuzuoti">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("yunxuzuoti") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="27px" 
                            RepeatDirection="Horizontal" SelectedValue='<%# Bind("yunxuzuoti") %>' 
                            Width="141px">
                            <asp:ListItem>允许</asp:ListItem>
                            <asp:ListItem>禁止</asp:ListItem>
                        </asp:RadioButtonList>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("yunxuzuoti") %>'></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="允许查看结果？" SortExpression="yunxuchakan">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("yunxuchakan") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" Height="21px" 
                            RepeatDirection="Horizontal" SelectedValue='<%# Bind("yunxuchakan") %>' 
                            Width="120px">
                            <asp:ListItem>允许</asp:ListItem>
                            <asp:ListItem>禁止</asp:ListItem>
                        </asp:RadioButtonList>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("yunxuchakan") %>'></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="限制IP?">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("xianzhiip") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RadioButtonList ID="RadioButtonList3" runat="server" 
                            RepeatDirection="Horizontal" SelectedValue='<%# Bind("xianzhiip") %>'>
                            <asp:ListItem>是</asp:ListItem>
                            <asp:ListItem>否</asp:ListItem>
                        </asp:RadioButtonList>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("xianzhiip") %>'></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="shijuanid" HeaderText="shijuanid" 
                    InsertVisible="False" ReadOnly="True" SortExpression="shijuanid" 
                    Visible="False" />
                <asp:CommandField ShowEditButton="True" />
            </Fields>
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:DetailsView>
        <asp:SqlDataSource ID="SqlDataSourceceshi" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            DeleteCommand="DELETE FROM [tb_teachershijuan] WHERE [shijuanid] = @shijuanid" 
            InsertCommand="INSERT INTO [tb_teachershijuan] ([ceshizhishidian], [createtime], [timelength], [manfen], [mingtifangshi], [yunxuzuoti], [yunxuchakan]) VALUES (@ceshizhishidian, @createtime, @timelength, @manfen, @mingtifangshi, @yunxuzuoti, @yunxuchakan)" 
            SelectCommand="SELECT [ceshizhishidian], [createtime], [timelength], [manfen], [mingtifangshi], [yunxuzuoti], [yunxuchakan], [shijuanid],[xianzhiip] FROM [tb_teachershijuan] WHERE ([shijuanid] = @shijuanid)" 
            UpdateCommand="UPDATE [tb_teachershijuan] SET [timelength] = @timelength, [yunxuzuoti] = @yunxuzuoti, [yunxuchakan] = @yunxuchakan,[xianzhiip]=@xianzhiip WHERE [shijuanid] = @shijuanid">
            <SelectParameters>
                <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" 
                    Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="shijuanid" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="timelength" Type="Int32" />
                <asp:Parameter Name="yunxuzuoti" Type="String" />
                <asp:Parameter Name="yunxuchakan" Type="String" />
                <asp:Parameter Name="shijuanid" Type="Int32" />
                <asp:Parameter Name="xianzhiip" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="ceshizhishidian" Type="String" />
                <asp:Parameter Name="createtime" Type="DateTime" />
                <asp:Parameter Name="timelength" Type="Int32" />
                <asp:Parameter Name="manfen" Type="Int32" />
                <asp:Parameter Name="mingtifangshi" Type="String" />
                <asp:Parameter Name="yunxuzuoti" Type="String" />
                <asp:Parameter Name="yunxuchakan" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        <br />
        题目详情<br />
                    <asp:GridView ID="GridViewtchtimu" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSourceteachershijuantimu" 
                        EmptyDataText="因为不是统一命题，学生的题目各不相同，不能显示试卷题目。" Width="970px">
                        <Columns>
                            <asp:BoundField DataField="题号" HeaderText="题号" SortExpression="题号" >
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="题型" HeaderText="题型" SortExpression="题型" >
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" 
                                SortExpression="题目" />
                            <asp:BoundField DataField="答案" HeaderText="答案" SortExpression="答案" >
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="资源文件" SortExpression="资源文件">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("资源文件") %>'>下载文件</asp:HyperLink>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("资源文件") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="分值" HeaderText="分值" SortExpression="分值" >
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceteachershijuantimu" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT tb_tiku.timu as 题目,tb_tiku.answer as 答案,tb_tiku.type as 题型,tb_tiku.filepath as 资源文件, [tb_teachershijuantimu].tihao as 题号, [tb_teachershijuantimu].fenzhi as 分值 from [tb_teachershijuantimu],[tb_tiku] where (tb_tiku.questionid= [tb_teachershijuantimu].questionid and   [tb_teachershijuantimu].[shijuanid] = @shijuanid) ORDER BY  [tb_teachershijuantimu].[tihao]">
            <SelectParameters>
                <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
</asp:Content>