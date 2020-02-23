<%@ Page Language="C#" MasterPageFile="MasterPageManager.master" AutoEventWireup="true" CodeFile="editstudent.aspx.cs" Inherits="teachermanage_studentmanage_xueshengzhuanban" Title="修改、删除学生信息"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
<TABLE class="layouttable"><TBODY><TR><TD class="biaotijishuoming">
            <table>
            <tr>
            <td id="wangyezuocebiaoti" style="text-align: left; vertical-align:top; width: 120px;">
                <span style="font-family: 微软雅黑;font-size: 14px; color:White;"><strong>
            修改学生信息</strong></span>
            </td>
                <td style="height: 20px; text-align: center; color:Blue;">
                </td>
            </tr>
            </table> 
</TD></TR><TR style="VERTICAL-ALIGN: top; HEIGHT: 20px"><TD style="HEIGHT: 20px"><TABLE style="WIDTH: 970px; HEIGHT: 20px"><TBODY><TR>
        <TD style="TEXT-ALIGN: center">系部：<asp:DropDownList ID="ddl_xibu" runat="server" 
                AutoPostBack="True" DataSourceID="SqlDataSourcexibu" DataTextField="xibuname" 
                DataValueField="xibuid">
            </asp:DropDownList>
            &nbsp;&nbsp; 班级：<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                DataTextField="banjiname" 
                DataValueField="banjiid" 
                onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                DataSourceID="SqlDataSourcebanji">
            </asp:DropDownList>
            &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" CausesValidation="False" 
                onclick="Button1_Click" Text="显示该班学生" />
            <asp:SqlDataSource ID="SqlDataSourcebanji" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [banjiid], [banjiname] FROM [tb_banji] WHERE ([xibuid] = @xibuid) ORDER BY [banjiid] DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_xibu" Name="xibuid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            &nbsp;<asp:SqlDataSource ID="SqlDataSourcexibu" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
            </asp:SqlDataSource>
        </TD></TR></TBODY></TABLE></TD></TR><TR><TD style="VERTICAL-ALIGN: top; HEIGHT: 59px">
        <asp:GridView id="GridView1" runat="server" Width="962px" ForeColor="#333333" 
            DataSourceID="SqlDataSource4" EmptyDataText="没有找到该班学生信息。" AllowSorting="True" 
            AutoGenerateColumns="False" PageSize="15" AllowPaging="True" CellPadding="4" 
            GridLines="None" DataKeyNames="banjistudentid">
<FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True"></FooterStyle>

<RowStyle BackColor="#EFF3FB"></RowStyle>

<EditRowStyle BackColor="#2461BF"></EditRowStyle>

<SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>

<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

<HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True"></HeaderStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
    <Columns>
        <asp:TemplateField HeaderText="序号" SortExpression="xuhao">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="5" 
                    Text='<%# Bind("xuhao") %>' Height="20px" Width="58px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBox2" ErrorMessage="请输入序号！" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="序号应为数字!" 
                    SetFocusOnError="True" Type="Integer" Operator="GreaterThan" 
                    ValueToCompare="0"></asp:CompareValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("xuhao") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="studentusername" HeaderText="学号" 
            SortExpression="studentusername" ReadOnly="True" />
        <asp:BoundField DataField="xingming" HeaderText="姓名" ReadOnly="True" />
        <asp:BoundField DataField="xingbie" HeaderText="性别"  ReadOnly="True" />
        <asp:TemplateField ShowHeader="False" HeaderText="编辑删除">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                    CommandName="Edit" Text="修改序号" ToolTip="修改学生在班级中的序号"></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                    CommandArgument='<%# Eval("studentusername") %>' CommandName="Delete" 
                    OnClientClick="return confirm('您确定要从班级中删除该学生吗？');" Text="删除" ToolTip="从班级中删除学生，不会彻底删除学生"></asp:LinkButton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="更新"></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                    CommandName="Cancel" Text="取消"></asp:LinkButton>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="banjistudentid" HeaderText="banjistudentid" SortExpression="banjistudentid" 
            ReadOnly="True" InsertVisible="False" Visible="False" />
        <asp:HyperLinkField DataNavigateUrlFields="studentusername" 
            DataNavigateUrlFormatString="editstudentinfo.aspx?stuusername={0}" 
            HeaderText="详细信息及修改" Target="_blank" Text="详细信息及修改" />
    </Columns>
</asp:GridView><asp:SqlDataSource id="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT tb_banjistudent.banjistudentid,tb_banjistudent.xuhao,tb_banjistudent.[studentusername],tb_student.[xingming], tb_student.[xingbie] FROM tb_banjistudent join  [tb_Student] on tb_banjistudent.studentusername=tb_student.username WHERE (tb_banjistudent.[banjiid] = @banjiid) ORDER BY tb_banjistudent.xuhao" 
            OldValuesParameterFormatString="original_{0}" 
            ConflictDetection="CompareAllValues" 
            UpdateCommand="UPDATE [tb_banjistudent] SET [xuhao] = @xuhao WHERE [banjistudentid] = @original_banjistudentid" 
            InsertCommand="INSERT INTO [tb_Student] ([username], [xuehao], [xingming], [xingbie], [banji]) VALUES (@username, @xuehao, @xingming, @xingbie, @banji)" 
            DeleteCommand="DELETE FROM [tb_banjistudent] WHERE [banjistudentid] = @original_banjistudentid">
<SelectParameters>
    <asp:ControlParameter ControlID="DropDownList3" Name="banjiid" 
        PropertyName="SelectedValue" />
</SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="original_banjistudentid" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="xuhao" />
        <asp:Parameter Name="original_banjistudentid" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="username" Type="String" />
        <asp:Parameter Name="xuehao" Type="Int32" />
        <asp:Parameter Name="xingming" Type="String" />
        <asp:Parameter Name="xingbie" Type="String" />
        <asp:Parameter Name="banji" Type="String" />
    </InsertParameters>
</asp:SqlDataSource> </TD></TR><TR><TD style="WIDTH: 212px; TEXT-ALIGN: left" vAlign=top></TD></TR></TBODY></TABLE>
</contenttemplate>
    </asp:UpdatePanel>
</asp:Content>

