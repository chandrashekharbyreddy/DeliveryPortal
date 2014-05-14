<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="DeliveryPortal.ProjectDetails" %>
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
    <table style="width: 100%;">
        <tr>
            <td colspan="4" align="center"><b>Project Details</b></td>  
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
            </td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>IDP:</td>
            <td>
                <asp:DropDownList ID="ddlIDP" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvIDP" runat="server" ErrorMessage="Please select IDP" ControlToValidate="ddlIDP" Display="None"></asp:RequiredFieldValidator>
            </td>
           <td class="label"><span class="error">*</span>Account:</td>
           <td>
               <asp:DropDownList ID="ddlAccount" runat="server"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="rfvAccount" runat="server" ErrorMessage="Please select Account" ControlToValidate="ddlAccount" Display="None"></asp:RequiredFieldValidator>
           </td>
        </tr>
        <tr> 
            <td class="label"><span class="error">*</span>Project Logical Name:</td><input type="hidden" id="hidProjectId" runat="server" />
            <td><asp:DropDownList ID="ddlProjectName" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ErrorMessage="Please select Project Logical Name" ControlToValidate="ddlProjectName" Display="None"></asp:RequiredFieldValidator>
            </td>
            <td class="label"><span class="error">*</span>Project Code(s):</td>
            <td>
            <asp:ListBox ID="lbProjectCode" runat="server" SelectionMode="Multiple" Width="153px">
                <asp:ListItem>5435564</asp:ListItem>
                <asp:ListItem Selected="True">7686887</asp:ListItem>
                <asp:ListItem>4561234</asp:ListItem>
                <asp:ListItem Selected="True">7894561</asp:ListItem>
                <asp:ListItem>1234567</asp:ListItem>
                </asp:ListBox>
                <asp:RequiredFieldValidator ID="rfvProjectCode" runat="server" ErrorMessage="Please select Project Code(s) " ControlToValidate="lbProjectCode" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>Engagement Manager:</td>
            <td><asp:DropDownList ID="ddlEM" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvEM" runat="server" ErrorMessage="Please select Engagement Manager" ControlToValidate="ddlEM" Display="None"></asp:RequiredFieldValidator>
            </td>
            <td class="label"><span class="error">*</span>Project Manager:</td>
            <td><asp:DropDownList ID="ddlPM" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPM" runat="server" ErrorMessage="Please select Project Manager" ControlToValidate="ddlPM" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>Start Date:</td>
            <td><asp:TextBox ID="datepickerStartDate" CssClass="datepicker" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="cvStartDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerStartDate"></asp:CustomValidator></td>
            <td class="label"><span class="error">*</span>End Date:</td>
            <td><asp:TextBox ID="datepickerEndDate" CssClass="datepicker" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="cvEndDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerEndDate"></asp:CustomValidator>
            </td>
        </tr>
        <tr><td class="label">Is Strategic:</td>
            <td>
                <asp:CheckBox ID="chkIsStrategic" runat="server" /></td>
            <td class="label">Onshore HC:</td>
            <td>
            <asp:Label ID="lblOnshoreHC" runat="server" ></asp:Label></td>
        </tr>
        <tr>
            <td class="label">Offshore HC:</td>
            <td>
            <asp:Label ID="lblOffshoreHC" runat="server" ></asp:Label></td>
            <td class="label">Total HC:</td>
            <td>
            <asp:Label ID="lblTotalHC" runat="server" ></asp:Label></td>
        </tr>
       <tr>
            <td colspan="2" align="Right">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" />
            </td>
            <td colspan="2" align="Left">
                <asp:Button ID="btnProjectList" runat="server" Text="Cancel" OnClick="btnProjectList_Click" CausesValidation="false" />
            </td>
        </tr>
        <%--<tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>--%>
    </table>
</asp:Content>
