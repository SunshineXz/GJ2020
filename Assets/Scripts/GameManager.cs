using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public ScoreDisplay player1ScoreDisplay;
    public ScoreDisplay player2ScoreDisplay;
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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        StartCoroutine(SpawnRepairItem());
    }

    public IEnumerator SpawnRepairItem()
    {
        yield return new WaitForSeconds(2f);
        System.Random random = new System.Random();
        repairSpawnPoints[random.Next(repairSpawnPoints.Count)].SpawnItem();
    }

    public void ScorePoint(int playerId)
    {
        if(playerId == 0)
        {
            player1Score++;
            player1ScoreDisplay.UpdateScore();
            CheckForWin(playerId, player1Score);
        }
        else if(playerId == 1)
        {
            player2Score++;
            player2ScoreDisplay.UpdateScore();
            CheckForWin(playerId, player2Score);
        }
        StartCoroutine(SpawnRepairItem());
    }

    public void CheckForWin(int playerId, int playerScore)
    {
        if(playerScore >= scoreToWin)
        {
            Debug.Log($"Player {playerId + 1} wins!");
            StartCoroutine(WaitForVictorySequence());
            FindObjectOfType<AudioManager>().Play("Goal");
        }
    }

    private IEnumerator WaitForVictorySequence()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }
}
