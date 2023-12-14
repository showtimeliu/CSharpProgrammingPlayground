using System;
using System.Threading;

class Program
{
    static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
    static bool stopThread = false;

    static void Main()
    {
        // Create a thread and start it
        Thread thread = new Thread(WorkerThread);
        thread.Start();

        // Wait for a key press to signal the event
        Console.WriteLine("Press any key to signal the event...");
        Console.ReadKey();

        // Signal the event
        manualResetEvent.Set();

        // Wait for another key press to reset the event
        Console.WriteLine("Press any key to reset the event...");
        Console.ReadKey();

        // Reset the event
        manualResetEvent.Reset();

        // Wait for another key press to signal the event again
        Console.WriteLine("Press any key to signal the event again...");
        Console.ReadKey();

        // Signal the event again
        manualResetEvent.Set();

        // Wait for a key press to exit
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        // Set the stopThread flag to true to exit the thread
        stopThread = true;

        // Set the event to release the thread from waiting
        manualResetEvent.Set();

        // Wait for the thread to complete
        thread.Join();
    }

    static void WorkerThread()
    {
        Console.WriteLine("Worker thread started.");

        while (!stopThread)
        {
            // Wait for the manual reset event to be signaled
            manualResetEvent.WaitOne();

            // Check if the event is signaled
            if (manualResetEvent.WaitOne(0))
            {
                Console.WriteLine("Event is signaled. Performing the action inside 'if'.");
                // Perform actions when the event is signaled

                // Simulate some work
                Thread.Sleep(2000);
            }
        }

        Console.WriteLine("Worker thread finished.");
        Console.ReadKey(); 
    }
}
