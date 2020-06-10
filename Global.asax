<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal"  %>
<%@ Import Namespace="System.Web.Security" %>


<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

        //获取Applicat对象中保存的网站总访问量
    }

    void Session_End(object sender, EventArgs e) 
    {
       
        // 在会话结束时运行的代码。
         
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。
        //删除用户cookie
        //HttpCookie mycookie = new HttpCookie(FormsAuthentication.FormsCookieName);
        //mycookie.Expires = DateTime.Now.AddMonths(-1);
        //Response.Cookies.Add(mycookie);

    }
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        string cookieName = FormsAuthentication.FormsCookieName;
        HttpCookie authCookie = Context.Request.Cookies[cookieName];
        FormsAuthenticationTicket authTicket = null;
        try
        {
            authTicket = FormsAuthentication.Decrypt(authCookie.Value);//解密
        }
        catch
        {
            return;
        }
        string[] roles = authTicket.UserData.Split(new char[] { ',' });//如果存取多个角色,我们把它分解
        FormsIdentity id = new FormsIdentity(authTicket);
        System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, roles);
        Context.User = principal;//存到HttpContext.User中
    }
       
</script>
