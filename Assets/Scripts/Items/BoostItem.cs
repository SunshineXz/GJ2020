using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : EffectItem
{
    public override void Use(CharacterMovementController characterMovementController)
    {
        characterMovementController.SetMovementSpeedToSlow();
    }
}
