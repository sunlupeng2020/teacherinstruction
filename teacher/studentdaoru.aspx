<%@ Page Language="C#" MasterPageFile="BanjiXuesheng.master" AutoEventWireup="true" CodeFile="studentdaoru.aspx.cs" Inherits="manager_studentdaoru" Title="导入学生信息" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BjXsContentPlaceHolder" Runat="Server">
    <TABLE style="WIDTH: 100%; HEIGHT: 114%" cellSpacing=0 cellPadding=0 border=0><TBODY><TR>
<TD style="TEXT-ALIGN: center"><h4>整体导入学生信息</h4></TD></TR>
    <TR>
<TD style="HEIGHT: 34px; TEXT-ALIGN: center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #FF0000; font-weight: bold">第一步：</span><a 
        href="../daorustumoban.xls">下载学生信息表模板</a>并填写学生信息
        </TD></TR>
    <TR>
<TD style="HEIGHT: 34px; TEXT-ALIGN: center"><span style="color: #FF0000; font-weight: bold">第二步,选择班级：</span> 班级：<asp:DropDownList 
        id="DropDownList3" runat="server" 
            DataSourceID="SqlDataSource3" DataTextField="banjiname" 
            DataValueField="banjiid">
                            </asp:DropDownList>
    &nbsp;</TD></TR>
    <TR>
<TD style="HEIGHT: 34px; TEXT-ALIGN: center">
    <span style="color: #FF0000; font-weight: bold">第三步：</span>班级信息表：<asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="FileUpload1" ErrorMessage="请选择学生信息文件。" 
            ValidationGroup="duquxuesheng" Width="162px" Display="Dynamic"></asp:RequiredFieldValidator>
        </TD></TR>
    <TR>
<TD style="HEIGHT: 34px; TEXT-ALIGN: center">
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="上传文件并导入到数据库" 
            ValidationGroup="duquxuesheng" Width="285px" />
        </TD></TR>
    <TR>
<TD style="HEIGHT: 22px; TEXT-ALIGN: center">
        </TD></TR>
    <TR>
<TD style="HEIGHT: 34px; " class="aligncentertd">
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" 
            SelectCommand="SELECT [banjiid], [banjiname] FROM [tb_banji]">
        </asp:SqlDataSource>
        </TD></TR>
    <TR>
<TD style="HEIGHT: 34px; TEXT-ALIGN: center">
    学生信息表是一个Excel文件，请按照模板中的格式要求填写每个同学的信息，然后再导入。</TD></TR>
    <TR>
<TD style="HEIGHT: 34px; TEXT-ALIGN: center"><asp:Label id="Label1" runat="server" 
        Width="756px" Text="" ForeColor="Red" Height="16px"></asp:Label> 
        </TD></TR>
</TBODY></TABLE>
</asp:Content>

