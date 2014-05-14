<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="IDPAttributeMaster.aspx.cs" Inherits="DeliveryPortal.IDPAttributeMaster" EnableViewState="true" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        //$(document).ready(function () {
        //    var counter = 2;
        //    $("#btnAdd").click(function () {
        //        if (counter > 10) {
        //            alert("Only 10 textboxes allow");
        //            return false;
        //        }
        //        var newTextBoxDiv = $(document.createElement('div')).attr("id", 'TextBoxDiv' + counter);
        //        newTextBoxDiv.html('<input type="text" name="textbox' + counter + '"id"textbox' + counter + '"value="">');
        //        newTextBoxDiv.appendTo("#TextBoxesGroup");
        //        counter++;
        //        alert(counter);
        //        $('#ContentPlaceHolder1_Hidden1').val(counter);
        //        alert($('#ContentPlaceHolder1_Hidden1').val());
        //    });

        //    $("#btnRemove").click(function () {
        //        if (counter == 1) {
        //            alert("No more textbox to remove");
        //            return false;
        //        }
        //        counter--;
        //        $('#ContentPlaceHolder1_Hidden1').val("counter");
        //        $("#TextBoxDiv" + counter).remove();
        //    });
           
        //});
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


            <td class="label" style="width: 20%"><span class="error">*</span> Attribute Name:</td>
            <td style="width: 80%">
                <asp:TextBox ID="txtAttributeName" runat="server" MaxLength="100"></asp:TextBox><input type="hidden" id="hidAttributeId" runat="server" />
                <asp:RequiredFieldValidator ID="reqAttributeName" Display="None" runat="server" ControlToValidate="txtAttributeName" ErrorMessage="Please enter Attribute Name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>Attribute Start Date:</td>
            <td>
                <asp:TextBox ID="datepickerStartDate" CssClass="datepicker" runat="server" MaxLength="11"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqAttributeStartDate" runat="server" Display="None" ControlToValidate="datepickerStartDate" ErrorMessage="Please Select Attribute Start Date"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="custStartDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerStartDate"></asp:CustomValidator>
            </td>
            <asp:CompareValidator ID="CompareStartDateEndDate" runat="server" Display="None" ErrorMessage="Start Date should be earlier to End Date " ControlToCompare="datepickerStartDate" ControlToValidate="datepickerEndDate" Operator="GreaterThanEqual"></asp:CompareValidator>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>Attribute End Date:</td>
            <td>
                <asp:TextBox ID="datepickerEndDate" CssClass="datepicker" runat="server" MaxLength="11"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqAttributeEndDate" runat="server" Display="None" ControlToValidate="datepickerEndDate" ErrorMessage="Please Select Attribute End Date"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="custEndDate" runat="server" ClientValidationFunction="ValidateDate" Display="None" ControlToValidate="datepickerEndDate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>Attribute Type:</td>
            <td>
                <asp:DropDownList ID="drpAttributeType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAttributeType_SelectedIndexChanged" ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqAttributeType" runat="server" Display="None" ControlToValidate="drpAttributeType" ErrorMessage="Please Select Attribute Type"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trAttriVal" runat="server" height="40px">
            <td class="label"><span class="error">*</span>Attribute Values:</td>
            <td>
                <asp:TextBox ID="txtAttributeValues" runat="server" style="width: 300px;"></asp:TextBox><br />Please enter comma(,) separated attribute values for Picklist, CheckBox & Radio button.
                <asp:RequiredFieldValidator ID="reqAttributeValues" runat="server" ControlToValidate="txtAttributeValues" Display="None" ErrorMessage="Please enter Attribute Values"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
       

        <tr>
            <td align="right">                

                <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" /></td>
            <td>
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="false"/></td>

        </tr>

    </table>
</asp:Content>
