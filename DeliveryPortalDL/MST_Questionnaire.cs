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
    
    public partial class MST_Questionnaire
    {
        public MST_Questionnaire()
        {
            this.MST_ReviewQuestion = new HashSet<MST_ReviewQuestion>();
        }
    
        public int QuestionnaireId { get; set; }
        public int IDPId { get; set; }
        public string QuestionnaireName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int ReviewTypeId { get; set; }
    
        public virtual MST_IDP MST_IDP { get; set; }
        public virtual MST_ReviewType MST_ReviewType { get; set; }
        public virtual ICollection<MST_ReviewQuestion> MST_ReviewQuestion { get; set; }
    }
}