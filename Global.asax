<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal"  %>
<%@ Import Namespace="System.Web.Security" %>


<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码
        int count = 0;
        System.IO.StreamReader srd;
        string filepath = Server.MapPath("~/webcount/counter.txt");
        srd = System.IO.File.OpenText(filepath);
        while (srd.Peek() != -1)
        {
            string str = srd.ReadLine();
            count = int.Parse(str);
        }
        srd.Close();
        object obj = count;
        Application["counter"] = obj;
        Application["useronline"] = 0;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码
        //在应用程序关闭时运行的代码
        int stat = 0;
        stat = (int)Application["counter"];
        string filepath = Server.MapPath("~/webcount/counter.txt");
        System.IO.StreamWriter srw = new System.IO.StreamWriter(filepath, false);
        srw.WriteLine(stat);
        srw.Close();

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码
        int stat = 0;
        stat = (int)Application["counter"];
        string filepath = Server.MapPath("~/webcount/counter.txt");
        System.IO.StreamWriter srw = new System.IO.StreamWriter(filepath, false);
        srw.WriteLine(stat);
        srw.Close();
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码
        Application.Lock();
        Application["useronline"] = (int)Application["useronline"] + 1;
        int stat = 0;
        //获取Applicat对象中保存的网站总访问量
        stat = (int)Application["counter"];
        stat += 1;
        //object obj = stat;
        Application["counter"] = stat;
        Application.UnLock();
        if (stat % 5 == 0)
        {
            string filepath = Server.MapPath("~/webcount/counter.txt");
            System.IO.StreamWriter srw = new System.IO.StreamWriter(filepath, false);
            srw.WriteLine(stat);
            srw.Close();
        }
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
        Application.Lock();
        Application["useronline"] = (int)Application["useronline"] - 1;
        Application.UnLock();

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
