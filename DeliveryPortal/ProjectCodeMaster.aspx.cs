using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class ProjectCodeMaster : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                PopulateAccounts();
            }
        }
        private void PopulateAccounts()
        {
            ddlAccount.DataSource = _projectDL.GetAccounts();
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataBind();
            ddlAccount.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            ProjectCodesModel projectCode = new ProjectCodesModel();
            projectCode.AccountId = Convert.ToInt32(ddlAccount.SelectedItem.Value);
            projectCode.ProjectCode = txtProjectCode.Text.Trim();
            projectCode.OnshoreHC =Convert.ToInt32(txtOnShoreHC.Text.Trim());
            projectCode.OffShoreHC = Convert.ToInt32(txtOffShoreHC.Text.Trim());
            projectCode.LastUpdateDate = DateTime.Now;
            projectCode.LastUpdatedBy = Common.EmployeeId;
            if (hidProjCode.Value != string.Empty)
            {
                projectCode.ProjectCodeId = int.Parse(hidProjCode.Value);
                _projectDL.UpdateProjectCode(projectCode);
            }
            else 
            {
                int newProjCodeId = _projectDL.InsertProjectCode(projectCode);
                hidProjCode.Value = newProjCodeId.ToString();
            }
            lblMessage.Text = "Data Saved Successfully";
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}