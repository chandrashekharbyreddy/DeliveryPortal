<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DeliveryPortal.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header" style="display: inline; width: 100%; vertical-align: central">
            <img src="Images\logo.png" width="214" height="60" border="0" style="vertical-align: central; display: inline" />
            <h1 style="font-size: 40px; color: #5A98AE; display: inline; vertical-align: central; padding-left: 200px">Delivery Portal</h1>
            <br />
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div>

            <table style="width: 100%">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:ValidationSummary runat="server" ID="vsErrors" />

                    </td>
                </tr>
                <tr>
                    <td class="label">Windows User Id : </td>
                    <td>
                        <asp:TextBox ID="txtWindowsUserId" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="vcrftxtWindowsUserId" ErrorMessage="Please enter Windows User Id" ControlToValidate="txtWindowsUserId" runat="server" Display="None" />
                        <asp:CustomValidator ID="vccutxtWindowsUserId" ValidateEmptyText="true" Display="None" ControlToValidate="txtWindowsUserId" runat="server" OnServerValidate="vccutxtWindowsUserId_ServerValidate" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 45%">Role : </td>
                    <td style="width: 55%">

                        <asp:DropDownList ID="ddlLevel" runat="server">
                            <asp:ListItem Text="PQL" Value="1" />
                            <asp:ListItem Text="PM" Value="2" />
                            <asp:ListItem Text="EM" Value="3" />
                            <asp:ListItem Text="SMR Reviewer" Value="4" />
                            <asp:ListItem Text="DE Reviewer" Value="5" />
                            <asp:ListItem Text="iDQA Reviewer" Value="6" />
                            <asp:ListItem Text="Leadership" Value="7" />
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button Text="Login" ID="btnLogin" runat="server" OnClick="btnLogin_Click" /></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
