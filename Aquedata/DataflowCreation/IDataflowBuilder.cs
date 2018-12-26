using System.Threading.Tasks.Dataflow;

namespace Aquedata.DataflowCreation
{
    public interface IDataflowBuilder
    {
        ITargetBlock<string> Build(DataflowConfiguration config);
    }
}