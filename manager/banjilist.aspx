<%@ Page Language="C#" MasterPageFile="~/manager/BanjiXuesheng.master" AutoEventWireup="true" CodeFile="banjilist.aspx.cs" Inherits="manager_banjilist" Title="班级列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BjXsContentPlaceHolder" Runat="Server">
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
<TABLE><TBODY>
        <tr>
            <td colspan="2">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
                </asp:SqlDataSource>
            </td>
    </tr>
        <TR><TD style="WIDTH: 267px; vertical-align: middle;">
            系部:<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource1" DataTextField="xibuname" DataValueField="xibuid" 
                ondatabound="DropDownList1_DataBound" 
                onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="151px">
            </asp:DropDownList>
            </TD>
            <td style="WIDTH: 420px; vertical-align: middle;" class="alignlefttd">
                <asp:Label ID="lbl_banjiname" runat="server"></asp:Label>
                学生列表</td>
    </TR>
    <tr>
        <td style="WIDTH: 150px; vertical-align: top;">
            <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource4" DataTextField="banjiname" 
                DataValueField="banjiid" Height="176px" ondatabound="ListBox1_DataBound" 
                onselectedindexchanged="ListBox1_SelectedIndexChanged" Width="257px">
            </asp:ListBox>
            <br />
            <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                onclick="Button2_Click" Text="查看该班学生" Width="146px" />
            <br />
            <asp:Button ID="Button3" runat="server" CausesValidation="False" 
                onclick="Button3_Click" Text="删除该班级" 
                ToolTip="班级没有学生，并且没有教师任该班的课，才能删除班级。" Width="145px" />
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [banjiid], [banjiname] FROM [tb_banji] WHERE ([xibuid] = @xibuid) ORDER BY [banjiid] DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList1" Name="xibuid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td style="WIDTH: 600px; vertical-align: top;">
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                
                SelectCommand="SELECT [banjistudentid], [xuhao], [studentusername] as 学号,tb_student.xingming as 姓名, tb_student.xingbie 性别, tb_zhuanye.zhuanyename 专业 FROM [tb_banjistudent] join tb_student on tb_student.username=tb_banjistudent.studentusername join tb_zhuanye on tb_zhuanye.zhuanyeid=tb_student.zhuanyeid WHERE ([banjiid] = @banjiid) ORDER BY [xuhao]" 
                DeleteCommand="DELETE FROM [tb_banjistudent] WHERE [banjistudentid] = @banjistudentid" 
                InsertCommand="INSERT INTO [tb_banjistudent] ([xuhao], [studentusername]) VALUES (@xuhao, @studentusername)" 
                UpdateCommand="UPDATE [tb_banjistudent] SET [xuhao] = @xuhao WHERE [banjistudentid] = @banjistudentid">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ListBox1" Name="banjiid" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="banjistudentid" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="xuhao" Type="Int32" />
                    <asp:Parameter Name="banjistudentid" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="xuhao" Type="Int32" />
                    <asp:Parameter Name="studentusername" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                DataKeyNames="banjistudentid" DataSourceID="SqlDataSource3" 
                ForeColor="#333333" Width="600px" EmptyDataText="没有该班学生信息。">
                <Columns>
                    <asp:BoundField DataField="banjistudentid" HeaderText="banjistudentid" 
                        SortExpression="banjistudentid" InsertVisible="False" ReadOnly="True" 
                        Visible="False" />
                    <asp:TemplateField HeaderText="序号" SortExpression="xuhao">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("xuhao") %>'></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="必须是正整数。" 
                                MaximumValue="10000" MinimumValue="1" Type="Integer" 
                                ValidationGroup="editxuhao"></asp:RangeValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("xuhao") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="学号" HeaderText="学号" SortExpression="学号" 
                        ReadOnly="True" />
                    <asp:BoundField DataField="姓名" HeaderText="姓名" ReadOnly="True" 
                        SortExpression="姓名" />
                    <asp:BoundField DataField="性别" HeaderText="性别" ReadOnly="True" 
                        SortExpression="性别" />
                    <asp:BoundField DataField="专业" HeaderText="专业" ReadOnly="True" 
                        SortExpression="专业" />
                    <asp:TemplateField ShowHeader="False" HeaderText="操作">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                CommandName="Update" Text="更新" ValidationGroup="editxuhao"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="修改序号"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                                CommandName="Delete" Text="删除" 
                                OnClientClick="return confirm('您确定要从该班删除该学生吗?')" 
                                ToolTip="只是从班级中删除学生，不会彻底删除学生数据"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="学号" 
                        DataNavigateUrlFormatString="editstudentinfo.aspx?stuusername={0}" 
                        HeaderText="编辑学生信息" Text="编辑学生信息" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Labelfankui" runat="server" __designer:wfdid="w3" 
                ForeColor="Red" Width="485px"></asp:Label>
        </td>
    </tr>
    </TBODY></TABLE>
</contenttemplate>
</asp:UpdatePanel>

</asp:Content>

