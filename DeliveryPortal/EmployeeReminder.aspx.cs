using DeliveryPortalDL;
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
    public partial class EmployeeReminder : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();


        public int FunctionalityId
        {
            get
            {
                if (ViewState["FunctionalityId"] != null)
                    return Convert.ToInt32(ViewState["FunctionalityId"]);
                else
                    return 0;
            }
            set { ViewState["FunctionalityId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FunctionalityId"] != null)
                {
                    FunctionalityId = int.Parse(Request.QueryString["FunctionalityId"]);
                    // PopulateEmployeeList();
                    //PopulateCheckBox(int.Parse(Request.QueryString["FunctionalityId"]));
                    lblFunctionalityName.Text = _projectDL.GetFunctionalityId(FunctionalityId);
                    PopulateCheckBox(FunctionalityId);
                }
            }
            lblMessage.Text = string.Empty;
        }

     

        //protected void btnEmployeeSearch_Click(object sender, EventArgs e)
        //{
        //    gridviewEmployee.DataSource = _projectDL.SearchEmployee(txtEmployeeCode.Text.Trim(), txtEmployeeName.Text.Trim());
        //    gridviewEmployee.DataBind();
            
        //}
        private void PopulateEmployeeList()
        {
            ProjectDL projectDL = new ProjectDL();
            gridviewEmployee.DataSource = projectDL.GetEmployeeModel();
            gridviewEmployee.DataBind();
        }
        private void PopulateCheckBox(int functionalityId)
        {
            gridviewEmployee.DataSource = _projectDL.GetCheckBox(functionalityId);
           gridviewEmployee.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FunctionalityModel functional = new FunctionalityModel();
            functional.FunctionalityId = FunctionalityId;
            functional.EmployeeIds = new List<int>();
            //emailConfiguration.EmployeeId=
            foreach (GridViewRow row in gridviewEmployee.Rows)
            {
                var check = row.FindControl("chkSelect") as CheckBox;
                if (check.Checked)
                {
                      functional.EmployeeIds.Add(Convert.ToInt32(gridviewEmployee.DataKeys[row.RowIndex].Values["EmployeeId"]));
                }
            }
            _projectDL.SaveEmailConfiguration(functional);
            lblMessage.Text = "Data Saved Successfully.";
            //gridviewEmployee.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmailConfigurationList.aspx");
        }

        
    }
}