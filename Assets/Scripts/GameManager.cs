using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public int scoreToWin = 5;
    public List<RepairSpawnPoint> repairSpawnPoints;

    static GameManager instance;

    public static GameManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        SpawnRepairItem();
    }

    public void SpawnRepairItem()
    {
        System.Random random = new System.Random();
        repairSpawnPoints[random.Next(repairSpawnPoints.Count)].SpawnItem();
    }

    public void ScorePoint(int playerId)
    {
        if(playerId == 0)
        {
            player1Score++;
            CheckForWin(playerId, player1Score);
        }
        else if(playerId == 1)
        {
            player2Score++;
            CheckForWin(playerId, player2Score);
        }
        SpawnRepairItem();
    }

    public void CheckForWin(int playerId, int playerScore)
    {
        if(playerScore >= scoreToWin)
        {
            Debug.Log($"Player {playerId + 1} wins!");
        }
    }
}
