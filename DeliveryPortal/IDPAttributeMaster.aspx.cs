using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class IDPAttributeMaster : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        int controlCounter = 0;

        public int AttributeId
        {
            get { return Convert.ToInt32(ViewState["AttributeId"]); }
            set { ViewState["AttributeId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["AttributeId"] != null)
                {
                    AttributeId = Convert.ToInt32(Request.QueryString["AttributeId"]);
                    hidAttributeId.Value = AttributeId.ToString();
                    getIDPAttribute();//call local get data function
                }
                else
                {
                    PopulateDropDown();
                    // populate dropdown
                }
            }
        }
        private void PopulateDropDown()
        {
            //ProjectDL projectDL = new ProjectDL();

            drpAttributeType.DataSource = _projectDL.GetIDPAttributesTypeList();

            drpAttributeType.DataTextField = "AttributeTypeName";
            drpAttributeType.DataValueField = "AttributeTypeId";
            drpAttributeType.DataBind();
            drpAttributeType.Items.Insert(0, new ListItem("--Select--", ""));
        }
        protected void getIDPAttribute()
        {
            IDPAttributeModel idpAttributeModel = new IDPAttributeModel();
            idpAttributeModel = _projectDL.GetIDPAttributeModel(AttributeId);

            txtAttributeName.Text = idpAttributeModel.AttributeName;
            if (idpAttributeModel.AttributeStartDate.HasValue)
            {
                DateTime startdate = idpAttributeModel.AttributeStartDate.Value;
                datepickerStartDate.Text = startdate.ToString("MM/dd/yyyy");
            }
            if (idpAttributeModel.AttributeEndDate.HasValue)
            {
                DateTime enddate = idpAttributeModel.AttributeEndDate.Value;
                datepickerEndDate.Text = enddate.ToString("MM/dd/yyyy");
            }
            drpAttributeType.DataSource = _projectDL.GetIDPAttributesTypeList();
            drpAttributeType.DataTextField = "AttributeTypeName";
            drpAttributeType.DataValueField = "AttributeTypeId";
            drpAttributeType.DataBind();
            drpAttributeType.Items.Insert(0, new ListItem("--Select--", ""));
            drpAttributeType.SelectedValue = idpAttributeModel.AttributeTypeId.ToString();
            drpAttributeType_SelectedIndexChanged(null, null);

            StringBuilder MyStringBuilder = new StringBuilder();
            if (idpAttributeModel.AttributeValueStringList.ToString() != string.Empty) { 
            foreach (string s in idpAttributeModel.AttributeValueStringList) 
            {
                MyStringBuilder.Append(s);
                MyStringBuilder.Append(",");
            }
            }
            int lastIndexOfComma = MyStringBuilder.ToString().LastIndexOf(',');
            if (lastIndexOfComma > -1) { txtAttributeValues.Text = MyStringBuilder.ToString().Remove(lastIndexOfComma); }
            
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IDPAttributeModel idpAttribute = new IDPAttributeModel();
                List<AttributeValuesModel> attriValuesList = new List<AttributeValuesModel>();
                AttributeValuesModel attributeValues = new AttributeValuesModel();
                //Control myControl = null;
                //List<string> str = null;
                idpAttribute.AttributeId = AttributeId;
                idpAttribute.AttributeName = txtAttributeName.Text.Trim();
                if (datepickerStartDate.Text.Trim() != string.Empty)
                {
                    idpAttribute.AttributeStartDate = Convert.ToDateTime(datepickerStartDate.Text.Trim());
                }
                if (datepickerEndDate.Text.Trim() != string.Empty)
                {
                    idpAttribute.AttributeEndDate = Convert.ToDateTime(datepickerEndDate.Text.Trim());
                }
                if (drpAttributeType.SelectedItem.Value != string.Empty)
                {
                    idpAttribute.AttributeTypeId = Convert.ToInt32(drpAttributeType.SelectedItem.Value);
                }
             
                int attriId = _projectDL.GetAttributeTypeId("TXT");
                if ((idpAttribute.AttributeTypeId) != attriId)
                {
                    string[] attrValues = txtAttributeValues.Text.Split(',');

                    idpAttribute.AttributeValueStringList = new List<string>();
                    for (int i = 0; i < attrValues.Count(); i++)
                    {
                        idpAttribute.AttributeValueStringList.Add(attrValues[i]);
                    }
                }
                
                

                //if (Hidden1.Value != string.Empty) 
                //{
                //    for (int i = 1; i <= Convert.ToInt32(Hidden1.Value); i++)
                //    {
                //        foreach (Control c in Page.Controls)
                //        {
                //            if (c.ID == "textbox" + i)
                //            {
                //                myControl = c;
                //                idpAttribute.AttributeValueStringList.Add(myControl == null ? string.Empty : ((TextBox)myControl).Text);
                //                //break;
                //            }
                //        }                        
                //    }
                //}
                if (hidAttributeId.Value != string.Empty)
                {
                    idpAttribute.AttributeId = int.Parse(hidAttributeId.Value);
                    _projectDL.UpdateIDPAttributeModel(idpAttribute);
                    //call update method
                }
                else
                {
                    //call Insert method
                    int newAttributeId = _projectDL.InsertIDPAttributeModel(idpAttribute);
                    hidAttributeId.Value = Convert.ToString(newAttributeId);

                }

                lblMessage.Text = "Data Saved Successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Sorry!! We couldnot save the Data. " + ex.Message;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDPAttributeList.aspx");
        }

        protected void drpAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int attriId, attriId2;
            attriId = _projectDL.GetAttributeTypeId("TXT");
            attriId2 = _projectDL.GetAttributeTypeId("DATE");
            if (Convert.ToInt32(drpAttributeType.SelectedItem.Value) == attriId || Convert.ToInt32(drpAttributeType.SelectedItem.Value) == attriId2)
            {
                trAttriVal.Visible = false;
                txtAttributeValues.Visible = false;
            }
            else 
            {
                trAttriVal.Visible = true;
                txtAttributeValues.Visible = true;
            }
        }

        
    }
}