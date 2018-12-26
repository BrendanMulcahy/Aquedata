using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Aquedata.Extensions
{
    public class TargetBlock<T> : ITargetBlock<T>
    {
        private readonly ITargetBlock<T> _target;

        public TargetBlock(ITargetBlock<T> target, Task completion)
        {
            _target = target;
            Completion = completion;
        }

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source,
            bool consumeToAccept)
        {
            return _target.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        public void Complete()
        {
            _target.Complete();
        }

        public void Fault(Exception exception)
        {
            _target.Fault(exception);
        }

        public Task Completion { get; }
    }
}