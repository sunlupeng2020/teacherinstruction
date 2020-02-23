<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zuoyebjqthz.aspx.cs" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="班级作业汇总" Inherits="teachermanage_zuoyebjqthz" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        班级：<asp:Label ID="Labelbanji" runat="server" Text="Label"></asp:Label>全部作业汇总
        <asp:GridView ID="GridView1" runat="server" 
            DataSourceID="ObjectDataSource1" onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="StuZuoyeHuiZong" TypeName="ZuoyeInfo">
            <SelectParameters>
                <asp:QueryStringParameter Name="kechengid" QueryStringField="kechengid" 
                    Type="Int32" />
                <asp:QueryStringParameter Name="banjiid" QueryStringField="banjiid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
