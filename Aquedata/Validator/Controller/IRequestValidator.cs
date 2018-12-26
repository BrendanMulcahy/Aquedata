using Aquedata.Model;

namespace Aquedata.Validator.Controller
{
    public interface IRequestValidator
    {
        RequestValidationResult Validate(ValidationRequest request);
    }
}