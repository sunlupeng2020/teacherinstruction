<%@ Page Language="C#" MasterPageFile="StudentMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="studentstudy_Default" Title="学生首页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 980px">
        <tr>
            <td  valign="top" rowspan="3">
                <table >
                    <tr>
                        <td style=" text-align: center">
                           <dl class="orange_th"><dd class="th">作业动态</dd></dl></td>
                    </tr>
                    <tr>
                        <td style="width: 980px; text-align: center;">
                            <asp:GridView ID="GridView1" runat="server" 
                                Width="918px" AutoGenerateColumns="False" 
                                ToolTip="显示目前要做的作业。" AllowPaging="True" PageSize="6" 
                                EmptyDataText="现在没有要做的作业。" onrowdatabound="GridView1_RowDataBound1" 
                                onpageindexchanging="GridView1_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField />
                                    <asp:BoundField DataField="课程" HeaderText="课程" />
                                    <asp:BoundField DataField="作业名称" HeaderText="作业名称" />
                                    <asp:BoundField DataField="布置时间" HeaderText="布置时间" />
                                    <asp:BoundField DataField="上交期限" HeaderText="上交期限" />
                                    <asp:HyperLinkField DataNavigateUrlFields="zuoyeid" 
                                        DataNavigateUrlFormatString="zuozuoye.aspx?zuoyeid={0}" HeaderText="做作业" 
                                        Text="去做作业"  />
                                </Columns>
                            <EmptyDataTemplate>
                            没有作业信息。
                            </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style=" height: 21px; text-align: center">
  <dl class="orange_th"><dd class="th">测试动态</dd></dl></td>
                    </tr>
                    <tr>
                        <td style="height: 66px; text-align:center;">
                            <asp:GridView ID="GridView2" runat="server"
                                Width="916px" AllowPaging="True" PageSize="6" AutoGenerateColumns="False" 
                                onrowdatabound="GridView2_RowDataBound" 
                                onpageindexchanging="GridView2_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField />
                                    <asp:BoundField DataField="kechengname" HeaderText="课程" />
                                    <asp:BoundField DataField="ceshiname" HeaderText="测试名称" />
                                    <asp:BoundField DataField="timelength" HeaderText="时长" />
                                    <asp:BoundField DataField="jiaojuan" HeaderText="状态" />
                                    <asp:HyperLinkField DataNavigateUrlFields="shijuanid" 
                                        DataNavigateUrlFormatString="canjiaceshi.aspx?shijuanid={0}" HeaderText="参加测试"  Target="_blank"
                                        Text="开始测试" />
                                </Columns>
                                <EmptyDataTemplate>
                                没有测试信息。
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

