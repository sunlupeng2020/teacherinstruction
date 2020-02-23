<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="zuozuoye.aspx.cs" Inherits="studentstudy_zuozuoye" Title="在线做作业" MasterPageFile="~/student/StudentMasterPage.master" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <style type="text/css">
        .style1
        {
            height: 24px;
            width: 231px;
        }
        .style2
        {
            height: 19px;
            width: 231px;
        }
        .style3
        {
            height: 24px;
            width: 183px;
        }
        .style4
        {
            height: 19px;
            width: 183px;
        }
        .style5
        {
            height: 24px;
            width: 323px;
        }
        .style6
        {
            height: 19px;
            width: 323px;
        }
        .style7
        {
            height: 24px;
            width: 161px;
        }
        .style8
        {
            height: 19px;
            width: 161px;
        }
    </style>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 980px; height: 100%">
            <tr>
                <td style="height: 61px; width: 968px;">
                    <asp:FormView ID="FormView1" runat="server" Width="970px" Font-Size="Small">
                        <ItemTemplate>
                        <table id="zuoyexinxi" style="border-color:Blue; border-width:1px; border-collapse:collapse; width: 893px;">
                        <tr>
                            <td style=" border: 1px solid Blue;" class="style3">作业名称:</td>
                            <td style=" border: 1px solid Blue; text-align: left; " class="style1"><asp:Label ID="zuoyemingchengLabel" runat="server" Text='<%# Bind("zuoyename") %>'></asp:Label></td>
                            <td style=" border: 1px solid Blue; text-align: left; " class="style7">
                                课程:</td>
                            <td style=" border: 1px solid Blue; text-align: left; " class="style5">
                                <asp:Label ID="kechengLabel" runat="server" Text='<%# Bind("kechengname") %>'></asp:Label></td>
                            <td style=" border-width:1px; border-color:Blue; width:200px;border-style:solid; text-align: left; height: 24px;">
                                上交时间:</td>
                            <td style=" border-width:1px; border-color:Blue; width:200px;border-style:solid; text-align: left; height: 24px;">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("shangjiaoriqi") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style=" border: 1px solid Blue;" class="style4">布置时间:</td>
                            <td style=" border: 1px solid Blue; text-align: left;" class="style2"><asp:Label ID="buzhishijianLabel" runat="server" Text='<%# Bind("buzhishijian") %>'></asp:Label></td>
                            <td style=" border: 1px solid Blue; text-align: left;" class="style8">上交期限:</td>
                            <td style=" border: 1px solid Blue; text-align: left;" class="style6"><asp:Label ID="shangjiaoqixianLabel" runat="server" Text='<%# Bind("shangjiaoqixian") %>'> </asp:Label></td>
                            <td style=" border-width:1px; border-color:Blue;width:200px; border-style:solid; height: 19px; text-align: left;">
                                &nbsp;</td>
                            <td style=" border-width:1px; border-color:Blue;width:200px; border-style:solid; height: 19px; text-align: left;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                        <td style=" border: 1px solid Blue; text-align: left;" colspan="6">提示:<asp:Label 
                                ID="tishiLabel" runat="server" Text='<%# Bind("shuoming") %>'></asp:Label>
                        </td>
                        </tr>
                        </table>
                        </ItemTemplate>
                    </asp:FormView>
                    </td>
            </tr>
            <tr>
                <td style="text-align:center; width: 968px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; width: 968px;">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td style="height: 18px; text-align: center; width: 968px;">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交作业" Width="237px" ToolTip="需要提交文件的题目请使用相应的按钮提交。" />
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
</asp:Content>
