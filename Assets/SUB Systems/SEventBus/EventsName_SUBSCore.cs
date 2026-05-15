namespace SUBS.Core.EventBus
{
    public static class EventsName_SUBSCore
    {
        public const string SIMULATION_SPEED_CHANGED = "SimulationSpeedChanged";
        public const string SIMULATION_SPEED_PAUSED = "SimulationSpeedPaused";
        public const string SIMULATION_SPEED_UNPAUSED = "SimulationSpeedUnPaused";

        public const string CAT_PREPAIR_TO_START_COMPLETE = "CAT_prepareToStartComplete";
        public const string CAT_STARTED = "CAT_started";
        public const string CAT_ON_ERROR_PAUSE = "CAT_onErrorPause";
        public const string CAT_STAGE_SKIPED = "CAT_stageSkiped";
        public const string CAT_STAGE_COMPLETE = "CAT_stageComplete";
        public const string CAT_PAUSED = "CAT_paused";
        public const string CAT_TEST_CANCELED = "CAT_testCanceled";
        public const string CAT_TEST_COMPLETE = "CAT_testComplete";
    }
}