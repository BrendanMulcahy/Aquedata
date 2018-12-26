using Aquedata.DataflowCreation;
using Aquedata.Model;

namespace Aquedata.Validation
{
    public class ValidationJobFactory : IValidationJobFactory
    {
        private readonly IDataflowConfigurationFactory _factory;
        private readonly IDataflowBuilder _builder;

        public ValidationJobFactory(IDataflowConfigurationFactory factory, IDataflowBuilder builder)
        {
            _factory = factory;
            _builder = builder;
        }

        public ValidationJob Create(ValidationRequest request)
        {
            // todo maybe the job should take in the fully built data pipeline and we build that pipeline here at request time?
            return new ValidationJob(_factory, _builder);
        }
    }
}