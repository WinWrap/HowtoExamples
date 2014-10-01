using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMessengerService
{
    class Program
    {
        static EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
        static MessengerServiceClient client;

        static void Main(string[] args)
        {
            client = new MessengerServiceClient();
            // Use the 'client' variable to call operations on the service.

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 100; ++i)
            {
                Thread thread = new Thread(SendMessageThread);
                thread.Start();
                threads.Add(thread);
            }

            Console.WriteLine("Start all threads...");

            // allow all threads to commence
            ewh.Set();

            // wait for all threads to finish
            threads.All(thread => { thread.Join(); return true; });

            Console.WriteLine("Finished.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            // Always close the client.
            client.Close();
        }

        static void SendMessageThread()
        {
            // wait for synchronized start
            ewh.WaitOne();
            string name = "Tom" + Thread.CurrentThread.ManagedThreadId;
            string s = client.SendMessage(name);
            if (!s.Contains(name))
                Console.WriteLine("\nFailure: ThreadId=" + Thread.CurrentThread.ManagedThreadId);
            else
                Console.Write(".");
        }
    }
}
