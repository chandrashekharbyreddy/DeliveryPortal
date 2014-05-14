<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="DEUpdateList.aspx.cs" Inherits="DeliveryPortal.DEUpdateList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="label">Project Name:</td>
            
            <td><asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox></td>
            
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="grdDEUpdateList" runat="server" AutoGenerateColumns="False" EmptyDataText="Record Not Found" EmptyDataRowStyle-Font-Bold="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdDEUpdateList_PageIndexChanging" OnRowDataBound="grdDEUpdateList_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Project Name" DataField="ProjectName" SortExpression="ProjectName"/>
                        <asp:BoundField HeaderText="Schedule Review Date" DataField="ScheduleDate" SortExpression="ScheduleDate" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField HeaderText="Review Date" DataField="ReviewDate" SortExpression="ReviewDate" DataFormatString="{0:dd-MMM-yyyy}" />
                        
                        <asp:HyperLinkField HeaderText="Action" DataNavigateUrlFields="DEReviewId, DEReviewCalendarId" DataNavigateUrlFormatString="~/DEMaster.aspx?DEReviewId={0}&DEReviewCalendarId={1}" />
                    </Columns>
                </asp:GridView>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
