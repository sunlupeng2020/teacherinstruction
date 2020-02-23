<%@ Page Title="考勤更新" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="dianmingxiugai.aspx.cs" Inherits="teachermanage_dianmingxiugai" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
<ContentTemplate>
    &nbsp;&nbsp; 班级：<asp:DropDownList ID="DropDownListbj" runat="server"  DataTextField="banjiname" 
            DataValueField="banjiid" AutoPostBack="True" AppendDataBoundItems="False" 
            EnableViewState="True" 
        onselectedindexchanged="DropDownListbj_SelectedIndexChanged" 
        ondatabound="DropDownListbj_DataBound">
    </asp:DropDownList>
  
        点名时间：<asp:DropDownList ID="DropDownList1" runat="server" 
    AutoPostBack="True" 
    DataTextField="dianmingtime" DataValueField="dianmingid" AppendDataBoundItems="False" 
            EnableViewState="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged" ondatabound="DropDownList1_DataBound" 
       >
</asp:DropDownList>&nbsp;&nbsp;  <asp:Button ID="Button1" runat="server" 
            Text="删除本次考勤信息" OnClientClick="return confirm('您确定要删除本次考勤信息吗？');" 
            onclick="Button1_Click" />
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
    AutoGenerateColumns="False" DataKeyNames="studianmingid" 
    DataSourceID="SqlDataSourcedmstu" Width="485px">
    <Columns>
        <asp:BoundField DataField="studianmingid" HeaderText="studianmingid" 
            InsertVisible="False" ReadOnly="True" SortExpression="studianmingid" 
            Visible="False" />
        <asp:BoundField DataField="xuhao" HeaderText="序号" ReadOnly="True" 
            SortExpression="xuhao" />
        <asp:BoundField DataField="studentusername" HeaderText="学号" ReadOnly="True" 
            SortExpression="studentusername" />
        <asp:BoundField DataField="xingming" HeaderText="姓名" ReadOnly="True" 
            SortExpression="xingming" />
        <asp:TemplateField HeaderText="状态" SortExpression="zhuangtai">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("zhuangtai") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownList2" runat="server" 
                    SelectedValue='<%# Bind("zhuangtai") %>'>
                    <asp:ListItem>在岗</asp:ListItem>
                    <asp:ListItem>请假</asp:ListItem>
                    <asp:ListItem>迟到</asp:ListItem>
                    <asp:ListItem>早退</asp:ListItem>
                    <asp:ListItem>旷课</asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:CommandField EditText="修改" HeaderText="修改" ShowEditButton="True" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourcedmstu" runat="server" 
    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
    DeleteCommand="DELETE FROM [tb_dianmingstu] WHERE [studianmingid] = @studianmingid" 
    InsertCommand="INSERT INTO [tb_dianmingstu] ([studentusername], [zhuangtai]) VALUES (@studentusername, @zhuangtai)" 
    SelectCommand="SELECT [tb_dianmingstu].[studianmingid], [tb_dianmingstu].[studentusername], [tb_dianmingstu].[zhuangtai],tb_banjistudent.xuhao,tb_student.xingming FROM [tb_dianmingstu] inner join tb_banjistudent on tb_banjistudent.banjiid=tb_dianmingstu.banjiid and tb_banjistudent.studentusername=tb_dianmingstu.studentusername inner join tb_student on tb_student.username=tb_dianmingstu.studentusername WHERE ([tb_dianmingstu].[dianmingid] = @dianmingid) order by tb_banjistudent.xuhao" 
    UpdateCommand="UPDATE [tb_dianmingstu] SET  [zhuangtai] = @zhuangtai WHERE [studianmingid] = @studianmingid">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList1" Name="dianmingid" 
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="studianmingid" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="zhuangtai" Type="String" />
        <asp:Parameter Name="studianmingid" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="studentusername" Type="String" />
        <asp:Parameter Name="zhuangtai" Type="String" />
    </InsertParameters>
</asp:SqlDataSource>
    <br />
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    <br />
</ContentTemplate>

</asp:UpdatePanel>
      
    <br />
</asp:Content>

