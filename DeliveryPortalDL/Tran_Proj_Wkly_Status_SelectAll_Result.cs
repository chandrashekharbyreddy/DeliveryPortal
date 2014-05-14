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
    
    public partial class Tran_Proj_Wkly_Status_SelectAll_Result
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> IDPId { get; set; }
        public Nullable<int> EMId { get; set; }
        public Nullable<int> PMId { get; set; }
        public Nullable<int> GeoId { get; set; }
        public Nullable<int> SectorId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> MethodologyId { get; set; }
        public Nullable<int> NoWId { get; set; }
        public Nullable<int> EstBasisId { get; set; }
        public Nullable<System.DateTime> LastDEReviewDate { get; set; }
        public Nullable<System.DateTime> LastIDQADate { get; set; }
        public Nullable<System.DateTime> LastSMRDate { get; set; }
        public Nullable<bool> IsStrategic { get; set; }
        public Nullable<int> WeeklyStatusId { get; set; }
        public Nullable<int> WeekId { get; set; }
        public Nullable<System.DateTime> WeekStartDate { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> PriorOverallStatusId { get; set; }
        public Nullable<int> CurrentOverallStatusId { get; set; }
        public string LatestUpdates { get; set; }
        public string RiskItems { get; set; }
        public string IssueItems { get; set; }
        public string CorrectiveActions { get; set; }
        public Nullable<int> OverrideStatusId { get; set; }
        public Nullable<int> FlagsUpdatedByLevel { get; set; }
        public Nullable<int> AttributeId { get; set; }
        public string AttributeName { get; set; }
        public Nullable<int> FlagId { get; set; }
        public string FlagName { get; set; }
        public Nullable<int> IsCurrentWk { get; set; }
        public Nullable<int> IsPreviousWk { get; set; }
    }
}