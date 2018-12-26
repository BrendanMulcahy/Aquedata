using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Aquedata.DataflowCreation;
using Aquedata.Model;

namespace Aquedata.Validation
{
    public class ValidationJob
    {
        private readonly IDataflowConfigurationFactory _configFactory;
        private readonly IDataflowBuilder _builder;

        public ValidationJob(IDataflowConfigurationFactory configFactory, IDataflowBuilder builder)
        {
            _configFactory = configFactory;
            _builder = builder;
        }

        public async Task Execute(ValidationRequest request)
        {
            var config = _configFactory.GetConfiguration(request.Format);
            var dataflow = _builder.Build(config);

            var accepted = dataflow.Post(request.Location);
            if (!accepted)
            {
                // Since this is the first/only message, this should never happen unless the dataflow is misconfigured
                throw new Exception("Could not post the initial message to the dataflow");
            }

            dataflow.Complete();
            await dataflow.Completion;
        }
    }
}