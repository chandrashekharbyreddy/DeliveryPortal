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
    
    public partial class MST_Geo
    {
        public MST_Geo()
        {
            this.MST_Account = new HashSet<MST_Account>();
            this.MST_Project = new HashSet<MST_Project>();
        }
    
        public int GeoId { get; set; }
        public string GeoName { get; set; }
        public Nullable<int> LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        public virtual ICollection<MST_Account> MST_Account { get; set; }
        public virtual ICollection<MST_Project> MST_Project { get; set; }
    }
}
