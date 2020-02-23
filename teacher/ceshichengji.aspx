<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshichengji.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master" Inherits="teachermanage_zuzhiceshi_ceshichengji" Title="学生测试成绩查询" EnableTheming="true"  %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
<TABLE class="layouttable"><TBODY><TR><TD>
</TD></TR><TR><TD style="WIDTH: 800px; HEIGHT: 13px; TEXT-ALIGN: center">
            班级:<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <SPAN style="FONT-SIZE: 11pt">
                测试名称: </SPAN>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        </SPAN></TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 800px; HEIGHT: 486px; TEXT-ALIGN: center"><asp:GridView id="GridView1" runat="server" ForeColor="#333333" DataSourceID="SqlDataSource4" CellPadding="4" AutoGenerateColumns="False" AllowSorting="True" Font-Size="11pt" Width="662px">
<FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>

<RowStyle BackColor="#E3EAEB"></RowStyle>
<Columns>
<asp:TemplateField HeaderText="序号"><ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="username" HeaderText="学号" SortExpression="xuehao"></asp:BoundField>
<asp:BoundField DataField="xingming" HeaderText="姓名" SortExpression="xingming"></asp:BoundField>
<asp:BoundField DataField="zongfen" HeaderText="成绩" SortExpression="zongfen"></asp:BoundField>
<asp:BoundField DataField="jiaojuanshijian" HeaderText="交卷时间" SortExpression="jiaojuanshijian"></asp:BoundField>
<asp:BoundField DataField="timeyanchang" HeaderText="延长时间" SortExpression="timeyanchang"></asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#7C6F57"></EditRowStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> <asp:SqlDataSource id="SqlDataSource4" runat="server" 
            SelectCommand="SELECT tb_Student.xuehao, tb_Student.xingming,tb_student.username,tb_studentkaoshi.zongfen, tb_studentkaoshi.jiaojuanshijian, tb_studentkaoshi.timeyanchang FROM tb_studentkaoshi INNER JOIN tb_Student ON tb_studentkaoshi.studentusername = tb_Student.username WHERE (tb_studentkaoshi.shijuanid = @shijuanid) ORDER BY tb_Student.xuehao" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource> &nbsp; &nbsp;&nbsp; </TD></TR></TBODY></TABLE>

</div>
</asp:Content>

