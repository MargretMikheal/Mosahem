using Mosahm.Domain.Entities.Profiles;
using System.Text.Json;

namespace Mosahm.Domain.Entities.Questions
{
    public class QuestionAnswer : BaseEntity
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public string? AnswerText { get; set; } 
        public int? ChoiceKey { get; set; }
        public JsonDocument? Json { get; set; }
    }
}
