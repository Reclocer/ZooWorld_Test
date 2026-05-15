using Sirenix.OdinInspector;
using SUBS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class TicksService : ServiceBase, ITicksService
{  
    [SerializeField,ReadOnly] 
    private Dictionary<TickPhase, Dictionary<float, TickData>> _ticks = new()
    {
        { TickPhase.Update, new Dictionary<float, TickData>() },
        { TickPhase.FixedUpdate, new Dictionary<float, TickData>() },
        { TickPhase.LateUpdate, new Dictionary<float, TickData>() }
    };

    private void FixedUpdate()
    {
        ProcessPhase(TickPhase.FixedUpdate, Time.fixedDeltaTime);
    }

    private void Update()
    {
        ProcessPhase(TickPhase.Update, Time.deltaTime);
    }    

    private void LateUpdate()
    {
        ProcessPhase(TickPhase.LateUpdate, Time.deltaTime);
    }

    private void ProcessPhase(TickPhase phase, float dt)
    {
        var phaseTicks = _ticks[phase];

        foreach (var pair in phaseTicks)
        {
            TickData tick = pair.Value;
            tick.Elapsed += dt;

            while (tick.Elapsed >= tick.Interval)
            {
                tick.Elapsed -= tick.Interval;

                try
                {
                    tick.Action?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }

    /// <summary>
    /// Add action to invoke every tick by seconds 
    /// </summary>
    public void AttachToTick(float sec, Action action, TickPhase phase = TickPhase.Update)
    {
        var phaseTicks = _ticks[phase];

        if (phaseTicks.TryGetValue(sec, out TickData tick))
        {
            if (tick.Action != null && tick.Action.GetInvocationList().Contains(action))
            {
                SLogger.Error($"[TickService] {phase} {sec}s duplicate action {action}");
                return;
            }

            tick.Action += action;
            return;
        }

        phaseTicks[sec] = new TickData
        {
            Interval = sec,
            Elapsed = 0f,
            Action = action
        };
    }

    /// <summary>
    /// Remove action from tick by seconds
    /// </summary>
    public void DisattachTick(float sec, Action action, TickPhase phase = TickPhase.Update)
    {
        var phaseTicks = _ticks[phase];

        if (!phaseTicks.TryGetValue(sec, out TickData tick))
            return;

        tick.Action -= action;

        if (tick.Action == null)
        {
            phaseTicks.Remove(sec);
        }
    }    
}
