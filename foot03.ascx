<%@ Control Language="C#" ClassName="foot03" %>

<script runat="server">
    
  

protected void  Page_Load(object sender, EventArgs e)
{
    ltruseronline.Text=Application["useronline"].ToString();
    ltrcounter.Text = Application["counter"].ToString();
}
</script>

<div class="clear_div2 footer">                            
<dl class="clear_div2 footer">
<dt>在线人数：<asp:Literal ID="ltruseronline" runat="server"></asp:Literal>  总访问量：<asp:Literal ID="ltrcounter" runat="server"></asp:Literal></dt>
<dt>郑州师范学院信息科学与技术学院&nbsp;&nbsp;设计开发<asp:HyperLink ID="HyperLink1" runat="server" 
        ImageUrl="~/images/ymxz.gif" 
        NavigateUrl="http://www.51aspx.com/code/codename/26745" Target="_blank" 
        ToolTip="本站源码下载（含数据库）">本站源码下载（含数据库）</asp:HyperLink>
            </dt><dd><span>联系人：孙老师</span><span>QQ:326641288</span><span>电话(集团号):617068</span><span>电子邮箱：slp2060@163.com</span>
<p>豫ICP备14002493</p></dd>
</dl>
</div>