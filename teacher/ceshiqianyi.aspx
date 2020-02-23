<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ceshiqianyi.aspx.cs" Inherits="teacher_ceshiqianyi" Title="试卷迁移__将试卷分发给其他班" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                测试项目迁移——将某测试项目应用于其他班</td>
        </tr>
        <tr>
            <td style="width: 229px">
                原测试名称：</td>
            <td>
                <asp:Label ID="lbl_yuanname" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 229px">
                原班级:</td>
            <td>
                <asp:Label ID="lbl_yuanbanji" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 229px">
                命题方式：</td>
            <td>
                <asp:Label ID="lbl_mingtifangshi" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 229px">
                测试知识点：</td>
            <td>
                <asp:Label ID="lbl_zhishidian" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 229px">
                新测试名称：</td>
                            <td>
                                <asp:TextBox ID="tbx_ceshiname" runat="server" Width="255px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="tbx_ceshiname" ErrorMessage="请输入测试名称!"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 229px">
                                新班级:</td>
                            <td>
                                <asp:DropDownList ID="ddl_banji" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 229px">
                                测试时长(分钟):</td>
                            <td>
                                <asp:DropDownList ID="Ddl_fenzhong" runat="server">
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
                                </asp:DropDownList>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 229px">
                                是否允许做题：</td>
            <td>
                <asp:RadioButtonList ID="Rbl_yunxuceshi" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem>允许</asp:ListItem>
                    <asp:ListItem>禁止</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="width: 229px">
                是否允许查看测试结果：</td>
            <td>
                <asp:RadioButtonList ID="Rbl_yunxuchakan" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem>允许</asp:ListItem>
                    <asp:ListItem>禁止</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="width: 229px">
                是否限制IP：</td>
            <td>
                <asp:RadioButtonList ID="Rbl_xianzhiip" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem>是</asp:ListItem>
                    <asp:ListItem>否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lbl_fankui" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 229px">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="提交" onclick="Button2_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

