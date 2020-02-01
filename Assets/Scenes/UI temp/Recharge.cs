using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recharge : MonoBehaviour
{

    private Ready ready;
    private Image knobImage;
    private void Awake()
    {
        knobImage = transform.Find("knob").GetComponent<Image>();

        ready = new Ready();

    }

    private void Update()
    {
        ready.Update();

        knobImage.fillAmount = ready.GetReadyNormalized();
    }
}

public class Ready
{
    public const int READY_MAX = 100;

    private float readyAmount;
    private float readyRegenAmount;

    public Ready()
    {
        readyAmount = 0;
        readyRegenAmount = 20f;
    }

    public void Update()
    {
        readyAmount += readyRegenAmount * Time.deltaTime;
        readyAmount = Mathf.Clamp(readyAmount, 0f, READY_MAX);
    }

    public float GetReadyNormalized ()
        {
            return readyAmount / READY_MAX;
        }

}
