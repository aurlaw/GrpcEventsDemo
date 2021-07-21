using System;
using System.Threading.Tasks;

namespace GrpcEventsHost
{
    public static class TaskExtension
    {
        public static async Task FireAndRunOnExceptionAsync(this Task task, Action<Exception> callback)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                callback.Invoke(e);
            }
        }        
    }
}