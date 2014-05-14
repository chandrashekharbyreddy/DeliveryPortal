<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DEDashboard.aspx.cs" Inherits="DeliveryPortal.DEDashboard" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .gridLines tr td, .gridLines th {
            border: 1px solid black;
        }
    </style>
    <table id="tblMain" style="width: 100%" border="1">
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="label">IDP :</td>
                        <td>
                            <asp:DropDownList ID="ddlIDP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIDP_SelectedIndexChanged" Width="100px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Account :</td>
                        <td>
                            <asp:DropDownList ID="ddlAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged" Width="100px"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="label" style="width: 7%">Geo : </td>
            <td style="width: 8%">
                <asp:DropDownList ID="ddlGeo" runat="server" Width="100px"></asp:DropDownList></td>
            <td style="width: 14%">
                <table>
                    <tr>
                        <td class="label">Sector : </td>
                        <td>
                            <asp:DropDownList ID="ddlSector" runat="server" Width="100px" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="label">Sub-sector : </td>
                        <td>
                            <asp:DropDownList ID="ddlSubSector" runat="server" Width="100px">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
            <td class="label" style="width: 7%">Week : </td>
            <td style="width: 8%">
                <asp:DropDownList ID="ddlWeek" runat="server"></asp:DropDownList></td>
            <td class="label" style="width: 12%">RA Projects Only : </td>
            <td style="width: 5%">
                <input id="chkRA" type="checkbox" runat="server" checked="checked" />
            </td>
            <td class="label" style="width: 13%">Strategic Projects Only : </td>
            <td style="width: 5%">
                <input id="chkRAAndStrategicProjects" type="checkbox" runat="server" checked="checked" />
            </td>
            <td style="width: 5%">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="13"></td>
        </tr>
        <tr>
            <td colspan="13" style="text-align: center">

                <div id="dvDashboard">
                    <asp:GridView CssClass="gridLines" ID="grdDEDashboard" runat="server" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowDataBound="grdDEDashboard_RowDataBound" SkinID="dashboardSkin" Width="100%">
                    </asp:GridView>
                </div>

            </td>

        </tr>

    </table>
</asp:Content>
