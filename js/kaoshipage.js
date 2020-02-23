//学习页面的Cookie写入与检测
//var zhuangtai="study";//状态为学习状态，另一种为考试状态




function setCookie()
{
    var exp=new Date();
    exp.setTime(exp.getTime()+180000);
    document.cookie="zhuangtai="+escape("kaoshi")+";expires="+exp.toGMTString()+";path=/";
}
function Myautorun()
{
    var zhuangtai=getCookie("zhuangtai");
    if(zhuangtai==null)
    {
        setCookie();
        setInterval("setCookie()",180000);
    }
    else if(zhuangtai=="kaoshi")
    {
        alert("检测到您打开了学习页面，不能进行考试，请关闭学习页面一分钟后再进行考试！\\n提醒：答疑系统的所有页面，游客页面、作业页面等均为学习页面。");
        document.getElementById("<%=Placeholder1.ClientID%>").setAttribute("visibility",false);
        window.history.go(-2);
    }
    //setCookie();
    //setInterval("setCookie()",180000);
}
//  Get current page url using JavaScript 
var currentPageUrl = ""; 
if (typeof this.href === "undefined") 
{
      currentPageUrl = document.location.toString().toLowerCase();
} 
else 
{     
    currentPageUrl = this.href.toString().toLowerCase(); 
} 


function getCookie(name)//读取cookie
{
//    var arr,reg=new RegExp("^|)"+name+"=([^;]*)(;|$)");
//    if(arr=document.cookie.match(reg))
//        return unescape(arr[2]);
//    else
//        return null;
var arr = document.cookie.match(new RegExp("(^|)" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}