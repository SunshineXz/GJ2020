using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : EffectItem
{
    public float movementSpeedMultiplier = 2.0f;
    public float boostTime = 2.0f;

    public override void Use(CharacterMovementController characterMovementController)
    {
        characterMovementController.SetMovementSpeedBoost(movementSpeedMultiplier, boostTime, gameObject);
    }
}
