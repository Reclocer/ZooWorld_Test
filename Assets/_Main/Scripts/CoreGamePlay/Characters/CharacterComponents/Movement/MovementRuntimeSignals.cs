namespace ZooWorld.CoreGamePlay
{
    public sealed class MovementRuntimeSignals
    {
        public bool ThreatDetected { get; set; }
        public bool InWater { get; set; }
        public bool UnderWater { get; set; }

        public void Reset()
        {
            ThreatDetected = false;
            InWater = false;
            UnderWater = false;
        }
    }
}
