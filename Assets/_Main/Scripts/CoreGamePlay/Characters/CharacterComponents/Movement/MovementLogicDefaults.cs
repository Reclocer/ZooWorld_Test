namespace ZooWorld.CoreGamePlay
{
    public static class MovementLogicDefaults
    {
        private static readonly GroundPlanarMovementLogic GroundPlanar = new();
        private static readonly ThreeDimensionalMovementLogic ThreeDimensional = new();

        public static MovementLogic Resolve(MovementType locomotionType)
        {
            return locomotionType is MovementType.Swim
                or MovementType.Dive
                or MovementType.Fly
                ? ThreeDimensional
                : GroundPlanar;
        }
    }
}
