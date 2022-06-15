using Authentication.Users;

namespace Authentication.Services.Abstracts;

public interface IAccountService
{

    Task<ServiceResult> RegisterAsync(User user, string password);
}