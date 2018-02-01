namespace baiji
{
    public class LogFileHandler : IProbeHandler
    {
        public LogFileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public void Handle(Probe probe)
        {
            System.IO.File.AppendAllLines(this.filePath, new[] {probe.ToString()});
        }

        private readonly string filePath;
    }
}