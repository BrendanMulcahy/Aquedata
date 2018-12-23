using System.Threading.Tasks.Dataflow;

namespace Aquedata.Dataflow
{
    public interface IDataflowBuilder
    {
        ITargetBlock<string> Build(DataflowConfiguration config);
    }
}