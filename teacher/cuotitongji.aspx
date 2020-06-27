<%@ Page Title="" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="cuotitongji.aspx.cs" Inherits="teacher_cuotitongji" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table>
    <tr>
        <td style=" width:240px; vertical-align:top; height:auto;" valign="top">
                  <asp:CheckBox ID="CheckBox1" runat="server" Text="显示下级知识点的题目" 
                      AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" /><br />
                  <div class="kechengtreeview">
                  <cc2:MyTreeView   ID="TreeViewsource" runat="server" Width="240px" ExpandDepth="1" ShowLines="True" 
                                        onselectednodechanged="TreeViewsource_SelectedNodeChanged" >
                      <SelectedNodeStyle ForeColor="#FF9900" Font-Bold="True" Font-Underline="True" />
                  </cc2:MyTreeView>
                  </div>
        </td>
        <td style="width:740px; text-align:left; vertical-align:top; overflow:auto;" valign="top">
        <table id="TABLE1" style="width: 740px">        
        <tr>
        <td style="width:740px">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 143px">
                        按关键字查询题目&gt;&gt;&gt;                     </td>
                    <td>
                        关键字:<asp:TextBox ID="TextBox1" runat="server" Width="122px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                ErrorMessage="请输入关键字！" ValidationGroup="chaxun" Display="Dynamic"></asp:RequiredFieldValidator>
                        &nbsp;
            <asp:Button ID="Button2" runat="server" Text="按关键字搜索题目" OnClick="Button2_Click" 
                            ValidationGroup="chaxun" Width="101px" />
                    </td>
                </tr>
                </table>
        </td>
        </tr>
        <tr>
        <td style="width: 740px">
              <asp:GridView ID="GridView1" runat="server" 
                  EmptyDataText="没有相关题目。" OnPageIndexChanging="GridView1_PageIndexChanging" 
                  AutoGenerateColumns="False" Width="740px" DataKeyNames="questionid" 
                  AllowPaging="True" ondatabound="GridView1_DataBound">
                  <Columns>
                      <asp:TemplateField HeaderText=" 序号" ItemStyle-Width="20px">
                          <ItemTemplate>
                              <asp:Label ID="timubh" runat="server"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                          </EditItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="questionid" HeaderText="questionid" 
                          Visible="False" />
                      <asp:BoundField DataField="timu" HeaderText="题目" SortExpression="题目" 
                          HtmlEncode="False" >
                          <ItemStyle Width="420px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="type" HeaderText="题型" >
                          <ItemStyle Width="60px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="answer" HeaderText="参考答案"  HtmlEncode="False" >
                          <ControlStyle Width="40px" />
                          <ItemStyle Width="40px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="huida" HeaderText="回答"  ItemStyle-Width="40px" >
<ItemStyle Width="40px"></ItemStyle>
                      </asp:BoundField>
                      <asp:BoundField DataField="shuoming" HeaderText="解析"  ItemStyle-Width="120px">
<ItemStyle Width="100px"></ItemStyle>
                      </asp:BoundField>
                  </Columns>
              </asp:GridView>          
        </td>
        </tr>
        </table><br />
<asp:Label ID="Labelfankui" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
            
            <asp:HiddenField ID="HFleixing" runat="server" />
            <asp:HiddenField ID="HFxiajitimu" runat="server" />
            <asp:HiddenField ID="HFzhishidianid" runat="server" />
            <asp:HiddenField ID="HFkeyword" runat="server" />
        </td>
    </tr>
    </table>
</asp:Content>

