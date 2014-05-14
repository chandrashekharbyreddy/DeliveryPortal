using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using DeliveryPortalDL;


namespace DeliveryPortal
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmployeeList.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
                EmployeeModel employee = new DeliveryPortalEntities.EmployeeModel();
                employee.EmailId = txtEmailId.Text;
                employee.EmployeeName = txtEmployeeName.Text;
                if (Convert.ToString(ddDesignation.SelectedItem) != "--Select--")
                {
                    employee.Designation = Convert.ToString(ddDesignation.SelectedItem);
                }
                else
                {
                    employee.Designation = null;
                }

                employee.Location = txtLocation.Text;
                employee.WindowsId = txtWindowsId.Text;
                employee.EmployeeCode = txtEmployeeCode.Text;
                ProjectDL projectDL = new ProjectDL();
                //if (Request.QueryString["EmployeeId"] == null)
                //{
                //    projectDL.InsertEmployeeDetails(employee);
                //    Message.Text = "Data Saved Successfully";
                //}
                if (hidEmployeeId.Value !=string.Empty)
                {
                    employee.EmployeeId = int.Parse(hidEmployeeId.Value);
                    projectDL.UpdateEmployeeDetails(employee);
                    Message.Text = "Data Saved Successfully";
                }
                else
                {
                   int newEmployeeId= projectDL.InsertEmployeeDetails(employee);
                    hidEmployeeId.Value = newEmployeeId.ToString();
                    Message.Text = "Data Saved Successfully";
                }
            }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeId"] != null)
                {
                    hidEmployeeId.Value = Convert.ToString(Request.QueryString["EmployeeId"]);
                    GetEmployeeDetails(int.Parse(Request.QueryString["EmployeeId"]));

                }
            }

        }

        private void GetEmployeeDetails(int employeeId)
        {

            EmployeeModel employee = _projectDL.GetEmployeeDetails(employeeId);
            txtEmployeeCode.Text = employee.EmployeeCode;
            txtWindowsId.Text = employee.WindowsId;
            txtEmployeeName.Text = employee.EmployeeName;
            txtEmailId.Text = employee.EmailId;
            txtLocation.Text = employee.Location;
            ddDesignation.SelectedValue = Convert.ToString(employee.Designation);

        }
    }
}
