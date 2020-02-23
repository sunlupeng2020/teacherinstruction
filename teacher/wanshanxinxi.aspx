<%@ Page Title="完善个人信息" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="wanshanxinxi.aspx.cs" Inherits="teachermanage_wanshanxinxi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="JavaScript" type="text/javascript">
    function checkIDCard(oSrc, args) {
        var isIDCard1 = new Object();
        var isIDCard2 = new Object();

        //身份证正则表达式(15位) 
        isIDCard1 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;

        //身份证正则表达式(18位) 

        isIDCard2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/;

        //验证身份证号
        if (isIDCard1.test(args.Value) || isIDCard2.test(args.Value)) {
            args.IsValid = true
        }
        else {
            args.IsValid = false;
        }
    }
</script>
    <table style="width:600px">
        <tr>
            <td style="width: 91px">
                姓名：</td>
            <td>
                <asp:TextBox ID="TBxxm" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TBxxm" ErrorMessage="请输入姓名！" ValidationGroup="g1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 91px">
                性别：</td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="29px" 
                    RepeatDirection="Horizontal" Width="117px">
                    <asp:ListItem Selected="True">男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="width: 91px">
                系部：</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="xibuname" DataValueField="xibuid" 
                    style="margin-left: 0px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 91px">
                身份证号：</td>
            <td>
                <asp:TextBox ID="TBxsfzh" runat="server" MaxLength="18" Width="160px"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ControlToValidate="TBxsfzh" ErrorMessage="请输入正确的身份证号！" 
                    ClientValidationFunction="checkIDCard" Display="Dynamic" 
                    ValidationGroup="g1">请输入正确的身份证号！</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 91px">
                头像：</td>
            <td>
                <asp:Image ID="Imagetx01" runat="server" Height="100px" Width="100px" />
            </td>
        </tr>
        <tr>
            <td style="width: 91px">
                上传新头像：</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="Buttontouxiang" runat="server" Text="更新头像" 
                    onclick="Buttontouxiang_Click" />
                <br />
                <span style="color: #FF0000">图像文件格式只能是.bmp,.jpg或.gif，文件大小不要超过500KB.</span></td>
        </tr>
        <tr>
            <td style="width: 91px">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="提交" onclick="Button1_Click" 
                    ValidationGroup="g1" />
            </td>
        </tr>
        <tr>
            <td style="width: 91px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

