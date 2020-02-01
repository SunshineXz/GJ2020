using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : EffectItem
{
    public float shieldTime = 2.0f;

    public override void Use(CharacterMovementController characterMovementController)
    {
        characterMovementController.SetShieldUp(shieldTime);
    }
}
