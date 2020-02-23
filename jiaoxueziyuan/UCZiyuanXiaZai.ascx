<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCZiyuanXiaZai.ascx.cs" Inherits="UCZiyuanXiaZai" %>
<div class="kechengtreeview">
         <cc2:MyTreeView ID="TreeView1" runat="server"
                ShowLines="True" ShowCheckBoxes="All" ExpandDepth="1">
                <NodeStyle />
         </cc2:MyTreeView>
</div>
<div style=" float:right; width:738px; display:inline;">
资源类型：<asp:DropDownList ID="DropDownListziyuanleixing" runat="server" 
                                AppendDataBoundItems="True" DataSourceID="SqlDataSource1" 
                                DataTextField="jiaoxueziyuanleixingname" 
                                DataValueField="jiaoxueziyuanleixingname">
                                <asp:ListItem Selected="True">全部</asp:ListItem>
                            </asp:DropDownList>
&nbsp; 媒体类型：<asp:DropDownList ID="DropDownListmeitileixing" runat="server" AppendDataBoundItems="True" 
                                DataSourceID="SqlDataSourcemeitileixing" DataTextField="ziyuanmeitileixingname" 
                                DataValueField="ziyuanmeitileixingname">
                                <asp:ListItem Selected="True">全部</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="Buttonzsdss" runat="server" onclick="Buttonzsdss_Click" 
                                Text="按知识点搜索" />
&nbsp;关键字：<asp:TextBox ID="TextBox1" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TextBox1" Display="None" ErrorMessage="请输入关键字！" 
                                ValidationGroup="g1"></asp:RequiredFieldValidator>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                    Text="搜索" ValidationGroup="g1" />
                     <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Width="740px" 
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                GridLines="None" EmptyDataText="抱歉，未找到相关教学资源。"  
                                OnRowDataBound="GridView1_RowDataBound" DataKeyNames="资源ID" 
                                AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="资源ID" HeaderText="资源ID" Visible="False" />
                                    <asp:BoundField DataField="资源名称" HeaderText="资源名称" />
                                    <asp:BoundField DataField="资源类型" HeaderText="资源类型" />
                                    <asp:BoundField DataField="媒体类型" HeaderText="媒体类型" />
                                    <asp:BoundField DataField="上传时间" HeaderText="上传时间" Visible="False" />
                                    <asp:HyperLinkField DataNavigateUrlFields="资源文件" HeaderText="文件下载" Text="下载" 
                                        Target="_blank" />
                                    <asp:HyperLinkField DataNavigateUrlFields="资源ID" 
                                        DataNavigateUrlFormatString="play.aspx?jiaoxueziyuanid={0}" HeaderText="播放" 
                                        Text="播放" Target="_blank" />
                                    <asp:CommandField CausesValidation="False" SelectText="资源详情" 
                                        ShowSelectButton="True" />
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                             <asp:Label ID="Label1" runat="server" Width="224px"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="g1" />
                <br />
                <asp:DataList ID="DataList1" runat="server" 
                    DataSourceID="SqlDataSourceziyuanxiangqing" style="text-align: left">
                    <ItemTemplate>
                        <table style="width:740px">
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    教学资源名称:</td>
                                <td style="text-align: left; width:620px">
                                    <asp:Label ID="jiaoxueziyuannameLabel" runat="server" 
                                        Text='<%# Eval("jiaoxueziyuanname") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    课程:</td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Label ID="kechengnameLabel" runat="server" 
                                        Text='<%# Eval("kechengname") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 22px; width: 120px;">
                                    上传者:
                                </td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Label ID="xingmingLabel" runat="server" Text='<%# Eval("xingming") %>' />
                                    老师<br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    介绍:</td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("instruction") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    公开程度:</td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Label ID="quanxianLabel" runat="server" Text='<%# Eval("quanxian") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    上传时间:</td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Label ID="createtimeLabel" runat="server" 
                                        Text='<%# Eval("createtime") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    关键字:
                                </td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("guanjianzi") %>'></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    文件大小:</td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Label ID="filesizeLabel" runat="server" Text='<%# Eval("filesize") %>' />
                                    字节</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 120px;">
                                    相关知识点:
                                </td>
                                <td style="text-align: left; width:620px;">
                                    <asp:Label ID="xiangguanzhishidianLabel" runat="server" 
                                        Text='<%# Eval("xiangguanzhishidian") %>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="SqlDataSourceziyuanxiangqing" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [jiaoxueziyuanname],tb_kecheng.kechengname,tb_teacher.xingming,[jiaoxueziyuanleixing], [ziyuanmeitileixing], tb_jiaoxueziyuan.[instruction], [quanxian], tb_jiaoxueziyuan.[createtime], [guanjianzi], [filesize], [xiangguanzhishidian] FROM [tb_Jiaoxueziyuan] inner join tb_kecheng on tb_kecheng.kechengid=tb_jiaoxueziyuan.kechengid inner join  tb_teacher on tb_teacher.username=tb_jiaoxueziyuan.username WHERE ([jiaoxueziyuanid] = @jiaoxueziyuanid)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="jiaoxueziyuanid" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="HFziyuanlx" runat="server" />
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [jiaoxueziyuanleixingname] FROM [tb_ZiyuanLeixing]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSourcemeitileixing" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                    SelectCommand="SELECT [ziyuanmeitileixingname] FROM [tb_ZiyuanMeitiLeixing]">
                </asp:SqlDataSource>
</div>
 