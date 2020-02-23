<%@ Page Title="考勤信息汇总" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="dianmingtongji.aspx.cs" Inherits="teachermanage_dianmingtongji" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UP1" runat="server">
  <ContentTemplate>
      班级：<asp:DropDownList ID="DropDownList2" runat="server" 
            DataTextField="banjiname" 
            DataValueField="banjiid" AutoPostBack="True" AppendDataBoundItems="True" 
            EnableViewState="True" 
            onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
          ondatabound="DropDownList2_DataBound">
    </asp:DropDownList>
      &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="考勤汇总" 
    onclick="Button1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        考勤次数：<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourcestu" ondatabound="GridView1_DataBound" Width="503px">
            <Columns>
                <asp:BoundField DataField="xuhao" HeaderText="序号" />
                <asp:BoundField DataField="studentusername" HeaderText="学号" />
                <asp:BoundField DataField="xingming" HeaderText="姓名" />
                <asp:TemplateField HeaderText="在岗"></asp:TemplateField>
                <asp:TemplateField HeaderText="请假"></asp:TemplateField>
                <asp:TemplateField HeaderText="迟到"></asp:TemplateField>
                <asp:TemplateField HeaderText="早退"></asp:TemplateField>
                <asp:TemplateField HeaderText="旷课"></asp:TemplateField>
                <asp:TemplateField HeaderText="详情">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text="查看详情"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
</asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourcestu" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
    SelectCommand="SELECT [xuhao], [studentusername],tb_student.xingming FROM [tb_banjistudent] inner join tb_student on tb_student.username=tb_banjistudent.studentusername WHERE ([tb_banjistudent].[banjiid] = @banjiid) order by [xuhao]">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="banjiid" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
  </ContentTemplate>


</asp:UpdatePanel>
      
    </asp:Content>

