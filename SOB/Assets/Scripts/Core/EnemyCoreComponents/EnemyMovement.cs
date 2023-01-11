using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public bool knockback;
    public float knockbackStartTime;
    public float knockbackDuration { get => core.Unit.UnitData.knockBackDuration; set => core.Unit.UnitData.knockBackDuration = value; }
    public Vector2 knockbackSpeed { get => core.Unit.UnitData.knockBackSpeed; set => core.Unit.UnitData.knockBackSpeed = value; }

    protected override void Awake()
    {
        base.Awake();
    }

    public void KnockBack()
    {
        knockback = true;
        knockbackStartTime = Time.time;
        RB.velocity = new Vector2(knockbackSpeed.x * -FancingDirection, knockbackSpeed.y);
    }

    public void CheckKnockBack()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            knockback = false;
            RB.velocity = new Vector2(0.0f, RB.velocity.y);
        }
    }
}
