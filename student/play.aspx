<%@ Page Language="C#" MasterPageFile="~/student/StudentMasterPage.master" AutoEventWireup="true" CodeFile="play.aspx.cs" Inherits="play_sva" Title="播放视频和音频" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="text-align: left;" valign="top">
                    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" Width="199px">
                        <ItemTemplate>
                            <table style="width: 353px">
                                <tr>
                                    <td style="width: 74px; text-align: right">
                                        资源名称：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="jiaoxueziyuannameLabel" runat="server" Text='<%# Eval("jiaoxueziyuanname") %>'>
                            </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 74px; height: 18px; text-align: right">
                            课程：</td>
                                    <td style="width: 239px; height: 18px">
                            <asp:Label ID="kechengnameLabel" runat="server" Text='<%# Eval("kechengname") %>'>
                            </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 74px; text-align: right">
                            上传者：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="usernameLabel" runat="server" Text='<%# Eval("username") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 74px; text-align: right">
                            类型：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="jiaoxueziyuanleixingLabel" runat="server" Text='<%# Eval("jiaoxueziyuanleixing") %>'>
                            </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 74px; text-align: right">
                            媒体类型：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="ziyuanmeitileixingLabel" runat="server" Text='<%# Eval("ziyuanmeitileixing") %>'>
                            </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 74px; text-align: right">
                            上传时间：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="createdateLabel" runat="server" Text='<%# Eval("createdate") %>'>
                            </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 74px; text-align: right">
                            关键字：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="guanjianziLabel" runat="server" Text='<%# Eval("guanjianzi") %>'>
                            </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 74px; text-align: right">
                            介绍：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="instructionLabel" runat="server" Text='<%# Eval("instruction") %>'>
                            </asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                        SelectCommand="SELECT [jiaoxueziyuanname], [kechengname], [username], [usershenfen], [jiaoxueziyuanleixing], [ziyuanmeitileixing], [pingjiahao], [pingjiayiban], [pingjiabuhao], [lilancishu], [ziyuanfile], [instruction], [quanxian], [createdate], [guanjianzi] FROM [tb_Jiaoxueziyuan] WHERE ([jiaoxueziyuanid] = @jiaoxueziyuanid)">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="jiaoxueziyuanid" QueryStringField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    <div id="player5" style="width:400px; margin:0px;" ></div>
<script type="text/javascript" src="swfobject.js"></script>
<script type="text/javascript">
	var s5 = new SWFObject("FlvPlayer201002.swf","playlist","400","300","7");
	s5.addParam("allowfullscreen","true");
	s5.addVariable("autostart","false");
	s5.addVariable("image","aspnet02.jpg");
	s5.addVariable("file","<%=ziyuanfile%>");
	s5.addVariable("width","400");
	s5.addVariable("height","300");
	s5.write("player5");
</script>
</td>
 </tr>
           
        </table>
    
    </div>
</asp:Content>
