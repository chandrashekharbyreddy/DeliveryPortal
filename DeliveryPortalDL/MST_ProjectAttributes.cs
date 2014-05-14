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
    
    public partial class MST_ProjectAttributes
    {
        public MST_ProjectAttributes()
        {
            this.MST_ProjectAttributes1 = new HashSet<MST_ProjectAttributes>();
            this.MST_ReviewQuestion = new HashSet<MST_ReviewQuestion>();
            this.Tran_Proj_DE_Attibute = new HashSet<Tran_Proj_DE_Attibute>();
            this.Tran_Proj_Wkly_Attributes_Status = new HashSet<Tran_Proj_Wkly_Attributes_Status>();
        }
    
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public Nullable<int> ParentAttributeId { get; set; }
        public Nullable<bool> IsDE { get; set; }
        public Nullable<System.DateTime> EffectiveStartDate { get; set; }
        public Nullable<System.DateTime> EffectiveEndDate { get; set; }
        public string SampleQuestions { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        public virtual ICollection<MST_ProjectAttributes> MST_ProjectAttributes1 { get; set; }
        public virtual MST_ProjectAttributes MST_ProjectAttributes2 { get; set; }
        public virtual ICollection<MST_ReviewQuestion> MST_ReviewQuestion { get; set; }
        public virtual ICollection<Tran_Proj_DE_Attibute> Tran_Proj_DE_Attibute { get; set; }
        public virtual ICollection<Tran_Proj_Wkly_Attributes_Status> Tran_Proj_Wkly_Attributes_Status { get; set; }
    }
}