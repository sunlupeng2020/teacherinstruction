<%@ Page Language="C#" MasterPageFile="StudentMasterPage.master" AutoEventWireup="true" CodeFile="jiaoshiceshi.aspx.cs" Inherits="studentstudy_jiaoshiceshi" Title="参加考试" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 960px;">
        <tr>
            <td style="text-align: center">
    <table style="width: 600px; height: 400px; text-align: center; border-right: dodgerblue 1px solid; border-top: dodgerblue 1px solid; border-left: dodgerblue 1px solid; border-bottom: dodgerblue 1px solid;border-collapse: collapse;">
        <tr>
            <td style="height: 40px; border-bottom: dodgerblue thin solid; border-left: dodgerblue thin solid; border-top: dodgerblue thin solid; border-right: dodgerblue thin solid;">
                <strong><span style="font-size: 14pt">参加考试</span></strong></td>
        </tr>
        <tr>
            <td style="width: 500px; height: 280px; text-align: left; overflow:auto; border-bottom: dodgerblue thin solid; border-left: dodgerblue thin solid; border-top: dodgerblue thin solid; border-right: dodgerblue thin solid;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    Width="590px" EmptyDataText="目前没有需要参加的考试项目。">
                    <Columns>
                        <asp:BoundField DataField="ceshiname" HeaderText="考试名称" 
                            SortExpression="ceshiname" />
                        <asp:BoundField DataField="shijuanid" HeaderText="shijuanid" 
                            SortExpression="shijuanid" Visible="False" />
                        <asp:BoundField DataField="timelength" HeaderText="时长(分钟)" 
                            SortExpression="timelength" />
                        <asp:BoundField DataField="jiaojuan" HeaderText="状态" 
                            SortExpression="jiaojuan" />
                        <asp:HyperLinkField DataNavigateUrlFields="shijuanid" 
                            DataNavigateUrlFormatString="canjiaceshi.aspx?shijuanid={0}" HeaderText="参加考试" 
                            Text="开始考试" Target="_blank" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; border-bottom: dodgerblue thin solid; border-left: dodgerblue thin solid; border-top: dodgerblue thin solid; border-right: dodgerblue thin solid;">
                <font color='red'>温馨提示：如果您要做作业、进行自测或查询考试成绩时莫名其妙地跳转到<br />
                本页面，是因为你有考试未完成，交卷并关闭考试页面，一分钟后就能够进行这些操作了。</font></td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</asp:Content>

