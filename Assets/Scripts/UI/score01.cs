using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score01 : MonoBehaviour
{
    private int repair = 0;
    public Text repairText;

    private void Update()
    {
        repairText.text = repair + "/5";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            repair++;
            
        }
    }
}
