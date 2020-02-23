<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ceshistutimu.aspx.cs" Inherits="teachermanage_ceshistutimu" MasterPageFile="~/teacher/TeacherMasterPage.master" Title="学生测试题目详情" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div>
        班级：<asp:Label ID="Labelbj" runat="server" Text="Label"></asp:Label>
        测试名称：<asp:Label ID="Labelcsmc" runat="server" Text="Label"></asp:Label>
        学生姓名：<asp:Label ID="Labelxm" runat="server" Text="Label"></asp:Label>
        &nbsp;学号：<asp:Label ID="Labelun" runat="server" Text="Label"></asp:Label>
   <asp:SqlDataSource id="SqlDataSource7" runat="server" SelectCommand="SELECT tb_studentkaoshiti.[shitiid], tb_studentkaoshiti.[fenzhi], [tb_studentkaoshiti].[answer], tb_studentkaoshiti.[defen], tb_studentkaoshiti.[timuhao], tb_studentkaoshiti.[filepath],tb_tiku.timu,tb_tiku.answer,tb_tiku.filepath,tb_tiku.[type]  FROM [tb_studentkaoshiti] inner join tb_tiku on tb_tiku.questionid=tb_studentkaoshiti.questionid  WHERE ((tb_studentkaoshiti.[studentusername] = @studentusername) AND (tb_studentkaoshiti.[shijuanid] = @shijuanid)) ORDER BY [timuhao]" ConnectionString="<%$ ConnectionStrings:kecheng2012ConnectionString %>" UpdateCommand="UPDATE [tb_studentkaoshiti] SET [defen] = @defen WHERE [shitiid] = @original_shitiid" InsertCommand="INSERT INTO [tb_studentkaoshiti] ([fenzhi], [answer], [defen], [timuhao], [filepath]) VALUES (@fenzhi, @answer, @defen, @timuhao, @filepath)" DeleteCommand="DELETE FROM [tb_studentkaoshiti] WHERE [shitiid] = @original_shitiid AND [fenzhi] = @original_fenzhi AND [answer] = @original_answer AND [defen] = @original_defen AND [timuhao] = @original_timuhao AND [filepath] = @original_filepath" ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="studentusername" 
                            QueryStringField="studentusername" Type="String" />
                        <asp:QueryStringParameter Name="shijuanid" QueryStringField="shijuanid" 
                            Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="original_shitiid" Type="Int32" />
                        <asp:Parameter Name="original_fenzhi" Type="Int32" />
                        <asp:Parameter Name="original_answer" Type="String" />
                        <asp:Parameter Name="original_defen" Type="Int32" />
                        <asp:Parameter Name="original_timuhao" Type="Int32" />
                        <asp:Parameter Name="original_filepath" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="defen" Type="Int32" />
                        <asp:Parameter Name="original_shitiid" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="fenzhi" Type="Int32" />
                        <asp:Parameter Name="answer" Type="String" />
                        <asp:Parameter Name="defen" Type="Int32" />
                        <asp:Parameter Name="timuhao" Type="Int32" />
                        <asp:Parameter Name="filepath" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>
        <br />
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="shitiid" DataSourceID="SqlDataSource7" 
                        OnRowUpdated="GridView3_RowUpdated" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="shitiid" HeaderText="shitiid" InsertVisible="False" 
                                ReadOnly="True" SortExpression="shitiid" Visible="False" />
                            <asp:BoundField DataField="timuhao" HeaderText="题号" ReadOnly="True" 
                                SortExpression="timuhao" >
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="type" HeaderText="题型" ReadOnly="True" 
                                SortExpression="type" >
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="timu" HeaderText="题目" HtmlEncode="False" 
                                ReadOnly="True" SortExpression="timu">
                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="filepath1" 
                                DataNavigateUrlFormatString="{0}" HeaderText="题目文件" Text="下载" >
                                <ItemStyle Width="40px" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="answer1" HeaderText="参考答案" ReadOnly="True" 
                                SortExpression="answer1">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="answer" HeaderText="回答" ReadOnly="True" 
                                SortExpression="answer">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="filepath" 
                                DataNavigateUrlFormatString="{0}" HeaderText="学生文件" Text="下载" >
                                <ItemStyle Width="40px" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="fenzhi" HeaderText="分值" ReadOnly="True" 
                                SortExpression="fenzhi" >
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="defen" HeaderText="得分" SortExpression="defen" >
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:CommandField EditText="批改" HeaderText="批改" ShowEditButton="True" >
                                <ItemStyle Width="60px" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
    
    </div>
</asp:Content>
