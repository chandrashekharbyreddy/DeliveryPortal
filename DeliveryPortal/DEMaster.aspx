<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DEMaster.aspx.cs" Inherits="DeliveryPortal.DEMaster" MasterPageFile="~/ChildMaster.Master" ValidateRequest="false" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphNote" runat="server">

    <script src="Scripts/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({
            selector: "textarea",
            plugins: [
               "advlist  lists ",
               "wordcount nonbreaking",
               "textcolor textcolor"
            ],

            toolbar1: "bold italic underline strikethrough | fontselect forecolor",
            toolbar2: "bullist numlist | undo redo |  removeformat ",
            menubar: false,
            toolbar_items_size: 'small',
            titlebar: 'Sample Q'

        });

        $(function () {
            $(".datepicker").datepicker();

        });

        function GenerateReport(reviewId)
        {
            alert(reviewId);
            alert('Data saved successfully');
        }

    </script>

    <table id="tblMain" style="width: 100%">
        <tr>
            <td></td>
            <td colspan="2">
                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Project Name : </td>
            <td>
                  <asp:Label Id="lblProjectName" runat="server" />
                <%--<asp:DropDownList ID="ddlProjects" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqProjects" runat="server" ControlToValidate="ddlProjects" ErrorMessage="Please select Project Name" InitialValue="" Display="None"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="label">Schedule Review Date : </td>
            <td>
                <asp:Label Id="lblScheduleDate" runat="server" />
                
            </td>
            <td class="label">Review Date : </td>
            <td>
                <asp:TextBox ID="datepickerReviewDate" CssClass="datepicker" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvReviewDate" runat="server" ControlToValidate="datepickerReviewDate" ErrorMessage="Please select Review Date" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="6"  align="center" >
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <input type="hidden" id="hidDEReviewId" runat="server" />
                <asp:GridView ID="grdDEMaster" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdDEMaster_RowDataBound" DataKeyNames="AttributeId">
                    <columns>
                        <asp:TemplateField HeaderText="Attribute">
                            <ItemTemplate>
                                <asp:Label ID="lblAttribute" runat="server" Text='<%# Bind("AttributeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sample Questions">
                            <ItemTemplate>
                                <asp:Label ID="lblSampleQuestions" runat="server" Text='<%# Bind("SampleQuestions") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RAG Status">
                            <ItemTemplate>
                                <asp:Label ID="lblFlagId" runat="server" Text='<%# Bind("FlagId") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlRAGStatus" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqRAGStatus" runat="server" ControlToValidate="ddlRAGStatus" ErrorMessage="Please select RAG Status" InitialValue="" Display="None"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observations">
                            <ItemTemplate>
                                <asp:TextBox ID="txtObservations" runat="server" TextMode="MultiLine" Rows="5" Text='<%# Bind("Observations") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recommendations">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRecommendations" runat="server" TextMode="MultiLine" Rows="5" Text='<%# Bind("Recommendations") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
            <td align="center" colspan="3">
                <asp:Button ID="btnGenerateDEReport" runat="server" Text="Generate DE Report" Enabled="false" OnClick="btnGenerateDEReport_Click"  />
            </td>
        </tr>
    </table>
</asp:Content>