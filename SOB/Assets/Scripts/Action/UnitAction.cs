using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitAction : MonoBehaviour
{
    [HideInInspector]
    protected string actionAnimBoolStr;
    public Core UnitCore;


    protected Animator baseAnimator;
    public GameObject BaseGameObject;
    public GameObject ActionSpriteGameObject;

    public AnimationEventHandler EventHandler;
}