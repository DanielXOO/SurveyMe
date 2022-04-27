using System.Threading.Tasks;
using SurveyMe.DomainModels;

namespace SurveyMe.Surveys.Foundation.Services.Account
{
    public interface IAccountService
    {
        Task<ServiceResult> SignInAsync(string username, string password);

        Task<ServiceResult> RegisterAsync(User user, string password);

        Task SignOutAsync();
    }
}