using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recharge : MonoBehaviour
{
    private Ready ready;
    private Image knobImage;

    public void Awake()
    {
        knobImage = transform.Find("knob").GetComponent<Image>();
    }

    private void Update()
    {
        if (ready != null)
        {
            ready.Update();
            knobImage.fillAmount = ready.GetReadyNormalized();
        }
    }

    public void PutOnCooldown(float cooldown)
    {
        ready = new Ready(cooldown);
        ready.PutOnCooldown();
    }
}

public class Ready
{
    private float cooldownDuration = 5f;
    private float nextReadyTime;
    private float cooldownTimeLeft;

    public Ready(float cooldown)
    {
        cooldownDuration = cooldown;
    }

    public void Update()
    {
        bool cooldownComplete = (Time.time > nextReadyTime);
        if (!cooldownComplete)
        {
            cooldownTimeLeft -= Time.deltaTime;
            float roundedCd = Mathf.Round(cooldownTimeLeft);
        }
    }

    public float GetReadyNormalized ()
    {
        return 1.01f - cooldownTimeLeft / cooldownDuration;
    }

    public void PutOnCooldown()
    {
        nextReadyTime = cooldownDuration + Time.time;
        cooldownTimeLeft = cooldownDuration;
    }

}
