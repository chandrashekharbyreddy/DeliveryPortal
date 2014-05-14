<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="DECalendarList.aspx.cs" Inherits="DeliveryPortal.DECalanderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>

        $(function () {
            $(".datepicker").datepicker();

        });

        function ValidateDate(oSrc, args) {
            args.IsValid = checkdate(args.Value);
            return args.IsValid;
        }
    function ConfirmDelete() {
        var count = 0;
        var gv = document.getElementById("<%=grdProjectList.ClientID%>");
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

        function checkdate(input) {
            var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input))
                alert("Invalid Date Format. Please correct and save again.")
            else { //Detailed check for valid date ranges
                var monthfield = input.split("/")[0]
                var dayfield = input.split("/")[1]
                var yearfield = input.split("/")[2]
                var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
                    alert("Invalid Day, Month, or Year range detected. Please correct and save again.")
                else
                    returnval = true
            }
            return returnval
        }


    </script>
    <script type="text/javascript" language="javascript">
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdProjectList.ClientID %>");
           var hdnPageSize = document.getElementById("<%=hidPageSize.ClientID %>");
           var hidPageIndex = document.getElementById("<%=hidPageIndex.ClientID%>");
           var PageIndex = parseInt(hidPageIndex.value);
           var PageSize = parseInt(hdnPageSize.value);
           if (GridVwHeaderChckbox.rows.length == PageSize + 2) {
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
    <table width="100%" cellspacing="4" cellpadding="4">

        <tr>

            <td class="label" style="width: 10%">Project code : </td>
            <td style="width: 15%">
                <asp:TextBox ID="txtProjectCode" runat="server"></asp:TextBox></td>
            <td class="label" style="width: 10%">Project Name : </td>
            <td style="width: 15%">
                <asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox></td>
            <td class="label" style="width: 10%">Date : </td>
            <td style="width: 15%">
                <asp:TextBox ID="datepickerDate" CssClass="datepicker" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="custValDate" runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="datepickerDate"></asp:CustomValidator>
            </td>
            <td style="width: 25%">
                <asp:Button ID="btnProjectSearch" runat="server" Text="Search" OnClick="btnProjectSearch_Click" />
                <input type="hidden" id="hidPageSize" runat="server" />
                 <input type="hidden" id="hidPageIndex" runat="server" />
                <asp:Button ID="btnAddNewProject" runat="server" Text="Add New" OnClick="btnAddNewProject_Click" /></td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:Button ID="btnDeleteProject" runat="server" Text="Delete Selected" OnClick="btnDeleteProject_Click" OnClientClick="return ConfirmDelete()"/>
            <label style="text-align:center">DE Calendar List</label> </td>
        </tr>


        <tr>
            <td colspan="7">
                <asp:GridView ID="grdProjectList" runat="server" AutoGenerateColumns="False" AllowPaging="true"  EmptyDataText="Record Not Found" EmptyDataRowStyle-Font-Bold="true" PageSize="10" DataKeyNames="DEReviewCalendarId" OnPageIndexChanging="grdProjectList_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectId" runat="server" Text='<% #Bind("DEReviewCalendarId")%>'>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" Onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:CheckBoxField --%>
                        <asp:HyperLinkField DataTextField="ProjectCode" HeaderText="Project Code" DataNavigateUrlFormatString="~/DECalendarMaster.aspx?DEReviewCalendarId={0}" DataNavigateUrlFields="DEReviewCalendarId" ItemStyle-Wrap="true" ItemStyle-Width="150px" SortExpression="ProjectCode" />
                        <asp:BoundField DataField="DEReviewCalendarId" HeaderText="DEReviewCalendarId" ReadOnly="True" SortExpression="DEReviewCalendarId" ItemStyle-Wrap="true" ItemStyle-Width="200px" Visible="false" />
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True" SortExpression="ProjectName" ItemStyle-Wrap="true" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="ReviewerName" HeaderText="Reviewer Name" ReadOnly="True" SortExpression="ReviewerName" ItemStyle-Wrap="true" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="ReviewDate" HeaderText="Review Date" ReadOnly="True" SortExpression="ReviewDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="ReviewStatusName" HeaderText="Review Status" ReadOnly="True" SortExpression="ReviewStatusName" ItemStyle-Wrap="true" ItemStyle-Width="150px" />
                        <%--<asp:TemplateField HeaderText="Review Status" SortExpression="ReviewStatus">
                            <ItemTemplate><%# (bool)Eval("IsStrategic") == true ? "Yes" : "No" %></ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </td>

        </tr>
    </table>

</asp:Content>

