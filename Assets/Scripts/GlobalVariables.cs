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

    public float PLAYER_MOVEMENT_SPEED = 10F;

    public float SLAP_STUN_DURATION =2F;
    public float SLAP_COOLDOWN_TIME = 2F;
    public float SHOOT_STUN_DURATION = 2F;
    public Vector3 SHOOT_BASE_SCALE;
    public float SHOOT_BASE_SPEED = 2F;
    public float SHOOT_COOLDOWN_TIME = 2F;
    
    public float SPEED_UP_MOVEMENT_BONUS = 2F;
    public float SPEED_UP_DURATION = 2F;
    public int ATTACK_UP_SIZE = 2;
    public float ATTACK_UP_SPEED_BONUS = 2F;
    public float ATTACK_UP_STUN_DURATION = 2F;
    public float DEFENSE_UP_DURATION = 2F;
    public float BULLET_TIME_DURATION = 2F;
    public float BULLET_TIME_REDUCED_COOLDOWN = 2F;
}
