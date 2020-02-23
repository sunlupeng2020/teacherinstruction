<%@ Page Title="测试管理" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ceshidefault.aspx.cs" Inherits="teachermanage_ceshidefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 980px">
        <tr>
            <td style="width: 245px">
                选择班级</td>
            <td style="width: 245px">
                选择测试名称</td>
            <td style="width: 245px">
                选择学生</td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="ListBoxbj" runat="server" AutoPostBack="True" 
                  DataTextField="banjiname" DataValueField="banjiid" Rows="8" Width="242px" 
                    onselectedindexchanged="ListBoxbj_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>
                <asp:ListBox ID="ListBoxcs" runat="server" 
                    DataTextField="ceshiname" DataValueField="shijuanid" AutoPostBack="True" 
                    onselectedindexchanged="ListBoxcs_SelectedIndexChanged" Rows="8" Width="242px"></asp:ListBox>

            </td>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" 
                    DataTextField="stu" DataValueField="username" Rows="8" Width="242px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">班级测试成绩汇总</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonqtzice" runat="server" 
                    onclick="LinkButtonqtzice_Click">全体学生自测情况</asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton ID="LinkButtonqtxscj" runat="server" 
                    onclick="LinkButtonqtxscj_Click">显示该测试学生成绩</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">自动批改该测试全部客观题并汇总成绩</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">测试详细信息及修改</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonsjfx" runat="server" 
                    onclick="LinkButtonsjfx_Click">试卷分析(最高分、最低分、得分率等)</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonyxzt" runat="server" 
                    onclick="LinkButtonyxzt_Click">允许学生进行测试</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonyxchk" runat="server" 
                    onclick="LinkButtonyxchk_Click">允许学生查看测试结果</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtoncsgl" runat="server" 
                    onclick="LinkButtoncsgl_Click">在线实时测试管理</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonpigai" runat="server" 
                    onclick="LinkButtonpigai_Click">手工批改主观题</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton7" runat="server" onclick="LinkButton7_Click" OnClientClick="return confirm('您确定要删除该测试吗？');">删除该测试</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonqianyi" runat="server" 
                    onclick="LinkButtonqianyi_Click">将该测试项目应用于其他班级</asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">显示该学生各次测试信息</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonstucstm" runat="server" 
                    onclick="LinkButtonstucstm_Click"> 显示学生测试题目详情</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click">学生自测情况统计</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton8" runat="server" onclick="LinkButton8_Click" 
                    ToolTip="针对组卷后新添加的学生">对该学生所选测试项目组卷</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
  
    </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>


