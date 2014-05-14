<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="DECalendarMaster.aspx.cs" Inherits="DeliveryPortal.DECalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>

        $(function () {
            $(".datepicker").datepicker();

        });

        function ValidateDate(oSrc, args) {
            args.IsValid = checkdate(args.Value);
            return args.IsValid;
        }

        function checkdate(input) {
            var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input))
                alert("Invalid Date Format. Please correct and save again.")
            else { //Detailed check for valid date ranges
                var monthfield = input.split("/")[0]
                var dayfield = input.split("/")[1]
                var yearfield = input.split("/")[2]
                var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
                    alert("Invalid Day, Month, or Year range detected. Please correct and save again.")
                else
                    returnval = true
            }
            return returnval
        }


    </script>
    <table width="80%">
                    <tr>
                        <td align="Left">
                            <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
                        </td>
                    </tr>
                </table>
    <table style="width: 100%;">
        <tr><td></td>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label></td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span> Project:</td>
            <td><asp:DropDownList ID="ddlProject" runat="server"> </asp:DropDownList><input type="hidden" id="hidDEReviewCalendarId" runat="server" /></td>
            <td>
                <asp:RequiredFieldValidator ID="rfProject" runat="server" Display="None" ErrorMessage="Please Select Project." ControlToValidate="ddlProject" InitialValue="" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span> Review Date:</td>
            <td>
                <asp:TextBox ID="datepickerDate" CssClass="datepicker" runat="server" MaxLength="11"></asp:TextBox>
                <asp:CustomValidator ID="custValDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerDate"></asp:CustomValidator>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfReviewDate" runat="server" ErrorMessage="Please Select a Date." Display="None" ControlToValidate="datepickerDate"></asp:RequiredFieldValidator></td>
            
        </tr>
        <tr>
            <td class="label"><span class="error">*</span> Reviewer:</td>
            <td>
                <asp:DropDownList ID="ddlReviewer" runat="server"></asp:DropDownList></td>
            <td>
                <asp:RequiredFieldValidator ID="rfReviewer" runat="server" ErrorMessage="Please Select Reviewer." InitialValue="" ControlToValidate="ddlReviewer" Display="None"></asp:RequiredFieldValidator></td>
            
        </tr>
        <tr>
            <td class="label"><span class="error">*</span> Status:</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
            <td>
                <asp:RequiredFieldValidator ID="rfStatus" runat="server" ErrorMessage="Please Select Status." InitialValue="" ControlToValidate="ddlStatus" Display="None"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
            </td>
            <td>
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
   
</asp:Content>
