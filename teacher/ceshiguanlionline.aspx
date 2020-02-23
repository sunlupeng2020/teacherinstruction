<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshiguanlionline.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master"  Title="在线实时测试管理" Inherits="teachermanage_zuzhiceshi_studentceshimanage2"  EnableTheming="true"  %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="vertical-align: top; height: 18px; text-align: left">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource2" 
                        Width="377px" DataKeyNames="shijuanid">
                        <EditItemTemplate>
                            ceshiname:
                            <asp:TextBox ID="ceshinameTextBox" runat="server" Text='<%# Bind("ceshiname") %>'>
                            </asp:TextBox><br />
                            kechengname:
                            <asp:TextBox ID="kechengnameTextBox" runat="server" Text='<%# Bind("kechengname") %>'>
                            </asp:TextBox><br />
                            ceshizhishidian:
                            <asp:TextBox ID="ceshizhishidianTextBox" runat="server" Text='<%# Bind("ceshizhishidian") %>'>
                            </asp:TextBox><br />
                            banji:
                            <asp:TextBox ID="banjiTextBox" runat="server" Text='<%# Bind("banji") %>'>
                            </asp:TextBox><br />
                            timelength:
                            <asp:TextBox ID="timelengthTextBox" runat="server" Text='<%# Bind("timelength") %>'>
                            </asp:TextBox><br />
                            manfen:
                            <asp:TextBox ID="manfenTextBox" runat="server" Text='<%# Bind("manfen") %>'>
                            </asp:TextBox><br />
                            mingtifangshi:
                            <asp:TextBox ID="mingtifangshiTextBox" runat="server" Text='<%# Bind("mingtifangshi") %>'>
                            </asp:TextBox><br />
                            yunxuzuoti:
                            <asp:TextBox ID="yunxuzuotiTextBox" runat="server" Text='<%# Bind("yunxuzuoti") %>'>
                            </asp:TextBox><br />
                            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                Text="更新">
                            </asp:LinkButton>
                            <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="取消">
                            </asp:LinkButton>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            ceshiname:
                            <asp:TextBox ID="ceshinameTextBox" runat="server" Text='<%# Bind("ceshiname") %>'>
                            </asp:TextBox><br />
                            kechengname:
                            <asp:TextBox ID="kechengnameTextBox" runat="server" Text='<%# Bind("kechengname") %>'>
                            </asp:TextBox><br />
                            ceshizhishidian:
                            <asp:TextBox ID="ceshizhishidianTextBox" runat="server" Text='<%# Bind("ceshizhishidian") %>'>
                            </asp:TextBox><br />
                            banji:
                            <asp:TextBox ID="banjiTextBox" runat="server" Text='<%# Bind("banji") %>'>
                            </asp:TextBox><br />
                            timelength:
                            <asp:TextBox ID="timelengthTextBox" runat="server" Text='<%# Bind("timelength") %>'>
                            </asp:TextBox><br />
                            manfen:
                            <asp:TextBox ID="manfenTextBox" runat="server" Text='<%# Bind("manfen") %>'>
                            </asp:TextBox><br />
                            mingtifangshi:
                            <asp:TextBox ID="mingtifangshiTextBox" runat="server" Text='<%# Bind("mingtifangshi") %>'>
                            </asp:TextBox><br />
                            yunxuzuoti:
                            <asp:TextBox ID="yunxuzuotiTextBox" runat="server" Text='<%# Bind("yunxuzuoti") %>'>
                            </asp:TextBox><br />
                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                Text="插入">
                            </asp:LinkButton>
                            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="取消">
                            </asp:LinkButton>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <table style="width: 950px">
                                <tr>
                                    <td style="width: 950px">
                                        测试名称：<asp:Label ID="ceshinameLabel" runat="server" Text='<%# Bind("ceshiname") %>'></asp:Label>
                                        &nbsp;&nbsp; 测试知识点：<asp:Label ID="ceshizhishidianLabel" runat="server" Text='<%# Bind("ceshizhishidian") %>'></asp:Label>
                                        &nbsp; 满分分值：<asp:Label ID="manfenLabel" runat="server" Text='<%# Bind("manfen") %>'></asp:Label>
                                        分</td>
                                </tr>
                                <tr>
                                    <td style="width: 950px">
                                        测试班级：
                            <asp:Label ID="banjiLabel" runat="server" Text='<%# Bind("ceshibanji") %>'></asp:Label>
                                        测试时长：<asp:Label ID="timelengthLabel" runat="server" Text='<%# Bind("timelength") %>'></asp:Label>
                                        分钟 &nbsp;&nbsp; 命题方式：<asp:Label ID="mingtifangshiLabel" runat="server" 
                                            Text='<%# Bind("mingtifangshi") %>'></asp:Label>
                                        &nbsp;&nbsp; 是否允许做题:
                            <asp:Label ID="yunxuzuotiLabel" runat="server" Text='<%# Bind("yunxuzuoti") %>'></asp:Label>
                                        &nbsp;&nbsp; 是否限制IP地址:<asp:Label ID="lblxianzhiip" runat="server" Text='<%# Bind("xianzhiip") %>'></asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="width: 950px; height: 21px;">
                                        <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument='<%# Eval("shijuanid") %>'
                                            OnCommand="LinkButton6_Command" Width="114px">全部延长5分钟</asp:LinkButton>&nbsp; &nbsp;<asp:LinkButton ID="LinkButton7" runat="server" CommandArgument='<%# Eval("shijuanid") %>'
                                            OnCommand="LinkButton7_Command" Width="118px">全部禁止答题</asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument='<%# Eval("shijuanid") %>'
                                            OnCommand="LinkButton8_Command">全部允许答题</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:LinkButton 
                                            ID="LinkButtonjiaojuan" runat="server" onclick="LinkButtonjiaojuan_Click">全部设为已交卷</asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp; &nbsp;<asp:LinkButton ID="LBtnweijiaojuan" runat="server" 
                                            onclick="LBtnweijiaojuan_Click">全部设为未交卷</asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="LinkButton10" runat="server" onclick="LinkButton10_Click">刷新学生测试信息表</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                        
                        SelectCommand="SELECT [shijuanid], [ceshiname], [ceshizhishidian], [ceshibanji], [timelength], [manfen], [mingtifangshi], [yunxuzuoti],[xianzhiip] FROM [tb_teachershijuan] WHERE ([shijuanid] = @shijuanid)">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="" Name="shijuanid" QueryStringField="shijuanid"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                               <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
            SelectCommand="SELECT [stks_id],[studentusername], [zongfen], [jiaojuan], [kaishi_shijian], [jiaojuanshijian], [yunxu], [timeyanchang],xingming,[ip] FROM [tb_studentkaoshi],tb_student WHERE ([shijuanid] = @shijuanid and tb_student.username=tb_studentkaoshi.studentusername) ORDER BY [studentusername]"
                        UpdateCommand="UPDATE [tb_studentkaoshi] SET  [jiaojuan] = @jiaojuan, [yunxu] = @yunxu, [timeyanchang] = @timeyanchang WHERE [stks_id] = @stks_id">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="shijuanid" QueryStringField="shijuanid"
                    Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="jiaojuan" Type="Byte" />
                <asp:Parameter Name="yunxu" Type="Byte" />
                <asp:Parameter Name="timeyanchang" Type="Int16" />
                <asp:Parameter Name="stks_id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 200px; text-align: center">
                    <asp:Timer ID="Timer1" runat="server" Interval="300000" ontick="Timer1_Tick">
                    </asp:Timer>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="stks_id"
            DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" PageSize="15" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="stks_id" HeaderText="ID号" InsertVisible="False" ReadOnly="True"
                    SortExpression="stks_id" Visible="False" />
                <asp:BoundField DataField="studentusername" HeaderText="学号" 
                    InsertVisible="False"  ReadOnly="True" />
                <asp:BoundField DataField="xingming" HeaderText="姓名" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="studentusername" HeaderText="studentusername" SortExpression="studentusername" ReadOnly="True"  Visible="False"/>
                <asp:BoundField DataField="zongfen" HeaderText="成绩" SortExpression="zongfen" 
                    ReadOnly="True" />
                <asp:BoundField DataField="kaishi_shijian" HeaderText="开始时间" SortExpression="kaishi_shijian" ReadOnly="True" />
                <asp:BoundField DataField="jiaojuanshijian" HeaderText="交卷时间" SortExpression="jiaojuanshijian" ReadOnly="True" />
                <asp:TemplateField HeaderText="交卷状态" SortExpression="jiaojuan">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("jiaojuan") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("jiaojuan") %>' Visible="False"></asp:Label>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("stks_id") %>'
                            OnCommand="LinkButton1_Command" ForeColor="Red">设为已交卷</asp:LinkButton><asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("stks_id") %>'
                            OnCommand="LinkButton2_Command">设为未交卷</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="是否允许" SortExpression="yunxu">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("yunxu") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("yunxu") %>' Visible="False"></asp:Label><asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Eval("stks_id") %>'
                            OnCommand="LinkButton3_Command">设置为允许答题</asp:LinkButton><asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Eval("stks_id") %>'
                            OnCommand="LinkButton4_Command" ForeColor="Red">设置为禁止答题</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="延长时间" SortExpression="timeyanchang">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("timeyanchang") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        已延长<asp:Label ID="Label3" runat="server" Text='<%# Bind("timeyanchang") %>'></asp:Label>
                        分钟
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument='<%# Eval("stks_id") %>'
                            OnCommand="LinkButton5_Command">延长5分钟</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ip" HeaderText="IP地址" />
                <asp:TemplateField HeaderText="清空IP地址">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton11" runat="server" 
                            CommandArgument='<%# Eval("stks_id") %>' OnCommand="LinkButton11_Click" 
                            ToolTip="清空学生IP地址后，学生可以换机器进行考试">清空IP地址</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle BackColor="#E3EAEB" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
                </td>
            </tr>
        </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>