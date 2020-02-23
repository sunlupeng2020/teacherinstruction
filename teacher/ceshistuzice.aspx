<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshistuzice.aspx.cs" Inherits="teachermanage_ceshistuzice" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="学生自测信息" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
            班级：<asp:Label ID="Labelbj" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
            学生姓名：<asp:Label ID="Labelxm" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp; 学号：<asp:Label ID="Labelstuun" runat="server"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" DataKeyNames="ziceid" DataSourceID="SqlDataSource1" 
                EmptyDataText="没有该学生的自测数据。" Width="902px">
                <Columns>
                    <asp:BoundField DataField="ziceid" HeaderText="ziceid" InsertVisible="False" 
                        ReadOnly="True" SortExpression="ziceid" Visible="False" />
                    <asp:BoundField DataField="kechengid" HeaderText="kechengid" 
                        SortExpression="kechengid" Visible="False" />
                    <asp:BoundField DataField="username" HeaderText="username" 
                        SortExpression="username" Visible="False" />
                    <asp:BoundField DataField="ceshiname" HeaderText="自测名称" 
                        SortExpression="ceshiname" />
                    <asp:BoundField DataField="ceshitime" HeaderText="测试时间" 
                        SortExpression="ceshitime" />
                    <asp:BoundField DataField="fenshu" HeaderText="成绩" SortExpression="fenshu" />
                    <asp:CommandField HeaderText="题目详情" SelectText="题目详情" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [ziceid], [kechengid], [username], [ceshiname], [ceshitime], [fenshu] FROM [tb_zice] WHERE (([kechengid] = @kechengid) AND ([username] = @username))">
                <SelectParameters>
                    <asp:SessionParameter Name="kechengid" SessionField="kechengid" Type="Int32" />
                    <asp:QueryStringParameter Name="username" QueryStringField="studentusername" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </p>
    
    </div>
    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" DataKeyNames="zicetimuid" 
        DataSourceID="SqlDataSource2" Width="855px">
        <Columns>
            <asp:BoundField DataField="zicetimuid" HeaderText="zicetimuid" 
                InsertVisible="False" ReadOnly="True" SortExpression="zicetimuid" 
                Visible="False" />
            <asp:BoundField DataField="tihao" HeaderText="题号" SortExpression="tihao" />
            <asp:BoundField DataField="timu" HeaderText="题目" SortExpression="timu" 
                HtmlEncode="False" />
            <asp:BoundField DataField="type" HeaderText="题型" SortExpression="type" />
            <asp:BoundField DataField="answer" HeaderText="参考答案" SortExpression="answer" />
            <asp:BoundField DataField="huida" HeaderText="回答" SortExpression="huida" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
        SelectCommand="SELECT [tb_zicetimu].[zicetimuid],[tb_zicetimu].[huida], [tb_zicetimu].[tihao],tb_tiku.timu,tb_tiku.answer,tb_tiku.[type] FROM [tb_zicetimu] inner join tb_tiku on tb_tiku.questionid=tb_zicetimu.questionid WHERE ([tb_zicetimu].[ziceid] = @ziceid) ORDER BY [tb_zicetimu].[tihao]">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="ziceid" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
