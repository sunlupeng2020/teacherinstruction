<%@ Page Language="C#" MasterPageFile="MasterPageDayi.master" AutoEventWireup="true" CodeFile="liulanhuida.aspx.cs" Inherits="onlinedayi_liulanhuida" Title="���ߴ��ɡ������⡢�ش�" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
����var fckeditorContentLength = 0;

����function checkFckeditorContent(source, arguments)

����{

����arguments.IsValid = (fckeditorContentLength  > 0);

����}

����function FCKeditor_OnComplete(editorInstance)

����{

����editorInstance.Events.AttachEvent('OnBlur', FCKeditor_OnBlur);

����}

����function FCKeditor_OnBlur(editorInstance)

����{

����fckeditorContentLength = editorInstance.GetXHTML(true).length;

����if(fckeditorContentLength > 0){

����document.getElementById("<%= this.CustomValidator1.ClientID %>").style.display = "none";

����}

����}
����
����</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<TABLE><TBODY><TR>
        <TD style="WIDTH: 958px; border-bottom:#3399ff 1px solid;; text-align: left;">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                SelectCommand="SELECT [wentiid], [wenti], [biaoti] FROM [tb_wenti] WHERE ([wentiid] = @wentiid)">
                <SelectParameters>
                    <asp:QueryStringParameter Name="wentiid" QueryStringField="wentiid" 
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1">
                <EditItemTemplate>
                    wentiid:
                    <asp:Label ID="wentiidLabel1" runat="server" Text='<%# Eval("wentiid") %>'></asp:Label>
                    <br />
                    wenti:
                    <asp:TextBox ID="wentiTextBox" runat="server" Text='<%# Bind("wenti") %>'>
                    </asp:TextBox>
                    <br />
                    biaoti:
                    <asp:TextBox ID="biaotiTextBox" runat="server" Text='<%# Bind("biaoti") %>'>
                    </asp:TextBox>
                    <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="����">
                    </asp:LinkButton>
                    <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="ȡ��">
                    </asp:LinkButton>
                </EditItemTemplate>
                <InsertItemTemplate>
                    wenti:
                    <asp:TextBox ID="wentiTextBox" runat="server" Text='<%# Bind("wenti") %>'>
                    </asp:TextBox>
                    <br />
                    biaoti:
                    <asp:TextBox ID="biaotiTextBox" runat="server" Text='<%# Bind("biaoti") %>'>
                    </asp:TextBox>
                    <br />
                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                        CommandName="Insert" Text="����">
                    </asp:LinkButton>
                    <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="ȡ��">
                    </asp:LinkButton>
                </InsertItemTemplate>
                <ItemTemplate>
                    <table style="width: 954px">
                        <tr>
                            <td colspan="2" style="height: 21px; text-align: left">
                                �������:<asp:Label ID="biaotiLabel" runat="server" Text='<%# Bind("biaoti") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                ��������:<asp:Label ID="wentiLabel" runat="server" Text='<%# Bind("wenti") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            �����ߣ�<asp:Label ID="Labeltiwenzhe" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#huidawenti" id="wolaihuida" runat="server">�����ش�</a>
        </TD></TR><TR>
    <TD style="WIDTH: 958px;  height: 18px; border-bottom:#3399ff 1px solid;">
        <h5>�ش����飺</h5></TD></TR><TR><TD style="border-bottom: #3399ff 1px solid; WIDTH: 958px; ">
        <asp:DataList ID="DataList1" runat="server" DataKeyField="huidaid" 
            DataMember="DefaultView" DataSourceID="SqlDataSource2" 
            OnItemCommand="DataList1_ItemCommand" onitemdatabound="DataList1_ItemDataBound" 
            Width="953px">
            <ItemTemplate>
                <table style="WIDTH: 960px">
                    <tbody>
                        <tr>
                            <td style="WIDTH: 250px; HEIGHT: 23px; TEXT-ALIGN: left">
                                <asp:Label ID="shenfenLabel" runat="server" Text='<%# Eval("shenfen") %>'></asp:Label>
                                _<asp:Label ID="usernameLabel" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                �Ļش�</td>
                            <td style="WIDTH: 701px; HEIGHT: 23px; TEXT-ALIGN: center">
                                ʱ�䣺<asp:Label ID="shijianLabel" runat="server" Text='<%# Eval("huidatime") %>'></asp:Label>
                                <asp:Label ID="Label_huidaid" runat="server" Text='<%# Eval("huidaid") %>' 
                                    Visible="False" Width="6px"></asp:Label>
                                <asp:Label ID="Labelwentiid" runat="server" Text='<%# Eval("wentiid") %>' 
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="TEXT-ALIGN: left">
                                <asp:Label ID="huidaLabel" runat="server" Text='<%# Eval("huida") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="HEIGHT: 21px">
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
                                    SelectCommand="SELECT DISTINCT [username], [pingjia] FROM [tb_huidapingjia] where (username=(select username from tb_wenti where wentiid=@wentiid)) and huidaid=@huidaid">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="Labelwentiid" Name="wentiid" 
                                            PropertyName="Text" />
                                        <asp:ControlParameter ControlID="Label_huidaid" Name="huidaid" 
                                            PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                �Ըûش�����ۣ�<asp:LinkButton ID="LinkButton1" runat="server" 
                                    CausesValidation="False" CommandArgument='<%# Eval("huidaid") %>' 
                                    CommandName="pingjiahuidahao" ToolTip="����������ۡ�"><img src="../images/good.gif" alt="����" height="30px" width ="30px" /></asp:LinkButton>
                                <asp:Label ID="pingjiahaoLabel" runat="server" Text='<%# Eval("pingjiahao") %>'></asp:Label>
                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                    CommandArgument='<%# Eval("huidaid") %>' CommandName="pingjiahuidabuhao" 
                                    ToolTip="����������ۡ�"><img src="../images/notgood.gif" alt="����" height="30px" width ="30px" /></asp:LinkButton>
                                <asp:Label ID="pingjiabuhaoLabel" runat="server" 
                                    Text='<%# Eval("pingjiabuhao") %>'></asp:Label>
                                <br />
                        <asp:FormView  ID="FormView2" runat="server" DataSourceID="SqlDataSource3">
                                    <EditItemTemplate>
                                        pingjia:
                                        <asp:TextBox ID="pingjiaTextBox" runat="server" Text='<%# Bind("pingjia") %>' />
                                        <br />
                                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                                            CommandName="Update" Text="����" />
                                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                                            CausesValidation="False" CommandName="Cancel" Text="ȡ��" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        pingjia:
                                        <asp:TextBox ID="pingjiaTextBox" runat="server" Text='<%# Bind("pingjia") %>' />
                                        <br />
                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                            CommandName="Insert" Text="����" />
                                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                                            CausesValidation="False" CommandName="Cancel" Text="ȡ��" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        &nbsp;������<asp:Literal ID="Literal2" runat="server" Text='<%# Eval("username") %>'></asp:Literal>
                                        �Ըûش�����ۣ�<asp:Literal ID="Literal1" runat="server" Text='<%# Eval("pingjia") %>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:FormView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="HEIGHT: 21px">
                                <hr />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
            </ItemTemplate>
        </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT [huida], [username], [huidatime], [shenfen], [pingjiahao], [pingjiabuhao], [huidaid], [wentiid] FROM [tb_huida] WHERE ([wentiid] = @wentiid) ORDER BY [huidaid]">
            <SelectParameters>
                <asp:QueryStringParameter Name="wentiid" QueryStringField="wentiid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        </TD></TR></TBODY></TABLE><TABLE id="huidatable" runat="server"><TR><TD style="WIDTH: 958px; BORDER-BOTTOM: #3399ff 1px solid;"><a name="huidawenti"><h5>�ش����⣺</h5></a> </TD></TR><TR><TD style="WIDTH: 958px; HEIGHT: 219px; TEXT-ALIGN: center"><FCKeditorV2:FCKeditor id="FCKeditor1" runat="server">
                </FCKeditorV2:FCKeditor> <asp:CustomValidator id="CustomValidator1" runat="server" Width="310px" ValidationGroup="g1" ValidateEmptyText="True" Display="Dynamic" ClientValidationFunction="checkFckeditorContent" ErrorMessage="�ش���Ϊ�գ����������Ļش�" ControlToValidate="FCKeditor1"></asp:CustomValidator></TD></TR><TR><TD style="WIDTH: 958px"><asp:Button id="Button1" onclick="Button1_Click" runat="server" Width="162px" ValidationGroup="g1" Text="�ύ"></asp:Button></TD></TR><TR><TD style="WIDTH: 958px"><asp:Label id="Label1" runat="server" ForeColor="Red" Width="398px"></asp:Label></TD></TR></TABLE>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

