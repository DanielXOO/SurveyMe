namespace Authentication.Services;

public sealed class ServiceResult
{
    private readonly IReadOnlyCollection<string> _errorMessages;


    public bool IsSuccessful { get; }

    public IReadOnlyCollection<string> ErrorMessages
    {
        get
        {
            if (!IsSuccessful)
            {
                return _errorMessages;
            }

            throw new InvalidOperationException("No error messages. Result is successful");
        }
    }


    private ServiceResult(bool isSuccessful, IReadOnlyCollection<string> errorMessages)
    {
        IsSuccessful = isSuccessful;
        _errorMessages = errorMessages;
    }


    public static ServiceResult CreateSuccessful()
    {
        return new ServiceResult(true, null);
    }

    public static ServiceResult CreateFailed(IReadOnlyCollection<string> errorMessages)
    {
        if (errorMessages == null || !errorMessages.Any())
        {
            throw new ArgumentOutOfRangeException(nameof(errorMessages), "No error messages");
        }

        return new ServiceResult(false, errorMessages);
    }
}