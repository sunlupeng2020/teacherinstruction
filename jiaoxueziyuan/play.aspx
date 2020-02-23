<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="play.aspx.cs" Inherits="play" Title="播放视频和音频" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>播放视频和音频</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
            <td class="biaotijishuoming" colspan="2">
            <table>
            <tr>
            <td id="wangyezuocebiaoti" style="text-align: left; vertical-align:top; width: 120px;">
                <span style="font-family: 微软雅黑;font-size: 14px; color:White;"><strong>
            播放媒体文件</strong></span>
            </td>
                <td style="height: 20px; text-align: center; color:Blue;">
                </td>
            </tr>
            </table> 
            </td>
            </tr>
            <tr>
                <td style="width: 307px; text-align: left;" valign="top">
                    <asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource1" 
                        Width="299px">
                        <ItemTemplate>
                            <table style="width: 361px">
                                <tr>
                                    <td style="width: 117px; text-align: right">
                                        资源名称：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="jiaoxueziyuannameLabel" runat="server" Text='<%# Eval("jiaoxueziyuanname") %>' Width="196px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 117px; height: 18px; text-align: right">
                                        课程：</td>
                                    <td style="width: 239px; height: 18px">
                            <asp:Label ID="kechengnameLabel" runat="server" Text='<%# Eval("kechengname") %>' Width="190px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 117px; text-align: right">
                                        上传者：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="usernameLabel" runat="server" Text='<%# Eval("xingming") %>' Width="144px" 
                                            Height="22px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 117px; text-align: right">
                                        资源类型：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="jiaoxueziyuanleixingLabel" runat="server" Text='<%# Eval("jiaoxueziyuanleixing") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 117px; text-align: right">
                                        媒体类型：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="ziyuanmeitileixingLabel" runat="server" Text='<%# Eval("ziyuanmeitileixing") %>' Width="189px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 117px; text-align: right">
                                        上传时间：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="createdateLabel" runat="server" Text='<%# Eval("createtime") %>' 
                                            Width="215px" Height="21px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 117px; text-align: right">
                                        关键字：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="guanjianziLabel" runat="server" Text='<%# Eval("guanjianzi") %>' Width="193px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 117px; text-align: right">
                                        介绍：</td>
                                    <td style="width: 239px">
                            <asp:Label ID="instructionLabel" runat="server" Text='<%# Eval("instruction") %>' 
                                            Width="218px" Height="20px"></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetZiyunaXiangqing" TypeName="ZiyuanInfo">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="jiaoxueziyuanid" 
                                QueryStringField="jiaoxueziyuanid" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
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
	s5.addVariable("image","../images/aspnet02.jpg");
	s5.addVariable("file","<%=ziyuanfile%>");
	s5.addVariable("width","400");
	s5.addVariable("height","300");
	s5.write("player5");
</script>
</td>
 </tr>
        </table>
    </div>
</form>
</body>
</html>