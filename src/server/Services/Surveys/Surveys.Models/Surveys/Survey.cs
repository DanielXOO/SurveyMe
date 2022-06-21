using Surveys.Models.Questions;

namespace Surveys.Models.Surveys;

public sealed class Survey
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public Guid AuthorId { get; set; }

    public ICollection<Question> Questions { get; set; }

    public bool RequirePersonInfo { get; set; }
}