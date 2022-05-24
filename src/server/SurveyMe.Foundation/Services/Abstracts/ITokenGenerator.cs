using System.Threading.Tasks;
using SurveyMe.DomainModels.Users;

namespace SurveyMe.Foundation.Services.Abstracts;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}