using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExceptionHandlingDemo
{
    internal class Program
    {
        private static void CurrentDomain_UnhandledException( object sender, UnhandledExceptionEventArgs e )
        {
            Console.WriteLine( $"Unhandled exception occurs {e.ExceptionObject}." );
        }

        private static Task DoWork( )
        {
            return Task.Run( ( ) => throw new Exception( "DoWork failed." ) );
        }

        private static Task<int> DoWork2( )
        {
            return Task.Run( ( ) =>
            {
                if ( false )
                    return 3;
                throw new Exception( "DoWork2 failed." );
            } );
        }

        private static void Main( string[ ] args )
        {
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var t = DoWork( );
            //var t2 = DoWork( );
            //var array = new[ ] { t, t2 };

            //try
            //{
            //    await Task.WhenAll( array );
            //}
            //catch ( Exception e )
            //{
            //    Console.WriteLine( $"Main exception occurs {e}." );
            //    //throw;
            //}
            //t.Wait( );
            //var result = DoWork2( );
            t = null;
            Thread.Sleep( 1000 );
            GC.Collect( );
            GC.WaitForPendingFinalizers( );

            Console.ReadKey( );
        }

        private static void TaskScheduler_UnobservedTaskException( object sender, UnobservedTaskExceptionEventArgs e )
        {
            e.SetObserved( );
            Console.WriteLine( $"Task unobservedTaskExeption occurs {e.Exception}." );
        }
    }
}