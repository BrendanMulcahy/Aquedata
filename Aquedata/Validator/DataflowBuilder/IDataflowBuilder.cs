using System.Threading.Tasks.Dataflow;

namespace Aquedata.Validator.DataflowBuilder
{
    public interface IDataflowBuilder
    {
        ITargetBlock<string> Build(DataflowBuilderConfiguration config);
    }
}