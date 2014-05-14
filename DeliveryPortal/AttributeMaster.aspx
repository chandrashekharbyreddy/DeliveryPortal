<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="AttributeMaster.aspx.cs" Inherits="DeliveryPortal.AttributeInput" ValidateRequest="false" %>

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
            <td class="label">Parent Attribute:</td>
            <td>
                <asp:DropDownList ID="ddlParentAttribute" runat="server"></asp:DropDownList>
                <asp:EntityDataSource ID="AttributeInputEntityData" runat="server" ConnectionString="name=DashboardEntities" DefaultContainerName="DashboardEntities" EnableFlattening="False" EntitySetName="MST_ProjectAttributes" EntityTypeFilter="MST_ProjectAttributes" Select="it.[AttributeId]">
                </asp:EntityDataSource>
            </td>
        </tr>
        <tr>
            <td class="label">Start Date:</td>
            <td>
                <asp:TextBox ID="datepickerStartDate" CssClass="datepicker" runat="server" MaxLength="11"></asp:TextBox>
                <asp:CustomValidator ID="custStartDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerStartDate"></asp:CustomValidator>
            </td>
            <asp:CompareValidator ID="CompareStartDateEndDate" runat="server" Display="None" ErrorMessage="Start Date should be earlier to End Date " ControlToCompare="datepickerStartDate" ControlToValidate="datepickerEndDate" Operator="GreaterThanEqual"></asp:CompareValidator>
        </tr>
        <tr>
            <td class="label">End Date:</td>
            <td>
                <asp:TextBox ID="datepickerEndDate" CssClass="datepicker" runat="server" MaxLength="11"></asp:TextBox>
                <asp:CustomValidator ID="custEndDate" runat="server" ClientValidationFunction="ValidateDate" Display="None" ControlToValidate="datepickerEndDate"></asp:CustomValidator>

            </td>

        </tr>

        <tr>
            <td class="label">Is DE Attribute?:</td>
            <td>
                <asp:CheckBox ID="chkIsDE" HeaderText="IsDE" SortExpression="IsDE" DataField="IsDE" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="label">Sample Questions:</td>
            <td>
                <asp:TextBox ID="txtSampleQuestion" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right">
                <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" /></td>
            <td>
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="False" /></td>

        </tr>

    </table>
</asp:Content>
