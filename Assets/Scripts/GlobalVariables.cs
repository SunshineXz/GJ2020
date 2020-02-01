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


    public float SLAP_STUN_DURATION;
    public float SLAP_COOLDOWN_TIME;
    public float SHOOT_STUN_DURATION;
    public float SHOOT_BASE_SIZE;
    public float SHOOT_BASE_SPEED;
    public float SHOOT_COOLDOWN_TIME;
    
    public float SPEED_UP_MOVEMENT_BONUS;
    public float SPEED_UP_DECREASE_RATE;
    public int ATTACK_UP_SIZE;
    public float ATTACK_UP_SPEED_BONUS;
    public float ATTACK_UP_STUN_DURATION;
    public float DEFENSE_UP_DURATION;
    public float BULLET_TIME_DURATION;
    public float BULLET_TIME_REDUCED_COOLDOWN;
}
