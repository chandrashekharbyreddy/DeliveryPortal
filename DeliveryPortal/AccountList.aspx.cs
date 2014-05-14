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
    public partial class AccountList : System.Web.UI.Page
    {
        ProjectDL projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAccountGrid();
                hdnPageSige.Value = grdAccount.PageSize.ToString();
                
            }
            //hidPageIndex.Value = grdAccount.PageIndex.ToString();

        }
        protected void PopulateAccountGrid()
        {
            //ProjectDL projectDL = new ProjectDL();
            grdAccount.DataSource = projectDL.GetAccountData();
           
            grdAccount.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //ProjectDL projectDL = new ProjectDL();
            grdAccount.DataSource = projectDL.SearchAccountData(txtAccountName.Text.Trim());
            grdAccount.DataBind();
        }

        protected void btnAddNewAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccountMaster.aspx");
        }

        //protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox checkHeaderBox = (CheckBox)grdAccount.HeaderRow.FindControl("chkboxSelectAll");
        //    foreach (GridViewRow row in grdAccount.Rows) 
        //    {
        //        CheckBox chkBoxrow = (CheckBox)row.FindControl("chkEmp");
        //        if (checkHeaderBox.Checked == true)
        //        {
        //            chkBoxrow.Checked = true;
        //        }
        //        else 
        //        {
        //            chkBoxrow.Checked = false;
        //        }
        //    }

        //}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ProjectDL projectDL = new ProjectDL();
            foreach (GridViewRow row in grdAccount.Rows) 
            {
                var check = row.FindControl("chkEmp") as CheckBox;
                
                if(check.Checked)
                {
                    int accountId=Convert.ToInt32(grdAccount.DataKeys[row.RowIndex].Values["AccountId"]);
                    projectDL.DeleteAccount(accountId);
                }
                
            }
            grdAccount.DataSource = projectDL.SearchAccountData(txtAccountName.Text.Trim());

            grdAccount.DataBind();
            hidPageIndex.Value = grdAccount.PageIndex.ToString();
        }

        protected void grdAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAccount.PageIndex = e.NewPageIndex;
            grdAccount.DataSource = projectDL.SearchAccountData(txtAccountName.Text.Trim());
            hidPageIndex.Value = grdAccount.PageIndex.ToString();
            grdAccount.DataBind();

        }

 
    }
}