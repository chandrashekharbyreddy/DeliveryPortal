using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class ProjectDetailsDynamic : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool ddlIDP_SelectedIndexChangedValue = false; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateProjectCodes();
                PopulateIDPs();
                PopulateAccounts();
                PopulateEMandPMIds();
                if (ddlIDP.SelectedValue != "" || ddlIDP.SelectedIndex != 0)
                {
                    CreateDynamicControls(int.Parse(ddlIDP.SelectedValue));
                }

                // Edit Mode gets Project ID
                if (Request.QueryString["ID"] != null)
                {
                    hidProjectId.Value = Request.QueryString["ID"];                    
                    GetProjectDetails(int.Parse(hidProjectId.Value));
                }

            }
        }

        /// <summary>
        /// Get all preject specific details except dynamic fields.
        /// </summary>
        /// <param name="projectId"></param>
        private void GetProjectDetails(int projectId)
        {
            ProjectModel project = _projectDL.GetProjectDetails(projectId);
            textProjectName.Text = project.ProjectName;
            ddlAccount.SelectedValue = project.AccountId.ToString();
            ddlIDP.SelectedValue = project.IdpId.ToString();
            ddlEM.SelectedValue = project.EMId.ToString();
            ddlPM.SelectedValue = project.PMID.ToString();
            if (project.IsStrategic.HasValue)
            {
                chkIsStrategic.Checked = project.IsStrategic.Value;
            }
            //CreateDynamicControls(project.IDPId);
            GetProjectDetailsValues(Convert.ToInt32(project.IdpId));
        }

        /// <summary>
        /// This method is used to recreate the controls along with dynamic values
        /// </summary>
        /// <param name="idpId"></param>
        private void GetProjectDetailsValues(int idpId)
        {

            //create the dynamic controls first
            CreateDynamicControls(idpId);
            // Get the dynamic fileds values
            CreateDynamicControlswithValues(idpId);
        }

        /// <summary>
        /// This will be used to get all the Create and Get all the dynamic values during edit
        /// </summary>
        /// <param name="idpId"></param>
        protected void CreateDynamicControlswithValues(int idpId)
        {
            if (idpId != 0)
            {
                DataTable dtAttributes = GetDynamicAttributesList(idpId);
                DataTable dtAttributeValues = GetDynamicAttributeValue(idpId);
                //RequiredFieldValidator validator = new RequiredFieldValidator();

                foreach (DataRow dr in dtAttributes.Rows)
                {
                    foreach (DataRow dataRow in dtAttributeValues.Select(" AttributeId = " + dr["AttributeId"]))
                    {
                        ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");

                            foreach (TableRow row in tblRecipients.Rows)
                            {
                                 TranProjIDPAttributesModel rt = new TranProjIDPAttributesModel();
                                 foreach (TableCell tc in row.Cells)
                                 {
                                     foreach (Control c in tc.Controls)
                                     {
                                         if (c is TextBox)
                                         {
                                             string strName = "txt" + dr["AttributeName"].ToString().Replace(" ", "");
                                             if (c.ID == strName)
                                             {
                                                 TextBox txt = (TextBox)cph.FindControl(c.ID);
                                                 txt.Text = dataRow["AttributeText"].ToString().Trim();
                                                    //Response.Write(dataRow["AttributeText"].ToString() + "<br>" + dataRow["AttributeValueId"].ToString());
                                             }
                                         }

                                         if (c is DropDownList)
                                         {
                                             string strName = "drp" + dr["AttributeName"].ToString().Replace(" ", "");
                                             if (c.ID == strName)
                                             {
                                                 DropDownList drpList = (DropDownList)cph.FindControl(c.ID);
                                                 drpList.SelectedValue= dataRow["AttributeValueId"].ToString();                                                 
                                                 //Response.Write(dataRow["AttributeText"].ToString() + "<br>" + dataRow["AttributeValueId"].ToString());
                                             }
                                         }

                                        
                                     }
                                 }
                            }                       
                    }
                }
            }
        }


        private void PopulateProjectCodes() 
        {
            //Populate Project Codes
            lbProjectCode.DataSource = _projectDL.GetProjectCodes();
            lbProjectCode.DataTextField = "ProjectCode";
            lbProjectCode.DataValueField = "ProjectCodeId";
            lbProjectCode.DataBind();
        }
        private void PopulateAccounts()
        {
            ddlAccount.DataSource = _projectDL.GetAccounts();
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataBind();
            ddlAccount.Items.Insert(0, new ListItem("--Select--", ""));
        }
        private void PopulateIDPs()
        {
            ddlIDP.DataSource = _projectDL.GetIDPs();
            ddlIDP.DataTextField = "IDPName";
            ddlIDP.DataValueField = "IDPId";
            ddlIDP.DataBind();
            ddlIDP.Items.Insert(0, new ListItem("--Select--", ""));
        }
        private void PopulateEMandPMIds()
        {
            List<EmployeeModel> employees = _projectDL.GetEmployees();

            ddlEM.DataSource = employees;
            ddlEM.DataTextField = "EmployeeName";
            ddlEM.DataValueField = "EmployeeId";
            ddlEM.DataBind();
            ddlEM.Items.Insert(0, new ListItem("--Select--", ""));

            ddlPM.DataSource = employees;
            ddlPM.DataTextField = "EmployeeName";
            ddlPM.DataValueField = "EmployeeId";
            ddlPM.DataBind();
            ddlPM.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private DataTable GetDynamicAttributesList(int IDPId)
        {
            DataTable dtAttributes = new DataTable();
            dtAttributes.Columns.Add("AttributeId", Type.GetType("System.Int32"));
            dtAttributes.Columns.Add("AttributeName", Type.GetType("System.String"));
            dtAttributes.Columns.Add("AttributeTypeId", Type.GetType("System.Int32"));
            
            List<IDPAttributeModel> idpAtributesList = _projectDL.GetIDPSpecificAttributes(IDPId);
            foreach (IDPAttributeModel idpAttribute in idpAtributesList)
            {
                if (idpAttribute != null)
                {
                    DataRow dr = dtAttributes.NewRow();
                    dr["AttributeId"] = idpAttribute.AttributeId;
                    dr["AttributeName"] = idpAttribute.AttributeName;
                    dr["AttributeTypeId"] = idpAttribute.AttributeTypeId;
                    dtAttributes.Rows.Add(dr);
                }

            }
            return dtAttributes;
        }
        private DataTable GetDynamicAttributeValueList(int IDPId)
        {
            DataTable dtAttributeValues = new DataTable();
            dtAttributeValues.Columns.Add("AttributeId", Type.GetType("System.Int32"));
            dtAttributeValues.Columns.Add("AttributeValue", Type.GetType("System.String"));
            dtAttributeValues.Columns.Add("AttributeText", Type.GetType("System.String"));
            dtAttributeValues.Columns.Add("AttributeValueId", Type.GetType("System.Int32"));
            List<AttributeValuesModel> AttributeValueListMan = _projectDL.GetIDPSpecificAttributeValues(IDPId);
            foreach (AttributeValuesModel attributevalue in AttributeValueListMan)
            {
                DataRow dr = dtAttributeValues.NewRow();
                dr["AttributeId"] = attributevalue.AttributeId;
                dr["AttributeValue"] = attributevalue.AttributeValue;
                dr["AttributeText"] = attributevalue.AttributeText;
                dr["AttributeValueId"] = attributevalue.AttributeValueId;
                dtAttributeValues.Rows.Add(dr);
            }
            //AttributeValueListMan = _projectDL.GetIDPSpecificAttributeValues(IDPId);
            
            return dtAttributeValues;
        }

        private DataTable GetDynamicAttributeValue(int IDPId)
        {
            DataTable dtAttributeValues = new DataTable();
            dtAttributeValues.Columns.Add("AttributeId", Type.GetType("System.Int32"));
            dtAttributeValues.Columns.Add("AttributeValue", Type.GetType("System.String"));
            dtAttributeValues.Columns.Add("AttributeText", Type.GetType("System.String"));
            dtAttributeValues.Columns.Add("AttributeValueId", Type.GetType("System.Int32"));
            List<AttributeValuesModel> AttributeValueListMan = _projectDL.GetIDPDynamicAttributeValue(IDPId);

            foreach (AttributeValuesModel attributevalue in AttributeValueListMan)
            {
                DataRow dr = dtAttributeValues.NewRow();
                dr["AttributeId"] = attributevalue.AttributeId;
                dr["AttributeValue"] = attributevalue.AttributeValue;
                dr["AttributeText"] = attributevalue.AttributeText;
                if (attributevalue.AttributeValueId == null)
                {
                    dr["AttributeValueId"] = DBNull.Value;
                }
                else
                {
                    dr["AttributeValueId"] = attributevalue.AttributeValueId;
                }
                
                dtAttributeValues.Rows.Add(dr);
            }
            //AttributeValueListMan = _projectDL.GetIDPSpecificAttributeValues(IDPId);

            return dtAttributeValues;
        }

        protected void ddlIDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iSelectedId = Convert.ToInt32(ddlIDP.SelectedValue);
            ddlIDP_SelectedIndexChangedValue = true;
            CreateDynamicControls(iSelectedId);
           // tab_index.Value = "1";
            
        }
        protected void CreateDynamicControls(int idpId)
        {
            if (idpId !=0)
            {

            DataTable dtAttributes = GetDynamicAttributesList(idpId);
            DataTable dtAttributeValues = GetDynamicAttributeValueList(idpId);
            //RequiredFieldValidator validator = new RequiredFieldValidator();

            if (ddlIDP_SelectedIndexChangedValue == false)
            {
                dtAttributes.Rows.Clear();

            }
            foreach (DataRow dr in dtAttributes.Rows)
            {
                Label lblId = new Label();
                lblId.ID = "lblAttributeId" + dr["AttributeId"].ToString().Replace(" ", "");
                lblId.Text = dr["AttributeId"].ToString();
                lblId.Visible = false;
                lblId.Width = 0;
                lblId.BorderColor = System.Drawing.Color.Black;

                // Create Dropdown Control
                if (int.Parse(dr["AttributeTypeId"].ToString()) == 2)
                {
                    RequiredFieldValidator rfvValidator = new RequiredFieldValidator();
                    rfvValidator.Attributes.Add("runat", "server");

                    DropDownList dropDownList = new DropDownList();
                    dropDownList.ID = "drp" + dr["AttributeName"].ToString().Replace(" ", "");
                    dropDownList.Items.Insert(0, new ListItem("--Select--", ""));

                    foreach (DataRow dataRow in dtAttributeValues.Select(" AttributeId = " + dr["AttributeId"]))
                    {
                        //Response.Write(dataRow["AttributeText"].ToString() + "<br>" + dataRow["AttributeValueId"].ToString());
                        dropDownList.Items.Add(new ListItem(dataRow["AttributeText"].ToString(), dataRow["AttributeValueId"].ToString()));
                    }

                    TableRow tableROW = new TableRow();
                    TableCell labelId = new TableCell();
                    tableROW.Cells.Add(labelId);                    
                    labelId.Controls.Add(lblId);

                    TableCell labelCell = new TableCell();
                    labelCell.Text = dr["AttributeName"].ToString();
                    labelCell.CssClass = "label";
                    tableROW.Cells.Add(labelCell);

                    TableCell dropdownListCell = new TableCell();
                    tableROW.Cells.Add(dropdownListCell);
                    dropdownListCell.Controls.Add(dropDownList);                    
                    rfvValidator.ErrorMessage = "Please Fill " + dr["AttributeName"].ToString().Replace(" ", "");                    
                    tblRecipients.Rows.Add(tableROW);
                }
                //Create TextBox Control
                if (int.Parse(dr["AttributeTypeId"].ToString()) == 4)
                {
                    TextBox textBox = new TextBox();
                    TableRow tableROW = new TableRow();
                    textBox.ID = "txt" + dr["AttributeName"].ToString().Replace(" ", "");
                    TableCell labelId = new TableCell();
                    tableROW.Cells.Add(labelId);
                    labelId.Controls.Add(lblId);

                    TableCell labelCell = new TableCell();
                    labelCell.Text = dr["AttributeName"].ToString();
                    labelCell.CssClass = "label";
                    tableROW.Cells.Add(labelCell);

                    TableCell textboxCell = new TableCell();
                    tableROW.Cells.Add(textboxCell);
                    textboxCell.Controls.Add(textBox);                    
                    tblRecipients.Rows.Add(tableROW);
                }
                //Create DateTime Calender Control
                if (int.Parse(dr["AttributeTypeId"].ToString()) == 7)
                {
                    TextBox txtDatePickerBox = new TextBox();
                    txtDatePickerBox.CssClass = "datepicker";
                    TableRow tableROW = new TableRow();
                    txtDatePickerBox.ID = "txt" + dr["AttributeName"].ToString().Replace(" ", "");

                    TableCell labelId = new TableCell();
                    tableROW.Cells.Add(labelId);
                    labelId.Controls.Add(lblId);               

                    TableCell labelCell = new TableCell();
                    labelCell.Text = dr["AttributeName"].ToString();
                    labelCell.CssClass = "label";
                    tableROW.Cells.Add(labelCell);

                    TableCell datepicker = new TableCell();
                    tableROW.Cells.Add(datepicker);
                    datepicker.Controls.Add(txtDatePickerBox);

                    tblRecipients.Rows.Add(tableROW);
                }
            }
            }
        }
        protected override void LoadViewState(object savedState)
        {
            //Getting the dropdown list value from view state.
            if (savedState is object[] && ((object[])savedState).Length == 2)
            {
                var viewState = (object[])savedState;
                var count = Int32.Parse(viewState[0].ToString());

                CreateDynamicControls(count);
                base.LoadViewState(viewState[1]);
            }
            else
            {
                base.LoadViewState(savedState);
            }
        }
        protected override object SaveViewState()
        {
            var viewState = new object[2];
            //Saving the dropdownlist value to the View State
            if (ddlIDP.SelectedValue != "")
            {
                viewState[0] = int.Parse(ddlIDP.SelectedValue);
            }
            else
            {
                viewState[0] = 0;
            }

            //viewState[0] = 4 ;
            viewState[1] = base.SaveViewState();
            return viewState;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try 
            { 
                //Saving Static fields First
                ProjectTempModel proj = new ProjectTempModel();
                if (ddlIDP.SelectedItem.Value != string.Empty)
                {
                    proj.IDPId = Convert.ToInt32(ddlIDP.SelectedItem.Value);
                }
                if (ddlAccount.SelectedItem.Value != string.Empty)
                {
                    proj.AccountId = Convert.ToInt32(ddlAccount.SelectedItem.Value);
                }
                proj.ProjectName = textProjectName.Text.Trim();
                if (ddlEM.SelectedItem.Value != string.Empty)
                {
                    proj.EMId = Convert.ToInt32(ddlEM.SelectedItem.Value);
                }
                if (ddlPM.SelectedItem.Value != string.Empty)
                {
                    proj.PMId = Convert.ToInt32(ddlPM.SelectedItem.Value);
                }
                proj.IsStrategic = chkIsStrategic.Checked;
                proj.LastUpdateDate = DateTime.Now;
                proj.LastUpdatedBy = Common.EmployeeId;
                

                //Collecting Project Code List
                List<int> ListProjectCode = new List<int>();
                foreach (ListItem item in lbProjectCode.Items)
                {
                    if (item.Selected)
                    {
                        ListProjectCode.Add(int.Parse(item.Value));
                    }
                }

               

            //Collecting Data from dynamic fields            
            string AttributeId = "";
            string AttributeValueId = "";
            string AttributeTextValue = "";           

            List<TranProjIDPAttributesModel> prjIDPAttr = new List<TranProjIDPAttributesModel>();
            
            ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");

            //Loop to iterate & get the respective control values
            foreach (TableRow row in tblRecipients.Rows)
            {
                TranProjIDPAttributesModel rt = new TranProjIDPAttributesModel();
                foreach (TableCell tc in row.Cells)
                {
                    foreach (Control c in tc.Controls)
                    {
                        if (c is Label)
                        {
                            if (c.ID.StartsWith("lblAttributeId"))
                            {
                                Label lbl = (Label)cph.FindControl(c.ID);
                                AttributeId = lbl.Text;
                            }                            
                        }
                        if (c is TextBox)
                        {
                            if (c.ID.StartsWith("txt"))
                            {
                                TextBox txt = (TextBox)cph.FindControl(c.ID);
                                AttributeTextValue = txt.Text;
                                AttributeValueId = "0";
                            }
                        }
                        if (c is DropDownList)
                        {
                            if (c.ID.StartsWith("drp"))
                            {
                                DropDownList drp = (DropDownList)cph.FindControl(c.ID);
                                AttributeValueId = drp.SelectedValue;
                                AttributeTextValue = drp.SelectedItem.Text;
                                //AttributeTextValue = drp.SelectedValue;
                            }
                        }
                    }
                }

                //Savin Dynamic Data
                //if (hidProjectId.Value != string.Empty)
                //{
                //    rt.ProjectId = Convert.ToInt32(hidProjectId.Value);
                //}
                //else
                //{
                //    rt.ProjectId = 0;
                //}

                rt.ProjectId = (hidProjectId.Value != string.Empty) ? rt.ProjectId = Convert.ToInt32(hidProjectId.Value) : 0;
                rt.AttributeId = Convert.ToInt32(AttributeId);
                rt.AttributeTextValue = AttributeTextValue;
                rt.AttributeValueId = Convert.ToInt32(AttributeValueId);
                rt.LastUpdatedBy = Common.EmployeeId;
                rt.LastUpdateDate = DateTime.Now;
                prjIDPAttr.Add(rt);
            }

            if (hidProjectId.Value != string.Empty)
            {

                _projectDL.UpdateProjDetail(proj);
                //Create Dynamic Attributes with values
                TranProjIDPAttributesModel tranProjIDPAttr = new TranProjIDPAttributesModel();

                foreach (TranProjIDPAttributesModel item in prjIDPAttr)
                {
                    _projectDL.UpdateProjAttributes(item);
                }
                
                lblMessage.Text = "Data Saved Successfully.";
            }
            else
            {
                // Create New Project, Performing Insert Functionality
                int newProjectId = _projectDL.InsertProjDetail(proj);
                hidProjectId.Value = newProjectId.ToString();

                _projectDL.MapProjectCodes(ListProjectCode, hidProjectId.Value.ToString());

                _projectDL.InsertProjAttributes(prjIDPAttr, hidProjectId.Value.ToString());
                lblMessage.Text = "Data Saved Successfully.";

            }
            
        }
            catch (Exception ex)
            {
                lblMessage.Text = "Sorry!! We couldnot save the Data." + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("ProjectsList.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            //foreach (object item in lbProjectCode.Items)
            //    {
            //        sb.Append(item.ToString());
            //        sb.Append(" ");
            //    }


            //foreach (int i in lbProjectCode.GetSelectedIndices())
            //{
            //    sb.Append(i.ToString());
            //    sb.Append(" ");
            //}

            //foreach (ListItem item in lbProjectCode.Items)
            //{
            //    if (item.Selected)
            //    {
            //        sb.Append(item.Value);
            //        sb.Append(",");
            //    }
            //}
            List<int> intProject = new List<int>();
            foreach (ListItem item in lbProjectCode.Items)
            {
                if (item.Selected)
                {
                    intProject.Add(int.Parse(item.Value));
                    //sb.Append(item.Value);
                    //sb.Append(",");
                    
                }
            }
            foreach (int i in intProject) 
            {
                Response.Write(i);
                
            }
            
            //Response.Write(sb.ToString());

        }
    }
}