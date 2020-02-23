<%@ Control Language="C#" ClassName="foot01" %>

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
<dd>
<p>豫ICP备14002493</p></dd>
</dl>
</div>