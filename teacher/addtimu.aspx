<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/teacher/TeacherMasterPage.master" CodeFile="addtimu.aspx.cs" ValidateRequest="false" Inherits="teachermanage_timuguanli_addtimu2" Title="添加题目" EnableTheming="true" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server"  ID="Content1">

    <script language="javascript" type="text/javascript">
 //TreeView onclick 触发事件
        //
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
    if(!public_IsCheckBox(objNode)) 
    {
        return;
    }
    var objCheckBox = objNode; 
    //根据CheckBox状态进行相应处理 
    if(objCheckBox.checked==true) 
    { 
        //递归选中父节点的 CheckBox 
        //setParentChecked(objCheckBox); 
        //递归取消选中的所有子节点
        document.getElementById("<%=this.TextBoxzhishidian.ClientID%>").value++; 

    } 
    else 
    {        
        //递归取消选中所有的子节点 
//        setChildUnChecked(objCheckBox); 
        document.getElementById("<%=this.TextBoxzhishidian.ClientID%>").value--;
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
            document.getElementById("<%=this.TextBoxzhishidian.ClientID%>").value++;
            setChildUnChecked(objParentCheckBox); 
            setParentUnChecked(objParentCheckBox); 
        }
    }
    else
    {
       if(!IsMyChildCheckBoxsChecked(objParentCheckBox))
       {       
            objParentCheckBox.checked = false;
            document.getElementById("<%=this.TextBoxzhishidian.ClientID%>").value--;
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
            document.getElementById("<%=this.TextBoxzhishidian.ClientID%>").value--;
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
function checkboxvalidate(sender,args)
{
    var obj=document.getElementById("<%=this.CheckBoxListduoxuanckda.ClientID %>");
    var k,bol=false;
    var ln=obj.all.tags('input').length;
    for(k=0;k<ln;k++)
    {
        if(obj.all.tags('input')[k].checked)
        {
            bol=true;
            break;
        }
    }
    if(bol)
        args.IsValid=true;
    else
        args.IsValid=false;
}
</script>
        <table>
            <tr>
                <td align="left" valign="top">
                                题型<asp:DropDownList ID="DropDownListtixing" runat="server" 
                                    AutoPostBack="True" DataSourceID="SqlDataSource2"
                                    DataTextField="mingcheng" DataValueField="mingcheng"  
                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1" 
                                    OnDataBound="DropDownList1_OnDataBound" Width="129px">
                                </asp:DropDownList><br />
                            <div class="kechengtreeview">
                            <cc2:MyTreeView ID="TreeViewsource" runat="server"
                                    ShowCheckBoxes="All" ShowLines="True" ExpandDepth="1" OnDataBound="TreeViewsource_DataBound">
                            </cc2:MyTreeView>
                            </div>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                        SelectCommand="SELECT [mingcheng], [tixingid] FROM [tb_timuleixing]"></asp:SqlDataSource>
                </td>
                <td style="width: 721px; height: 323px; text-align: left;" valign="top">
                    <table id="timutable" runat="server"  class="zujuantable">
                        <tr>
                            <td style="width: 80px;">
                                题目</td>
                            <td style="width: 500px;">
                                <FCKeditorV2:FCKeditor ID="FCKeditortigan" runat="server" ToolbarStartExpanded="false">
                                </FCKeditorV2:FCKeditor>
                                　　　　　　　<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FCKeditortigan" Width="92px" ErrorMessage="请输入题目。" EnableClientScript="False">请输入题目。</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr id="cankaodaantr" runat="server">
                            <td style="width: 80px; ">
                                参考答案</td>
                            <td style="width: 500px; height:auto; text-align: left">
                                <FCKeditorV2:FCKeditor ID="FCKeditorcankaodaan" runat="server"  ToolbarStartExpanded="false">
                                </FCKeditorV2:FCKeditor>
                                <asp:RadioButtonList ID="RadioButtonListdanxuanckda" runat="server" RepeatDirection="Horizontal"
                                    Width="129px" RepeatLayout="Flow">
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem Selected="True">C</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:CheckBoxList ID="CheckBoxListduoxuanckda" runat="server" RepeatDirection="Horizontal"
                                    Width="358px" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True">A</asp:ListItem>
                                    <asp:ListItem Selected="True">B</asp:ListItem>
                                    <asp:ListItem Selected="True">C</asp:ListItem>
                                    <asp:ListItem Selected="True">D</asp:ListItem>
                                    <asp:ListItem Selected="True">E</asp:ListItem>
                                </asp:CheckBoxList>
                                <asp:RadioButtonList ID="RadioButtonListpanduandaan" runat="server" Height="24px"
                                    RepeatDirection="Horizontal" Width="188px" RepeatLayout="Flow">
                                    <asp:ListItem Value="T">正确</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="F">错误</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorcankaodaan" runat="server"
                                    ControlToValidate="FCKeditorcankaodaan" ErrorMessage="请输入参考答案。" Width="185px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="checkboxvalidate"
                                    ErrorMessage="请选择参考答案。" Width="179px" Display="Dynamic"></asp:CustomValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                难度</td>
                            <td style="width: 500px;">
                                            <asp:DropDownList ID="DropDownListnandu" runat="server">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem Selected="True">2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                            </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                用途</td>
                            <td style="width: 500px;">
                                            <asp:RadioButtonList ID="RadioButtonListkaoshiorlianxi" runat="server"
                                                RepeatDirection="Horizontal" Width="228px" RepeatLayout="Flow">
                                                <asp:ListItem Selected="True" Value="练习题">练习题</asp:ListItem>
                                                <asp:ListItem Value="考试题">考试题</asp:ListItem>
                                            </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                知识点</td>
                            <td style="width: 500px;">
                                &nbsp; 在知识树中选择，一个以上。已选择<asp:TextBox ID="TextBoxzhishidian" runat="server" Width="21px" ReadOnly="True" Enabled="False">0</asp:TextBox>
                                个。<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBoxzhishidian" MaximumValue="1000" MinimumValue="1" Type="Integer" Display="Dynamic">请至少选择一个知识点。</asp:RangeValidator></td>
                        </tr>
                        <tr id="ziyuanfile" runat="server">
                            <td style="width: 80px;">
                                资源文件</td>
                            <td style="width: 500px;">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="331px" /><asp:RegularExpressionValidator 
 id="RegularExpressionValidator1" runat="server" 
 ErrorMessage="只允许上传mp3,avi,jpg,gif,swf,txt,doc,rar文件!" 
 ValidationExpression="^(([a-zA-Z]:)|(\\{2}[\w\u4e00-\u9fa5]+)\$?)(\\([\w\u4e00-\u9fa5][\w\u4e00-\u9fa5].*))+(.mp3|.MP3|.avi|.AVI|.RAR|.rar|.txt|.TXT|.jpg|.JPG|.doc|.DOC|.swf|.SWF|.gif|.GIF)$"
 ControlToValidate="FileUpload1" Display="Dynamic"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 80px; ">
                                题目解析</td>
                            <td style="width: 500px; ">
                                <FCKeditorV2:FCKeditor ID="FCKeditorshuoming" runat="server"  ToolbarStartExpanded="false">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="height: 27px">
                                <asp:Button ID="Buttonsubmit" runat="server" OnClick="Buttonsubmit_Click" Text="提交"
                                    Width="175px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Lbl_fankui" runat="server" Width="261px" ForeColor="Red" SkinID="Lbl_fankui"></asp:Label></td>
            </tr>
        </table>
</asp:Content>
