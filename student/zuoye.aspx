<%@ Page Language="C#" MasterPageFile="StudentMasterPage.master" AutoEventWireup="true" CodeFile="zuoye.aspx.cs" Inherits="studentstudy_zuoye" Title="电子作业" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 980px; height: 191px;">
        <tr>
            <td style="text-align: center; width: 980px;">
                <asp:GridView ID="GridView1" runat="server" EmptyDataText="没有该课程的作业信息。" 
                    AutoGenerateColumns="False" DataKeyNames="zuoyeid" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" 
                    onrowdatabound="GridView1_RowDataBound" PageSize="5" Width="896px">
                    <Columns>
                        <asp:BoundField />
                        <asp:BoundField DataField="zuoyeid" HeaderText="作业ID" ShowHeader="False" 
                            Visible="False" />
                        <asp:BoundField DataField="作业名称" HeaderText="作业名称" />
                        <asp:BoundField DataField="布置时间" HeaderText="布置时间" />
                        <asp:BoundField DataField="上交期限" HeaderText="上交期限" />
                        <asp:BoundField DataField="上交日期" HeaderText="上交时间" />
                        <asp:TemplateField HeaderText="成绩">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("成绩") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("成绩") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="zuoyeid" Text="做作业" 
                            DataNavigateUrlFormatString="zuozuoye.aspx?zuoyeid={0}" Target="_blank" 
                            HeaderText="做作业" />
                        <asp:HyperLinkField DataNavigateUrlFields="zuoyeid" 
                            DataNavigateUrlFormatString="chakanzuoyememo.aspx?zuoyeid={0}" Text="作业详情" 
                            Target="_blank" HeaderText="查看作业" />
                        <asp:TemplateField HeaderText="做作业?" ShowHeader="False" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("允许做题") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("允许做题") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看作业?" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("允许查看") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("允许查看") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 19px; text-align: center; width: 980px;">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Width="624px"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

