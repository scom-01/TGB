using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;

    //Movement
    public event Action OnFixedStartMovement;
    public event Action OnFixedStopMovement;
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    public event Action OnStartFlip;
    public event Action OnStopFlip;

    public event Action OnTeleportToTarget;

    //Action
    public event Action OnAttackAction;
    public event Action OnActionRectOn;
    public event Action OnActionRectOff;
    public event Action OnShootProjectile;
    
    //Effect
    public event Action OnEffectSpawn;

    //Sound
    public event Action OnSoundClip;

    //Cam
    public event Action OnShakeCam;

    //State
    public event Action OnStartInvincible;
    public event Action OnStopInvincible;

    public void AnimationFinishedTrigger() => OnFinish?.Invoke();
    public void FixedStartMovementTrigger() => OnFixedStartMovement?.Invoke();
    public void FixedStopMovementTrigger() => OnFixedStopMovement?.Invoke();
    public void TeleportToTargetTrigger() => OnTeleportToTarget?.Invoke();
    public void StartMovementTrigger() => OnStartMovement?.Invoke();
    public void StopMovementTrigger() => OnStopMovement?.Invoke();
    public void StartFlipTrigger() => OnStartFlip?.Invoke();
    public void StopFlipTrigger() => OnStopFlip?.Invoke();
    public void AttackActionTrigger() => OnAttackAction?.Invoke();
    public void StartActionRectTrigger() => OnActionRectOn?.Invoke();
    public void StopActionRectTrigger() => OnActionRectOff?.Invoke();
    public void ShootProjectileTrigger() => OnShootProjectile?.Invoke();
    public void SpawnEffectTrigger() => OnEffectSpawn?.Invoke();
    public void SpawnSoundClipTrigger() => OnSoundClip?.Invoke();
    public void ShakeCamTrigger() => OnShakeCam?.Invoke();
    public void StartInvincibleTrigger() => OnStartInvincible?.Invoke();
    public void StopInvincibleTrigger() => OnStopInvincible?.Invoke();
}
