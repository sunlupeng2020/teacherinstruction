<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/teacher/TeacherMasterPage.master" CodeFile="xiugaitimu.aspx.cs" ValidateRequest="false" Inherits="teachermanage_timuguanli_xiugaitimu" Title="查询、修改题目" EnableTheming="true"  %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td style=" width:240px; vertical-align:top; height:auto;" valign="top">
                                题型<div class="kechengtreeview">
                  <cc2:MyTreeView   ID="TreeViewsource" runat="server" Width="240px" 
                          ShowCheckBoxes="All" ExpandDepth="1" ShowLines="True" >
                  </cc2:MyTreeView>
                  </div>
                                 <br />
                                
                <asp:SqlDataSource ID="SqlDataSourcekecheng" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    
                        SelectCommand="SELECT [kechengid], [kechengname] FROM [tb_Kecheng]">
                </asp:SqlDataSource>
                                
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>"
                    SelectCommand="SELECT [mingcheng] FROM [tb_timuleixing] ORDER BY [tixingid]"></asp:SqlDataSource>
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
              <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                  EmptyDataText="没有相关题目。" OnPageIndexChanging="GridView1_PageIndexChanging" 
                  AutoGenerateColumns="False" Width="740px" DataKeyNames="questionid">
                  <Columns>
                      <asp:BoundField DataField="questionid" HeaderText="questionid" 
                          Visible="False" />
                      <asp:BoundField DataField="题目" HeaderText="题目" SortExpression="题目" 
                          HtmlEncode="False" >
                          <ItemStyle Width="300px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="answer" HeaderText="参考答案"  HtmlEncode="False" >
                          <ControlStyle Width="240px" />
                          <ItemStyle Width="240px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="type" HeaderText="题型" >
                          <ItemStyle Width="80px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="文件">
                          <ItemTemplate>
                              <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("filepath") %>'
                                  Text="下载" Target="_blank"></asp:HyperLink>
                          </ItemTemplate>
                          <ItemStyle Width="50px" />
                      </asp:TemplateField>
                      <asp:BoundField DataField="xingming" HeaderText="提供者" >
                          <ItemStyle Width="60px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="操作">
                          <ItemTemplate>
                              <asp:LinkButton ID="LinkButton3" runat="server" 
                                  CommandArgument='<%# Eval("questionid") %>' onclick="LinkButton3_Click" 
                                  ToolTip="只有题目提供者和课程管理员可以修改题目">修改</asp:LinkButton>
                              <br />
                              <asp:LinkButton ID="LinkButton2" runat="server" 
                                  CommandArgument='<%# Eval("questionid") %>' onclick="LinkButton2_Click" 
                                  OnClientClick="return confirm('您确定要删除该题目吗？');" 
                                  ToolTip="题目提供者和课程管理员可以删除题目，已使用的题目不能删除。">删除</asp:LinkButton>
                          </ItemTemplate>
                          <ItemStyle Width="40px" />
                      </asp:TemplateField>
                      <asp:BoundField DataField="tigongzhe" HeaderText="username" Visible="False" />
                      <asp:BoundField DataField="kechengid" HeaderText="kechengid" Visible="False" />
                  </Columns>
              </asp:GridView>          
        </td>
        </tr>
        </table><br />
<asp:Label ID="Labelfankui" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
            
            <asp:HiddenField ID="HFleixing" runat="server" />
            <asp:HiddenField ID="HFdata" runat="server" />
            <asp:HiddenField ID="HFxdtx" runat="server" />
            <asp:HiddenField ID="HFxdkc" runat="server" />
            <asp:HiddenField ID="HFkechengid" runat="server" />
            <asp:HiddenField ID="HFtixing" runat="server" />
        </td>
    </tr>
    </table>
</asp:Content>