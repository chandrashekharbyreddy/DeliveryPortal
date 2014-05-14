<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewProjectAttributes.ascx.cs" Inherits="DeliveryPortal.UserControls.ViewProjectAttributes" %>



<tr id="tblProjectAttribute" visible="false" runat="server" >
    <td style="color: white; background-color: #64A9C1; width: 10%; font-size: 8pt">
        <asp:Label ID="lblCurrentWeek" runat="server"></asp:Label>
        <asp:HiddenField ID="hdnWeeklyStatusId" runat="server" />
    </td>


    <td style="width: 40%">

        <asp:DataList ID="dlAttributes" runat="server" DataKeyField="AttributeId" RepeatDirection="Horizontal" Width="100%" GridLines="Both" RepeatColumns="5"
            ForeColor="#333333"  CellPadding="4" OnItemDataBound="dlAttributes_ItemDataBound" RepeatLayout="Flow" CssClass="hidebr">

            <ItemTemplate>
                <div id="dvHeader" runat="server" style="text-align: center; font-size:8pt; font-weight:bold; padding: 3px;   " visible="false" >
                    <asp:Label runat="server" Text='<%# Eval("AttributeName") %>' ID="lblAttributeName"></asp:Label>
                </div>
                <div id="dvItem" runat="server" style="text-align: center; color: #333333; ">
                    <asp:Image ID="imgFlagStatus" runat="server" ImageUrl='<%# (Eval("FlagName") != null) ? "~/Images/circle_" + Eval("FlagName") + ".png": "~/Images/circle_grey.png" %>' />
                </div>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="tdContainer" />
        </asp:DataList>

    </td>

    <td style="width: 50%; vertical-align:top">
        <table style="width: 100%; " >
            <tr id="trItemHeaders" style="font-size:8pt; font-weight:bold; padding: 3px; vertical-align:top " runat="server" visible="false">
     
                <td style="width: 25%">Latest Updates</td>
                <td style="width: 25%">Risk Items</td>
                <td style="width: 25%">Issues</td>
                <td style="width: 25%">Corrective Actions</td>
            </tr>
            <tr id="trItems" runat="server" style="font-size: 7pt; vertical-align:top">
                <td style="width: 25%;  vertical-align:top">

                    <asp:Label ID="lblLatestUpdates" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 25%">

                    <asp:Label ID="lblRiskItems" runat="server"></asp:Label>

                </td>

                <td style="width: 25%">

                    <asp:Label ID="lblIssues" runat="server"></asp:Label>

                </td>
                <td style="width: 25%">

                    <asp:Label ID="lblCorrectiveAction" runat="server"></asp:Label>
                </td>
            </tr>

        </table>
    </td>
</tr>



