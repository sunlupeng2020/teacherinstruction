<%@ Page Language="C#" MasterPageFile="~/manager/MasterPageManager.master" AutoEventWireup="true" CodeFile="glymanage.aspx.cs" Inherits="manager_guanliyuanmanage" Title="管理员管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 100%">
        <tr>
            <td>
            <div id="leftmenu">
            <ul>
                <li> <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                    onclick="LinkButton2_Click">添加管理员</asp:LinkButton> </li>
                <li><asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                    onclick="LinkButton3_Click">查询管理员</asp:LinkButton></li>
                <li><asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click" 
                    CausesValidation="False">教师升级为管理员</asp:LinkButton></li>
                <li><asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" 
                    onclick="LinkButton5_Click">删除管理员</asp:LinkButton></li>
            </ul>
            </div>
            </td>
            <td>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
        <asp:View ID="View1" runat="server">
        <table style="width: 51%">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <h4>添加管理员(或将教师升级为系部管理员）</h4></td>
        </tr>
        <tr>
            <td style="height: 17px">
                用户名:</td>
            <td style="height: 17px">
                <asp:TextBox ID="tbx_username" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="tbx_username" ErrorMessage="请输入用户名！"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                密码:</td>
            <td>
                <asp:TextBox ID="tbx_pwd" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="tbx_pwd" ErrorMessage="请输入密码!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                姓名:</td>
            <td>
                <asp:TextBox ID="tbx_xm" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="tbx_xm" ErrorMessage="请输入姓名!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                性别:</td>
            <td>
                <asp:DropDownList ID="ddl_xb" runat="server">
                    <asp:ListItem Selected="True">男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                管理的系部:</td>
            <td>
                <asp:DropDownList ID="ddl_xibu" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="SqlDataSource_xibu" DataTextField="xibuname" 
                    DataValueField="xibuid">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource_xibu" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [xibuid], [xibuname] FROM [tb_Xibu]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button_add" runat="server" onclick="Button1_Click" Text="提交" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 424px">
                        <h4>系统管理员信息</h4></td>
                    <td>
                      <h4>各系部管理员信息</h4></td>
                </tr>
                <tr>
                    <td rowspan="3" style="width: 424px">
                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                            DataSourceID="SqlDataSourcesupermanager" Height="50px" Width="260px" 
                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4">
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <Fields>
                                <asp:BoundField DataField="xingming" HeaderText="姓名" 
                                    SortExpression="xingming" />
                                <asp:BoundField DataField="xingbie" HeaderText="性别" SortExpression="xingbie" />
                                <asp:BoundField DataField="xibuname" HeaderText="系部" 
                                    SortExpression="xibuname" />
                            </Fields>
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <EditRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="SqlDataSourcesupermanager" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                            SelectCommand="SELECT [xingming], [xingbie], tb_xibu.xibuname FROM [tb_Teacher] inner join tb_xibu on tb_xibu.xibuid=tb_teacher.xibuid WHERE (tb_teacher.[manager] = @manager)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="supermanager" Name="manager" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        系部:<asp:DropDownList ID="DropDownList1" runat="server" 
                            DataSourceID="SqlDataSource_xibu" DataTextField="xibuname" 
                            DataValueField="xibuid" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="btn_chaxunguanliyuan" runat="server" Text="查询" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSourcexibumanager" Width="289px" 
                            EmptyDataText="没有该系部管理员信息。">
                            <Columns>
                                <asp:BoundField DataField="xingming" HeaderText="姓名" 
                                    SortExpression="xingming" />
                                <asp:BoundField DataField="xingbie" HeaderText="性别" SortExpression="xingbie" />
                                <asp:BoundField DataField="xibuname" HeaderText="所在系部" 
                                    SortExpression="xibuname" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSourcexibumanager" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                            SelectCommand="SELECT [xingming], [xingbie],tb_xibu.xibuname FROM [tb_Teacher] inner join tb_xibu on tb_xibu.xibuid=tb_teacher.xibuid WHERE (([guanlixibuid] = @xibuid) AND ([manager] = @manager))">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" Name="xibuid" 
                                    PropertyName="SelectedValue" Type="Int32" />
                                <asp:Parameter DefaultValue="manager" Name="manager" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 424px">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="height: 26px;" colspan="2">
                        <h4>将教师设置为管理员</h4></td>
                </tr>
                <tr>
                    <td style="width: 284px">
                        系部:</td>
                    <td>
                        <asp:DropDownList ID="DDLView3xibu" runat="server" 
                            DataSourceID="SqlDataSource_xibu" DataTextField="xibuname" 
                            DataValueField="xibuid" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 284px">
                        教师:</td>
                    <td>
                        <asp:DropDownList ID="DDL_v3_teacher" runat="server" 
                            DataSourceID="SqlDataSourceteacher" DataTextField="xingming" 
                            DataValueField="username" ondatabound="DDL_v3_teacher_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceteacher" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                            SelectCommand="SELECT [username], [xingming] FROM [tb_Teacher] WHERE ([xibuid] = @xibuid)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DDLView3xibu" Name="xibuid" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 284px">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btn_upteacher" runat="server" onclick="btn_upteacher_Click" 
                            Text="提交" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <h4>删除管理员</h4>——将教师的管理员身份去掉，保留其教师身份</td>
                </tr>
                <tr>
                    <td>
                        系部:</td>
                    <td>
                        <asp:DropDownList ID="ddl_v4xibu" runat="server" 
                            DataSourceID="SqlDataSource_xibu" DataTextField="xibuname" 
                            DataValueField="xibuid" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        管理员:</td>
                    <td>
                        <asp:DropDownList ID="ddl_v4gly" runat="server" 
                            DataSourceID="SqlDataSourcev4gly" DataTextField="xingming" 
                            DataValueField="username" ondatabound="ddl_v4gly_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourcev4gly" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                            SelectCommand="SELECT [xingming], [username] FROM [tb_Teacher] WHERE ([guanlixibuid] = @guanlixibuid)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddl_v4xibu" Name="guanlixibuid" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btn_delgly" runat="server" onclick="btn_delgly_Click" 
                            Text="提交" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</td>
        </tr>
        <tr>
        <td colspan="2"><asp:Label ID="lbl_fankui" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

