<%@ Page Language="C#" MasterPageFile="MasterPageDayi.master" AutoEventWireup="true" CodeFile="tichuwenti.aspx.cs" Inherits="onlinedayi_tichuwenti" Title="在线答疑——提出疑问" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">
function client_OnTreeNodeChecked() 
{ 
    var objNode; 
    if(!public_IsObjectNull(event.srcElement)) 
    { 
        //IE 
        objNode = event.srcElement; 
    } 
    else 
    { 
        //FF 
        objNode = event.target; 
    } 

    //判断是否 Click 的 CheckBox 
//    if(!public_IsCheckBox(objNode)) 
//    {
//        public_Node2CheckBox(objNode); 
//    }
    if(public_IsCheckBox(objNode)) 
    {
        var objCheckBox = objNode; 
        //根据CheckBox状态进行相应处理 
        if(objCheckBox.checked==true) 
        { 
            //递归选中父节点的 CheckBox 
            //setParentChecked(objCheckBox); 
            //递归取消选中的所有子节点
            document.getElementById("<%=this.TextBox1.ClientID%>").value++; 
    //        var parentNode=public_Node2CheckBox(objCheckBox);
    ////        var parentNode=public_GetParentNode(objCheckBox);
    //        if(!public_IsObjectNull(parentNode)&&public_IsCheckBox(parentNode)&&IsMyChildCheckBoxsChecked(parentNode))
    //            setChildUnChecked(parentNode);
            //取消所有选中的子结点  
    //        setParentUnChecked(objCheckBox);
    //        setChildUnChecked(objCheckBox);
            //递归选中所有的子节点 
    //        setChildChecked(objCheckBox); 
        } 
        else 
        {        
            //递归取消选中所有的子节点 
    //        setChildUnChecked(objCheckBox); 
            document.getElementById("<%=this.TextBox1.ClientID%>").value--;
            //递归取消选中父节点(如果当前节点的所有其他同级节点也都未被选中). 
            //setParentUnChecked(objCheckBox); 
        }
    }
    
     
} 
//判断对象是否为空 
function public_IsObjectNull(element) 
{ 
    if(element==null || element == "undefined") 
        return true; 
    else 
        return false; 
} 

//判断对象是否为 CheckBox 
function public_IsCheckBox(element) 
{ 
    if(public_IsObjectNull(element)) 
        return false; 
        
    if(element.tagName!="INPUT"||element.type!="checkbox") 
        return false; 
    else 
        return true; 
} 
//得到包含所有子节点的 Node(Div 对象) 
function public_CheckBox2Node(element) 
{ 
    var objID = element.getAttribute("ID"); 
    objID = objID.substring(0,objID.indexOf("CheckBox")); 
    return document.getElementById(objID+"Nodes"); 
} 
//得到父节点的 CheckBox 
function public_Node2CheckBox(element) 
{ 
    var objID = element.getAttribute("ID"); 
    objID = objID.substring(0,objID.indexOf("Nodes")); 
    return document.getElementById(objID+"CheckBox"); 
} 
//得到本节点所在的 Node(Div 对象) ,实际是得到本结点的祖先节点
//function public_GetParentNode(element) 
//{ 
//    var parent = element.parentNode; 
//    var upperTagName = "DIV"; 
//    //如果这个元素还不是想要的 tag 就继续上溯 
//    while (parent && (parent.tagName.toUpperCase() != upperTagName)) 
//    { 
//        parent = parent.parentNode ? parent.parentNode : parent.parentElement; 
//    } 
//    return parent; 
//} 
//得到本结点的父结点
function public_GetParentNode(element) 
{ 
    var parent = element.parentNode; 
    var upperTagName = "DIV"; 
    //如果这个元素还不是想要的 tag 就继续上溯 
    while(parent && (parent.tagName.toUpperCase() != upperTagName)) 
    { 
        parent = parent.parentNode ? parent.parentNode : parent.parentElement; 
    } 
    return parent; 
} 


//设置节点的父节点 Checked 
function setParentChecked(currCheckBox) 
{ 
    var objParentNode= public_GetParentNode(currCheckBox); 
    if(public_IsObjectNull(objParentNode)) 
        return;    
    
    var objParentCheckBox = public_Node2CheckBox(objParentNode); 

    if(!public_IsCheckBox(objParentCheckBox)) 
        return; 
    objParentCheckBox.checked = true; 
    //setParentChecked(objParentCheckBox); 
} 
//如果父节点的子节点未全部被选中，则取消父节点的选中
function setParentUnChecked(currCheckBox) 
{ 
    var objParentNode= public_GetParentNode(currCheckBox); 
    if(public_IsObjectNull(objParentNode)) 
        return;  
    var objParentCheckBox = public_Node2CheckBox(objParentNode); 
    if(!public_IsCheckBox(objParentCheckBox)) 
        return; 
    if(objParentCheckBox.checked ==false)
    {
        if(IsMyChildCheckBoxsChecked(objParentCheckBox))
        {
            objParentCheckBox.checked = true;
            document.getElementById("<%=this.TextBox1.ClientID%>").value++;
            setChildUnChecked(objParentCheckBox); 
            setParentUnChecked(objParentCheckBox); 
        }
    }
    else
    {
       if(!IsMyChildCheckBoxsChecked(objParentCheckBox))
       {       
            objParentCheckBox.checked = false;
            document.getElementById("<%=this.TextBox1.ClientID%>").value--;
            //setParentUnChecked(objParentCheckBox);       
        }
    }
} 

//设置节点的子节点 UnChecked 
function setChildUnChecked(currObj) 
{ 
    var currNode; 
    if(public_IsCheckBox(currObj)) 
    { 
        currNode = public_CheckBox2Node(currObj); 
        if (public_IsObjectNull(currNode)) 
            return; 
    } 
    else 
        currNode = currObj; 
      
    var currNodeChilds = currNode.childNodes; 
    var count = currNodeChilds.length; 
    for(var i=0;i <count;i++) 
    { 
        var childCheckBox = currNodeChilds[i]; 
        if(public_IsCheckBox(childCheckBox)&&childCheckBox.checked==true) 
        { 
            childCheckBox.checked = false;
            document.getElementById("<%=this.TextBox1.ClientID%>").value--;
        } 
        setChildUnChecked(childCheckBox); 
    } 
} 
//设置节点的子节点 Checked 
function setChildChecked(currObj) 
{ 
    var currNode; 
    if(public_IsCheckBox(currObj)) 
    { 
        currNode = public_CheckBox2Node(currObj); 
        if (public_IsObjectNull(currNode)) 
            return; 
    } 
    else 
        currNode = currObj; 
        
    var currNodeChilds = currNode.childNodes; 
    var count = currNodeChilds.length; 
    for(var i=0;i <count;i++) 
    { 
        var childCheckBox = currNodeChilds[i]; 
        if(public_IsCheckBox(childCheckBox)) 
        { 
            childCheckBox.checked = true; 
        } 
        setChildChecked(childCheckBox); 
    } 
} 
//判断某节点的子节点是否都为Checked
function IsMyChildCheckBoxsChecked(currObj) 
{ 
    var retVal = true; 
    var currNode; 
    if(public_IsCheckBox(currObj)) 
    { 
        currNode = currObj; 
    } 
    else
    {
        currNode = public_Node2CheckBox(currObj); 
    } 
    var currNodeChilds = currNode.childNodes; 
    var count = currNodeChilds.length; 
    for(var i=0;i <count;i++) 
    { 
        if (retVal == false) 
            break; 
        var childCheckBox = currNodeChilds[i]; 
        if(public_IsCheckBox(childCheckBox))
        {
            childCheckBox=public_Node2CheckBox(childCheckBox);
        }
        if(childCheckBox.checked == false) 
        { 
                retVal = false; 
                return retVal; 
        }
    } 
    return retVal; 
} 
//判断该节点的子节点是否都为 UnChecked 
function IsMyChildCheckBoxsUnChecked(currObj) 
{ 
    var retVal = true; 
    
    var currNode; 
    if(public_IsCheckBox(currObj) && currObj.checked == true) 
    { 
        return false; 
    } 
    else 
        currNode = currObj; 
      
    var currNodeChilds = currNode.childNodes; 
    var count = currNodeChilds.length; 
    for(var i=0;i <count;i++) 
    { 
        if (retVal == false) 
            break; 
        var childCheckBox = currNodeChilds[i]; 
        if(public_IsCheckBox(childCheckBox) && childCheckBox.checked == true) 
        { 
            retVal = false; 
            return retVal; 
        } 
        else 
            retVal = IsMyChildCheckBoxsUnChecked(childCheckBox);        
    } 
    return retVal; 
} 
</script>
    <table>
        <tr>
            <td style="width: 200px; height: 212px; text-align: left" valign="top">
                <div class="kechengtreeview">
                <cc2:MyTreeView ID="TreeView1" runat="server" ShowCheckBoxes="All" ShowLines="True"
                    Width="198px" OnDataBound="TreeViewsource_DataBound" ExpandDepth="1">
                    </cc2:MyTreeView>
                  <br /> 已选择<asp:TextBox ID="TextBox1" runat="server" Width="19px"></asp:TextBox>个知识点。<br />
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBox1"
                    Display="Dynamic" ErrorMessage="请选择问题对应的知识点！" MaximumValue="1000" MinimumValue="1"
                    Type="Integer" Width="195px"></asp:RangeValidator></div>
               </td>
            <td style="width: 640px; height: 212px" valign="top">
                <table style="width: 640px">
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            问题标题<br />
                            (最多200字)</td>
                        <td style="width: 2px; text-align: left">
                            <asp:TextBox ID="TextBox2" runat="server" Height="45px" TextMode="MultiLine" Width="540px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2"
                                ErrorMessage="请输入问题标题！" Width="151px"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            问题描述<br />
                            (最多2000字)</td>
                        <td style="width: 540px">
                            <fckeditorv2:fckeditor id="FCKeditor1" runat="server"></fckeditorv2:fckeditor>
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交问题" 
                                Width="229px" ToolTip="只有登录用户才能提问。" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Width="324px"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

