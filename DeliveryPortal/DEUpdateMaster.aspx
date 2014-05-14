<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="DEUpdateMaster.aspx.cs" Inherits="DeliveryPortal.DEUpdateMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script src="Scripts/tinymce.min.js"></script>    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <script>

             $(function () {
                 $(".datepicker").datepicker();

             });
        </script>
        <asp:ValidationSummary runat="server" ID="vsSummary" />
    <table style="width: 100%;">
        <tr><td></td>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label></td>
        </tr>
        <tr>
            <td class="label" colspan="1"><span class="error">*</span> Project Name:</td>
            <td><asp:TextBox runat="server" ID="txtProjectName"></asp:TextBox><input type="hidden" id="hidProjectId" runat="server" /><asp:RequiredFieldValidator ID="rftxtProject" runat="server" ControlToValidate="txtProjectName" Display="None" ErrorMessage="Please Enter Project Name" ></asp:RequiredFieldValidator></td>
            </tr>
                <tr>
            <td class="label" colspan="1"><span class="error">*</span> Actual Review Date</td>
            <td><asp:TextBox runat="server" ID="txtActualReviewDate" CssClass="datepicker"></asp:TextBox><asp:RequiredFieldValidator ID="rfttxtActualReviewDate" runat="server" ControlToValidate="txtActualReviewDate" Display="None" ErrorMessage="Please Enter Actual Review Date" ></asp:RequiredFieldValidator></td>
            </tr>
                        <tr>
            <td class="label" colspan="1"><span class="error">*</span> Schedule Review Date</td>
            <td><asp:TextBox runat="server" ID="txtScheduledReviewDate" CssClass="datepicker"></asp:TextBox><asp:RequiredFieldValidator ID="rfttxtScheduleReviewDate" runat="server" ControlToValidate="txtScheduledReviewDate" Display="None" ErrorMessage="Please Enter Scheduled Review Date" ></asp:RequiredFieldValidator></td>
            </tr>
<%--       <tr>
           
         <td class="label">
                <asp:Button ID="BtnSubmit" runat="server" Text="Save" OnClick="BtnSubmit_Click" />
            </td>
            <td>
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="false"/>
            </td>
           
        </tr>--%>
        </table>>
</asp:Content>
