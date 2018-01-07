using System;
using System.Threading.Tasks;

namespace TaskExceptionHandlingDemo
{
    public static class TaskExtensions
    {
        public static Task FailFastOnException( this Task task )
        {
            task.ContinueWith( c => Environment.FailFast( "Task faulted", c.Exception ),
                TaskContinuationOptions.OnlyOnFaulted );
            return task;
        }

        public static Task IgnoreExceptions( this Task task )
        {
            task.ContinueWith( c => Console.WriteLine( c.Exception ),
                TaskContinuationOptions.OnlyOnFaulted );
            return task;
        }
    }
}