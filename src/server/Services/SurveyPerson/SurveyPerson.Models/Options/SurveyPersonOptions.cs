namespace SurveyPerson.Models.Options;

public sealed class SurveyPersonOptions
{
    public Guid Id { get; set; }

    public Guid SurveyId { get; set; }

    public bool RequireFirstName { get; set; }

    public bool RequireSecondName { get; set; }

    public bool RequireGender { get; set; }

    public bool RequireAges { get; set; }
}