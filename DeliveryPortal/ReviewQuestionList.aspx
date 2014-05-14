<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReviewQuestionList.aspx.cs" Inherits="DeliveryPortal.ReviewQuestionList" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript" language="javascript">
          function confirmation() {
              // return confirm("Are you sure.?");
              var count = 0;

              var gv = document.getElementById("<%=GridViewReview.ClientID%>");

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
                var GridVwHeaderChckbox = document.getElementById("<%=GridViewReview.ClientID %>");
                var hdnPageSize = document.getElementById("<%=hidPageSize.ClientID %>");
                var hidPageIndex = document.getElementById("<%=hidPageIndex.ClientID%>");
                var PageIndex = parseInt(hidPageIndex.value);
                var PageSize = parseInt(hdnPageSize.value);
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

    <table width="100%" cellspacing="4" cellpadding="4">
        <tr>

                        <td class="label" width="20%">Question: </td>
            <td width="15%">
                            <asp:TextBox ID="txtQuestionName" runat="server"></asp:TextBox></td>
            <td>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
                            <input type="hidden" id="hidPageSize" runat="server" />
                             <input type="hidden" id="hidPageIndex" runat="server" />
                            <asp:Button ID="BtnAddNewQuestion" runat="server" Text="Add New Question" OnClick="BtnAddNewQuestion_Click" /></td>
                        
                    </tr>
          <tr>
            <td colspan="3" class="auto-style1">
                <asp:Button ID="BtnDeleteAttribute" runat="server" align="left" Text="Delete" OnClick="BtnDeleteAttribute_Click" OnClientClick="return confirmation()" />
             <label style="text-align:center">Question List</label>      </td>
                       
                    </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GridViewReview" runat="server" DataKeyNames="QuestionId" AutoGenerateColumns="False" EmptyDataText="Record Not Found" EmptyDataRowStyle-Font-Bold="true" AllowPaging="true" PageSize="5" OnPageIndexChanging="GridViewReview_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" CssClass="checkBox" runat="server" Onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" CssClass="checkBox" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField HeaderText="Question" DataTextField="QuestionDescription" SortExpression="QuestionDescription" DataNavigateUrlFields="QuestionId" DataNavigateUrlFormatString="~/ReviewMaster.aspx?QuestionId={0}" ItemStyle-Wrap="true" ItemStyle-Width="150px" />
                         <asp:TemplateField HeaderText="Questionnaire Name">
                            <ItemTemplate>
                                <asp:Label ID="QuestionnaireName" Text='<%# Eval("QuestionnaireName") %>' runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                         <asp:TemplateField HeaderText="Roll Up">
                            <ItemTemplate>
                                <asp:Label ID="lblAttributes" Text='<%# Bind("AttributeName") %>' runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

    </table>



</asp:Content>
