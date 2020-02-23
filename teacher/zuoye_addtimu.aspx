<%@ Page Language="C#" MasterPageFile="TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zuoye_addtimu.aspx.cs" Inherits="teachermanage_zuoye_addtimu" Title="为作业选择题目" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" 
                    Height="34px" Width="703px">
                    <ItemTemplate>
                        作业名称：<asp:Label ID="Label2" runat="server" Text='<%# Eval("zuoyename") %>'></asp:Label>
                        &nbsp;创建时间：<asp:Label ID="Label3" runat="server" Text='<%# Eval("createtime") %>'></asp:Label>
                        &nbsp; 总分：<asp:Label ID="Label5" runat="server" Text='<%# Eval("manfen") %>'></asp:Label>
                    </ItemTemplate>
                </asp:FormView>
                                </td>
                            </tr>
        <tr>
        <td>选择知识点</td>
            <td>
                                    题型:<asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="mingcheng" 
                    DataValueField="mingcheng" AppendDataBoundItems="True" 
                                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                        <asp:ListItem>全部题型</asp:ListItem>
                </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_searchtimu" runat="server" 
                    Text="查询课程或知识点相关题型题目" onclick="btn_searchtimu_Click" />
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 240px" valign="top">
                                 <div class="kechengtreeview">
                           <cc2:MyTreeView runat="server" ShowCheckBoxes="All" ShowLines="True" Height="148px" 
                                        Width="238px" ID="TreeView1"
                                        ToolTip="可选择若干知识点，查询对应题目。" ExpandDepth="1" />
</div>
                <br />
                                </td>
                                <td style="width: 740px" valign="top">
                                    <asp:GridView ID="grvw_timu" runat="server" AutoGenerateColumns="False" 
                                        onrowdatabound="grvw_timu_RowDataBound" DataKeyNames="questionid" 
                                        AllowPaging="True" onpageindexchanging="grvw_timu_PageIndexChanging" 
                                        EmptyDataText="抱歉，未找到相关题目。">
                                        <Columns>
                                            <asp:BoundField HeaderText="编号" />
                                            <asp:BoundField DataField="题型" HeaderText="题型" />
                                            <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" />
                                            <asp:BoundField DataField="answer" HeaderText="答案" />
                                            <asp:TemplateField HeaderText="相关文件">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" 
                                                        NavigateUrl='<%# Eval("filepath") %>' Text="相关文件"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="分值">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Height="22px" Width="30px">10</asp:TextBox><asp:RangeValidator
                                                        ID="RangeValidator1" runat="server" ErrorMessage="数字[1-100]." Type="Integer" MaximumValue="100" MinimumValue="1" ControlToValidate="TextBox1" ValidationGroup="fenshu" Display="Dynamic"></asp:RangeValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="btn_add" runat="server" 
                                                        CommandArgument='<%# Eval("questionid") %>' ToolTip="将该题目添加到作业。" 
                                                        ValidationGroup="fenshu" OnCommand="AddTimuToZuoye" Text="添加" ></asp:Button>
                                                    <br />
                                                    <asp:Button ID="btn_del" runat="server" CausesValidation="False" 
                                                        CommandArgument='<%# Eval("questionid") %>' ToolTip="将该题目从作业中删除。" OnCommand="DelTimuFromZuoye" Text="删除" OnClientClick="javascript:return confirm('您确定要从作业中删除该题目吗？')"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="questionid" HeaderText="题目id" Visible="False" />--%>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:Label ID="lbl_fankui" runat="server" ForeColor="Red"></asp:Label>  <br /> <asp:HyperLink ID="Hylk_buzhi" runat="server">选题完成,去布置该作业</asp:HyperLink>
                                </td>
                            </tr>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [tixingid], [mingcheng] FROM [tb_timuleixing]">
                </asp:SqlDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetZuoyeInfo" TypeName="ZuoyeInfo">
            <SelectParameters>
                <asp:QueryStringParameter Name="zuoyeid" QueryStringField="zuoyeid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>                                  
    </table>
</asp:Content>
