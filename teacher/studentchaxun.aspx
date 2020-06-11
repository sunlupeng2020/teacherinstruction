<%@ Page Language="C#" MasterPageFile="BanjiXuesheng.master" AutoEventWireup="true" CodeFile="studentchaxun.aspx.cs" Inherits="manager_studentchaxun" Title="查找学生" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BjXsContentPlaceHolder" Runat="Server">
        学号：<asp:TextBox ID="tbx_xuehao" runat="server"></asp:TextBox>
        <asp:Button ID="btn_xhchaxun" runat="server" onclick="btn_xhchaxun_Click" 
            Text="按学号查询" ValidationGroup="xuehaochaxun" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="tbx_xuehao" ErrorMessage="请输入学号!" 
            ValidationGroup="xuehaochaxun"></asp:RequiredFieldValidator>
        <br />
        姓名：<asp:TextBox ID="tbx_xingming" runat="server"></asp:TextBox>
        <asp:Button ID="btn_xmchaxun" runat="server" onclick="btn_xmchaxun_Click" 
            Text="按姓名查询" ValidationGroup="xingmingchaxun" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="tbx_xingming" ErrorMessage="请输入姓名！" 
            ValidationGroup="xingmingchaxun"></asp:RequiredFieldValidator>
        <br />
        <asp:GridView ID="grv_stu" runat="server" AutoGenerateColumns="False" 
            Width="582px" EmptyDataText="未找到学生信息。">
            <Columns>
                <asp:BoundField DataField="username" HeaderText="学号" />
                <asp:BoundField DataField="xingming" HeaderText="姓名" />
                <asp:BoundField DataField="xingbie" HeaderText="性别" />
                <asp:BoundField DataField="zhuanye" HeaderText="专业" />
                <asp:BoundField DataField="banjiname" HeaderText="班级" />
            </Columns>
        </asp:GridView>
</asp:Content>

