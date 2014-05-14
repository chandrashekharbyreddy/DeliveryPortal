<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="AccountMaster.aspx.cs" Inherits="DeliveryPortal.AccountMaster" %>
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
            <td><asp:TextBox runat="server" ID="txtAccount"></asp:TextBox><input type="hidden" id="hidAccountId" runat="server" /><asp:RequiredFieldValidator ID="rftxtAccount" runat="server" ControlToValidate="txtAccount" Display="None" ErrorMessage="Please Enter Account Name" ></asp:RequiredFieldValidator></td>
            </tr>
        <tr><td class="label">Geography :</td>
            <td>
                <asp:DropDownList ID="drpGeo" runat="server" ></asp:DropDownList></td></tr>
        <tr><td class="label">Sector :</td>
            <td>
                <asp:DropDownList ID="drpSector" runat="server"  OnSelectedIndexChanged="drpSector_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
        </tr>
        <tr> <td class="label">Sub-sector : </td>
                        <td>
                            <asp:DropDownList ID="drpSubSector" runat="server" >
                                <%--<asp:ListItem Text="--Select--" Value="0"></asp:ListItem>--%>
                            </asp:DropDownList></td></tr>
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
