namespace Answers.Models.Answers;

public class OptionQuestionAnswer
{
    public Guid Id { get; set; }
    
    public Guid OptionId { get; set; }

    public CheckboxQuestionAnswer CheckboxQuestionAnswer { get; set; }

    public Guid CheckboxAnswerId { get; set; }
}