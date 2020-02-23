<%@ Page Language="C#" MasterPageFile="MasterPageManager.master" Title="学生详细信息及修改" AutoEventWireup="true" CodeFile="editstudentinfo.aspx.cs" Inherits="teachermanage_editstudentinfo"%>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content1" runat="server">
    <div class="layoutdiv">
      <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
            DataKeyNames="username" DataSourceID="SqlDataSourcestu" Height="200px" 
            Width="300px" BorderColor="#0066CC" BorderStyle="Solid" BorderWidth="1px" 
            EmptyDataText="未找到学生的信息。">
            <CommandRowStyle HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            <FieldHeaderStyle BorderColor="#0066CC" BorderStyle="Solid" BorderWidth="1px" 
                HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
            <Fields>
                <asp:BoundField DataField="username" HeaderText="学号：" ReadOnly="True" 
                    SortExpression="username" >
                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                <ItemStyle BorderColor="#0066CC" BorderStyle="Solid" BorderWidth="1px" 
                    HorizontalAlign="Left" Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="姓名：" SortExpression="xingming">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Height="22px" MaxLength="20" 
                            Text='<%# Bind("xingming") %>'></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="请输入学生姓名！"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("xingming") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("xingming") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                    <ItemStyle BorderColor="#0066CC" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="性别：" SortExpression="xingbie">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" 
                            SelectedValue='<%# Bind("xingbie") %>'>
                            <asp:ListItem>男</asp:ListItem>
                            <asp:ListItem>女</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("xingbie") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("xingbie") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                    <ItemStyle BorderColor="#0066CC" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="专业：" SortExpression="zhuanyename">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList4" runat="server" 
                            DataSourceID="ObjectDataSource1" DataTextField="zhuanyename" 
                            DataValueField="zhuanyeid" SelectedValue='<%# Bind("zhuanyeid") %>'>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                            SelectMethod="GetZhuanyeDataSet" TypeName="ZhuanyeInfo">
                        </asp:ObjectDataSource>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("zhuanye") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("zhuanyename") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                    <ItemStyle BorderColor="#0066CC" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="200px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
            </Fields>
            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            <EditRowStyle BorderColor="#0066CC" BorderStyle="Solid" BorderWidth="1px" 
                HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
            <AlternatingRowStyle BorderColor="#0066CC" BorderStyle="Solid" 
                BorderWidth="1px" />
        </asp:DetailsView>
        <asp:SqlDataSource ID="SqlDataSourcestu" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            DeleteCommand="DELETE FROM [tb_Student] WHERE [username] = @username" 
            InsertCommand="INSERT INTO [tb_Student] ([username], [xuehao], [xingming], [xingbie], [zhuanyeid]) VALUES (@username, @xuehao, @xingming, @xingbie, @zhuanyeid)" 
            SelectCommand="SELECT [username],[xingming], [xingbie],tb_zhuanye.zhuanyename,tb_student.zhuanyeid FROM [tb_Student] join tb_zhuanye on tb_zhuanye.zhuanyeid=tb_student.zhuanyeid WHERE ([username] = @username)" 
            
            
            UpdateCommand="UPDATE [tb_Student] SET  [xingming] = @xingming, [xingbie] = @xingbie, [zhuanyeid] = @zhuanyeid WHERE [username] = @username">
            <SelectParameters>
                <asp:QueryStringParameter Name="username" QueryStringField="stuusername" 
                    Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="username" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="xingming" Type="String" />
                <asp:Parameter Name="xingbie" Type="String" />
                <asp:Parameter Name="zhuanyeid" />
                <asp:Parameter Name="username" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="xuehao" Type="Int32" />
                <asp:Parameter Name="xingming" Type="String" />
                <asp:Parameter Name="xingbie" Type="String" />
                <asp:Parameter Name="zhuanyeid" Type="Int32" />
            </InsertParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
