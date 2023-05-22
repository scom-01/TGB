using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnFixedStartMovement;
    public event Action OnFixedStopMovement;
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    public event Action OnStartFlip;
    public event Action OnStopFlip;
    public event Action OnAttackAction;
    public event Action OnEffectSpawn;
    public event Action OnSoundClip;
    public event Action OnShakeCam;


    public void AnimationFinishedTrigger() => OnFinish?.Invoke();
    public void FixedStartMovementTrigger() => OnFixedStartMovement?.Invoke();
    public void FixedStopMovementTrigger() => OnFixedStopMovement?.Invoke();
    public void StartMovementTrigger() => OnStartMovement?.Invoke();
    public void StopMovementTrigger() => OnStopMovement?.Invoke();
    public void StartFlipTrigger() => OnStartFlip?.Invoke();
    public void StopFlipTrigger() => OnStopFlip?.Invoke();
    public void AttackActionTrigger() => OnAttackAction?.Invoke();
    public void SpawnEffectTrigger() => OnEffectSpawn?.Invoke();
    public void SpawnSoundClipTrigger() => OnSoundClip?.Invoke();
    public void ShakeCamTrigger() => OnShakeCam?.Invoke();
}
