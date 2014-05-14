<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DEReport.aspx.cs" Inherits="DeliveryPortal.DEReport" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .gridLines tr td, .gridLines th {
            border: 1px solid black;
        }
    </style>
    <table id="tblMain" style="width: 100%">
        <tr>
            <td />
        </tr>

        <tr>
            <td>
                <asp:GridView CssClass="gridLines" ID="grdAttributeSummary" runat="server" AutoGenerateColumns="false" CellPadding="10" ForeColor="#333333" GridLines="Both" SkinID="dashboardSkin" Width="600" OnRowDataBound="grdAttributeSummary_RowDataBound">

                    <Columns>

                        <asp:TemplateField HeaderText="Attribute">
                            <ItemTemplate>
                                <asp:Label ID="lblAttribute" runat="server" Font-Bold="true" Text='<%#Bind("Attribute") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RAG">
                            <ItemTemplate>
                                <asp:Label ID="lblRAG" runat="server" Text='<%#Bind("RAG") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="400">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td />
        </tr>

        <tr>
            <td style="text-align: center">
                <asp:GridView CssClass="gridLines" ID="grdDEReport" runat="server" AutoGenerateColumns="false" CellPadding="10" ForeColor="#333333" GridLines="Both" SkinID="dashboardSkin" OnRowDataBound="grdDEReport_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr">
                            <ItemTemplate>
                                <asp:Label ID="lblSrNumber" runat="server" Text='<%#Bind("Sr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivery KPI" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="lblAttributeName" runat="server" Font-Bold="true" Text='<%#Bind("[Attribute Name]") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblSampleQuestions" runat="server" Text='<%#Bind("[Sample Questions]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RAG Status" ItemStyle-Width="60px">
                            <ItemTemplate>
                                <asp:Label ID="lblRAGStatus" runat="server" Text='<%#Bind("[RAG Status]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reviewer Observations" ItemStyle-Width="300px">
                            <ItemTemplate>
                                <asp:Label ID="lblReviewerObservations" runat="server" Text='<%#Bind("[Reviewer Observations]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action Points" ItemStyle-Width="300px">
                            <ItemTemplate>
                                <asp:Label ID="lblActionPoints" runat="server" Text='<%#Bind("[Action Points]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSendEmail" runat="server" Text="Send E-Mail" OnClick="btnSendEmail_Click" />
            </td>
            
        </tr>
    </table>
</asp:Content>
