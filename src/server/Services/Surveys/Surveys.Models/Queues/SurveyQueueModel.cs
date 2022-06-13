namespace Surveys.Models.Queue;

public class SurveyQueueModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public EventType EventType { get; set; }
}