using DeliveryPortalDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class DEDashboard : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        ReportDL _reportDL = new ReportDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateIDP();
                PopulateAccounts();
                PopulateGeoLocations();
                PopulatePrimarySectors();
                PopulateWeeks();
                
            }
        }

        private void PopulateWeeks()
        {
            int weekNo = GetWeekOfYear(DateTime.Now);
            ddlWeek.Items.Add(new ListItem("Current Week", weekNo.ToString()));
            
            weekNo = GetWeekOfYear(DateTime.Now.AddDays(-7));
            ddlWeek.Items.Add(new ListItem("Last Week", weekNo.ToString()));
        }

        private static int GetWeekOfYear(DateTime inputDate)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            int weekNo = currentCulture.Calendar.GetWeekOfYear(
                            inputDate,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);

            return weekNo;
        }

        private void PopulateAccounts()
        {
            ddlAccount.DataSource = _projectDL.GetAccounts();
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataBind();
            ddlAccount.Items.Insert(0, new ListItem("All", "0"));
        }
        private void PopulateIDP() 
        {
            ddlIDP.DataSource = _projectDL.GetIDPs();
            ddlIDP.DataTextField = "IDPName";
            ddlIDP.DataValueField = "IDPId";
            ddlIDP.DataBind();
            ddlIDP.Items.Insert(0, new ListItem("All", "0"));
        }

        private void PopulateGeoLocations()
        {
            ddlGeo.DataSource = _projectDL.GetGeoLocations();
            ddlGeo.DataTextField = "GeoName";
            ddlGeo.DataValueField = "GeoId";
            ddlGeo.DataBind();
            ddlGeo.Items.Insert(0, new ListItem("All", "0"));
        }

        private void PopulatePrimarySectors()
        {
            //ddlSector.DataSource = _projectDL.GetSectors();
            ddlSector.DataSource = _projectDL.GetPrimarySectors();
            ddlSector.DataTextField = "SectorName";
            ddlSector.DataValueField = "SectorId";
            ddlSector.DataBind();
            ddlSector.Items.Insert(0, new ListItem("All", "0"));
        }

        private void PopulateSubSectors(int primarySectorId)
        {
            ddlSubSector.DataSource = null;
            ddlSubSector.DataSource = _projectDL.GetSubSectors(primarySectorId);
            ddlSubSector.DataTextField = "SectorName";
            ddlSubSector.DataValueField = "SectorId";
            ddlSubSector.DataBind();
            ddlSubSector.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdDEDashboard.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            string sectorId = "0", sectorId2="0";
            if (ddlSector.SelectedIndex > 0)
            {
                sectorId = ddlSector.SelectedValue;
            }
            if (ddlSubSector.SelectedIndex > 0)
            {
                sectorId2 = ddlSubSector.SelectedValue;
            }

            grdDEDashboard.DataSource = _reportDL.GetDEDashboardDetails(int.Parse(ddlIDP.SelectedValue),int.Parse(ddlAccount.SelectedValue), int.Parse(ddlGeo.SelectedValue), int.Parse(sectorId), int.Parse(ddlWeek.SelectedValue), chkRAAndStrategicProjects.Checked, chkRA.Checked,int.Parse(sectorId2));
            grdDEDashboard.DataBind();

            grdDEDashboard.Visible = true;
        }

        protected void grdDEDashboard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text.Contains("-"))
                {
                    string strRed = "<img src='Images/circle_red.png' >";
                    string strGreen = "<img src='Images/circle_green.png'>";
                    string strAmber = "<img src='Images/circle_amber.png'>";
                    string strGrey = "<img src='Images/circle_grey.png'>";

                    // Set Fixed width for first column (Project Name)
                    e.Row.Cells[0].Width = Unit.Pixel(150);
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[0].Attributes.Add("style", "padding:3px");
                    if (e.Row.Cells[0].Text.Contains("~~NextLine~~"))
                    {
                        e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("~~NextLine~~", "<br/>");
                    }


                    e.Row.Cells[1].Width = Unit.Pixel(30);

                    e.Row.Cells[2].Width = Unit.Pixel(30);
                    e.Row.Cells[3].Width = Unit.Pixel(40);
                    e.Row.Cells[1].Attributes.Add("style", "padding:3px");
                    e.Row.Cells[2].Attributes.Add("style", "padding:3px");
                    e.Row.Cells[3].Attributes.Add("style", "padding:3px");


                    Dictionary<string, int> dicHeaders = new Dictionary<string, int>();
                    if (ViewState["Headers"] != null)
                    {
                        dicHeaders = (Dictionary<string, int>)ViewState["Headers"];
                        if (dicHeaders != null)
                        {
                            if (ViewState["ProjectAttributeHeaders"] != null)
                            {
                                List<string> projectAttributeHeaders = (List<string>)ViewState["ProjectAttributeHeaders"];
                                foreach (string projectAttributeHeader in projectAttributeHeaders)
                                {
                                    int indexOfAttribute = dicHeaders[projectAttributeHeader];
                                    TableCell cell = e.Row.Cells[indexOfAttribute];
                                    cell.HorizontalAlign = HorizontalAlign.Right;
                                    cell.Width = Unit.Pixel(25);
                                    cell.Attributes.Add("style", "padding-right:5px");
                                    if (((DataRowView)e.Row.DataItem)[projectAttributeHeader] != DBNull.Value)
                                    {
                                        string attributeValue = (string)((DataRowView)e.Row.DataItem)[projectAttributeHeader];

                                        if (attributeValue == "Green")
                                        {
                                            cell.Text = strGreen;
                                        }
                                        else if (attributeValue == "Amber")
                                        {
                                            cell.Text = strAmber;
                                        }
                                        else if (attributeValue == "Red")
                                        {
                                            cell.Text = strRed;
                                        }
                                    }
                                    else
                                    {
                                        cell.Text = strGrey;
                                    }
                                }
                            }

                            int indexOfPriorWeekStatus = dicHeaders["Prior Week Status"];
                            TableCell cellPriorWeekStatus = e.Row.Cells[indexOfPriorWeekStatus];
                            if (((DataRowView)e.Row.DataItem)["Prior Week Status"] != DBNull.Value)
                            {
                                string priorWeekStatus = (string)((DataRowView)e.Row.DataItem)["Prior Week Status"];

                                if (priorWeekStatus == "Green")
                                {
                                    cellPriorWeekStatus.Text = strGreen;
                                }
                                else if (priorWeekStatus == "Amber")
                                {
                                    cellPriorWeekStatus.Text = strAmber;
                                }
                                else if (priorWeekStatus == "Red")
                                {
                                    cellPriorWeekStatus.Text = strRed;
                                }
                                else
                                {
                                    cellPriorWeekStatus.Text = strGrey;
                                }
                            }
                            else
                            {
                                cellPriorWeekStatus.Text = strGrey;
                            }

                            int indexOfCurrentWeekStatus = dicHeaders["Current Week Status"];
                            TableCell cellCurrentWeekStatus = e.Row.Cells[indexOfCurrentWeekStatus];
                            string currentWeekStatus = "";
                            if (((DataRowView)e.Row.DataItem)["Current Week Status"] != DBNull.Value)
                            {
                                currentWeekStatus = (string)((DataRowView)e.Row.DataItem)["Current Week Status"];

                                if (currentWeekStatus == "Green")
                                {
                                    cellCurrentWeekStatus.Text = strGreen;
                                }
                                else if (currentWeekStatus == "Amber")
                                {
                                    cellCurrentWeekStatus.Text = strAmber;
                                }
                                else if (currentWeekStatus == "Red")
                                {
                                    cellCurrentWeekStatus.Text = strRed;
                                }
                                else
                                {
                                    cellCurrentWeekStatus.Text = strGrey;
                                }
                            }
                            else
                            {
                                cellCurrentWeekStatus.Text = strGrey;
                            }

                            if (((DataRowView)e.Row.DataItem)["R AR Since"] != DBNull.Value)
                            {
                                DateTime rarSince = (DateTime)((DataRowView)e.Row.DataItem)["R AR Since"];

                                int indexOfRARSince = dicHeaders["R AR Since"];
                                TableCell cell = e.Row.Cells[indexOfRARSince];
                                Label lblRARSince = new Label();
                                if (currentWeekStatus != "Green")
                                {
                                    lblRARSince.Text = rarSince.ToString("dd-MMM");
                                }
                                cell.HorizontalAlign = HorizontalAlign.Left;
                                cell.Controls.Add(lblRARSince);
                            }

                            if (((DataRowView)e.Row.DataItem)["Latest Updates"] != DBNull.Value)
                            {
                                string latestUpdates = (string)((DataRowView)e.Row.DataItem)["Latest Updates"];
                                if (dicHeaders.ContainsKey("Latest Updates"))
                                {
                                    int indexOfLatestUpdates = dicHeaders["Latest Updates"];
                                    TableCell cell = e.Row.Cells[indexOfLatestUpdates];
                                    Label lblLatestUpdates = new Label();
                                    lblLatestUpdates.Text = latestUpdates;
                                    
                                    cell.HorizontalAlign = HorizontalAlign.Left;
                                    cell.VerticalAlign = VerticalAlign.Top;
                                    cell.Style.Add("padding", "5px");
                                    cell.Controls.Add(lblLatestUpdates);
                                }
                            }

                            if (((DataRowView)e.Row.DataItem)["Risks"] != DBNull.Value)
                            {
                                string risks = (string)((DataRowView)e.Row.DataItem)["Risks"];
                                if (dicHeaders.ContainsKey("Risks"))
                                {
                                    int indexOfRisks = dicHeaders["Risks"];
                                    TableCell cell = e.Row.Cells[indexOfRisks];
                                    Label lblRisks = new Label();
                                    lblRisks.Text = risks;
                                    
                                    cell.HorizontalAlign = HorizontalAlign.Left;
                                    cell.VerticalAlign = VerticalAlign.Top;
                                    cell.Style.Add("padding", "5px");
                                    cell.Controls.Add(lblRisks);
                                }
                            }

                            if (((DataRowView)e.Row.DataItem)["Issues"] != DBNull.Value)
                            {
                                string issues = (string)((DataRowView)e.Row.DataItem)["Issues"];
                                if (dicHeaders.ContainsKey("Issues"))
                                {
                                    int indexOfIssues = dicHeaders["Issues"];
                                    TableCell cell = e.Row.Cells[indexOfIssues];
                                    Label lblIssues = new Label();
                                    lblIssues.Text = issues;
                                    cell.HorizontalAlign = HorizontalAlign.Left;
                                    cell.VerticalAlign = VerticalAlign.Top;
                                    cell.Style.Add("padding", "5px");
                                    cell.Controls.Add(lblIssues);
                                }
                            }

                            if (((DataRowView)e.Row.DataItem)["Corrective Actions"] != DBNull.Value)
                            {
                                string correctiveActions = (string)((DataRowView)e.Row.DataItem)["Corrective Actions"];
                                if (dicHeaders.ContainsKey("Corrective Actions"))
                                {
                                    int indexOfCorrectiveActions = dicHeaders["Corrective Actions"];
                                    TableCell cell = e.Row.Cells[indexOfCorrectiveActions];
                                    Label lblCorrectiveActions = new Label();
                                    lblCorrectiveActions.Text = correctiveActions;
                                    cell.HorizontalAlign = HorizontalAlign.Left;
                                    cell.VerticalAlign = VerticalAlign.Top;
                                    cell.Style.Add("padding", "5px");
                                    cell.Controls.Add(lblCorrectiveActions);
                                }
                            }
                        }
                    }
                }
                else
                {
                    int columnCount = e.Row.Cells.Count;

                    for (int cellIndex = e.Row.Cells.Count; cellIndex > 1; cellIndex--)
                    {
                        e.Row.Cells.RemoveAt(cellIndex-1);
                    }

                    e.Row.Cells[0].Height = Unit.Parse("15px");
                    e.Row.Cells[0].ColumnSpan = columnCount;
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Style.Add("padding", "4px");
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = FontUnit.Parse("12px");
                    Style style = new Style();
                    style.BackColor = System.Drawing.Color.FromArgb(0x548dd4);
                    e.Row.Cells[0].ApplyStyle(style);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                Dictionary<string, int> dicHeaders = new Dictionary<string, int>();
                List<string> projectAttributeHeaders = new List<string>();
                for (int cellIndex = 0; cellIndex < e.Row.Cells.Count; cellIndex++)
                {
                    dicHeaders.Add(e.Row.Cells[cellIndex].Text, cellIndex);
                    if (e.Row.Cells[cellIndex].Text.StartsWith(" ") && e.Row.Cells[cellIndex].Text.Length > 1)
                    {
                        Style style = new Style();
                        style.CssClass = "verticaltext";
                        e.Row.Cells[cellIndex].ApplyStyle(style);
                        e.Row.Cells[cellIndex].HorizontalAlign = HorizontalAlign.Left;
                        projectAttributeHeaders.Add(e.Row.Cells[cellIndex].Text);
                    }
                }

                for (int cellCount = e.Row.Cells.Count; cellCount >= e.Row.Cells.Count - 3; cellCount--)
                {
                    e.Row.Cells[cellCount - 1].Style.Add("text-align", "center");
                }

                //e.Row.Cells[e.Row.Cells.Count - 1].Style.Add("min-width", "100px");//this one

                ViewState["Headers"] = dicHeaders;
                ViewState["ProjectAttributeHeaders"] = projectAttributeHeaders;
            }
            
            
        }

        protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdDEDashboard.Visible = false;
            if (ddlSector.SelectedIndex > 0)
            {
                PopulateSubSectors( int.Parse(ddlSector.SelectedValue));
            }
        }

        protected void ddlIDP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

    }
}