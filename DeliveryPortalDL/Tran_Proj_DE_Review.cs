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
    
    public partial class Tran_Proj_DE_Review
    {
        public Tran_Proj_DE_Review()
        {
            this.Tran_Proj_DE_Attibute = new HashSet<Tran_Proj_DE_Attibute>();
        }
    
        public int DEReviewId { get; set; }
        public int DEReviewCalendarId { get; set; }
        public int ProjectId { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        public virtual MST_Project MST_Project { get; set; }
        public virtual Tran_DE_Calendar Tran_DE_Calendar { get; set; }
        public virtual ICollection<Tran_Proj_DE_Attibute> Tran_Proj_DE_Attibute { get; set; }
    }
}
