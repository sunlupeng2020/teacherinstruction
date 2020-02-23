<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zuoyeyuzhi.aspx.cs" Inherits="teachermanage_yuzhizuoye" Title="作业预制" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
        <td width="980px">
        <table align="center">
                    <tr>
                        <td style="height: 50px" valign="top">
                            作业名称：<asp:TextBox ID="TextBoxzuoyename" runat="server" Width="367px" 
                                MaxLength="50"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxzuoyename"
                                Display="Dynamic" ErrorMessage="请输入电子作业名称！"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 22px" valign="top">
                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="创建作业" 
                                Width="168px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 31px" valign="top">
                            <asp:Label ID="lbl_fankui" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="height: 31px" valign="top">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" 
                                EmptyDataText="没有作业信息。" DataKeyNames="zuoyeid" 
                                onrowdatabound="GridView1_RowDataBound" 
                                onpageindexchanging="GridView1_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbl_zybh" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="zuoyename" HeaderText="作业名称" 
                                        SortExpression="zuoyename" />
                                    <asp:BoundField DataField="createtime" HeaderText="创建时间" 
                                        SortExpression="createtime" />
                                    <asp:BoundField DataField="manfen" HeaderText="总分" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                CommandName="Delete" Text="删除" CommandArgument='<%# Eval("zuoyeid") %>' 
                                                onclick="LinkButton1_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="添加题目">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                                NavigateUrl='<%# Eval("zuoyeid", "zuoye_addtimu.aspx?zuoyeid={0}") %>' 
                                                Target="_blank" Text="添加题目"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改题目">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" 
                                                NavigateUrl='<%# Eval("zuoyeid", "zuoye_edittimu.aspx?zuoyeid={0}") %>' 
                                                Text="修改题目"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="布置作业">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" 
                                                CommandArgument='<%# Eval("zuoyeid") %>' onclick="LinkButton2_Click"  CausesValidation="False">布置该作业</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
         </td>
        </tr>
    </table>
</asp:Content>

