﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="DEVerification.aspx.cs" Inherits="DeliveryPortal.DEVerification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="Scripts/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({
            selector: "textarea",
            plugins: [
               "advlist  lists ",
               "wordcount nonbreaking",
               "textcolor textcolor"
            ],

            toolbar1: "bold italic underline strikethrough | fontselect forecolor",
            toolbar2: "bullist numlist | undo redo |  removeformat ",
            menubar: false,
            statusbar: false,
            toolbar_items_size: 'small',
            titlebar: 'Sample Q'

        });

        $(function () {
            $(".datepicker").datepicker();

        });

    </script>

    <table id="tblMain" style="width: 100%">
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true" />
            </td>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
            </td>            
        </tr>
        <tr>
            <td class="label">Project Name:</td>
            <td><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
            <td class="label">
                Review Date:
            </td>
            <td>
                <asp:Label ID="lblReviewDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="grdDEMaster" runat="server" AutoGenerateColumns="false" DataKeyNames="AttributeId" OnRowDataBound="grdDEMaster_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Attribute" ItemStyle-Width="0" HeaderStyle-Width="0" >
                            <ItemTemplate>
                                <asp:Label ID="lblAttributeId" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attribute" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblAttribute" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sample Questions" ItemStyle-Width="23%">
                            <ItemTemplate>
                                <asp:Label ID="lblSampleQuestions" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RAG Status" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRAGStatus" runat="server" Text="Label"></asp:Label>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observations" ItemStyle-Width="18%">
                            <ItemTemplate>
                                <asp:Label ID="lblObservations" runat="server" Text='<%# Bind("Observations") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recommendations" ItemStyle-Width="17%">
                            <ItemTemplate>
                                <asp:Label ID="lblRecommendations" runat="server" Text='<%# Bind("Recommendations") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Corrective Actions" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblCorrectiveActions" runat="server" Text='<%# Bind("CorrectiveActions") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ETA" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblETA" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqStatus" runat="server" ControlToValidate="ddlStatus" ErrorMessage="Please select Status" InitialValue="" Display="None"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnDone" runat="server" Text="Done" OnClick="btnDone_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
