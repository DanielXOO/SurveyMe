using System.Threading.Tasks;
using SurveyMe.DomainModels.Users;

namespace SurveyMe.Foundation.Services.Abstracts;

public interface IAccountService
{
    Task<string> SignInAsync(string username, string password);

    Task<ServiceResult> RegisterAsync(User user, string password);
}