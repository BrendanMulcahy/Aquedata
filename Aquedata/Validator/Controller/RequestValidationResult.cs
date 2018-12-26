using System;

namespace Aquedata.Validator.Controller
{
    public class RequestValidationResult
    {
        public RequestValidationResult()
        {
            IsValid = true;
            InvalidReason = String.Empty;
        }

        public RequestValidationResult(string invalidReason)
        {
            IsValid = false;
            InvalidReason = invalidReason;
        }

        public bool IsValid { get; }

        public string InvalidReason { get; }
    }
}