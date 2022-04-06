using System;

namespace SurveyMe.Surveys.Foundation.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string message) : base(message) { }
    
    public NotFoundException(string message, Exception inner) : base(message, inner) { }
}