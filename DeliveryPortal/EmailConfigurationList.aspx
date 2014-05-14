<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="EmailConfigurationList.aspx.cs" Inherits="DeliveryPortal.EmailConfigurationList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
       <tr><td><label style="text-align:center">Email Configuration</label>
          </td></tr>
        <tr>
            <td>
                <asp:GridView ID="grdEmailList" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdEmailList_PageIndexChanging">
                    <Columns>
                        <asp:HyperLinkField DataTextField="FunctionalityName" HeaderText="Functionality" SortExpression="FunctionalityName" DataNavigateUrlFields="FunctionalityId" DataNavigateUrlFormatString="EmployeeReminder.aspx?FunctionalityId={0}" />
                        
                        <asp:BoundField DataField="EmailIds" HeaderText="Email ID" SortExpression="EmailIds" />
                    </Columns>
                </asp:GridView>
                
            </td>
           
        </tr>
        
        
    </table>
</asp:Content>
