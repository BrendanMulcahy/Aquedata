using System;
using System.Threading.Tasks.Dataflow;

namespace Aquedata.Validator.DataflowCreation
{
    public class DataflowBuilder : IDataflowBuilder
    {
        public ITargetBlock<string> Build(DataflowConfiguration config)
        {
            return new ActionBlock<string>(s => Console.WriteLine(s));
        }
    }
}