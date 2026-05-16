namespace ZooWorld.CoreGamePlay
{
    public enum MovementTransitionCondition
    {
        None = 0,
        ThreatDetected = 1,
        ThreatCleared = 2,
        EnteredWater = 3,
        ExitedWater = 4,
        InWater = 5,
        UnderWater = 6,
        LeftUnderWater = 7,
        Manual = 8,
    }
}
