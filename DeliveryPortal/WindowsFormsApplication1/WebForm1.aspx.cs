using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MvcDemoApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    CreateDynamicControls(int.Parse(ddlRecipients.SelectedValue));
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    CreateDynamicControls(int.Parse(ddlRecipients.SelectedValue));
            //}
        }

        private DataTable GetDynamicAttributesList()
        {
            DataTable dtAttributes = new DataTable();
            dtAttributes.Columns.Add("AttributeId", Type.GetType("System.Int32"));
            dtAttributes.Columns.Add("AttributeName", Type.GetType("System.String"));
            dtAttributes.Columns.Add("AttributeTypeId", Type.GetType("System.Int32"));

            DataRow dr = dtAttributes.NewRow();
            dr["AttributeId"] = "1";
            dr["AttributeName"] = "Nature of worker";
            dr["AttributeTypeId"] = "2";    // Dropdownlist
            dtAttributes.Rows.Add(dr);

            dr = dtAttributes.NewRow();
            dr["AttributeId"] = "2";
            dr["AttributeName"] = "Estimation Basis";
            dr["AttributeTypeId"] = "2";    // Dropdownlist
            dtAttributes.Rows.Add(dr);

            dr = dtAttributes.NewRow();
            dr["AttributeId"] = "3";
            dr["AttributeName"] = "Detailed Tech Stack";
            dr["AttributeTypeId"] = "4";    // Textbox
            dtAttributes.Rows.Add(dr);

            dr = dtAttributes.NewRow();
            dr["AttributeId"] = "4";
            dr["AttributeName"] = "PMTS Compliance";
            dr["AttributeTypeId"] = "5";    // Textbox
            dtAttributes.Rows.Add(dr);

            return dtAttributes;
        }

        private DataTable GetAttributeValuesList()
        {
            DataTable dtAttributeValues = new DataTable();
            dtAttributeValues.Columns.Add("AttributeId", Type.GetType("System.Int32"));
            dtAttributeValues.Columns.Add("AttributeValue", Type.GetType("System.String"));
            dtAttributeValues.Columns.Add("AttributeText", Type.GetType("System.String"));
            dtAttributeValues.Columns.Add("AttributeValueId", Type.GetType("System.Int32"));

            DataRow dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 1;
            dr["AttributeValue"] = "Pure AD";
            dr["AttributeText"] = "Pure AD";
            dr["AttributeValueId"] = 1;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 1;
            dr["AttributeValue"] = "Pure AM";
            dr["AttributeText"] = "Pure AM";
            dr["AttributeValueId"] = 2;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 1;
            dr["AttributeValue"] = "Product Enhancement";
            dr["AttributeText"] = "Product Enhancement";
            dr["AttributeValueId"] = 3;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 1;
            dr["AttributeValue"] = "Enhancement only";
            dr["AttributeText"] = "Enhancement only";
            dr["AttributeValueId"] = 4;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 2;
            dr["AttributeValue"] = "Great";
            dr["AttributeText"] = "Great";
            dr["AttributeValueId"] = 5;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 2;
            dr["AttributeValue"] = "WBS";
            dr["AttributeText"] = "WBS";
            dr["AttributeValueId"] = 6;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 2;
            dr["AttributeValue"] = "Others";
            dr["AttributeText"] = "Others";
            dr["AttributeValueId"] = 7;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 4;
            dr["AttributeValue"] = "Y";
            dr["AttributeText"] = "Y";
            dr["AttributeValueId"] = 8;
            dtAttributeValues.Rows.Add(dr);

            dr = dtAttributeValues.NewRow();
            dr["AttributeId"] = 4;
            dr["AttributeValue"] = "N";
            dr["AttributeText"] = "N";
            dr["AttributeValueId"] = 9;
            dtAttributeValues.Rows.Add(dr);

            return dtAttributeValues;
        }

        private void CreateDynamicControls(int count)
        {
            //for (int i = 0; i < count; i++)
            //{
            //    var textBox = new TextBox();
            //    var textboxCell = new TableCell();
            //    var tableRow = new TableRow();
            //    tableRow.Cells.Add(textboxCell);
            //    textboxCell.Controls.Add(textBox);
            //    tblRecipients.Rows.Add(tableRow);
            //}

            //DropDownList dropDownList = new DropDownList();
            //dropDownList.ID = "ddlNumbers";            
            //dropDownList.Items.Add(new ListItem("One", "1"));
            //dropDownList.Items.Add(new ListItem("Two", "2"));
            //dropDownList.Items.Add(new ListItem("Three", "3"));
            //dropDownList.Items.Add(new ListItem("Four", "4"));
            //TableCell dropdownListCell = new TableCell();
            //TableRow tableROW = new TableRow();

            //tableROW.Cells.Add(dropdownListCell);
            //dropdownListCell.Controls.Add(dropDownList);
            //tblRecipients.Rows.Add(tableROW);

            DataTable dtAttributes = GetDynamicAttributesList();
            DataTable dtAttributeValues = GetAttributeValuesList();

            foreach (DataRow dr in dtAttributes.Rows)
            {
                if (int.Parse(dr["AttributeTypeId"].ToString()) == 2)
                {
                    DropDownList dropDownList = new DropDownList();
                    dropDownList.ID = "drp" + dr["AttributeName"].ToString().Replace(" ", "");                    
                    foreach(DataRow dataRow in dtAttributeValues.Select(" AttributeId = " + dr["AttributeId"]))
                    {
                        dropDownList.Items.Add(new ListItem(dataRow["AttributeText"].ToString(), dataRow["AttributeValue"].ToString()));
                    }

                    TableRow tableROW = new TableRow();

                    TableCell labelCell = new TableCell();
                    labelCell.Text = dr["AttributeName"].ToString();
                    tableROW.Cells.Add(labelCell);

                    TableCell dropdownListCell = new TableCell();                    
                    tableROW.Cells.Add(dropdownListCell);
                    dropdownListCell.Controls.Add(dropDownList);
                    tblRecipients.Rows.Add(tableROW);
                }
                if (int.Parse(dr["AttributeTypeId"].ToString()) == 4)
                {
                    TextBox textBox = new TextBox();

                    TableRow tableROW = new TableRow();

                    TableCell labelCell = new TableCell();
                    labelCell.Text = dr["AttributeName"].ToString();
                    tableROW.Cells.Add(labelCell);

                    TableCell textboxCell = new TableCell();
                    tableROW.Cells.Add(textboxCell);
                    textboxCell.Controls.Add(textBox);
                    tblRecipients.Rows.Add(tableROW);
                }
            }
        }

        protected override object SaveViewState()
        {
            var viewState = new object[2];
            //Saving the dropdownlist value to the View State
            viewState[0] = int.Parse(ddlRecipients.SelectedValue); ;
            viewState[1] = base.SaveViewState();
            return viewState;
        }

        protected override void LoadViewState(object savedState)
        {
            //Getting the dropdown list value from view state.
            if (savedState is object[] && ((object[])savedState).Length == 2)
            {
                var viewState = (object[])savedState;
                var count = int.Parse(viewState[0].ToString());
                CreateDynamicControls(count);
                base.LoadViewState(viewState[1]);
            }
            else
            {
                base.LoadViewState(savedState);
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            //lblValues.Text = "";
            //To get the textbox value, you can loop throw
            //the table cells and read the textbox controls
            foreach (TableRow row in tblRecipients.Rows)
            {
                var textbox = row.Cells[1].Controls[0] as TextBox;
                //lblValues.Text += textbox.Text + "<br/>";
            }
        }

        protected void ddlRecipients_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CreateDynamicControls(int.Parse(ddlRecipients.SelectedValue));
        }

        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            lblValues.Text = "";
            //To get the textbox value, you can loop throw
            //the table cells and read the textbox controls
            foreach (TableRow row in tblRecipients.Rows)
            {
                var textbox = row.Cells[1].Controls[0] as TextBox;
                if (textbox != null)
                {
                    lblValues.Text += textbox.Text + "<br/>";
                }
                var dropdownList = row.Cells[1].Controls[0] as DropDownList;
                if (dropdownList != null)
                {
                    lblValues.Text += dropdownList.SelectedValue + "<br/>";
                }
            }
        }
    }
}