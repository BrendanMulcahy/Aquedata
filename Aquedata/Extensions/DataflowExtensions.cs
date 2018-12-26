using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Aquedata.Extensions
{
    public static class DataflowExtensions
    {
        public static ITargetBlock<T> CreateBatchedActionBlock<T>(int batchSize, Action<T[]> action)
        {
            var batchedBlock = new BatchBlock<T>(batchSize);
            var actionBlock = new ActionBlock<T[]>(action);

            batchedBlock.LinkTo(actionBlock, PropagateCompletion);

            return EncapsulateTarget(batchedBlock, actionBlock.Completion);
        }

        public static DataflowLinkOptions PropagateCompletion = new DataflowLinkOptions { PropagateCompletion = true };

        public static ITargetBlock<T> EncapsulateTarget<T>(ITargetBlock<T> target, Task completion)
        {
            return new TargetBlock<T>(target, completion);
        }
    }
}