<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectsList.aspx.cs" Inherits="DeliveryPortal.ProjectsList" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--   <script type="text/javascript" language="javascript">
       function CheckAllEmp(Checkbox) {
           var GridVwHeaderChckbox = document.getElementById("chkboxSelectAll");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsById("chkEmp")[0].checked = Checkbox.checked;
            }
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
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

    <table width="100%" cellspacing="4" cellpadding="4" align="center">
        <tr>
            <td class="label" style="width:15%">Project code : </td>
            <td style="width:15%">
                <asp:TextBox ID="txtProjectCode" runat="server"></asp:TextBox></td>
            <td class="label" style="width:15%">Project Name : </td>
            <td style="width:15%">
                <asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox></td>
            <td style="width:40%">
                <asp:Button ID="btnProjectSearch" runat="server" Text="Search" OnClick="btnProjectSearch_Click" />
                <asp:Button ID="btnAddNewProject" runat="server" Text="Add New Project" OnClick="btnAddNewProject_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Button ID="btnDeleteProject" runat="server" Text="Delete Selected" OnClick="btnDeleteProject_Click" OnClientClick="return ConfirmDelete();" />
                <input type="hidden" id="hidPageSize" runat="server" />
                <input type="hidden" id="hidPageIndex" runat="server" />
            <label style="text-align:center">Project List</label></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="grdProjectList" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" DataKeyNames="ProjectId" OnPageIndexChanging="grdProjectList_PageIndexChanging" EmptyDataText="Records Not Found">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectId" runat="server" Text='<% #Bind("ProjectId")%>'>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server"  Onclick="CheckAllEmp(this);"/>
                            </HeaderTemplate>
                            <ItemStyle VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:CheckBoxField --%>
                        <asp:HyperLinkField DataTextField="ProjectCode" HeaderText="Project Code" DataNavigateUrlFormatString="~/ProjectMaster.aspx?ID={0}" DataNavigateUrlFields="ProjectId" ItemStyle-Wrap="true" ItemStyle-Width="150px" SortExpression="ProjectCode" />
                        <asp:HyperLinkField DataTextField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName" ItemStyle-Wrap="true" ItemStyle-Width="200px" DataNavigateUrlFormatString="~/ProjectMaster.aspx?ID={0}" DataNavigateUrlFields="ProjectId"/>
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" ReadOnly="True" SortExpression="StartDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="EndDate" HeaderText="End Date" ReadOnly="True" SortExpression="EndDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="150px" />
                        <asp:TemplateField HeaderText="Is Strategic" SortExpression="IsStrategic" ItemStyle-Width="150px">
                            <ItemTemplate><%# (bool)Eval("IsStrategic") == true ? "Yes" : "No" %></ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </td>

        </tr>
    </table>

</asp:Content>
