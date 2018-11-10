using Aquedata.Model;

namespace Aquedata.Validation
{
    public interface IRequestValidator
    {
        RequestValidationResult Validate(ValidationRequest request);
    }
}