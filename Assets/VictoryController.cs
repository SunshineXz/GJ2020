using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public Material p1Mat;
    public Material p2Mat;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        var gameManager = GameManager.Instance;

        if(gameManager.player1Score > gameManager.player2Score)
        {
            Player1.GetComponent<Renderer>().material = p1Mat;
            Player2.GetComponent<Renderer>().material = p2Mat;
        }
        else
        {
            Player1.GetComponent<Renderer>().material = p2Mat;
            Player2.GetComponent<Renderer>().material = p1Mat;
        }
    }
}
