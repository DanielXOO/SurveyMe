
using SurveyMe.DomainModels;

namespace SurveyMe.Foundation.Services.Abstracts;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}