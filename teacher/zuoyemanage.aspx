<%@ Page Title="��ҵ����" Language="C#" MasterPageFile="~/teacher/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="zuoyemanage.aspx.cs" Inherits="teachermanage_zuoyemanage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table style="width: 100%">
        <tr>
            <td style="width: 260px">
                ѡ��༶</td>
            <td style="width: 260px">
                ѡ����ҵ</td>
            <td style="width: 200px">
                ѡ��ѧ��</td>
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
                    >�ð�ȫ����ҵͳ��</asp:LinkButton>
            </td>
            <td style="width: 260px">
                <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">����ѧ������ҵ</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonyxck" runat="server" 
                    onclick="LinkButtonyxck_Click">����ѧ���鿴��ҵ</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonzdpgkgt" runat="server" 
                    onclick="LinkButtonzdpgkgt_Click">�Զ����ĸ���ҵ�͹��Ⲣ���ܳɼ�</asp:LinkButton>
                <br />
                <asp:LinkButton ID="HyperLinkzypigai" runat="server" 
                    onclick="HyperLinkzypigai_Click">���ĸ���ҵ��������,��������</asp:LinkButton>
                <br />
                <asp:LinkButton ID="HyperLinkzuoyejbxx" runat="server" onclick="HyperLinkzuoyejbxx_Click">��ҵ������Ϣ���޸�</asp:LinkButton>
                <br />
                <asp:LinkButton ID="HyperLinkqtmzytj" runat="server" 
                    onclick="HyperLinkqtmzytj_Click">ȫ��ѧ������ҵ�Ͻ����ɼ���Ϣ</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonzuoyefenxi" runat="server" 
                    onclick="LinkButtonzuoyefenxi_Click">��������ҵƽ���֡��÷��ʵ�</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButtonshanchuzy" runat="server" 
                    onclick="LinkButtonshanchuzy_Click" OnClientClick="return confirm('��ȷ��Ҫɾ������ҵ��?');">�Ӱ༶��ҵ��ɾ������ҵ</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">������ҵ���ø�������</asp:LinkButton>
              </td>
            <td style="width: 200px">
                <asp:LinkButton ID="HyperLinkstuqbzy" runat="server" 
                    onclick="HyperLinkstuqbzy_Click">��ʾ��ѧ��ȫ����ҵ���</asp:LinkButton>
                <br />
                <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click" 
                    ToolTip="���ڲ�����ҵ������ӵ�ѧ��">����ѡ��ҵ���ø���ѧ��</asp:LinkButton>
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
                ���ڴ���,���Ժ�...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

