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
    
    public partial class Tran_IDP_Attributes
    {
        public int IDPAttributeId { get; set; }
        public int IDPId { get; set; }
        public int AttributeId { get; set; }
    
        public virtual MST_Attributes MST_Attributes { get; set; }
        public virtual MST_IDP MST_IDP { get; set; }
    }
}
