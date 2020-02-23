<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dianmingstudent.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="学生点名详情" Inherits="teachermanage_dianmingstudent" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
 <div>
    
        课程：<asp:Label ID="Labelkc" runat="server"></asp:Label>
&nbsp; 班级：<asp:Label ID="Labelbj" runat="server"></asp:Label>
&nbsp;学生姓名：<asp:Label ID="Labelxm" runat="server"></asp:Label>
&nbsp;学号：<asp:Label ID="Labelun" runat="server"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="dianmingtime" HeaderText="点名时间" 
                    SortExpression="dianmingtime" />
                <asp:BoundField DataField="zhuangtai" HeaderText="状态" 
                    SortExpression="zhuangtai" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>