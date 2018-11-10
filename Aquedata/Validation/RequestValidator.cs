using Aquedata.Model;

namespace Aquedata.Validation
{
    public class RequestValidator : IRequestValidator
    {
        public RequestValidationResult Validate(ValidationRequest request)
        {
            // todo validate format is available
            if (string.IsNullOrWhiteSpace(request.Format))
            {
                return new RequestValidationResult($"{nameof(request.Format)} is null or whitespace.");
            }

            // todo validate path
            if (string.IsNullOrWhiteSpace(request.Location))
            {
                return new RequestValidationResult($"{nameof(request.Location)} is null or whitespace.");
            }

            return new RequestValidationResult();
        }
    }
}