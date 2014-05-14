<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="DeliveryPortal.AccountList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script>
            function confirmation()
            {
                // return confirm("Are you sure.?");
                var count = 0;

                var gv = document.getElementById("<%=grdAccount.ClientID%>");

                var chk = gv.getElementsByTagName("input");

                for (var i = 0; i < chk.length; i++) {

                    if (chk[i].checked && chk[i].id.indexOf("chkboxSelectAll") == -1) {

                        count++;

                    }

                }

                if (count == 0) {

                    alert("No records to delete.");

                    return false;

                }

                else {

                    return confirm("Do you want to delete " + count + " records.");

                }


            }
            function CheckAllEmp(Checkbox) {
                var GridVwHeaderChckbox = document.getElementById("<%=grdAccount.ClientID %>");
                var hdnPageSize = document.getElementById("<%=hdnPageSige.ClientID %>");
                var hidPageIndex = document.getElementById("<%=hidPageIndex.ClientID%>");
                var PageIndex = parseInt(hidPageIndex.value);
                var PageSize = parseInt(hdnPageSize.value);
                if (GridVwHeaderChckbox.rows.length == PageSize + 2 ) {
                    for (i = 1; i < GridVwHeaderChckbox.rows.length - 1; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                    }
                }
                else if (GridVwHeaderChckbox.rows.length != PageSize + 2 && PageIndex > 0) {
                    for (i = 1; i < GridVwHeaderChckbox.rows.length - 1; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                    }
                }
                else {
                    for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName('input')[0].checked = Checkbox.checked;
                    }
                }
            }
    </script>
    <table width="100%">
        
        <tr>
            <td class="label" style="width:20%">Account Name:</td>
            <td style="width:15%">
                <asp:TextBox ID="txtAccountName" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            
                <asp:Button ID="btnAddNewAccount" runat="server" Text="Add New Account" OnClick="btnAddNewAccount_Click" />
            
                
                <asp:Button ID="hidButton" Visible="false" OnClick="btnDelete_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnDelete" align="left" Text="Delete" OnClientClick="return confirmation()" OnClick="btnDelete_Click" runat="server" />
             <input type="hidden" runat="server" id="hdnPageSige" />
                <input type="hidden" runat="server" id="hidPageIndex" />
                 <label style="text-align:center">Account List</label>
            </td>
        </tr>
        <tr>
            <td colspan="3"  Width="100%">
                <asp:GridView ID="grdAccount" runat="server" DataKeyNames="AccountId" AutoGenerateColumns="False" EmptyDataText="Record Not Found" EmptyDataRowStyle-Font-Bold="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAccount_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" Onclick="CheckAllEmp(this);"/>
                            </HeaderTemplate>
                            <ItemStyle VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField HeaderText="Account Name" DataTextField="AccountName" SortExpression="AccountName" DataNavigateUrlFields="AccountId" DataNavigateUrlFormatString="~/AccountMaster.aspx?AccountId={0}" />
                        <asp:BoundField HeaderText="Geography" DataField="Geography" SortExpression="Geography" ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField HeaderText="Sector" DataField="SectorName" SortExpression="SectorName" ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField HeaderText="Sub-Sector" DataField="SubSectorName" SortExpression="SubSectorName" ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField HeaderText="Last Updated Date" DataField="LastUpdatedDate" SortExpression="LastUpdatedDate" DataFormatString="{0:dd-MMM-yyyy}" />
                    </Columns>

                </asp:GridView>
            </td>
        </tr>
    </table>    
</asp:Content>
