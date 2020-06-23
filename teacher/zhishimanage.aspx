<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zhishimanage.aspx.cs" Inherits="teachermanage_kechengguanli_zhishimanage" Title="建立课程结构" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">
var oEditer;
function CustomValidate(source, arguments)//验证知识点介绍
{
    var value = oEditer.GetXHTML(true);
    if(value.length>5000)
    {
       arguments.IsValid = false;     
    }
    else 
    { 
        arguments.IsValid = true; 
    } 
}
function FCKeditor_OnComplete(editorInstance)
{ 
    oEditer = editorInstance;
}
</script>
    <table>
        <tr>
            <td style="width: 232px;" valign="top">
                <div class="kechengtreeview">   
                <cc2:MyTreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                    Width="194px" ExpandDepth="3" ShowLines="True" Font-Names="宋体" 
                         ToolTip="单击选择一个知识点，在右侧输入、创建它的子知识点。">
                    <HoverNodeStyle ForeColor="Black" />
                </cc2:MyTreeView>
                </div>
                <asp:LinkButton ID="Btnshuaxintree" runat="server" OnClick="Btnshuaxintree_Click"
                Text="刷新课程结构" Width="100px" CausesValidation="False" />
                <br />
            </td>
            <td style="width: 760px; vertical-align:top; text-align:left;">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 662px;">
                <tr>
                        <td style="height: 20px; width: 742px; text-align: center;">
                            创建下级知识点</td>
                    </tr>
                 <tr>
                        <td style="width: 742px">
                            上级知识点：<asp:TextBox ID="TextBox1" runat="server" Enabled="False" ReadOnly="True" 
                                Width="271px" ToolTip="在左侧知识树中单击选择。章的上级知识点是课程，节的上级知识点是章。"></asp:TextBox>
            <asp:TextBox ID="Txtshangweiid" runat="server" Visible="False" Width="38px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="请在左侧知识树中选择上位知识点！" Width="236px"></asp:RequiredFieldValidator></td>
                    </tr>
                    
                    <tr>
                        <td style="height: 35px; width: 742px; text-align: left;">
                            <span style="font-family: 宋体; text-align: left;">知识点名称：</span><asp:TextBox 
                                ID="Txtzhishiname" runat="server"
                                Width="379px" MaxLength="50" ToolTip="最多50个汉字。"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorzhishiname" runat="server"
            ControlToValidate="Txtzhishiname" Display="Dynamic" ErrorMessage="请输入知识点名称。"
            Width="136px"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 742px">
                            知识点序号：<asp:TextBox ID="Txtzhishixuhao" runat="server" Width="100px" MaxLength="4" >1</asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txtzhishixuhao"
                    Display="Dynamic" ErrorMessage="序号输入有误，应为英文数字。" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="Txtzhishixuhao" Display="Dynamic" ErrorMessage="请输入序号。"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 742px">
                            知识点介绍：</td>
                    </tr>
                    <tr>
                        <td style="width: 742px; height: 107px;" >
                            <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="300px">
                            </FCKeditorV2:FCKeditor>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ControlToValidate="FCKeditor1" Display="Dynamic" 
                                ErrorMessage="知识点介绍太长，请不要超过5000字符。" 
                                ClientValidationFunction="CustomValidate"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 742px; text-align: center">
            <asp:Button ID="BtnAddzhishidian" runat="server" Text="添加到知识结构中" Width="123px" OnClick="BtnAddzhishidian_Click" />
                            <br />
                            <asp:CheckBox ID="CheckBoxtishi" runat="server" Text="不显示提示对话框" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px; width: 742px;">
                            <asp:SqlDataSource ID="SqlDataSourcezhishileixing" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
            SelectCommand="SELECT [zhishileixing] FROM [tb_ZhishiLeixing]">
        </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
       <asp:Label ID="Lbl_fankui" runat="server" Width="797px" SkinID="Lbl_fankui"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

