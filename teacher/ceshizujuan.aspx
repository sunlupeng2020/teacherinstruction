<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="TeacherMasterPage.master" CodeFile="ceshizujuan.aspx.cs" Inherits="teachermanage_ceshizujuan" EnableTheming="true"  Title="在线组卷" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">

    <script language="javascript" type="text/javascript">
function GetIndex(checkbox)
{
    if(checkbox.checked==true) {
        document.getElementById("<%=Labelhidden.ClientID%>").value += "," + checkbox.value + ",";
    }
    else {
        document.getElementById("<%=Labelhidden.ClientID%>").value = document.getElementById("<%=Labelhidden.ClientID%>").value.replace("," + checkbox.value + ",", "");
    }
}

        //TreeView onclick 触发事件
        //需要认真研究 
        //原则：1、如果父结点被选中，则取消其所有子结点的选中，
        //2、如果一个结点的所有子结点全部被选中,则取父结点被选中，并取消该结点父结点所有子结点的选中
        //3、父结点和子结点不能同时被选中

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
            document.getElementById("<%=this.TextBoxzhishidianshu.ClientID%>").value++; 
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
            document.getElementById("<%=this.TextBoxzhishidianshu.ClientID%>").value--;
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
            document.getElementById("<%=this.TextBoxzhishidianshu.ClientID%>").value++;
            setChildUnChecked(objParentCheckBox); 
            setParentUnChecked(objParentCheckBox); 
        }
    }
    else
    {
       if(!IsMyChildCheckBoxsChecked(objParentCheckBox))
       {       
            objParentCheckBox.checked = false;
            document.getElementById("<%=this.TextBoxzhishidianshu.ClientID%>").value--;
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
            document.getElementById("<%=this.TextBoxzhishidianshu.ClientID%>").value--;
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
        <asp:UpdateProgress id="UpdateProgress1" runat="server"><ProgressTemplate>
<IMG src="../images/jd002.gif"  alt="请稍候..." style="position:absolute;left:50%;top:50%;"/>
</ProgressTemplate>
</asp:UpdateProgress>
<TABLE><TBODY>
<TR>
<TD style="VERTICAL-ALIGN: top; HEIGHT:auto; TEXT-ALIGN:center">
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Width="980px" 
        NavigationStyle-HorizontalAlign="Center" DisplaySideBar="False">
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton0" runat="server" 
                onclick="StartNextButton_Click" Text="下一步:填写详细信息" ValidationGroup="sgzj01" />
        </StartNavigationTemplate>
        <NavigationStyle HorizontalAlign="Center" />
        <WizardSteps>
            <asp:WizardStep runat="server" StepType="Start" title="Step 1">
                <table style="WIDTH: 600px; height: 340px;">
                    <tbody>
                        <tr>
                            <td style="TEXT-ALIGN: left; width:300px; vertical-align: top; overflow: auto; height: 300px;">
                                <div class="kechengtreeview">
                                <cc2:MyTreeView ID="TreeView1" runat="server" ExpandDepth="1" 
                                        onclick="client_OnTreeNodeChecked()" ShowCheckBoxes="All" ShowLines="True" 
                                        ToolTip="勾选测试知识点。">
                                </cc2:MyTreeView>
                                </div>
                            </td>
                            <td  
                                
                                style="VERTICAL-ALIGN:bottom; WIDTH: 300px; HEIGHT: 30px; TEXT-ALIGN: left">
                                您选择了<asp:TextBox ID="TextBoxzhishidianshu" runat="server" Enabled="False" 
                                    EnableViewState="False" ReadOnly="True" Width="35px">0</asp:TextBox>
                                个知识点。<br />
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" 
                                    ControlToValidate="TextBoxzhishidianshu" ErrorMessage="RangeValidator" MaximumValue="10000" 
                                    MinimumValue="1" Type="Integer" ValidationGroup="sgzj01">请选择测试知识点！</asp:RangeValidator>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" title="Step 2">
                <table class="zujuantable">
                    <tbody>
                        <tr>
                            <td style="width: 91px;" >
                                测试名称</td>
                            <td  class="lefttd" >
                                <asp:TextBox ID="TextBoxceshimingcheng" runat="server" Width="398px" 
                                    MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="TextBoxceshimingcheng" ErrorMessage="请输入测试名称。"  ValidationGroup="sgzj02" 
                                    SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="WIDTH: 91px; ">
                                测试班级</td>
                            <td  class="lefttd">
                                <asp:DropDownList ID="DropDownList_banji" runat="server" DataTextField="banjiname" DataValueField="banjiid">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                题目详情</td>
                            <td class="lefttd">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" 
                                    OnLoad="PlaceHolder1_DataBinding"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td style="WIDTH: 91px;">
                                总分</td>
                            <td class="lefttd">
                                总分应为100分，根据您的设置计算，总分是<asp:TextBox ID="TextBoxzongfen" runat="server" 
                                    Enabled="False" Width="30px">0</asp:TextBox>
                                分。 <asp:Button
                                        ID="计算总分" runat="server" Text="计算总分" OnClick="JisuanZongfen"  CausesValidation="False" /><asp:RangeValidator ID="RangeValidator4" runat="server"  ValidationGroup="sgzj02" 
                                    ControlToValidate="TextBoxzongfen" ErrorMessage="总分应为100分，请重新设置或重新计算！" 
                                    MaximumValue="100" MinimumValue="100" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 91px;" >
                                选题方式</td>
                            <td class="lefttd" >
                                <asp:RadioButtonList ID="RadioButtonListxuantifangshi" runat="server" 
                                    AutoPostBack="True" 
                                    OnSelectedIndexChanged="RadioButtonListxuantifanshi_SelectedIndexChanged" 
                                    RepeatDirection="Horizontal" ToolTip="某种题型的题目选择数要不少于设定的数目。" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="0">按我所选全部题目组卷</asp:ListItem>
                                    <asp:ListItem Value="1">从我所选题目中随机组卷</asp:ListItem>
                                    <asp:ListItem Value="2">全自动随机组卷</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 91px;">
                                组卷方式</td>
                            <td  class="lefttd">
                                <asp:RadioButtonList ID="RadioButtonListzujuanfangshi" runat="server" 
                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="全体学生相同">全部学生，题目相同</asp:ListItem>
                                    <asp:ListItem Enabled="False" Value="全体学生不同">全部学生，题目不同</asp:ListItem>
                                    <asp:ListItem Value="对学生保密">只组一套，用于生成纸质试卷</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 91px;">
                                测试时间</td>
                            <td  class="lefttd">
                                <asp:DropDownList ID="DropDownList4" runat="server">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>60</asp:ListItem>
                                    <asp:ListItem>70</asp:ListItem>
                                    <asp:ListItem>80</asp:ListItem>
                                    <asp:ListItem>90</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>120</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                </asp:DropDownList>
                                分钟</td>
                        </tr>
                        <tr>
                            <td style="width: 91px;">
                                允许做题？</td>
                            <td  class="lefttd">
                                <asp:RadioButtonList ID="RadioButtonList9" runat="server" 
                                    RepeatDirection="Horizontal" ToolTip="如果只组一套卷，则本选项不起作用" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="允许">允许学生开始做题</asp:ListItem>
                                    <asp:ListItem Value="禁止">不允许学生开始做题</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 91px;" >
                                允许查看？</td>
                            <td  class="lefttd">
                                <asp:RadioButtonList ID="RadioButtonList10" runat="server"  
                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="允许">测试完成后，允许立即查看测试结果</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="禁止">禁止立即查看，经教师允许后方可查看测试结果</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 91px;">
                                限制IP</td>
                            <td class="lefttd">
                                <asp:CheckBox ID="chbxianzhiip" runat="server" Text="限制IP"  ToolTip="学生考试时IP地址必须唯一，以防止作弊。"/></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:HiddenField ID="HiddenFieldtixings" runat="server" />
                                <asp:HiddenField ID="HiddenFieldnodeandchildren" runat="server" />
                                <asp:HiddenField ID="HiddenFieldzhishidianname" runat="server" />
                                <asp:HiddenField ID="HiddenFieldnodeids" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Finish" Title="Step 3">
                <table style="width: 100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                手工组卷第三步：选择题目</td>
                        </tr>
                        <tr>
                            <td style="WIDTH: 100%; ">
                                <asp:Literal ID="Literalxuantitishi" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="WIDTH: 100%; ">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="题号" OnRowDataBound="Gridview1_RowDataBound" Width="956px" 
                                    AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="30">
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <ItemTemplate>
                                                <input id="CheckBox2" runat="server" onclick="GetIndex(this)" value='<%#DataBinder.Eval(Container.DataItem,"题号")%>' type="checkbox" />
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="题号" ShowHeader="False" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("题号") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("题号") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="题型" HeaderText="题型" >
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" >
                                            <ItemStyle Width="400px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="答案" HeaderText="参考答案" >
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="资源文件" 
                                            DataNavigateUrlFormatString="{0}" HeaderText="资源文件" Text="下载文件" >
                                            <ItemStyle Width="80px" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="WIDTH: 100%; ">
                                <input id="Labelhidden" runat="server"  type="hidden" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Complete" Title="Step 4">
                <table style="width: 89%">
                    <tbody>
                        <tr>
                            <td colspan="2" style="width: 800px; ">
                                在线组卷信息汇总</td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                测试名称：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelceshimingcheng" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                课程：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelkecheng" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                班级：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelbanji" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                知识范围：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelzhishidian" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                题型：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labeltixing" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                组卷方式：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelzujuanfangshi" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                测试时长：</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelshichang" runat="server"></asp:Label>
                                分钟。</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                满分分值：</td>
                            <td style="text-align: left">
                                100分。</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                允许做题？</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelyunxu" runat="server"></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td style="text-align: right">
                                限制IP？</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelxianzhiip" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                交卷后立即查看？</td>
                            <td style="text-align: left">
                                <asp:Label ID="Labelyunxuchakan" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                为下列同学组卷成功：</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Literal ID="Literalstudentxiangxi" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" 
                CommandName="MovePrevious" Text="上一步" />
            <asp:Button ID="FinishButton" runat="server" onclick="FinishButton_Click" 
                Text="选题完成,开始组卷" />
        </FinishNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" 
                CommandName="MovePrevious" Text="上一步" />
            <asp:Button ID="StepNextButton" runat="server" onclick="StepNextButton_Click" 
                Text="下一步:选择题目" ValidationGroup="sgzj02"  CausesValidation="True"/>
        </StepNavigationTemplate>
    </asp:Wizard>
    </TD></TR><TR><TD style="HEIGHT: 30px; TEXT-ALIGN: center"> 
        &nbsp;</TD></TR></TBODY></TABLE>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>