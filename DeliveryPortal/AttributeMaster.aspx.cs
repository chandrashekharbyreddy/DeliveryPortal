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
    public partial class AttributeInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["AttributeId"] != null)
                {
                    AttributeId = Convert.ToInt32(Request.QueryString["AttributeId"]);
                    hidAttributeId.Value = AttributeId.ToString();
                    GetProAttribute();
                }
                else
                {
                    PopulateDropDown();
                }
            }

        }
        private void PopulateDropDown()
        {
            ProjectDL projectDL = new ProjectDL();

            ddlParentAttribute.DataSource = projectDL.GetProjectAttributesList();

            ddlParentAttribute.DataTextField = "AttributeName";
            ddlParentAttribute.DataValueField = "AttributeId";
            ddlParentAttribute.DataBind();
            ddlParentAttribute.Items.Insert(0, new ListItem("--Select--", ""));
        }
        private void GetProAttribute()
        {
            ProjectDL projectDL = new ProjectDL();
            AttributeModel attributeModel = new AttributeModel();
            
            attributeModel = projectDL.GetProjectAttribute(AttributeId);
           
            txtAttributeName.Text = attributeModel.AttributeName;
            ddlParentAttribute.DataSource = projectDL.getParentAttributeIdList(AttributeId);
            ddlParentAttribute.DataTextField = "AttributeName";
            ddlParentAttribute.DataValueField = "AttributeId";
            ddlParentAttribute.DataBind();
            ddlParentAttribute.Items.Insert(0, new ListItem("--Select--", ""));
           // string str = projectDL.GetParentAttributeName(AttributeId);
            //ddlParentAttribute.Items.Remove(str);
            if (attributeModel.ParentAttributeId.HasValue)
                ddlParentAttribute.SelectedValue = attributeModel.ParentAttributeId.Value.ToString();
            if (attributeModel.IsDE.HasValue)
            {
                chkIsDE.Checked = attributeModel.IsDE.Value;
            }
            else
            {
                chkIsDE.Checked = false;
            }
            if (attributeModel.EffectiveStartDate.HasValue)
            {
                DateTime startDate = attributeModel.EffectiveStartDate.Value;
                datepickerStartDate.Text = startDate.ToString("MM/dd/yyyy");
            }

            if (attributeModel.EffectiveEndDate.HasValue)
            {
                DateTime endDate = attributeModel.EffectiveEndDate.Value;
                datepickerEndDate.Text = endDate.ToString("MM/dd/yyyy");
            }
            
            txtSampleQuestion.Text = attributeModel.SampleQuestions;
            
        }

        public int AttributeId
        {
            get { return Convert.ToInt32(ViewState["AttributeId"]); }
            set { ViewState["AttributeId"] = value; }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AttributeModel attributeModel = new AttributeModel();
                ProjectDL projectDL = new ProjectDL();
                attributeModel.AttributeName = txtAttributeName.Text.Trim();
                if (ddlParentAttribute.SelectedItem.Value != string.Empty)
                {
                    attributeModel.ParentAttributeId = Convert.ToInt32(ddlParentAttribute.SelectedItem.Value);
                }
                else
                {
                    attributeModel.ParentAttributeId = null;
                }
                attributeModel.AttributeId = AttributeId;
                attributeModel.IsDE = chkIsDE.Checked;
                if (datepickerStartDate.Text.Trim() != string.Empty)
                {
                    attributeModel.EffectiveStartDate=Convert.ToDateTime(datepickerStartDate.Text.Trim());
                }
                if (datepickerEndDate.Text.Trim() != string.Empty)
                {
                    attributeModel.EffectiveEndDate = Convert.ToDateTime(datepickerEndDate.Text.Trim());
                }
                if (txtSampleQuestion.Text.Trim() != string.Empty)
                {
                    attributeModel.SampleQuestions = Convert.ToString(txtSampleQuestion.Text.Trim());
                }
                attributeModel.LastUpdatedDate = DateTime.Now;
                attributeModel.LastUpdateBy = Common.EmployeeId;
                if (hidAttributeId.Value != string.Empty)
                {
                    attributeModel.AttributeId = int.Parse(hidAttributeId.Value);
                    projectDL.UpdateProjectAttribute(attributeModel);
                }
                else
                {
                    int newAttributeId = projectDL.InsertProjectAttribute(attributeModel);
                    hidAttributeId.Value = newAttributeId.ToString();
                }



                lblMessage.Text = "Data Saved Successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Sorry!! We couldnot save the Data." + ex.Message;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttributesList.aspx");
        }

    }
}