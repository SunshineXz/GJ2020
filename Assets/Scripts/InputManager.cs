using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InputManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerControllerMapping
    {
        public string playerName;
        public string controllerName = "Keyboard";
    }

    public PlayerControllerMapping player1;
    public PlayerControllerMapping player2;

    // Start is called before the first frame update
    void Start()
    {
        player1 = new PlayerControllerMapping();
        player2 = new PlayerControllerMapping();
        player1.playerName = "Player1";
        player2.playerName = "Player2";

        var controllerNames = Input.GetJoystickNames();

        List<string> nonNullControllerNames = new List<string>(controllerNames).Where(x => !String.IsNullOrEmpty(x)).ToList();

        if (nonNullControllerNames.Count == 1)
        {
            player1.controllerName = "Keyboard";
            player2.controllerName = nonNullControllerNames[0];
            
        } else if(nonNullControllerNames.Count >= 2)
        {
            player1.controllerName = nonNullControllerNames[0];
            player2.controllerName = nonNullControllerNames[1];
        }
    }

    public bool GetButtonDown(string buttonName, string playerName)
    {
        string player = "";

        if (playerName == player1.playerName && player1.controllerName != "Keyboard")
            player = "_Player1";
        else if (playerName == player2.playerName)
            player = "_Player2";

        return Input.GetButtonDown(buttonName + player);
    }

    public float GetAxis(string axisName, string playerName)
    {
        string player = "";

        if (playerName == player1.playerName && player1.controllerName != "Keyboard")
            player = "_Player1";
        else if (playerName == player2.playerName)
            player = "_Player2";

        return Input.GetAxis(axisName + player);
    }

    public bool GetAxisOrButtonDown(string inputName, string playerName)
    {
        if (playerName == player1.playerName)
        {
            if (player1.controllerName == "Keyboard")
                return Input.GetButtonDown(inputName);
            else
                return Input.GetAxis(inputName + "_" + playerName) > 0;
        }
        else if (playerName == player2.playerName)
            return Input.GetAxis(inputName + "_" + playerName) > 0;

        return false;
    }
}
