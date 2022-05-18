namespace SurveyMe.WebApplication.Models.ViewModels
{
    public sealed class UserDeleteOrEditViewModel
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string ReturnUrl { get; set; }
    }
}