using Aquedata.Model;

namespace Aquedata.Validation
{
    public interface IRequestValidator
    {
        bool Validate(ValidationRequest value);
    }
}