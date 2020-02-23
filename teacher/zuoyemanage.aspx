<%@ Page Title="作业管理" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zuoyemanage.aspx.cs" Inherits="teachermanage_zuoyemanage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td style="width: 260px">
                选择班级</td>
            <td style="width: 260px">
                选择作业</td>
            <td style="width: 200px">
                选择学生</td>
        </tr>
        <tr>
            <td style="width: 260px">
                <asp:ListBox ID="ListBoxbanji" runat="server" AutoPostBack="True" 
                    DataTextField="banjiname" 
                    DataValueField="banjiid" Rows="10" Width="260px" 
                    onselectedindexchanged="ListBox2_SelectedIndexChanged" 
                    ondatabound="ListBoxbanji_DataBound"></asp:ListBox>
            </td>
            <td style="width: 260px">
                <asp:ListBox ID="ListBoxzuoye" runat="server" 
                    DataTextField="zuoyename" 
                    DataValueField="zuoyeid" Rows="10" Width="260px" AutoPostBack="True" 
                    onselectedindexchanged="ListBox3_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>
                <asp:ListBox ID="ListBoxstu" runat="server" 
                    DataTextField="stu" DataValueField="username" Rows="10" Width="229px" 
                    style="margin-left: 0px"></asp:ListBox>
            </td>
        </tr>
          <tr>
            <td style="width: 260px">
                <asp:LinkButton ID="HyperLinkqtzytj" runat="server" onclick="HyperLinkqtzytj_Click"
                    >该班全部作业统计</asp:LinkButton>
            </td>
            <td style="width: 260px">
                <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">允许学生做作业</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonyxck" runat="server" 
                    onclick="LinkButtonyxck_Click">允许学生查看作业</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonzdpgkgt" runat="server" 
                    onclick="LinkButtonzdpgkgt_Click">自动批改该作业客观题并汇总成绩</asp:LinkButton>
                <br />
                <asp:LinkButton ID="HyperLinkzypigai" runat="server" 
                    onclick="HyperLinkzypigai_Click">批改该作业的主观题,设置评语</asp:LinkButton>
                <br />
                <asp:LinkButton ID="HyperLinkzuoyejbxx" runat="server" onclick="HyperLinkzuoyejbxx_Click">作业基本信息及修改</asp:LinkButton>
                <br />
                <asp:LinkButton ID="HyperLinkqtmzytj" runat="server" 
                    onclick="HyperLinkqtmzytj_Click">全体学生该作业上交、成绩信息</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonzuoyefenxi" runat="server" 
                    onclick="LinkButtonzuoyefenxi_Click">分析该作业平均分、得分率等</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonshanchuzy" runat="server" 
                    onclick="LinkButtonshanchuzy_Click" OnClientClick="return confirm('您确定要删除该作业吗?');">从班级作业中删除该作业</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">将该作业布置给其他班</asp:LinkButton>
              </td>
            <td style="width: 200px">
                <asp:LinkButton ID="HyperLinkstuqbzy" runat="server" 
                    onclick="HyperLinkstuqbzy_Click">显示该学生全部作业情况</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click" 
                    ToolTip="用于布置作业后新添加的学生">将所选作业布置给该学生</asp:LinkButton>
                <br />
              </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
      </ContentTemplate>
    </asp:UpdatePanel> 
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
            AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <asp:Image ID="Image1" runat="server" Height="34px" 
                    ImageUrl="~/images/jd002.gif" Width="36px" />
                <br />
                正在处理,请稍候...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

