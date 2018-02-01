using System.Diagnostics;

namespace baiji
{
    public class DebugConsoleHandler : IProbeHandler
    {
        public void Handle(Probe probe)
        {
            Debug.WriteLine($"Gen0: {probe.Gen0Size / 1024}, Gen1: {probe.Gen1Size / 1024}, Gen2: {probe.Gen2Size / 1024}, LOH: {probe.LohSize / 1024}");
            Debug.WriteLine($"GCs: gen0:{probe.Gen0Gcs}, gen1:{probe.Gen1Gcs}, gen2:{probe.Gen2Gcs}, objects pinned: {probe.PinnedObjects}");
            Debug.WriteLine($"commited: {probe.CommitedSpace / 1024}, reserved: {probe.ReservedSpace / 1024}");
            Debug.WriteLine("\n\n\n");
        }
    }
}