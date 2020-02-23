<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zuoye_editbuzhi.aspx.cs" Inherits="teachermanage_zuoye_editbuzhi" Title="修改作业布置信息" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" 
            Width="805px">
            <ItemTemplate>
                作业名称：<asp:Label ID="Label1" runat="server" Text='<%# Eval("作业名称") %>'></asp:Label><br />
                班级：<asp:Label ID="Label2" runat="server" Text='<%# Eval("班级") %>'></asp:Label><br />
                <asp:Label ID="Label5" runat="server" Text='<%# Eval("允许做题") %>'></asp:Label>做题<br />
                <asp:Label ID="Label6" runat="server" Text='<%# Eval("允许查看结果") %>'></asp:Label>查看批改结果<br />
                布置时间：<asp:Label ID="Label3" runat="server" Text='<%# Eval("布置时间") %>'></asp:Label><br />
                上交期限：<asp:Label ID="Label4" runat="server" Text='<%# Eval("上交期限") %>'></asp:Label><br />
                &nbsp;
                <br />
                说明：<asp:Label ID="Label7" runat="server" Text='<%# Eval("说明") %>'></asp:Label>
            </ItemTemplate>
        </asp:FormView>
    </p>
                            <table class="alignrighttd" style="width: 533px; height: 197px">
                                <tr>
                                    <td class="aligncentertd" colspan="2">
                                        修改作业布置信息</td>
                                </tr>
                                <tr>
                                    <td class="alignrighttd">
                                        上交期限：</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                        TargetControlID="TextBox1">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="请选择上交期限。"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="alignrighttd">
                    允许做题？</td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">允许</asp:ListItem>
                        <asp:ListItem>禁止</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="alignrighttd">
                    允许查看批改结果？</td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">允许</asp:ListItem>
                        <asp:ListItem>禁止</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="alignrighttd">
                    说明：</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Height="41px" TextMode="MultiLine" 
                        Width="312px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="aligncentertd" colspan="2">
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="提交" 
                        style="height: 21px" />
                </td>
            </tr>
            <tr>
                <td class="aligncentertd" colspan="2">
                    <asp:Label ID="lbl_fankui" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetZuoyeBuzhiInfo" TypeName="ZuoyeInfo">
            <SelectParameters>
                <asp:QueryStringParameter Name="zuoyebuzhiid" QueryStringField="zuoyebuzhiid" 
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    <p>
        &nbsp;</p>

</asp:Content>

