using Mosahm.Domain.Enums;
using System.Text.Json;

namespace Mosahm.Domain.Entities
{
    public class Question : BaseEntity
    {
        public int Order {  get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public JsonDocument? Options { get; set; }
        public bool IsRequired { get; set; }
        public Guid OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public ICollection<QuestionAnswer>? QuestionAnswers { get; set; }

    }
}
