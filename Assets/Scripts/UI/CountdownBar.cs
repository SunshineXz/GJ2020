using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownBar : MonoBehaviour
{

    private Start start;
    private Image BarImage;
    public void Awake()
    {
        BarImage = transform.Find("CountownBar").GetComponent<Image>();

        start = new Start();

    }

    private void Update()
    {
        start.Update();

        BarImage.fillAmount = start.GetReadyNormalized();
    }
}

public class Start
{
    public const int READY_MAX = 100;

    private float startAmount;
    private float startDepleteAmount;

    public Start()
    {
        startAmount = 100;
        startDepleteAmount = 20f;
    }

    public void Update()
    {
        startAmount -= startDepleteAmount * Time.deltaTime;
        startAmount = Mathf.Clamp(startAmount, 0f, READY_MAX);
    }

    public float GetReadyNormalized()
    {
        return startAmount / READY_MAX;
    }

}
