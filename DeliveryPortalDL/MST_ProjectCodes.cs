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
    
    public partial class MST_ProjectCodes
    {
        public MST_ProjectCodes()
        {
            this.Tran_Proj_ProjCode_Details = new HashSet<Tran_Proj_ProjCode_Details>();
        }
    
        public int ProjectCodeId { get; set; }
        public int AccountId { get; set; }
        public string ProjectCode { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
        public Nullable<int> OnshoreHC { get; set; }
        public Nullable<int> OffShoreHC { get; set; }
    
        public virtual MST_Account MST_Account { get; set; }
        public virtual ICollection<Tran_Proj_ProjCode_Details> Tran_Proj_ProjCode_Details { get; set; }
    }
}
