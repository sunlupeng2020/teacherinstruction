<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="createcourse.aspx.cs" Inherits="createactrualcourse" Title="新建课程"  %>
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
            <td style="width: 100px; height: 30px;">
                <span style="font-size: 10pt; font-family: 新宋体">
                课程名称</span></td>
            <td style="width: 500px; height: 30px; text-align: left;">
                <asp:TextBox ID="TxtKechengName" runat="server" Width="314px" 
                    ToolTip="50个汉字以内，不要与左侧现有课程名重复。" MaxLength="100"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtKechengName"
                    Display="Dynamic" ErrorMessage="请输入课程名称。" Font-Names="宋体"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="height: 100px">
                <span style="font-size: 10pt; font-family: 宋体">课程简介</span></td>
            <td style="text-align: left; height: 100px; width: 500px;">
                <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="300px">
                </FCKeditorV2:FCKeditor>
      </td>
        </tr>
        <tr>
            <td style="height: 55px">
                课程图像</td>
            <td style="text-align: left; width: 319px; height: 55px;">
                <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="图片大小以150*220像素为宜，大小不要超过500KB。" /><br />
                 <asp:RegularExpressionValidator 
 id="RegularExpressionValidator1" runat="server" 
 ErrorMessage="只允许使用jpg,gif,bmp,png图片!" 
 ValidationExpression="^(([a-zA-Z]:)|(\\{2}[\w\u4e00-\u9fa5]+)\$?)(\\([\w\u4e00-\u9fa5][\w\u4e00-\u9fa5].*))+(.jpg|.JPG|.gif|.GIF|.png|.PNG|.bmp|.BMP)$"
 ControlToValidate="FileUpload1" Display="Dynamic">只允许使用jpg,gif,bmp,png图像!</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="BtnCreateCourse" runat="server" Text="提交" Width="142px" OnClick="BtnCreateCourse_Click" /></td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="2">
                <asp:Label ID="Lbl_fankui" runat="server" Width="419px" Font-Size="10pt" ForeColor="Red" Font-Names="宋体"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

