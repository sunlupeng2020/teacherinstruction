<%@ Page Title="���߿���" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="dianming.aspx.cs" Inherits="teachermanage_dianming" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            &nbsp;&nbsp; �༶��<asp:DropDownList ID="DropDownList2" runat="server" 
             DataTextField="banjiname" 
            DataValueField="banjiid" AutoPostBack="True" AppendDataBoundItems="False" 
                ondatabound="DropDownList2_DataBound">
        </asp:DropDownList>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="studentusername" DataSourceID="SqlDataSource1" Width="663px" 
                PageSize="20">
            <Columns>
                <asp:BoundField DataField="xuhao" HeaderText="���" SortExpression="xuhao" />
                <asp:BoundField DataField="studentusername" HeaderText="ѧ��" 
                    SortExpression="studentusername" />
                <asp:BoundField DataField="xingming" HeaderText="����" 
                    SortExpression="xingming" />
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal" Width="293px">
                            <asp:ListItem Selected="True">�ڸ�</asp:ListItem>
                            <asp:ListItem>�ٵ�</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>
                            <asp:ListItem>���</asp:ListItem>
                            <asp:ListItem>����</asp:ListItem>
                        </asp:RadioButtonList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="�ύ" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT [xuhao], [studentusername],tb_student.xingming FROM [tb_banjistudent] inner join tb_student on tb_student.username=tb_banjistudent.studentusername WHERE ([tb_banjistudent].[banjiid] = @banjiid) order by [xuhao]">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="banjiid" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourcestu" runat="server" 
            SelectMethod="GetStudent" TypeName="BanjiInfo">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="banjiid" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </ContentTemplate>
        </asp:UpdatePanel>
 </asp:Content>

