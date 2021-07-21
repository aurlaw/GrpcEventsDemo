using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcEventsHost
{
	public static class CancellationTokenExtensions
	{
        public static async Task WaitUntilCancelled(this CancellationToken cancellationToken)
        {
            var taskCompletionSource =
              new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            using (
                cancellationToken.Register(
                    state => { ((TaskCompletionSource<object>)state).TrySetResult(null); },
                    taskCompletionSource,
                    false
                )
            )
            {
                await taskCompletionSource.Task.ConfigureAwait(false);
            }
        }
        
        public static TaskCompletionSource<object> CreateCancellable(this CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
            cancellationToken.Register(state => { ((TaskCompletionSource<object>)state).TrySetResult(null); },
                                    taskCompletionSource,
                                    false);

            return taskCompletionSource;
        }

    }
}
