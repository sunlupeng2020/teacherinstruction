<%@ Page Language="C#" MasterPageFile="MasterPageDayi.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="onlinedayi_Default" Title="在线答疑首页——查询问题" EnableTheming="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
<TABLE class="layouttable"><TBODY><TR><TD style="WIDTH: 240px; HEIGHT: 315px"><TABLE style="WIDTH: 239px"><TBODY><TR><TD style="WIDTH: 239px; TEXT-ALIGN: left" vAlign=top><DIV class="kechengtreeview">
<cc2:MyTreeView id="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ShowCheckBoxes="All" ExpandDepth="1" ShowLines="True">
</cc2:MyTreeView>
</DIV></TD></TR><TR><TD style="WIDTH: 239px">您选择了<asp:TextBox id="TextBox1" runat="server" Width="31px" ReadOnly="True" Enabled="False"></asp:TextBox>个知识点。<BR /><asp:RangeValidator id="RangeValidator1" runat="server" ValidationGroup="group1" Type="Integer" MinimumValue="1" MaximumValue="1000" ErrorMessage="请选择问题对应的知识点！" Display="Dynamic" ControlToValidate="TextBox1"></asp:RangeValidator></TD></TR><TR><TD style="WIDTH: 239px"><asp:Button id="Button1" onclick="Button1_Click" runat="server" Width="144px" ValidationGroup="group1" Text="搜索知识点相关问题"></asp:Button></TD></TR></TBODY></TABLE></TD><TD style="HEIGHT: 315px" vAlign=top><TABLE style="WIDTH: 100%; HEIGHT: 100%" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 725px; HEIGHT: 25px; TEXT-ALIGN: left">按关键字搜索问题：<asp:TextBox id="TextBox2" runat="server" Width="187px"></asp:TextBox> <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ValidationGroup="group2" ErrorMessage="请输入关键字！" Display="Dynamic" ControlToValidate="TextBox2"></asp:RequiredFieldValidator> <asp:Button id="Button2" onclick="Button2_Click" runat="server" ValidationGroup="group2" Text="搜一下"></asp:Button></TD></TR><TR>
    <TD style="WIDTH: 725px; HEIGHT: 200px" valign="top"><asp:GridView id="GridView1" 
            runat="server" ForeColor="#333333" Width="728px" AutoGenerateColumns="False" 
            EmptyDataText="没有数据。" GridLines="None" CellPadding="4" 
            DataKeyNames="问题ID号" AllowPaging="True" 
            onpageindexchanging="GridView1_PageIndexChanging">
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

<RowStyle BackColor="#EFF3FB"></RowStyle>
<Columns>
<asp:BoundField DataField="问题ID号" HeaderText="问题号" Visible="False"></asp:BoundField>
<asp:HyperLinkField DataNavigateUrlFields="问题ID号" DataNavigateUrlFormatString="liulanhuida.aspx?wentiid={0}" DataTextField="问题标题" HeaderText="问题标题"></asp:HyperLinkField>
<asp:BoundField DataField="提问时间" HeaderText="提问时间"></asp:BoundField>
<asp:BoundField DataField="回答数" HeaderText="回答数"></asp:BoundField>
    <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton2" runat="server" 
                CommandArgument='<%# Eval("问题ID号") %>' onclick="LinkButton2_Click">删除</asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>

<PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#2461BF"></EditRowStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> </TD></TR><TR><TD style="WIDTH: 725px">
        <asp:HiddenField ID="HFshuju" runat="server"/>
        <asp:HiddenField ID="HFchaxfs" runat="server" />
        </TD></TR></TBODY></TABLE></TD></TR><TR><TD colSpan=2><asp:Label id="Lbl_fankui" runat="server" ForeColor="Red" Width="510px"></asp:Label></TD></TR></TBODY></TABLE>
</asp:Content>

