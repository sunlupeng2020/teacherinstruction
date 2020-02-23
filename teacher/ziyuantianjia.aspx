<%@ Page Language="C#" MasterPageFile="TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ziyuantianjia.aspx.cs" Inherits="jiaoxueziyuan_ziyuantianjia" Title="添加教学资源" EnableTheming="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <script language="javascript" type="text/javascript">
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
    if(!public_IsCheckBox(objNode)) 
    {
        return;
    }
    var objCheckBox = objNode; 
    //根据CheckBox状态进行相应处理 
    if(objCheckBox.checked==true) 
    { 
        document.getElementById("<%=this.TextBoxxiangguanzhishiid.ClientID%>").value++; 
    } 
    else 
    {        
        document.getElementById("<%=this.TextBoxxiangguanzhishiid.ClientID%>").value--;
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
</script>
    <div style="width:980px; text-align:center;">
            <table>
                <tr>
                <td style="width: 273px; text-align: left">
         <div class="kechengtreeview">
         <cc2:MyTreeView ID="TreeView1" runat="server"
                ShowLines="True" ShowCheckBoxes="All" ExpandDepth="1">
                <NodeStyle />
         </cc2:MyTreeView>
        </div>                
                </td>
                <td style="text-align: left" valign="top">
                <table class="fill_table">
                <tr>
                    <td style="width: 100px; height: 26px; text-align: right;">
                        资源名称</td>
                    <td style="width: 458px; height: 26px; text-align: left;">
                        <asp:TextBox ID="TextBoxziyuanname" runat="server" Width="273px" 
                            MaxLength="100"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxziyuanname"
        Display="Dynamic" ErrorMessage="资源名称不能为空。"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right;">
                        资源类型</td>
                    <td style="width: 458px; text-align: left;">
                        <asp:DropDownList ID="DropDownListziyuanleixing" runat="server" DataSourceID="SqlDataSourceziyuanleixing" DataTextField="jiaoxueziyuanleixingname" DataValueField="jiaoxueziyuanleixingname">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 21px; text-align: right;">
                        媒体类型</td>
                    <td style="width: 458px; height: 21px; text-align: left;">
                        <asp:DropDownList ID="DropDownListmeitileixing" runat="server" DataSourceID="SqlDataSourcemeitileixing" DataTextField="ziyuanmeitileixingname" DataValueField="ziyuanmeitileixingname">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 24px; text-align: right;" >
                        资源文件</td>
                    <td style="width: 458px; height: 24px; text-align: left;">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="247px" ToolTip="注意：文件大小不能超过4MB。" />
                        (最大4MB)<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1"
        Display="Dynamic" ErrorMessage="请选择上传文件。"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 22px; text-align: right;">
                        开放程度</td>
                    <td style="width: 458px; height: 22px; text-align: left;">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Size="10pt" 
                            RepeatDirection="Horizontal" Width="276px" RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="完全公开">完全公开</asp:ListItem>
                            <asp:ListItem Value="只对教师">只对教师</asp:ListItem>
                            <asp:ListItem Value="仅限本人">仅限本人</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 26px; text-align: right;">
                        关键字</td>
                    <td style="width: 458px; height: 26px; text-align: left;">
                        <asp:TextBox ID="TextBoxguanjianzi" runat="server" Width="266px"></asp:TextBox>
    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right;">
                        相关知识点</td>
                    <td style="width: 458px">
                        请在左侧知识树中选择。您已经选择了<asp:TextBox
                            ID="TextBoxxiangguanzhishiid" runat="server" Width="36px" Enabled="False" 
                            ReadOnly="True">0</asp:TextBox>个。<asp:RangeValidator
                            ID="RangeValidator1" runat="server" ControlToValidate="TextBoxxiangguanzhishiid"
                            ErrorMessage="RangeValidator" MaximumValue="1000" MinimumValue="1" Type="Integer">请选择资源相关的知识点。</asp:RangeValidator></td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right;">
                        内容介绍</td>
                    <td style="width: 458px">
                        <asp:TextBox ID="TextBoxinstruction" runat="server" Height="88px" TextMode="MultiLine"
                            Width="411px" MaxLength="150" Rows="4"></asp:TextBox></td>
                </tr>
                </table>
                </td>
                </tr>

                
            </table>
            <asp:Button ID="Buttonaddziyuan" runat="server" OnClick="Buttonaddziyuan_Click" Text="提交" />
    </div>
      
        <div style=" float:left; text-align:center; width:950px; height: 40px;">
    <asp:Label ID="Lbl_fankui" runat="server" Text="" Width="581px" ForeColor="#FF0000"></asp:Label></div>
    &nbsp;
    <asp:SqlDataSource ID="SqlDataSourceziyuanleixing" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" SelectCommand="SELECT [jiaoxueziyuanleixingname] FROM [tb_ZiyuanLeixing]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourcemeitileixing" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
        SelectCommand="SELECT [ziyuanmeitileixingname] FROM [tb_ZiyuanMeitiLeixing]"></asp:SqlDataSource>
</asp:Content>

