<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zuoye_id_buzhi01.aspx.cs" Inherits="teachermanage_zuoye_id_buzhi01" Title="布置作业" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" 
            HorizontalAlign="Left">
            <ItemTemplate>
                        课程：<asp:Label ID="Label1" runat="server" Text='<%# Eval("kechengname") %>'></asp:Label>
                        &nbsp;作业：<asp:Label ID="Label2" runat="server" Text='<%# Eval("zuoyename") %>'></asp:Label>
                        &nbsp;创建时间：<asp:Label ID="Label3" runat="server" Text='<%# Eval("createtime") %>'></asp:Label>
                        &nbsp; 总分：<asp:Label ID="Label5" runat="server" Text='<%# Eval("manfen") %>'></asp:Label>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetZuoyeInfo" TypeName="ZuoyeInfo">
            <SelectParameters>
                <asp:QueryStringParameter Name="zuoyeid" QueryStringField="zuoyeid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <tr>
            <td class="aligncentertd">
                <br />
                <table style="width: 100%">
                    <tr>
                        <td class="alignrighttd" style="width: 116px">
                            选择班级：</td>
                        <td class="alignlefttd">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="137px" DataTextField="banjiname" DataValueField="banjiid"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="alignrighttd" style="width: 116px">
                            上交期限：</td>
                        <td class="alignlefttd">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="TextBox1">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="请输入上交期限！" ControlToValidate="TextBox1" EnableClientScript="False"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="alignrighttd" style="width: 116px">
                            是否允许做题：</td>
                        <td class="alignlefttd">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True">允许</asp:ListItem>
                    <asp:ListItem>禁止</asp:ListItem>
                </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="alignrighttd" style="width: 116px">
                            是否允许查看结果：</td>
                        <td class="alignlefttd">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True">允许</asp:ListItem>
                    <asp:ListItem>禁止</asp:ListItem>
                </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="alignrighttd" style="width: 116px">
                            说明：</td>
                        <td class="alignlefttd">
                            <asp:TextBox ID="TextBox2" runat="server" Height="66px" TextMode="MultiLine" 
                                Width="184px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="aligncentertd">
                <asp:Button ID="btn_buzhizuoye" runat="server" Text="提交" onclick="btn_buzhizuoye_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="aligncentertd">
                            <asp:Label ID="lbl_fankui" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                作业题目信息<asp:GridView ID="grvw_zuoyetimu" runat="server" AutoGenerateColumns="False" 
                                        DataKeyNames="zuoyetimuid" EmptyDataText="该作业目前没有题目。" 
                                        onrowdatabound="grvw_zuoyetimu_RowDataBound" 
                    DataSourceID="ObjectDataSource2">
                                        <Columns>
                                            <asp:BoundField HeaderText="编号" ReadOnly="True" >
                                                <ItemStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="题型" HeaderText="题型" ReadOnly="True" >
                                                <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" 
                                                ReadOnly="True" >
                                                <ItemStyle Width="400px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="参考答案" HeaderText="答案" ReadOnly="True"  
                                                HtmlEncode="False" >
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:HyperLinkField DataNavigateUrlFields="相关文件" HeaderText="相关文件" >
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                            <asp:BoundField DataField="分值" HeaderText="分值" >
                                                <ItemStyle Width="40px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
            SelectMethod="GetTeacherZuoyeTimuOrderByTixing" TypeName="ZuoyeInfo">
            <SelectParameters>
                <asp:QueryStringParameter Name="zuoyeid" QueryStringField="zuoyeid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

