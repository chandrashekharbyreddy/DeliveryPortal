using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalDL;
using DeliveryPortalEntities;

namespace DeliveryPortal
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateEmployeeList();
                hidPageSize.Value = gridviewEmployee.PageSize.ToString();
            }
        }
        private void PopulateEmployeeList()
        {
            ProjectDL projectDL = new ProjectDL();
            gridviewEmployee.DataSource = projectDL.GetEmployeeModel();
            gridviewEmployee.DataBind();
        }

        protected void btnEmployeeSearch_Click(object sender, EventArgs e)
        {
            gridviewEmployee.DataSource = _projectDL.SearchEmployee(txtEmployeeCode.Text.Trim(), txtEmployeeName.Text.Trim());
            gridviewEmployee.DataBind();
            
        }

        protected void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeDetails.aspx");
        }

       

        protected void BtnDeleteAttribute_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridviewEmployee.Rows)
            {
                var check = row.FindControl("chkSelect") as CheckBox;
                if (check.Checked)
                {
                    int id = Convert.ToInt32(gridviewEmployee.DataKeys[row.RowIndex].Values["EmployeeId"]);
                    _projectDL.DeleteEmployee(id);
                    gridviewEmployee.DataSource = _projectDL.SearchEmployee(txtEmployeeCode.Text.Trim(), txtEmployeeName.Text.Trim());
                }
            }
            gridviewEmployee.DataBind();
            hidPageIndex.Value = gridviewEmployee.PageIndex.ToString();
            
        }

        protected void gridviewEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridviewEmployee.PageIndex = e.NewPageIndex;
            gridviewEmployee.DataSource = _projectDL.SearchEmployee(txtEmployeeCode.Text.Trim(), txtEmployeeName.Text.Trim());
            hidPageIndex.Value = gridviewEmployee.PageIndex.ToString();
            gridviewEmployee.DataBind();
        }

        

    }
}