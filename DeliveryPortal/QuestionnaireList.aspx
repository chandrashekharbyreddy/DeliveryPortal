<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireList.aspx.cs" Inherits="DeliveryPortal.QuestionnaireList"  MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script>
            function confirmation() {
                // return confirm("Are you sure.?");
                var count = 0;

                var gv = document.getElementById("<%=grdQuestionnaireList.ClientID%>");

                var chk = gv.getElementsByTagName("input");

                for (var i = 0; i < chk.length; i++) {

                    if (chk[i].checked && chk[i].id.indexOf("chkboxSelectAll") == -1) {

                        count++;

                    }

                }

                if (count == 0) {

                    alert("No records to delete.");

                    return false;

                }

                else {

                    return confirm("Do you want to delete " + count + " records.");

                }


            }
            function CheckAllEmp(Checkbox) {
                var GridVwHeaderChckbox = document.getElementById("<%=grdQuestionnaireList.ClientID %>");
                var hidPageSize = document.getElementById("<%=hidPageSize.ClientID %>");
                var hidPageIndex = document.getElementById("<%=hidPageIndex.ClientID%>");
                var PageIndex = parseInt(hidPageIndex.value);
                var PageSize = parseInt(hidPageSize.value);
                if (GridVwHeaderChckbox.rows.length == PageSize + 2) {
                    for (i = 1; i < GridVwHeaderChckbox.rows.length - 1; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                    }
                }
                else if (GridVwHeaderChckbox.rows.length != PageSize + 2 && PageIndex > 0) {
                    for (i = 1; i < GridVwHeaderChckbox.rows.length - 1; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                    }
                }
                else {
                    for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                    }
                }
            }
    </script>

    <table width="100%" cellspacing="4" cellpadding="4" align="center">
        <tr>
            <td class="label" style="width:15%">Questionnaire Name : </td>
            <td style="width:15%">
                <asp:TextBox ID="txtQuestionnaireName" runat="server"></asp:TextBox></td>
            <td class="label" style="width:15%">Questionnaire Type : </td>
            <td style="width:15%">
                <asp:TextBox ID="txtQuestionnairetype" runat="server" style="height: 26px"></asp:TextBox></td>
            <td class="auto-style2">
            <td> 
                <asp:Button ID="btnQuestionnaireSearch" runat="server" Text="Search" OnClick="btnQuestionnaireSearch_Click" />
            </td>  
                 <td>  
                 <asp:Button ID="btnAddNewQuestionnaire" runat="server" Text="Add New Questionnaire" OnClick="btnAddNewQuestionnaire_Click" />
                 </td>
            </td>
        </tr>
        <tr>
            <td colspan="4" dir="ltr">
                <asp:Button ID="btnDeleteQuestionnaire" runat="server" Text="Delete Selected" OnClick="btnDeleteQuestionnaire_Click" OnClientClick="return ConfirmDelete();" />
                <input type="hidden" id="hidPageSize" runat="server" />
                <input type="hidden" id="hidPageIndex" runat="server" />
            <label style="text-align:center" class="auto-style1">Questionnaire List</label></td>
        </tr>
        <tr>
            <td colspan="8">
                <asp:GridView ID="grdQuestionnaireList" runat="server"  DataKeyNames="QuestionnaireId" AutoGenerateColumns="False" AllowPaging="true" PageSize="10"  EmptyDataText="Records Not Found" OnPageIndexChanging="grdQuestionnaireList_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" Onclick="CheckAllEmp(this);"/>
                            </HeaderTemplate>
                            <ItemStyle VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField HeaderText="Questionnaire Name" DataTextField="QuestionnaireName"   SortExpression="QuestionnaireName" DataNavigateUrlFields="QuestionnaireId"  ItemStyle-Wrap="true" ItemStyle-Width="150px" DataNavigateUrlFormatString="~/QuestionnairMaster.aspx?ID={0}" />
                        <asp:TemplateField HeaderText="Questionnaire Type" SortExpression="ReviewTypeName" >
                            <ItemTemplate>
                                <asp:Label ID="ReviewTypeName" Text='<%# Eval("ReviewTypeName") %>' runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IDP" SortExpression="IDPName">
                            <ItemTemplate>
                                <asp:Label ID="IDPName" Text='<%# Eval("IDPName") %>' runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblQuestionnaireId" runat="server" Text='<% #Bind("QuestionnaireId")%>'>></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Status" SortExpression="IsActive" ItemStyle-Width="150px">
                            <ItemTemplate><%# (bool)Eval("IsActive") == true ? "Yes ": "No" %></ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </td>

        </tr>
    </table>

</asp:Content>

