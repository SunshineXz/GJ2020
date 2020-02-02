using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public List<Image> items;

    public Material black;
    public Material normal;

    public int score = 0;

    void Awake()
    {
        foreach(Image i in items)
        {
            i.material = black;
        }
    }

    public void UpdateScore()
    {
        score++;
        var item = items[score-1];
        item.material = normal;
    }
}
