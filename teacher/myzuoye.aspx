<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="myzuoye.aspx.cs" Inherits="teachermanage_myzuoye" Title="我的作业" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                             <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" 
                                EmptyDataText="没有作业信息。" DataKeyNames="zuoyeid" 
                                onrowdatabound="GridView1_RowDataBound" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" Width="960px" 
                                 onrowdeleting="GridView1_RowDeleting" 
    onpageindexchanging="GridView1_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
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
                                                CommandName="Delete" Text="删除" CommandArgument='<%# Eval("zuoyeid") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="添加题目">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                                NavigateUrl='<%# Eval("zuoyeid", "zuoye_addtimu.aspx?zuoyeid={0}") %>' 
                                                Target="_blank" Text="添加题目"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看/修改题目">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" 
                                                NavigateUrl='<%# Eval("zuoyeid", "zuoye_edittimu.aspx?zuoyeid={0}") %>' 
                                                Text="查看/修改题目"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="布置作业">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" runat="server"
                                                CommandArgument='<%# Eval("zuoyeid") %>' onclick="LinkButton3_Click">布置该作业</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="布置信息" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                                CommandName="Select" Text="查看布置信息" ToolTip="看看该作业曾布置给哪些班"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                             作业：<asp:Label ID="lbl_zuoyemingcheng" runat="server"></asp:Label>
                             &nbsp;&nbsp;&nbsp;&nbsp; 布置记录<br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="zuoyebuzhiid" EmptyDataText="未找到作业的布置信息。" 
        onrowdatabound="GridView2_RowDataBound" 
        onrowediting="GridView2_RowEditing" onrowupdating="GridView2_RowUpdating" 
        Width="960px" ToolTip="更多功能，请到作业管理页面。">
        <Columns>
            <asp:BoundField HeaderText="" />
            <asp:BoundField DataField="班级" HeaderText="班级" />
            <asp:BoundField DataField="布置时间" HeaderText="布置时间" />
            <asp:TemplateField HeaderText="上交期限">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("上交期限") %>'></asp:TextBox>
                    <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                        TargetControlID="TextBox1">
                    </cc1:CalendarExtender>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("上交期限") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="允许做题?">
                <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow" 
                        SelectedValue='<%# Bind("允许做题") %>'>
                        <asp:ListItem>允许</asp:ListItem>
                        <asp:ListItem>禁止</asp:ListItem>
                    </asp:RadioButtonList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("允许做题") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="查看结果？">
                <EditItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow" 
                        SelectedValue='<%# Bind("查看结果") %>'>
                        <asp:ListItem>允许</asp:ListItem>
                        <asp:ListItem>禁止</asp:ListItem>
                    </asp:RadioButtonList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("允许查看结果") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="zuoyebuzhiid" 
                DataNavigateUrlFormatString="zuoye_editbuzhi.aspx?zuoyebuzhiid={0}" 
                HeaderText="修改布置信息" Text="修改布置信息" Target="_blank" />
            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandArgument='<%# Eval("zuoyebuzhiid") %>' onclick="LinkButton1_Click" OnClientClick="javascript:return confirm('你确定要删除该班该作业吗？')"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

