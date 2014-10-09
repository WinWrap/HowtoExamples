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

            List<WaitForSendMessageResult> waits = new List<WaitForSendMessageResult>();
            for (int i = 0; i < 10; ++i)
                waits.Add(new WaitForSendMessageResult("Tom" + i));

            // start all waits
            Console.WriteLine("Start all requests...");
            waits.All(wait => { wait.Start(); return true; });

            while (!waits.All(wait => wait.Done))
                Thread.Sleep(100);

            waits.All(wait =>)
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
            EventWaitHandle ewh_;
            IAsyncResult ar_;

            public enum WaitState { Idle, Started, Done };
            public string Name { get; private set; }
            public WaitState State { get; private set; }

            public WaitForSendMessageResult(string name)
            {
                Name = name;
                State = WaitState.Idle;
            }

            public void Start()
            {
                if (State == WaitState.Started)
                    throw new Exception("Already started");

                State = WaitState.Started;
                ewh_ = new EventWaitHandle(false, EventResetMode.ManualReset);
                ar_ = client.BeginSendMessage(Name, Callback, this);
            }

            void Callback(IAsyncResult ar)
            {
                ewh_.Set();
            }

            public string WaitForResult()
            {
                if (State != WaitState.Started)
                    throw new Exception("Not started");

                ewh_.WaitOne();
                string result = client.EndSendMessage(ar_);
                State = WaitState.Done;
                return result;
            }

            public void Dispose()
            {
                State = WaitState.Idle;
                if (ewh_ != null)
                {
                    ewh_.Dispose();
                    ewh_ = null;
                }

                ar_ = null;
            }
        }
    }
}
