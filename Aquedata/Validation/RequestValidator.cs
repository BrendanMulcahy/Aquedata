using Aquedata.Model;

namespace Aquedata.Validation
{
    public class RequestValidator : IRequestValidator
    {
        public bool Validate(ValidationRequest value)
        {
            return true;
        }
    }
}