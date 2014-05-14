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
    public partial class ProjectAttributes : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        List<AttributeModel> _projectAttributes = new List<AttributeModel>();

        protected void Page_Load(object sender, EventArgs e)
        {
            _projectAttributes = _projectDL.GetProjectAttributesList();
            if (!IsPostBack)
            {
                PopulateProjectAttributes();
                hidPageSize.Value = GridViewAttribute.PageSize.ToString();
            }
        }
        private void PopulateProjectAttributes()
        {
            GridViewAttribute.DataSource = _projectAttributes;
            GridViewAttribute.DataBind();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            PopulateProjectAttributesSearch();
        }
        private void PopulateProjectAttributesSearch()
        {

            ProjectDL projectDL = new ProjectDL();
            GridViewAttribute.DataSource = projectDL.SearchProjectAttributes(txtAttributeName.Text.Trim());
            GridViewAttribute.DataBind();
        }

        protected string GetParentAttributeName(object parentAttributeId)
        {
            string parentAttributeName = string.Empty;
            if (parentAttributeId != null)
            {
                AttributeModel parentAttribute = _projectAttributes.Where(p => p.AttributeId == int.Parse(parentAttributeId.ToString())).FirstOrDefault();
                if (parentAttribute != null)
                {
                    parentAttributeName = parentAttribute.AttributeName;
                }
            }
            return parentAttributeName;
        }

        protected void BtnAddNewAttribute_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttributeMaster.aspx");
        }

        protected void GridViewAttribute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox c = e.Row.FindControl("chkIsDE") as CheckBox;
                Label lbl = e.Row.FindControl("lblIsDE") as Label;
                if (c != null && lbl != null)
                {
                    c.Checked = (lbl.Text.ToUpper() == "TRUE");
                }
            }

        }

        //protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox checkBoxHeader = (CheckBox)GridViewAttribute.HeaderRow.FindControl("chkAll");
        //    foreach (GridViewRow row in GridViewAttribute.Rows)
        //    {
        //        CheckBox chkrow = (CheckBox)row.FindControl("chkSelect");
        //        if (checkBoxHeader.Checked == true)
        //        {
        //            chkrow.Checked = true;
        //        }
        //        else
        //        {
        //            chkrow.Checked = false;
        //        }
                
        //    }
        //}

        protected void BtnDeleteAttribute_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in GridViewAttribute.Rows)
            {
                var check = row.FindControl("chkSelect") as CheckBox;
                if (check.Checked)
                {
                    int id = Convert.ToInt32(GridViewAttribute.DataKeys[row.RowIndex].Values["AttributeId"]);
                    _projectDL.DeleteAttribute(id);
                    GridViewAttribute.DataSource = _projectDL.SearchProjectAttributes(txtAttributeName.Text.Trim());
                }
            }
            GridViewAttribute.DataBind();
            hidPageIndex.Value = GridViewAttribute.PageIndex.ToString();

        }

        protected void GridViewAttribute_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewAttribute.PageIndex = e.NewPageIndex;
            GridViewAttribute.DataSource = _projectDL.SearchProjectAttributes(txtAttributeName.Text.Trim());
            hidPageIndex.Value = GridViewAttribute.PageIndex.ToString();
            GridViewAttribute.DataBind();
        }

    }
}