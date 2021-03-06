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
    using System.Collections.Generic;
    
    public partial class MST_Account
    {
        public MST_Account()
        {
            this.MST_Project = new HashSet<MST_Project>();
            this.MST_Project_Temp = new HashSet<MST_Project_Temp>();
        }
    
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> SectorID2 { get; set; }
        public Nullable<int> GeoID { get; set; }
        public Nullable<int> LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        public virtual MST_Geo MST_Geo { get; set; }
        public virtual MST_Sector MST_Sector { get; set; }
        public virtual MST_Sector MST_Sector1 { get; set; }
        public virtual ICollection<MST_Project> MST_Project { get; set; }
        public virtual ICollection<MST_Project_Temp> MST_Project_Temp { get; set; }
    }
}
