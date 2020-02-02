using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCharacterController : MonoBehaviour
{
    private Animator anim;
    public bool isWinner;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (isWinner)
            anim.SetFloat("Walk", 1.0f);
        else
            anim.SetBool("Stunned", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
