using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace baiji
{
    public class MemoryBenchmark : IDisposable
    {
        public MemoryBenchmark()
        {
            this.interval = 5000;
        }

        public MemoryBenchmark WithInterval(int milliseconds)
        {
            this.interval = milliseconds;
            return this;
        }

        public MemoryBenchmark WithInstanceName(string instanceName)
        {
            this.instanceName = instanceName;
            return this;
        }
        
        public MemoryBenchmark WithProcessId(int pid)
        {
            this.instanceName = ProcessUtils.GetProcessInstanceName(pid);
            return this;
        }

        public MemoryBenchmark WithHandler(IProbeHandler handler)
        {
            this.handler = handler;
            return this;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
            this.gen0counter.Dispose();
            this.gen1counter.Dispose();
            this.gen2counter.Dispose();
            this.lohCounter.Dispose();
            this.pinnedObjectsCounter.Dispose();
            this.commitedSpaceCounter.Dispose();
            this.reservedSpaceCounter.Dispose();
            this.gen0GcsCounter.Dispose();
            this.gen1GcsCounter.Dispose();
            this.gen2GcsCounter.Dispose();
        }

        public void Start()
        {
            this.PrepareCounters();
            this.PrepareTimer();
            this.timer.Start();
        }
        
        private void PrepareTimer()
        {
            Debug.WriteLine("creating the timer in thread #" + Thread.CurrentThread.ManagedThreadId);
            
            this.timer = new Timer
            {
                Enabled = true,
                AutoReset = true,
                Interval = this.interval
            };
            this.timer.Elapsed += this.TimerOnElapsed;
        }
        
        private void PrepareCounters()
        {
            this.gen0counter = new PerformanceCounter(".NET CLR Memory", "Gen 0 heap size", this.instanceName);
            this.gen1counter = new PerformanceCounter(".NET CLR Memory", "Gen 1 heap size", this.instanceName);
            this.gen2counter = new PerformanceCounter(".NET CLR Memory", "Gen 2 heap size", this.instanceName);
            this.lohCounter = new PerformanceCounter(".NET CLR Memory", "Large Object Heap size", this.instanceName);

            this.pinnedObjectsCounter = new PerformanceCounter(".NET CLR Memory", "# of Pinned Objects", this.instanceName);
            this.gen0GcsCounter = new PerformanceCounter(".NET CLR Memory", "# Gen 0 Collections", this.instanceName);
            this.gen1GcsCounter = new PerformanceCounter(".NET CLR Memory", "# Gen 1 Collections", this.instanceName);
            this.gen2GcsCounter = new PerformanceCounter(".NET CLR Memory", "# Gen 2 Collections", this.instanceName);

            this.commitedSpaceCounter = new PerformanceCounter(".NET CLR Memory", "# Total committed Bytes", this.instanceName);
            this.reservedSpaceCounter = new PerformanceCounter(".NET CLR Memory", "# Total reserved Bytes", this.instanceName);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var probe = new Probe(
                this.gen0counter.RawValue,
                this.gen1counter.RawValue,
                this.gen2counter.RawValue,
                this.lohCounter.RawValue,
                this.pinnedObjectsCounter.RawValue,
                this.commitedSpaceCounter.RawValue,
                this.reservedSpaceCounter.RawValue,
                this.gen0GcsCounter.RawValue,
                this.gen1GcsCounter.RawValue,
                this.gen2GcsCounter.RawValue,
                DateTime.UtcNow.Ticks
            );

            this.handler?.Handle(probe);
        }

        private Timer timer;
        private PerformanceCounter gen0counter;
        private PerformanceCounter gen1counter;
        private PerformanceCounter gen2counter;
        private PerformanceCounter lohCounter;
        private PerformanceCounter pinnedObjectsCounter;
        private PerformanceCounter commitedSpaceCounter;
        private PerformanceCounter reservedSpaceCounter;
        private PerformanceCounter gen0GcsCounter;
        private PerformanceCounter gen1GcsCounter;
        private PerformanceCounter gen2GcsCounter;
        private int interval;
        private IProbeHandler handler;
        private string instanceName;
    }
}
