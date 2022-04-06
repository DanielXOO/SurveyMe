using System;
using System.Collections.Generic;

namespace SurveyMe.Surveys.Foundation.Exceptions;

public sealed class BadRequestException : Exception
{
    public BadRequestException() : base() { }
    
    public BadRequestException(string message) : base(message) { }
    
    public BadRequestException(string message, Exception inner) : base(message, inner) { }
}