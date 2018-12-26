using Aquedata.Model;

namespace Aquedata.Validator.Controller
{
    public interface IValidationJobFactory
    {
        ValidationJob Create(ValidationRequest request);
    }
}