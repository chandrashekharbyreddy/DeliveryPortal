using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class ReviewQuestionModel
    {
        public int QuestionId { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionDescription { get; set; }
        public string QuestionGuideLines { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public string QuestionnaireName { get; set; }
    }
}
