using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Aquedata.Extensions
{
    public static class DataflowExtensions
    {
        public static DataflowLinkOptions PropagateCompletion = new DataflowLinkOptions {PropagateCompletion = true};

        public static ITargetBlock<T> CreateBatchedActionBlock<T>(int batchSize, Action<T[]> action)
        {
            var batchedBlock = new BatchBlock<T>(batchSize);
            var actionBlock = new ActionBlock<T[]>(action);

            batchedBlock.LinkTo(actionBlock, PropagateCompletion);

            return EncapsulateTarget(batchedBlock, actionBlock.Completion);
        }

        public static ITargetBlock<T> EncapsulateTarget<T>(ITargetBlock<T> target, Task completion)
        {
            return new TargetBlock<T>(target, completion);
        }

        public static ITargetBlock<T> ToTargetPipeline<T, T2>(IPropagatorBlock<T, T2> target, ITargetBlock<T2> other)
        {
            target.LinkTo(other, PropagateCompletion);
            return new TargetBlock<T>(target, other.Completion);
        }

        public static void CompleteWhenAllOrAnyFaulted(this IDataflowBlock block, List<Task> completions)
        {
            if (!completions.Any())
            {
                block.Complete();
            }

            Task<Task> task = Task.WhenAny(completions);

            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    block.Fault(t.Exception);
                }
                else
                {
                    var remainingTasks = completions.Where(c => c != t).ToList();
                    block.CompleteWhenAllOrAnyFaulted(remainingTasks);
                }
            });
        }
    }
}