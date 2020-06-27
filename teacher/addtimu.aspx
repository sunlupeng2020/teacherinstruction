<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/teacher/TeacherMasterPage.master" CodeFile="addtimu.aspx.cs" ValidateRequest="false" Inherits="teachermanage_timuguanli_addtimu2" Title="添加题目" EnableTheming="true" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server"  ID="Content1">
        <table>
            <tr>
                <td align="left" valign="top">
                                题型<asp:DropDownList ID="DropDownListtixing" runat="server" 
                                    AutoPostBack="True" DataSourceID="SqlDataSource2"
                                    DataTextField="mingcheng" DataValueField="mingcheng"  
                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1" 
                                    OnDataBound="DropDownList1_OnDataBound" Width="129px">
                                </asp:DropDownList><br />
                            <div class="kechengtreeview">
                            <cc2:MyTreeView ID="TreeViewsource" runat="server" ShowLines="True" ExpandDepth="1" 
                                    OnDataBound="TreeViewsource_DataBound" 
                                    onselectednodechanged="TreeViewsource_SelectedNodeChanged">
                            </cc2:MyTreeView>
                            </div>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                        SelectCommand="SELECT [mingcheng], [tixingid] FROM [tb_timuleixing]"></asp:SqlDataSource>
                </td>
                <td style="width: 721px; height: 323px; text-align: left;" valign="top">
                    <table id="timutable" runat="server"  class="zujuantable">
                        <tr>
                            <td style="width: 80px;">
                                题目</td>
                            <td style="width: 500px;">
                                <FCKeditorV2:FCKeditor ID="FCKeditortigan" runat="server" ToolbarStartExpanded="false">
                                </FCKeditorV2:FCKeditor>
                                　　　　　　　<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FCKeditortigan" Width="92px" ErrorMessage="请输入题目。" EnableClientScript="False">请输入题目。</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr id="cankaodaantr" runat="server">
                            <td style="width: 80px; ">
                                参考答案</td>
                            <td style="width: 500px; height:auto; text-align: left">
                                <asp:RadioButtonList ID="RadioButtonListdanxuanckda" runat="server" RepeatDirection="Horizontal"
                                    Width="120px" RepeatLayout="Flow" Height="19px">
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem Selected="True">C</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:CheckBoxList ID="CheckBoxListduoxuanckda" runat="server" RepeatDirection="Horizontal"
                                    Width="176px" RepeatLayout="Flow" Height="16px">
                                    <asp:ListItem Selected="True">A</asp:ListItem>
                                    <asp:ListItem Selected="True">B</asp:ListItem>
                                    <asp:ListItem Selected="True">C</asp:ListItem>
                                    <asp:ListItem Selected="True">D</asp:ListItem>
                                    <asp:ListItem Selected="True">E</asp:ListItem>
                                </asp:CheckBoxList>
                                <asp:RadioButtonList ID="RadioButtonListpanduandaan" runat="server" Height="21px"
                                    RepeatDirection="Horizontal" Width="113px" RepeatLayout="Flow">
                                    <asp:ListItem Value="T">正确</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="F">错误</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="checkboxvalidate"
                                    ErrorMessage="请选择参考答案。" Width="179px" Display="Dynamic"></asp:CustomValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                知识点</td>
                            <td style="width: 500px;">
                                &nbsp;<asp:Label ID="Labelzhishidian" runat="server"></asp:Label>
&nbsp;<asp:TextBox ID="TextBoxzhishidian" runat="server" Width="21px" ReadOnly="True" Enabled="False" 
                                    Visible="False">0</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="TextBoxzhishidian" ErrorMessage="请在左侧知识树中单击选择一个知识点。"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px; ">
                                题目解析</td>
                            <td style="width: 500px; ">
                                <FCKeditorV2:FCKeditor ID="FCKeditorshuoming" runat="server"  ToolbarStartExpanded="false">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="height: 27px">
                                <asp:Button ID="Buttonsubmit" runat="server" OnClick="Buttonsubmit_Click" Text="提交"
                                    Width="175px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Lbl_fankui" runat="server" Width="261px" ForeColor="Red" SkinID="Lbl_fankui"></asp:Label></td>
            </tr>
        </table>
</asp:Content>
