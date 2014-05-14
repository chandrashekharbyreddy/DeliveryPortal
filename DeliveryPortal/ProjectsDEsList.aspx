<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectsDEsList.aspx.cs" Inherits="DeliveryPortal.ProjectsDEsList" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="80%" cellspacing="4" cellpadding="4" align="center">
        <tr>
            <th height="20" colspan="2"></th>
        </tr>
        <tr>
            <td class="label">Project Name : </td>
            <td><asp:DropDownList ID="ddlProjects" runat="server" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:GridView ID="grdProjectList" runat="server" AutoGenerateColumns="False" Width="200px" OnRowDataBound="grdProjectList_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlReviewDate" HeaderText="Review Date" HeaderStyle-HorizontalAlign="Center" runat="server" Text='<%#Eval("ReviewDate")%>' ></asp:HyperLink>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input type="hidden" id="hidReviewId" runat="server" value='<%#Eval("DEReviewId")%>'/>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>