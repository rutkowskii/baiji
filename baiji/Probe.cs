using System.Text;

namespace baiji
{
    
    /*
     * total .net: gen counters put together
     * total unmanaged: private bytes - total.net
     * 
     */
    
    public class Probe
    {
        public readonly long Gen0Size;
        public readonly long Gen1Size;
        public readonly long Gen2Size;
        public readonly long LohSize;
        public readonly long PinnedObjects;
        public readonly long CommitedSpace;
        public readonly long ReservedSpace;
        public readonly long Gen0Gcs;
        public readonly long Gen1Gcs;
        public readonly long Gen2Gcs;
        public readonly long UtcTicks;

        public Probe(
            long gen0Size,
            long gen1Size,
            long gen2Size,
            long lohSize,
            long pinnedObjects,
            long commitedSpace,
            long reservedSpace,
            long gen0Gcs,
            long gen1Gcs,
            long gen2Gcs,
            long utcTicks)
        {
            this.Gen0Size = gen0Size;
            this.Gen1Size = gen1Size;
            this.Gen2Size = gen2Size;
            this.LohSize = lohSize;
            this.PinnedObjects = pinnedObjects;
            this.CommitedSpace = commitedSpace;
            this.ReservedSpace = reservedSpace;
            this.Gen0Gcs = gen0Gcs;
            this.Gen1Gcs = gen1Gcs;
            this.Gen2Gcs = gen2Gcs;
            this.UtcTicks = utcTicks;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"UtcTicks: {this.UtcTicks},");
            sb.Append($"Gen0Size: {this.Gen0Size},");
            sb.Append($"Gen1Size: {this.Gen1Size},");
            sb.Append($"Gen2Size: {this.Gen2Size},");
            sb.Append($"LohSize: {this.LohSize},");
            sb.Append($"PinnedObjects: {this.PinnedObjects},");
            sb.Append($"CommitedSpace: {this.CommitedSpace},");
            sb.Append($"ReservedSpace: {this.ReservedSpace},");
            sb.Append($"Gen0Gcs: {this.Gen0Gcs},");
            sb.Append($"Gen1Gcs: {this.Gen1Gcs},");
            sb.Append($"Gen2Gcs: {this.Gen2Gcs},");
            return sb.ToString();
        }
    }
}
