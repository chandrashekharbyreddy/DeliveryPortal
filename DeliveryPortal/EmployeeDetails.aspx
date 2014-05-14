<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="DeliveryPortal.EmployeeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="80%">
        <tr>
            <td align="Left">
                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
            </td>
        </tr>
    </table>
    <table id="tblEmployee" style="width:100%">
        <tr>
            <td colspan="4" style="text-align:center"><strong>Employee Details</strong></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center"><asp:Label ID="Message" runat="server" CssClass="error"></asp:Label></td>
        </tr>
        <tr>
            <td class="label">
                <span class="error">*</span> Employee Name : 
            </td>
            <td>
                <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="100"></asp:TextBox><input type="hidden" id="hidEmployeeId" runat="server" />
                <asp:RequiredFieldValidator ID="rfEmployeeName" Display="None" ControlToValidate="txtEmployeeName" runat="server" ErrorMessage="Please Enter Employee Name"></asp:RequiredFieldValidator>
            </td>
            <td class="label">Designation :
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddDesignation">
                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                    <asp:ListItem Text="Software Engineer"></asp:ListItem>
                    <asp:ListItem Text="Senior Software Engineer"></asp:ListItem>
                    <asp:ListItem Text="Consultant"></asp:ListItem>
                    <asp:ListItem Text="Senior Consultant"></asp:ListItem>
                    <asp:ListItem Text="Manager"></asp:ListItem>
                    <asp:ListItem Text="Senior Manager"></asp:ListItem>
                    <asp:ListItem Text="Associate Director"></asp:ListItem>
                    <asp:ListItem Text="Director"></asp:ListItem>
                    <asp:ListItem Text="Vice President"></asp:ListItem>
                    <asp:ListItem Text="Senior Vice President"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label">Location :
            </td>
            <td>
                <asp:TextBox ID="txtLocation" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            <td class="label">Email Id : 
            </td>
            <td>
                <asp:TextBox ID="txtEmailId" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <span class="error">*</span>  Windows Id : 
            </td>
            <td>
                <asp:TextBox ID="txtWindowsId" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rftxtWindowsId" Display="None" runat="server" ControlToValidate="txtWindowsId" ErrorMessage="Please Enter Windows id"></asp:RequiredFieldValidator>
            </td>
            <td class="label">
                 Employee Code: 
            </td>
            <td>
                <asp:TextBox ID="txtEmployeeCode" runat="server"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td></td>
            <td align="right">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" />
            </td>
            <td>
                <asp:Button ID="btnReset" runat="server" Text="Cancel" OnClick="btnReset_Click" CausesValidation="false" />
            </td>
        </tr>



    </table>
</asp:Content>
