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
    
    public partial class MST_Attributes
    {
        public MST_Attributes()
        {
            this.Tran_Proj_IDP_Attributes = new HashSet<Tran_Proj_IDP_Attributes>();
            this.Tran_IDP_Attributes = new HashSet<Tran_IDP_Attributes>();
        }
    
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public Nullable<System.DateTime> AttributeStartDate { get; set; }
        public Nullable<System.DateTime> AttributeEndDate { get; set; }
        public int AttributeTypeId { get; set; }
    
        public virtual MST_AttributeTypes MST_AttributeTypes { get; set; }
        public virtual ICollection<Tran_Proj_IDP_Attributes> Tran_Proj_IDP_Attributes { get; set; }
        public virtual ICollection<Tran_IDP_Attributes> Tran_IDP_Attributes { get; set; }
    }
}
