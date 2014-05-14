<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectMaster.aspx.cs" Inherits="DeliveryPortal.ProjectMaster" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script>

        $(function () {
            $(".datepicker").datepicker();

        });

        function ValidateDate(oSrc, args)
        {
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

    <table width="80%" cellspacing="4" cellpadding="4" align="center">

        <tr>
            <td colspan="4" align="center"><b>Project Details</b></td>
            
        </tr>
        <tr align="center">
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
            <td class="label">Project Code : </td>
            <td>
                <asp:TextBox ID="txtProjectCode" runat="server" MaxLength="50" Enabled="false"></asp:TextBox><input type="hidden" id="hidProjectId" runat="server" />
                <%--<asp:RequiredFieldValidator ID="reqProjectID" Display="None" runat="server" ControlToValidate="txtProjectCode" ErrorMessage="Please enter Project code"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="label"><span class="error">*</span>Account : </td>
            <td>
                <asp:DropDownList ID="drpAccount" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="redAccount" runat="server" Display="None" ControlToValidate="drpAccount" InitialValue="" ErrorMessage="Please select Account"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>Project Name : </td>
            <td>
                <asp:TextBox ID="txtProjectName" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqProjectName" runat="server"  Display="None" ControlToValidate="txtProjectName" ErrorMessage="Please enter Project name"></asp:RequiredFieldValidator>
            </td>
            <td class="label">IDP : </td>
            <td>
                <asp:DropDownList ID="drpIDP" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td class="label"><span class="error">*</span>Engagement Manager : </td>
            <td>
                <asp:DropDownList ID="drpEMs" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqEngagementManager" runat="server"  Display="None" ControlToValidate="drpEMs" ErrorMessage="Please select Engagement Manager"></asp:RequiredFieldValidator>
            </td>
            <td class="label"><span class="error">*</span>Project Manager : </td>
            <td>
                <asp:DropDownList ID="drpPMs" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqProjectManager" runat="server"  Display="None" ControlToValidate="drpPMs" ErrorMessage="Please select Project Manager"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Geography : </td>
            <td>
                <asp:DropDownList ID="drpGeo" runat="server" Enabled="false"></asp:DropDownList></td>
            <td class="label">Sector : </td>
            <td>
                <asp:DropDownList ID="drpSector" runat="server" Enabled="false"></asp:DropDownList></td>
        </tr>
        <tr>
            <td class="label">Start Date : </td>
            <td>
                <asp:TextBox ID="datepickerStartDate" CssClass="datepicker" runat="server" Enabled="false"></asp:TextBox>
                <asp:CustomValidator ID="custValStartDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerStartDate"></asp:CustomValidator>
            </td>
            <td class="label">End Date : </td>
            <td>
                <asp:TextBox ID="datepickerEndDate" CssClass="datepicker" runat="server" Enabled="false"></asp:TextBox>
                <asp:CustomValidator ID="custValEndDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerEndDate"></asp:CustomValidator>
            </td>
            <asp:CompareValidator ID="CompareStartDateEndDate" runat="server" Display="None" ErrorMessage="Start Date should be earlier to End Date " ControlToCompare="datepickerStartDate" ControlToValidate="datepickerEndDate" Operator="GreaterThanEqual"></asp:CompareValidator>
        </tr>
        <tr>
            <td class="label">Methododlogy : </td>
            <td>
                <asp:DropDownList ID="drpMethodology" runat="server" Enabled="false"></asp:DropDownList></td>
            <td class="label">Nature of Work : </td>
            <td>
                <asp:DropDownList ID="drpNow" runat="server" Enabled="false"></asp:DropDownList></td>
        </tr>
        <tr>
            <td class="label">Estimation Basis : </td>
            <td>
                <asp:DropDownList ID="drpEST" runat="server" Enabled="false"></asp:DropDownList>
            </td>
            <td class="label">Last DE Review Date : </td>
            <td>
                <asp:Label ID="lblLastDEReviewDate" runat="server" Text="NA"></asp:Label></td>
        </tr>
        <tr>
            <td class="label">Last IDQA Date : </td>
            <td>
                <asp:Label ID="lblLastIDQADate" runat="server" Text="NA"></asp:Label></td>
            <td class="label">Last SMR Date : </td>
            <td>
                <asp:Label ID="lblLastSMRDate" runat="server" Text="NA"></asp:Label></td>
        </tr>
        <tr>
            <td class="label">Is Strategic : </td>
            <td>
                <asp:CheckBox ID="chkIsStrategic" runat="server" /></td>
           <td></td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td colspan="2" align="Right">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" />
            </td>
            <td colspan="2" align="Left">
                <asp:Button ID="btnProjectList" runat="server" Text="Cancel" OnClick="btnProjectList_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
    

</asp:Content>
