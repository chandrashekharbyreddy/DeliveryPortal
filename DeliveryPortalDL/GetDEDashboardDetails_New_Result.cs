//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryPortalDL
{
    using System;
    
    public partial class GetDEDashboardDetails_New_Result
    {
        public Nullable<int> ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string AccountName { get; set; }
        public Nullable<int> GeoID { get; set; }
        public string GeoName { get; set; }
        public Nullable<int> SectorID { get; set; }
        public string SectorName { get; set; }
        public Nullable<bool> IsStrategic { get; set; }
        public Nullable<System.DateTime> RARSince { get; set; }
        public string PriorOverallStatus { get; set; }
        public string CurrentOverallStatus { get; set; }
        public string LatestUpdates { get; set; }
        public string RiskItems { get; set; }
        public string IssueItems { get; set; }
        public string CorrectiveActions { get; set; }
        public Nullable<int> WeekId { get; set; }
    }
}