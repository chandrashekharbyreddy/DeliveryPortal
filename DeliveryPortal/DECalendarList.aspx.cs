




using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalEntities;
using DeliveryPortalDL;
using System.Data;

namespace DeliveryPortal
{
    public partial class DECalanderList : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdProjectList.DataSource = _projectDL.GetDEModel();
                grdProjectList.DataBind();
                hidPageSize.Value = grdProjectList.PageSize.ToString();
            }
        }
        //protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    //CheckBox ChkBoxHeader = (CheckBox).HeaderRow.FindControl("chkboxSelectAll");
        //    CheckBox ChkBoxHeader = (CheckBox)grdProjectList.HeaderRow.FindControl("chkboxSelectAll");
        //    foreach (GridViewRow row in grdProjectList.Rows)
        //    {
        //        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
        //        if (ChkBoxHeader.Checked == true)
        //        {
        //            ChkBoxRows.Checked = true;
        //        }
        //        else
        //        {
        //            ChkBoxRows.Checked = false;
        //        }
        //    }
        //}

        protected void btnDeleteProject_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdProjectList.Rows)
            {
                var check = row.FindControl("chkEmp") as CheckBox;
                if (check.Checked)
                {
                    int id = Convert.ToInt32(grdProjectList.DataKeys[row.RowIndex].Values["DEReviewCalendarId"]);
                    _projectDL.DeleteDECalendar(id);
                    grdProjectList.DataSource = _projectDL.GetDEModel();
                    
                }
            }
            grdProjectList.DataBind();
            hidPageIndex.Value = grdProjectList.PageIndex.ToString();
            txtProjectCode.Text = string.Empty;
            txtProjectName.Text = string.Empty;
            datepickerDate.Text = string.Empty;
        }

        protected void btnProjectSearch_Click(object sender, EventArgs e)
        {
            DateTime dateValue;
            if (DateTime.TryParse(datepickerDate.Text, out dateValue))
            {
                // DateTime dt = Convert.ToDateTime(datepickerDate.Text);
                grdProjectList.DataSource = _projectDL.SearchDECalendar(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim(), dateValue);
                grdProjectList.DataBind();
            }
            else
            {
                grdProjectList.DataSource = _projectDL.SearchDECalendar(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim(), null);
                grdProjectList.DataBind();
            }
        }

        protected void btnAddNewProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("DECalendarMaster.aspx");
        }
        protected void grdProjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProjectList.PageIndex = e.NewPageIndex;
            grdProjectList.DataSource = _projectDL.SearchDECalendar(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim(),null);
            hidPageIndex.Value = grdProjectList.PageIndex.ToString();
            grdProjectList.DataBind();
        }

    }
}
