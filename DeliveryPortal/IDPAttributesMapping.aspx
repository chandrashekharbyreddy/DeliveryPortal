<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IDPAttributesMapping.aspx.cs" Inherits="DeliveryPortal.IDPAttributesMapping" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="80%" cellspacing="4" cellpadding="4" align="center" border="1">
        <tr>
            <td colspan="2" align="center"><b>IDP Attributes Mappings</b></td>
        </tr>
        <tr height="10px">
            <td colspan="2"></td>
        </tr>
        <tr>
            <td class="label" >Select IDP : </td>
            <td><asp:DropDownList ID="ddlIDPs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDPs_SelectedIndexChanged" ></asp:DropDownList></td>
        </tr>
        <tr align="center" height="20px" >
            <td colspan="2" align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:CheckBoxList ID="chkAttributes" runat="server" ></asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnAssignAttributes" runat="server" Text="Assign Attributes" OnClick="btnAssignAttributes_Click" Visible="false" />
            </td>
        </tr>
    </table>
</asp:Content>
