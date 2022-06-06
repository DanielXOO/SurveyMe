using Authentication.Api.Models.Users;

namespace Authentication.Api.Services.Abstracts;

public interface IAccountService
{

    Task<ServiceResult> RegisterAsync(User user, string password);
}