using System;

namespace ZooWorld.CoreGamePlay
{
    [Flags]
    public enum CharacterTrait
    {
        //Base
        None = 0,

        //Movement
        CanRun = 11,
        CanJump = 12,
        CanCrawl = 13,
        CanSwim = 13,
        CanFly = 14,
        CanClimb = 15,

        //Damage
        DealsTouchDamage = 50,
    }
}
