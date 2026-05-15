using System;

public interface ITicksService
{
    /// <summary>
    /// Add action to invoke every tick by seconds 
    /// </summary>
    void AttachToTick(float sec, Action action, TickPhase phase = TickPhase.Update);

    /// <summary>
    /// Remove action from tick by seconds
    /// </summary>
    void DisattachTick(float sec, Action action, TickPhase phase = TickPhase.Update);
}
