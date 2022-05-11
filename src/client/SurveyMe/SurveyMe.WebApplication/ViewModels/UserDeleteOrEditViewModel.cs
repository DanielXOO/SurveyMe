namespace SurveyMe.WebApplication.ViewModels
{
    public sealed class UserDeleteOrEditViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ReturnUrl { get; set; }
    }
}