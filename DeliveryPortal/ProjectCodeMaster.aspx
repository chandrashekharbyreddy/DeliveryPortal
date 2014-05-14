<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="ProjectCodeMaster.aspx.cs" Inherits="DeliveryPortal.ProjectCodeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary runat="server" ID="vsSummary" />
    <table style="width: 100%;">
    <tr><td></td>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label></td>
        </tr>
        <tr>
            <td class="label" colspan="1"><span class="error">*</span> Account Name :</td>
            <td><asp:DropDownList ID="ddlAccount" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvAccount" ControlToValidate="ddlAccount" runat="server" ErrorMessage="Please Select Account" Display="None"></asp:RequiredFieldValidator>
            </td>
            </tr>
        <tr><td class="label" colspan="1"><span class="error">*</span> Project Code :</td>
            <td><asp:TextBox ID="txtProjectCode" runat="server"></asp:TextBox><input type="hidden" id="hidProjCode" runat="server" /><asp:RequiredFieldValidator ID="rfvProjectCode" runat="server" ControlToValidate="txtProjectCode" ErrorMessage="Please fill Project Code" Display="None"></asp:RequiredFieldValidator></td>
        </tr>
        <tr><td class="label" colspan="1">On-Shore Head Count : </td>
            <td><asp:TextBox ID="txtOnShoreHC" runat="server"></asp:TextBox></td>
        </tr>
        <tr><td class="label" colspan="1">Off-Shore Head Count : </td>
            <td><asp:TextBox ID="txtOffShoreHC" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
         <td class="label">
                <asp:Button ID="BtnSubmit" runat="server" Text="Save" OnClick="BtnSubmit_Click" />
            </td>
            <td>
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="false" />
            </td>
        </tr>
        </table>
</asp:Content>
