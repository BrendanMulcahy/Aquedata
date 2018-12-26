using System;
using System.Threading.Tasks.Dataflow;

namespace Aquedata.DataflowCreation
{
    public class DataflowBuilder : IDataflowBuilder
    {
        public ITargetBlock<string> Build(DataflowConfiguration config)
        {
            return new ActionBlock<string>(s => Console.WriteLine(s));
        }
    }
}