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
    
    public partial class Tran_Proj_Wkly_Attributes_Status
    {
        public int WeeklyStatusId { get; set; }
        public int AttributeId { get; set; }
        public Nullable<int> FlagId { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        public virtual MST_Flags MST_Flags { get; set; }
        public virtual MST_ProjectAttributes MST_ProjectAttributes { get; set; }
        public virtual Tran_Proj_Wkly_Status Tran_Proj_Wkly_Status { get; set; }
    }
}
