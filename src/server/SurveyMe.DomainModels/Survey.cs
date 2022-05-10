namespace SurveyMe.DomainModels;

public sealed class Survey
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public User Author { get; set; }

    public Guid AuthorId { get; set; }

    public ICollection<Question> Questions { get; set; }

    public ICollection<SurveyAnswer> Answers { get; set; }
}