using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace baiji.sample
{
      internal class Program
    {
        public static void Main(string[] args)
        {
            var mb = new MemoryBenchmark()
                .WithHandler(new DebugConsoleHandler())
                .WithInterval(5000)
                .WithProcessId(Process.GetCurrentProcess().Id);
            mb.Start();
            
            Thread.Sleep(600000);
            
            new Task(StupidAllocation1).Start();
            new Task(StupidAllocation2).Start();


            Console.ReadKey();
            mb.Dispose();
        }

       


        private static void StupidAllocation1()
        {
            var r = new Random();
            var ll = new List<long[]>();

            for (var i = 0; i < 100000; i++)
            {
                var l = new long[1000];
                ll.Add(l);
                if (i % 20 == 0)
                {
                    Console.WriteLine("Allocation #1 in progress");
                }
                Thread.Sleep(100);
            }
        }
        
        private static void StupidAllocation2()
        {
            var s = "efbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioef" +
                    "biufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioe" +
                    "fbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufeb" +
                    "feiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebui" +
                    "oefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofeb" +
                    "uioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuioefbiufebfeiofebuio";
            var sum = string.Empty;

            for (var i = 0; i < 100000; i++)
            {
                sum += s;
                if (i % 20 == 0)
                {
                    Console.WriteLine("Allocation #2 in progress");
                }
                Thread.Sleep(100);
            }
        }
    }
}
