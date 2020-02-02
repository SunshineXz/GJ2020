using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    static GlobalVariables instance;

    public static GlobalVariables GlobalVariablesInstance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<GlobalVariables>();
            }
            return instance;
        }
    }

    public float PLAYER_MOVEMENT_SPEED = 15F;

    public float SLAP_STUN_DURATION =1F;
    public float SLAP_COOLDOWN_TIME = 2F;
    public float SHOOT_STUN_DURATION = 2F;
    public Vector3 SHOOT_BASE_SCALE = new Vector3(1,1,1);
    public float SHOOT_BASE_SPEED = 2.5F;
    public float SHOOT_COOLDOWN_TIME = 2F;
    public float SLOW_SPEED_MULTIPLIER = 0.25F;
    public float SLOW_TIME = 0.5F;
    
    public float SPEED_UP_MOVEMENT_BONUS = 1.5F;
    public float SPEED_UP_DURATION = 3F;
    public int ATTACK_UP_SIZE = 2;
    public float ATTACK_UP_SPEED_BONUS = 2F;
    public float ATTACK_UP_STUN_DURATION = 2F;
    public float DEFENSE_UP_DURATION = 2F;
    public float BULLET_TIME_DURATION = 5F;
    public float BULLET_TIME_REDUCED_COOLDOWN = 0.5F;
}
