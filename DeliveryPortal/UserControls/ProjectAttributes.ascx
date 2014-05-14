<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectAttributes.ascx.cs" Inherits="DeliveryPortal.UserControls.ProjectAttributes" %>

<div id="dvProjectAttribute" runat="server" visible="false" style="background-color: white; width: 100%">
    <table style="width: 100%; border: solid 1px black; padding: 4px">
        <tr>
            <td colspan="4">
                <asp:Label CssClass="error" ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="color: white; background-color: #64A9C1; font-size: 8pt">
            <td rowspan="4">
                <div >
                    Week :
                    <asp:Label ID="lblCurrentWeek" runat="server"></asp:Label>
                </div>
            </td>
            <asp:HiddenField ID="hdnWeeklyStatusId" runat="server" />
        </tr>
        <tr>
            <td colspan="4">

                <%--     <asp:GridView DataKeyNames="AttributeId, FlagId" ID="gvAttributes" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAttributes_RowDataBound" CellPadding="4" ForeColor="#333333" Width="30%" Visible="false">

                    <Columns>

                        <asp:BoundField DataField="AttributeName" HeaderText="Attribute" />
                        <asp:ImageField NullDisplayText="" NullImageUrl="~/Images/circle_.png" HeaderText="Status" DataImageUrlField="FlagName" DataImageUrlFormatString="~/images/circle_{0}.png" ControlStyle-Height="15px" ControlStyle-Width="15px">
                            <ControlStyle Height="15px" Width="15px"></ControlStyle>
                        </asp:ImageField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlFlag" runat="server" Style="width: 110px"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
                <asp:DataList ID="dlAttributes" runat="server" DataKeyField="AttributeId" RepeatDirection="Horizontal" Width="100%" OnItemDataBound="dlAttributes_ItemDataBound" GridLines="Both"
                    ForeColor="#333333" Font-Size="10pt" CellPadding="4">

                    <ItemTemplate>
                        <div style="text-align: center; background-color: #7BADBE; color: white; padding: 3px; width: 100%">
                            <asp:Label runat="server" Text='<%# Eval("AttributeName") %>' ID="lblAttributeName"></asp:Label>
                            <asp:Label ID="lblFlagId" Text='<%# Eval("FlagId") %>' runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblIsEditable" Text='<%# Eval("IsLevelEditable") %>' runat="server" Visible="false"></asp:Label>
                        </div>

                        <div style="text-align: center; padding: 3px; color: #333333; width: 100%">
                            <asp:DropDownList ID="ddlFlag" runat="server" style="width: 110px" ></asp:DropDownList>
                            <asp:Image ID="imgFlagStatus" runat="server" ImageUrl='<%# (Eval("FlagName") != null) ? "~/Images/circle_" + Eval("FlagName") + ".png": "~/Images/circle_grey.png" %>' />
                        </div>
                    </ItemTemplate>


                </asp:DataList>

            </td>
        </tr>
        <tr id="trItemHeaders" style="color: white; font-weight: bold; background-color: #7BADBE" runat="server">
            <td style="width: 25%">Latest Updates</td>
            <td style="width: 25%">Risk Items</td>
            <td style="width: 25%">Issues</td>
            <td style="width: 25%">Corrective Actions</td>
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:TextBox Style="width: 90%;" ID="txtLatestUpdates" TextMode="MultiLine" runat="server" Rows="5"></asp:TextBox>
                <asp:Label ID="lblLatestUpdates" runat="server" Text=""></asp:Label>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtRiskItems" Style="width: 90%" TextMode="MultiLine" runat="server" Rows="5"></asp:TextBox>
                <asp:Label ID="lblRiskItems" runat="server"></asp:Label>

            </td>

            <td style="width: 25%">
                <asp:TextBox Style="width: 90%" ID="txtIssues" TextMode="MultiLine" runat="server" Rows="5"></asp:TextBox>
                <asp:Label ID="lblIssues" runat="server"></asp:Label>

            </td>
            <td style="width: 25%">
                <asp:TextBox Style="width: 90%" ID="txtCorrectiveAction" TextMode="MultiLine" runat="server" Rows="5"></asp:TextBox>
                <asp:Label ID="lblCorrectiveAction" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</div>
