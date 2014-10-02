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
            string s = null;
            using (WaitForSendMessageResult wfsmr = new WaitForSendMessageResult())
            {
                wfsmr.Start(name);
                s = wfsmr.WaitForResult();
            }

            if (s.Contains(name))
                Console.Write(".");
            else
                Console.WriteLine("\nFailure: ThreadId=" + Thread.CurrentThread.ManagedThreadId);
        }

        class WaitForSendMessageResult : IDisposable
        {
            IAsyncResult ar_;
            EventWaitHandle ewh_ = new EventWaitHandle(false, EventResetMode.ManualReset);

            public void Start(string name)
            {
                ar_ = client.BeginSendMessage(name, Callback, this);
            }

            void Callback(IAsyncResult ar)
            {
                ewh_.Set();
            }

            public string WaitForResult()
            {
                ewh_.WaitOne();
                return client.EndSendMessage(ar_);
            }

            public void Dispose()
            {
                ewh_.Dispose();
                ewh_ = null;
            }
        }
    }
}
