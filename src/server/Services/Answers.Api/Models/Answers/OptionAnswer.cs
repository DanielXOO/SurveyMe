namespace Answers.Api.Models.Answers;

public class OptionAnswer
{
    public Guid Id { get; set; }
    
    public Guid OptionId { get; set; }

    public CheckboxAnswer CheckboxAnswer { get; set; }

    public Guid CheckboxAnswerId { get; set; }
}