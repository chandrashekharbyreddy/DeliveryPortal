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
    public partial class AccountMaster : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGeoLocations();
                //PopulateSectors();
                PopulatePrimarySectors();
                //PopulateSubSectors();

                if (Request.QueryString["AccountId"] != null)
                {
                    hidAccountId.Value = Convert.ToString(Request.QueryString["AccountId"]);
                    GetAccountDetails(int.Parse(Request.QueryString["AccountId"]));
                }

            }

        }

        private void GetAccountDetails(int accountId)
        {
            AccountModel account = _projectDL.GetAccountDetails(accountId);
            txtAccount.Text = account.AccountName;
            if (account.GeoID.HasValue)
            {
                drpGeo.SelectedValue = account.GeoID.Value.ToString();
            }
            drpSubSector.Items.Insert(0, new ListItem("--Select--", ""));
            if (account.SectorID.HasValue)
            {
                drpSector.SelectedValue = account.SectorID.Value.ToString();
                if (!string.IsNullOrEmpty(drpSector.SelectedValue))
                {
                    PopulateSubSectors(int.Parse(drpSector.SelectedValue));
                    if (account.SectorID2.HasValue)
                    {
                        drpSubSector.SelectedValue = account.SectorID2.Value.ToString();
                    }
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            AccountModel account = new AccountModel();
            account.AccountName = txtAccount.Text.Trim();
            account.LastUpdateBy = Common.EmployeeId;
            ProjectDL projectDL = new ProjectDL();
            if (drpGeo.SelectedItem.Value != string.Empty)
            {
                account.GeoID = Convert.ToInt32(drpGeo.SelectedItem.Value);
            }
            if (drpSector.SelectedItem.Value != string.Empty)
            {
                account.SectorID = Convert.ToInt32(drpSector.SelectedItem.Value);
            }
            if (drpSubSector.SelectedItem.Value != string.Empty)
            {
                account.SectorID2 = Convert.ToInt32(drpSubSector.SelectedItem.Value);
            }
            if (hidAccountId.Value != string.Empty)
            {
                account.AccountId = int.Parse(hidAccountId.Value);
                projectDL.UpdateAccountDetails(account);
                lblMessage.Text = "Data Saved Successfully";
            }
            else
            {
                int newAccountId = projectDL.InsertAccountDetails(account);
                hidAccountId.Value = newAccountId.ToString();
                lblMessage.Text = "Data Saved Successfully";
            }
        }
        private void PopulateGeoLocations()
        {
            drpGeo.DataSource = _projectDL.GetGeoLocations();
            drpGeo.DataTextField = "GeoName";
            drpGeo.DataValueField = "GeoID";
            drpGeo.DataBind();
            drpGeo.Items.Insert(0, new ListItem("--Select--", ""));
        }

        //private void PopulateSectors()
        //{
        //    drpSector.DataSource = _projectDL.GetSectors();
        //    drpSector.DataTextField = "SectorName";
        //    drpSector.DataValueField = "SectorID";
        //    drpSector.DataBind();
        //    drpSector.Items.Insert(0, new ListItem("--Select--", ""));
        //}
        //private void PopulateSectors2()
        //{
        //    drpSubSector.DataSource = _projectDL.GetSectors();
        //    drpSubSector.DataTextField = "SectorName";
        //    drpSubSector.DataValueField = "SectorID2";
        //    drpSubSector.DataBind();
        //    drpSubSector.Items.Insert(0, new ListItem("--Select--", ""));
        //}

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountList.aspx");
        }

        protected void drpSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSector.SelectedIndex > 0)
            {
                PopulateSubSectors(int.Parse(drpSector.SelectedValue));
            }
        }
        private void PopulatePrimarySectors()
        {
            //ddlSector.DataSource = _projectDL.GetSectors();
            drpSector.DataSource = _projectDL.GetPrimarySectors();
            drpSector.DataTextField = "SectorName";
            drpSector.DataValueField = "SectorId";
            drpSector.DataBind();
            drpSector.Items.Insert(0, new ListItem("--Select--", ""));
            drpSubSector.Items.Insert(0, new ListItem("--Select--", ""));
        }
        private void PopulateSubSectors(int primarySectorId)
        {
            drpSubSector.DataSource = null;
            drpSubSector.DataSource = _projectDL.GetSubSectors(primarySectorId);
            drpSubSector.DataTextField = "SectorName";
            drpSubSector.DataValueField = "SectorId";
            drpSubSector.DataBind();
            drpSubSector.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
}