using Aquedata.Model;

namespace Aquedata.Validation
{
    public class ValidationJobFactory : IValidationJobFactory
    {
        public ValidationJob Create(ValidationRequest request)
        {
            // todo maybe the job should take in the fully built data pipeline and we build that pipeline at request time?
            return new ValidationJob();
        }
    }
}