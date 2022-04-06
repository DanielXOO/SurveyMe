using System;

namespace SurveyMe.Surveys.Foundation.Exceptions;

public class ForbidException : Exception
{
    public ForbidException() : base() { }
    
    public ForbidException(string message) : base(message) { }
    
    public ForbidException(string message, Exception inner) : base(message, inner) { }
}