<%@ Page Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zhishidianbianji.aspx.cs" Inherits="kechengguanli_sykcshunxu" Title="知识点编辑" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

    <script language="javascript" type="text/javascript">
var oEditer;
function CustomValidate(source, arguments)//验证知识点介绍
{
    var value = oEditer.GetXHTML(true);
    if(value.length>5000)
    {
       arguments.IsValid = false;     
    }
    else 
    { 
        arguments.IsValid = true; 
    } 
}
function FCKeditor_OnComplete(editorInstance)
{ 
    oEditer = editorInstance;
}
</script>
 <table>
       <tr >
            <td>
    <div class="kechengtreeview">
            <cc2:MyTreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                ShowLines="True" Width="210px" ExpandDepth="1">
                </cc2:MyTreeView>
    </div>
            </td>
            <td valign="top" style="width: 741px;">
                <table>
                    <tr>
                        <td>
                            当前知识点：<asp:TextBox ID="Lbl_zhishiname" runat="server" Width="300px" ReadOnly="True" ToolTip="在左侧知识树中单击选择。" Enabled="False"></asp:TextBox>
             <asp:Label ID="Lbl_zhishiid" runat="server" Visible="False" Width="40px"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Lbl_zhishiname"
                                ErrorMessage="请在左侧知识树中选择知识点！"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 699px; height: 19px; text-align: left;">
                            位置：<asp:Label ID="Lbl_dangqianweizhi" runat="server" Text="知识点在结构树中的位置" Width="601px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 699px" valign="top">
             <div>
                 <div style="text-align: left">
                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                         DeleteCommand="DELETE FROM [tb_KechengJiegou] WHERE [kechengjiegouid] = @original_kechengjiegouid AND [jiegouname] = @original_jiegouname AND (([instruction] = @original_instruction) OR ([instruction] IS NULL AND @original_instruction IS NULL)) AND (([zhishileixing] = @original_zhishileixing) OR ([zhishileixing] IS NULL AND @original_zhishileixing IS NULL)) AND (([xuhao] = @original_xuhao) OR ([xuhao] IS NULL AND @original_xuhao IS NULL)) AND (([createdate] = @original_createdate) OR ([createdate] IS NULL AND @original_createdate IS NULL))"
                         InsertCommand="INSERT INTO [tb_KechengJiegou] ([jiegouname], [instruction], [zhishileixing], [xuhao], [createdate]) VALUES (@jiegouname, @instruction, @zhishileixing, @xuhao, @createdatel)"
                         SelectCommand="SELECT [kechengjiegouid], [jiegouname], [instruction], [zhishileixing], [xuhao], [createtime]  FROM [tb_KechengJiegou] WHERE ([kechengjiegouid] = @kechengjiegouid)"
                         UpdateCommand="UPDATE [tb_KechengJiegou] SET [jiegouname] = @jiegouname, [instruction] = @instruction, [xuhao] = @xuhao,createtime=getdate(),zhishileixing=@zhishileixing WHERE [kechengjiegouid] = @original_kechengjiegouid" 
                         ConflictDetection="CompareAllValues" 
                         OldValuesParameterFormatString="original_{0}">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="Lbl_zhishiid" Name="kechengjiegouid" PropertyName="Text"
                                 Type="Int32" />
                         </SelectParameters>
                         <DeleteParameters>
                             <asp:Parameter Name="original_kechengjiegouid" />
                             <asp:Parameter Name="original_jiegouname" />
                             <asp:Parameter Name="original_instruction" />
                             <asp:Parameter Name="original_zhishileixing" />
                             <asp:Parameter Name="original_xuhao" />
                             <asp:Parameter Name="original_createdate" />
                         </DeleteParameters>
                         <UpdateParameters>
                             <asp:Parameter Name="jiegouname" />
                             <asp:Parameter Name="instruction" />
                             <asp:Parameter Name="xuhao" />
                             <asp:Parameter Name="zhishileixing" />
                             <asp:Parameter Name="original_kechengjiegouid" />
                         </UpdateParameters>
                         <InsertParameters>
                             <asp:Parameter Name="jiegouname" />
                             <asp:Parameter Name="instruction" />
                             <asp:Parameter Name="zhishileixing" />
                             <asp:Parameter Name="xuhao" />
                             <asp:Parameter Name="createdatel" />
                         </InsertParameters>
                     </asp:SqlDataSource>
                     <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="kechengjiegouid"
                         DataSourceID="SqlDataSource2" Height="74px" Width="668px" 
                         >
                         <Fields>
                             <asp:TemplateField HeaderText="名称" SortExpression="jiegouname">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("jiegouname") %>' 
                                         MaxLength="50" Height="23px" Width="379px"></asp:TextBox><asp:RequiredFieldValidator
                                         ID="RequiredFieldValidatorzhishiname" runat="server" ControlToValidate="TextBox3"
                                         Display="Dynamic" ErrorMessage="请输入知识点名称。" ValidationGroup="g1" 
                                         Width="134px" Height="17px"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                             ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox3"
                                             Display="Dynamic" ErrorMessage="知识点名称最多50个汉字，不能有非法字符，两端不能有空格。" ValidationExpression="^[\u4e00-\u9fa5A-Za-z\.][\u4e00-\u9fa5A-Za-z0-9，。？：；‘’！“”—…、\x20-\xfe]{0,49}$"
                                             ValidationGroup="g1" Width="464px"></asp:RegularExpressionValidator>
                                 </EditItemTemplate>
                                 <InsertItemTemplate>
                                     <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("jiegouname") %>' MaxLength="50"></asp:TextBox>
                                 </InsertItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("jiegouname") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:BoundField DataField="kechengjiegouid" HeaderText="kechengjiegouid" InsertVisible="False"
                                 ReadOnly="True" SortExpression="kechengjiegouid" Visible="False" />
                             <asp:TemplateField HeaderText="序号" SortExpression="xuhao">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("xuhao") %>'></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox2"
                                         Display="Dynamic" ErrorMessage="序号输入有误,应为数字。" ValidationExpression="^[1-9]\d*$"
                                         ValidationGroup="g1"></asp:RegularExpressionValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                         ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="请输入序号。" 
                                         ValidationGroup="g1"></asp:RequiredFieldValidator>
                                 </EditItemTemplate>
                                 <InsertItemTemplate>
                                     <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("xuhao") %>'></asp:TextBox>
                                 </InsertItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("xuhao") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:BoundField DataField="createtime" HeaderText="更新日期" SortExpression="createtime" ReadOnly="True" />
                             <asp:TemplateField HeaderText="介绍" SortExpression="instruction">
                                 <EditItemTemplate>
                                     <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="300px" 
                                         Value='<%# Bind("instruction") %>' Width="100%">
                                     </FCKeditorV2:FCKeditor>
                                     <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                         ControlToValidate="FCKeditor1" Display="Dynamic" 
                                         ErrorMessage="知识点介绍太长，请不要超过5000字符。" 
                                         ClientValidationFunction="CustomValidate" ValidationGroup="g1" ></asp:CustomValidator>
                                 </EditItemTemplate>
                                 <InsertItemTemplate>
                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("instruction") %>'></asp:TextBox>
                                 </InsertItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("instruction") %>' Width="532px"></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="知识类型">
                                 <ItemTemplate>
                                     <asp:Label ID="Label4" runat="server" Text='<%# Eval("zhishileixing") %>'></asp:Label>
                                 </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownListdetaillx" runat="server" 
                                        DataSourceID="SqlDataSource4" DataTextField="zhishileixing" 
                                        DataValueField="zhishileixing" 
                                        SelectedValue= '<%# Bind("zhishileixing") %>' >
                                    </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                         SelectCommand="SELECT [zhishileixing] FROM [tb_ZhishiLeixing]"></asp:SqlDataSource>
                                </EditItemTemplate>
                             </asp:TemplateField>
                             <asp:CommandField ShowEditButton="True" ValidationGroup="g1" />
                         </Fields>
                         <HeaderStyle Width="100px" />
                     </asp:DetailsView>
                 </div>
             </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button
                         ID="Button_shanchu" runat="server" OnClick="Button1_Click1" Text="删除该知识点" 
                                OnClientClick="javascript:return confirm('你确定要删除该知识点吗?');"  Width="113px" 
                                ToolTip="知识点删除后可能导致知识结构混乱，请谨慎操作！" Enabled="False" /></td>
                    </tr>
                </table>
                     </td>
        </tr>
        <tr>
            <td>
            <asp:LinkButton ID="Button12" runat="server" OnClick="Button12_Click1" 
                    Text="刷新课程结构" Width="124px" CausesValidation="False"/>
            </td>
            <td>
                     <asp:Label ID="Label1" runat="server" Width="607px" 
                    Font-Bold="True" ForeColor="#FF3366"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

