namespace ZooWorld.CoreGamePlay
{
    public static class MovementTraits
    {
        /// <summary>
        /// Maps movement type to required character traits
        /// </summary>
        public static CharacterTrait ToTrait(MovementType locomotionType)
        {
            return locomotionType switch
            {
                MovementType.Walk or MovementType.Idle => CharacterTrait.CanWalk,
                MovementType.Run => CharacterTrait.CanRun | CharacterTrait.CanWalk,
                MovementType.Jump => CharacterTrait.CanJump | CharacterTrait.CanWalk,
                MovementType.Crawl => CharacterTrait.CanCrawl,
                MovementType.Swim => CharacterTrait.CanSwim,
                MovementType.Dive => CharacterTrait.CanSwim,
                MovementType.Fly => CharacterTrait.CanFly,
                MovementType.Climb => CharacterTrait.CanClimb,
                _ => CharacterTrait.None,
            };
        }
    }
}
