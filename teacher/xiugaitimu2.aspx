<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/teacher/TeacherMasterPage.master" CodeFile="xiugaitimu2.aspx.cs" Inherits="teachermanage_timuguanli_xiugaitimu2" Title="修改题目" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
        <table>
            <tr>
                <td style="width: 219px; height: 1028px; text-align: left;" valign="top">
               <div class="kechengtreeview">
                    <cc2:MyTreeView ID="TreeViewsource" runat="server"
                         ExpandDepth="2" ShowLines="True" 
                        onselectednodechanged="TreeViewsource_SelectedNodeChanged">
                    </cc2:MyTreeView>
                    </div>
                </td>
                <td style="height: 1028px; vertical-align:top;">
                <table id="timutable" runat="server" class="fill_table" >
                <tr >
                    <td style="width: 100px; ">
                        题型</td>
                    <td style="width: 591px;">
                        &nbsp;<asp:Label
                            ID="Labelquestionid" runat="server" Visible="False" Width="88px"></asp:Label><asp:Label ID="Labeltixing" runat="server" Text="Label" Width="171px"></asp:Label>&nbsp;
                        <span style="font-size: 11pt; color: #ff3300"></span></td>
                </tr>
                <tr>
                    <td style="width: 100px;">
                        题目</td>
                    <td style="width: 591px;">
                        <FCKeditorV2:FCKeditor ID="FCKeditortigan" runat="server">
                        </FCKeditorV2:FCKeditor>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FCKeditortigan"
                            ErrorMessage="题目不能为空，请输入题目。" Width="228px" Display="Dynamic"></asp:RequiredFieldValidator></td>
                </tr>
                <tr  id="cankaodaantr" runat="server">
                    <td >参考答案</td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonListdanxuanckda" runat="server" RepeatDirection="Horizontal"
                            Width="193px" RepeatLayout="Flow">
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                            <asp:ListItem>D</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CheckBoxList ID="CheckBoxListduoxuanckda" runat="server" RepeatDirection="Horizontal"
                            Width="226px"  RepeatLayout="Flow">
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                            <asp:ListItem>D</asp:ListItem>
                            <asp:ListItem>E</asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:RadioButtonList ID="RadioButtonListpanduandaan" runat="server" 
                            RepeatDirection="Horizontal" Width="129px" RepeatLayout="Flow">
                            <asp:ListItem Value="T">正确</asp:ListItem>
                            <asp:ListItem Value="F">错误</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CustomValidator ID="CustomValidatorduoxuan" runat="server" ErrorMessage="请选择参考答案。"
                             Width="146px" ClientValidationFunction="checkboxvalidate" Display="Dynamic"></asp:CustomValidator></td>
                </tr>
                <tr>
                    <td style="height: 20px">
                        知识点</td>
                    <td style="height: 20px">
                        <asp:HiddenField ID="HFzhishidianid" runat="server" />
                        <asp:Label ID="Labelzhishdidian" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        题目<br />
                        解析</td>
                    <td>
                        <FCKeditorV2:FCKeditor ID="FCKeditorshuoming" runat="server">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="Buttonsubmit" runat="server" Text="提交" Width="175px" OnClick="Buttonsubmit_Click" /></td>
                </tr>
            </table>
                   </td>
            </tr>
            <tr><td colspan="2"> <asp:Label ID="Labelfankui" runat="server" ForeColor="Red" Width="414px"></asp:Label>
            </td>
            </tr>
        </table>
</asp:Content>

