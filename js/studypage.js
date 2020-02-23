//学习页面的Cookie写入与检测
//var zhuangtai="study";//状态为学习状态，另一种为考试状态
function setCookie()
{
    var exp=new Date();
    exp.setTime(exp.getTime()+60000);
    document.cookie="zhuangtai="+escape("study")+";expires="+exp.toGMTString()+";path=/";
}
function Myautorun()
{
    var currentPageUrl = window.location.pathname.toLowerCase();
    var index=currentPageUrl.lastIndexOf("/");
    var curpage=currentPageUrl.substring(index+1);
    if(curpage!="default.aspx"||curpage!="zuoye.aspx"||curpage!="jiaoshiceshi.aspx"||curpage!="xuanzekecheng.aspx")
    {
        var zhuangtai=getCookie("zhuangtai");
        if(zhuangtai==null)
        {
            setCookie();
            setInterval("setCookie()",60000);
        }
        else if(zhuangtai=="kaoshi")
        {
            alert("检测到您正在进行考试，不能打开学习页面，请交卷后再打开本页！");
            document.getElementById("maindiv").setAttribute("visibility",false);
        }        
    }
    //setCookie();
    //setInterval("setCookie()",180000);
}
//  Get current page url using JavaScript 
//var currentPageUrl = ""; 
//if (typeof this.href === "undefined") 
//{
//      currentPageUrl = document.location.toString().toLowerCase();
//} 
//else 
//{     
//    currentPageUrl = this.href.toString().toLowerCase(); 
//} 


function getCookie(name)//读取cookie
{
    var arr = document.cookie.match(new RegExp("(^|)" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
//    var arr,reg=new RegExp("^|)"+name+"=([^;]*)(;|$)");
//    if(arr=document.cookie.match(reg))
//        return unescape(arr[2]);
//    else
//        return null;
}