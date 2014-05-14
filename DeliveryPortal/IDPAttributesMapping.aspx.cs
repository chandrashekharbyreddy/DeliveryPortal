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
    public partial class IDPAttributesMapping : System.Web.UI.Page
    {
        IDPDL _idpDL = new IDPDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateIDPs();
            }
        }

        private void PopulateIDPs()
        {
            ddlIDPs.DataSource = _idpDL.GetIDPs();
            ddlIDPs.DataTextField = "IDPName";
            ddlIDPs.DataValueField = "IDPId";
            ddlIDPs.DataBind();
            ddlIDPs.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void GenerateControls(int idpId)
        {
            chkAttributes.DataSource = _idpDL.GetAttributesList();            
            chkAttributes.DataTextField = "AttributeName";
            chkAttributes.DataValueField = "AttributeId";
            chkAttributes.TextAlign = TextAlign.Right;
            chkAttributes.DataBind();

            List<IDPAttributesMappingsModel> idpAttributes = _idpDL.GetIDPAttributes(int.Parse(ddlIDPs.SelectedValue));

            foreach (ListItem item in chkAttributes.Items)
            {
                if (idpAttributes.Count > 0)
                {
                    if (idpAttributes.Where(i => i.AttributeId == int.Parse(item.Value)).Count() == 1)
                    {
                        item.Selected = true;
                    }                    
                }
            }
        }

        protected void ddlIDPs_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (ddlIDPs.SelectedIndex > 0)
            {                
                GenerateControls(int.Parse(ddlIDPs.SelectedValue));
                btnAssignAttributes.Visible = true;
            }
            else
            {
                chkAttributes.DataSource = null;
                chkAttributes.DataBind();
                btnAssignAttributes.Visible = false;
            }
        }
        

        protected void btnAssignAttributes_Click(object sender, EventArgs e)
        {
            try
            {
                IDPAttributesMappingsModel idpAttribute = null;
                List<IDPAttributesMappingsModel> idpAttributes = new List<IDPAttributesMappingsModel>();
                foreach (ListItem chkAttribute in chkAttributes.Items)
                {
                    if (chkAttribute.Selected)
                    {
                        idpAttribute = new IDPAttributesMappingsModel();
                        idpAttribute.IDPId = int.Parse(ddlIDPs.SelectedValue);
                        idpAttribute.AttributeId = int.Parse(chkAttribute.Value);
                        idpAttributes.Add(idpAttribute);
                    }
                }
                _idpDL.SetIDPAttributes(idpAttributes, int.Parse(ddlIDPs.SelectedValue));

                lblMessage.Text = "Data saved successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error saving data  : " + ex.Message;
            }
        }
    }
}