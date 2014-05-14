
<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="True" CodeBehind="ReviewMaster.aspx.cs" Inherits="DeliveryPortal.ReviewMaster"  ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/tinymce.min.js"></script>
    <script>
        tinymce.init({
            selector: "textarea",
            plugins: [
               "advlist  lists ",
               "wordcount nonbreaking",
               "textcolor textcolor"
            ],
            width: 450,

            toolbar1: "bold italic underline strikethrough | fontselect forecolor | bullist numlist | undo redo |  removeformat ",

            menubar: false,
            toolbar_items_size: 'small'

        });
    </script>
    <table style="width: 100%;">
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
            </td>

        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label></td>

        </tr>
        <tr>
            <td class="label" style="width: 20%"><span class="error">*</span> Questionnaire Name:</td>
            <td style="width: 80%">
                <asp:DropDownList ID="ddlQuestionnaireId" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqQuestionnaireName" Display="None" runat="server" ControlToValidate="ddlQuestionnaireId" ErrorMessage="Please Select Questionnaire Name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label"style="width: 20%"><span class="error">*</span>Question:</td>
            <td>
                <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine"></asp:TextBox><input type="hidden" id="hidQuestionId" runat="server" />
                  <%--<asp:RequiredFieldValidator ID="reqQuestion"  Display="None"  runat="server" ControlToValidate="txtQuestion" ErrorMessage="Please Enter Question"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="label">Question Guidelines:</td>
            <td>
                <asp:TextBox ID="txtQuestionGuidelines" runat="server" TextMode="MultiLine"></asp:TextBox>
                 
            </td>
        </tr>
        <tr>
           <%-- <td class="label">IsActive:</td>
            <td>
                <asp:CheckBox ID="chkActive" HeaderText="IsDE" SortExpression="IsDE" DataField="IsDE" runat="server" OnCheckedChanged="chkActive_CheckedChanged" />
            </td>--%>
        </tr>
        <tr>
            <td class="label"style="width: 20%"><span class="error">*</span>Roll Up:</td>
            <td>
                <asp:DropDownList ID="drpRollUp" runat="server"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="reqRollUp" Display="None" runat="server" ControlToValidate="drpRollUp" ErrorMessage="Please Select RollUp"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" CausesValidation="true"/></td>
            <td>
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="False" /></td>

        </tr>

    </table>
</asp:Content>

