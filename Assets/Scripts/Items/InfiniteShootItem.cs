using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteShootItem : EffectItem
{
    public float buffTime = 5.0f;

    public override void Use(CharacterMovementController characterMovementController)
    {
        characterMovementController.SetInfiniteShoot(buffTime);
    }
}
