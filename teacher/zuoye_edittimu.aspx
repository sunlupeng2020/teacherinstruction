<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zuoye_edittimu.aspx.cs" Inherits="teachermanage_zuoye_edittimu" Title="编辑作业题目" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" 
                    HorizontalAlign="Center">
                    <ItemTemplate>
                        作业名称：<asp:Label ID="Label2" runat="server" Text='<%# Eval("zuoyename") %>'></asp:Label>
                        &nbsp;创建时间：<asp:Label ID="Label3" runat="server" Text='<%# Eval("createtime") %>'></asp:Label>
                        &nbsp; 总分：<asp:Label ID="Label5" runat="server" Text='<%# Eval("manfen") %>'></asp:Label>
                    </ItemTemplate>
                </asp:FormView>
                                </td>
        </tr>
        <tr>
            <td>
                                    <asp:GridView ID="grvw_zuoyetimu" runat="server" AutoGenerateColumns="False" 
                                        DataKeyNames="zuoyetimuid" EmptyDataText="该作业目前没有题目。" 
                                        onrowdatabound="grvw_zuoyetimu_RowDataBound" 
                                        onrowcancelingedit="grvw_zuoyetimu_RowCancelingEdit" 
                                        onrowediting="grvw_zuoyetimu_RowEditing" 
                                        onrowupdating="grvw_zuoyetimu_RowUpdating" 
                                        ondatabound="grvw_zuoyetimu_DataBound">
                                        <Columns>
                                            <asp:BoundField ReadOnly="True" >
                                                <ItemStyle Width="20px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="题型" HeaderText="题型" ReadOnly="True" 
                                                HtmlEncode="False" >
                                                <ItemStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" 
                                                ReadOnly="True" >
                                                <ItemStyle Width="390px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="参考答案" HeaderText="答案" ReadOnly="True" 
                                                HtmlEncode="False" >
                                                <ItemStyle Width="340px" />
                                            </asp:BoundField>
                                            <asp:HyperLinkField DataNavigateUrlFields="相关文件" HeaderText="相关文件" >
                                                <ItemStyle Width="40px" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="分值">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("分值") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Height="23px" 
                                                        Text='<%# Bind("分值") %>' Width="37px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入分值." ControlToValidate="TextBox1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                                        ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="数字,[1-100]." 
                                                        MaximumValue="100" MinimumValue="1" ValidationGroup="bianjizuoyetimufenzhi" Type="Integer"></asp:RangeValidator>
                                                </EditItemTemplate>
                                                <ItemStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False" HeaderText="编辑">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                        CommandName="Edit" Text="编辑" ToolTip="修改分值"></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" 
                                                        CommandName="Update" Text="更新" ValidationGroup="bianjizuoyetimufenzhi"></asp:LinkButton>
                                                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                                        CommandName="Cancel" Text="取消"></asp:LinkButton>
                                                </EditItemTemplate>
                                                <ItemStyle Width="40px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button2" runat="server" Text="删除"
                                                        CommandArgument='<%# Eval("zuoyetimuid") %>' ToolTip="从作业中删除该题目"  OnCommand="DeleteTimuFromZuoye"></asp:Button>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server">题目没问题，去布置该作业</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetZuoyeInfo" TypeName="ZuoyeInfo">
            <SelectParameters>
                <asp:QueryStringParameter Name="zuoyeid" QueryStringField="zuoyeid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

