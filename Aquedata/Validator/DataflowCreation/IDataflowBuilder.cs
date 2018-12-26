using System.Threading.Tasks.Dataflow;

namespace Aquedata.Validator.DataflowCreation
{
    public interface IDataflowBuilder
    {
        ITargetBlock<string> Build(DataflowConfiguration config);
    }
}