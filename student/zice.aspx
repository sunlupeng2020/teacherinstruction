<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/student/StudentMasterPage.master" EnableTheming="true"  AutoEventWireup="true" CodeFile="zice.aspx.cs" Inherits="studentstudy_zice" Title="在线自测" EnableViewState="true"  %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

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
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
    <asp:SqlDataSource id="SqlDataSource1" runat="server" 
        SelectCommand="SELECT [kechengname], [kechengid] FROM [tb_Kecheng]" 
        ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"></asp:SqlDataSource> 
              <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" DisplaySideBar="False">
                  <StartNavigationTemplate>
                      <asp:Button ID="StartNextButton" runat="server" onclick="StartNextButton_Click" 
                          Text="下一步：开始测试" ValidationGroup="g1" />
                  </StartNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" title="Step 1">
            <TABLE style="WIDTH: 600px; height: 300;"><TBODY><TR><TD style="WIDTH: 300px; TEXT-ALIGN: left">
                <div class="kechengtreeview">
                <cc2:MyTreeView ID="TreeView1" runat="server" ExpandDepth="1" 
                        ShowCheckBoxes="All" ShowLines="True" ToolTip="勾选知识点，确定自测范围。" Width="231px">
                </cc2:MyTreeView>
                </div>                
                </TD><TD style="WIDTH: 300px; TEXT-ALIGN: left">您选了<asp:TextBox 
                        ID="TextBox1" runat="server" Enabled="False" ReadOnly="True" Width="31px">0</asp:TextBox>
                    个知识点。<br /> 测试题目数：<asp:RadioButtonList ID="RBL_Timushu" runat="server" 
                        ToolTip="如果题库中相关题目数量不足，则不能进行测试。" Height="16px" 
                        RepeatDirection="Horizontal" Width="172px">
                        <asp:ListItem Value="5" Text="5道题" />
                        <asp:ListItem Selected="True" Value="10" Text="10道题" />
                    </asp:RadioButtonList>
                  <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="请选择测试知识点！" 
                        MaximumValue="1000" MinimumValue="0" Type="Integer" ValidationGroup="g1" 
                        Width="128px"></asp:RangeValidator>
                    </TD></TR></TBODY></TABLE>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" title="Step 2">
             <TABLE style="WIDTH: 700px"><TBODY>
        <TR><TD>测试知识点：<asp:Label id="Labelzhishidian" runat="server" Width="447px"></asp:Label></TD>
            </TR><TR>
                    <TD>题目数量： <asp:Label id="Labeltimushu" runat="server" Width="125px"></asp:Label></TD>
                    </TR>
                 <TR>
                    <TD>
                        <asp:PlaceHolder ID="PlaceHolderceshitimu" runat="server" 
                            OnLoad="PlaceHolderceshitimu_PreRender"></asp:PlaceHolder>
                     </TD></TR>
                 <tr>
                     <td colspan="2">
                         <asp:Label ID="Labelfankui" runat="server" ForeColor="Red" Width="584px"></asp:Label>
                     </td>
                 </tr>
                 </TBODY></TABLE>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Complete">
            <table>
            <tr>
            <td><h3>自测总结</h3></td>
            </tr>
                <tr>
                    <td>
                        测试知识点：<asp:Label ID="Labelzhshd" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        题目数量：<asp:Label ID="Labeltmsl" runat="server" Text="Label"></asp:Label> &nbsp; &nbsp; &nbsp;测试成绩：<asp:Label ID="Labelchj" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Labelfkxx" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                        题目详情<hr />
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Literal ID="Literaltimuxq" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            </asp:WizardStep>
        </WizardSteps>
                  <FinishNavigationTemplate>
                      <asp:Button ID="FinishButton" runat="server" 
                          onclick="FinishButton_Click" Text="交卷" />
                  </FinishNavigationTemplate>
    </asp:Wizard>            
    <asp:HiddenField ID="HiddenField1" runat="server" /></ContentTemplate></asp:UpdatePanel>
 </asp:Content>