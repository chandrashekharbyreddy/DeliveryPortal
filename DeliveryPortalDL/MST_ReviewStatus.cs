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
    
    public partial class MST_ReviewStatus
    {
        public MST_ReviewStatus()
        {
            this.Tran_DE_Calendar = new HashSet<Tran_DE_Calendar>();
            this.Tran_Proj_DE_Attibute = new HashSet<Tran_Proj_DE_Attibute>();
        }
    
        public int ReviewStatusId { get; set; }
        public string ReviewStatusName { get; set; }
        public string ReviewStatusCode { get; set; }
    
        public virtual ICollection<Tran_DE_Calendar> Tran_DE_Calendar { get; set; }
        public virtual ICollection<Tran_Proj_DE_Attibute> Tran_Proj_DE_Attibute { get; set; }
    }
}
