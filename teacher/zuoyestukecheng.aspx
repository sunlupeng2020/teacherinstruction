<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zuoyestukecheng.aspx.cs"  MasterPageFile="~/teacher/TeacherMasterPage.master" Inherits="teachermanage_zuoyestukecheng" Title="学生作业详情" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
 <div>
         班级：<asp:Label ID="Labelbanji" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;
学号：<asp:Label ID="Labelstuusername" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;
姓名：<asp:Label ID="Labelxingming" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False" DataKeyNames="zuoyeid" 
            onrowdatabound="GridView1_RowDataBound" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" Width="970px">
            <Columns>
                <asp:BoundField />
                <asp:BoundField DataField="作业名称" HeaderText="作业名称" />
                <asp:BoundField DataField="状态" HeaderText="状态" />
                <asp:BoundField DataField="布置时间" HeaderText="布置时间"></asp:BoundField>
                <asp:BoundField DataField="上交期限" HeaderText="上交期限"></asp:BoundField>
                <asp:BoundField DataField="上交时间" HeaderText="上交时间" />
                <asp:BoundField DataField="成绩" HeaderText="成绩" />
                <asp:TemplateField HeaderText="评语">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("评语") %>' 
                            ToolTip='<%# Eval("评语") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("评语") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="200px" />
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" SelectText="题目详情" />
            </Columns>
        </asp:GridView>
 <asp:GridView ID="GridView2" runat="server"
            AutoGenerateColumns="False" onrowdatabound="GridView2_RowDataBound" 
                Width="970px">
            <Columns>
                <asp:BoundField />
                <asp:BoundField DataField="题型" HeaderText="题型" />
                <asp:BoundField DataField="题目" HeaderText="题目" HtmlEncode="False" />
                <asp:BoundField DataField="参考答案" HeaderText="参考答案" HtmlEncode="False" />
                <asp:BoundField DataField="回答" HeaderText="回答" HtmlEncode="False" />
                <asp:HyperLinkField DataNavigateUrlFields="作业文件" HeaderText="作业文件" 
                    Text="作业文件" />
                <asp:BoundField DataField="分值" HeaderText="分值" />
                <asp:BoundField DataField="得分" HeaderText="得分" />
            </Columns>
        </asp:GridView>
       <br />
    </div>
</asp:Content>
