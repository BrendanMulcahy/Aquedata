using Aquedata.Model;

namespace Aquedata.Validation
{
    public interface IValidationJobFactory
    {
        ValidationJob Create(ValidationRequest request);
    }
}