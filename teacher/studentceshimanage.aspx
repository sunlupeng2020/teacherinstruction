<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="TeacherMasterPage.master" CodeFile="studentceshimanage.aspx.cs" Inherits="teachermanage_zuzhiceshi_studentceshimanage"  EnableTheming="true"  Title="学生测试管理" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
        <table class="layouttable">
            <tr>
                <td class="biaotijishuoming">
                <table>
            <tr>
            <td id="wangyezuocebiaoti" style="text-align: left; vertical-align:top; width: 120px;">
                <span style="font-family: 微软雅黑;font-size: 14px; color:White;"><strong>
            学生测试管理</strong></span>
            </td>
                <td style="height: 20px; text-align: center; color:Blue;">
              </td>
            </tr>
            </table> 
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                        SelectCommand="SELECT [stks_id], xuehao,xingming,banji,[kaoshiname], [studentusername], [zongfen], [jiaojuan], [kaishi_shijian], [jiaojuanshijian], [yunxu], [timeyanchang] FROM [tb_studentkaoshi],tb_student WHERE ([shijuanid] = @shijuanid and tb_student.username=tb_studentkaoshi.studentusername) ORDER BY [studentusername]"
                        UpdateCommand="UPDATE [tb_studentkaoshi] SET [jiaojuan] = @jiaojuan,[yunxu] = @yunxu, [timeyanchang] = @timeyanchang WHERE [stks_id] = @stks_id" 
                        ConflictDetection="CompareAllValues" OldValuesParameterFormatString="{0}">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="18" Name="shijuanid" QueryStringField="shijuanid"
                                Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="jiaojuan" Type="Byte" />
                            <asp:Parameter Name="yunxu" Type="Byte" />
                            <asp:Parameter Name="timeyanchang" Type="Int16" />
                            <asp:Parameter Name="stks_id" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 200px">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="stks_id" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="更新"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="取消"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="编辑"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="stks_id" HeaderText="stks_id" InsertVisible="False" ReadOnly="True"
                            SortExpression="stks_id" ShowHeader="False" Visible="False" />
                        <asp:BoundField DataField="xuehao" HeaderText="学号"  ReadOnly="True"/>
                        <asp:BoundField DataField="xingming" HeaderText="姓名" ReadOnly="True"/>
                        <asp:BoundField DataField="banji" HeaderText="班级" ReadOnly="True"/>
                        <asp:BoundField DataField="kaoshiname" HeaderText="测试名称" SortExpression="kaoshiname" ReadOnly="True"/>
                        <asp:BoundField DataField="studentusername" HeaderText="学生用户名" SortExpression="studentusername" ReadOnly="True" ShowHeader="False" Visible="False" />
                        <asp:BoundField DataField="zongfen" HeaderText="得分" SortExpression="zongfen" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="jiaojuan" HeaderText="交卷？" SortExpression="jiaojuan" />
                        <asp:BoundField DataField="kaishi_shijian" HeaderText="开始时间" SortExpression="kaishi_shijian" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="jiaojuanshijian" HeaderText="交卷时间" SortExpression="jiaojuanshijian" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="yunxu" HeaderText="允许" SortExpression="yunxu" />
                        <asp:BoundField DataField="timeyanchang" HeaderText="延长(分钟)" SortExpression="timeyanchang" />
                    </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
</asp:Content>