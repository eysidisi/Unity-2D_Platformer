using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Use this for initialization
    static int playerScore ;
    static int opponentScore ;
    static public float startTime ;
    static public float roundCountdown;
    static bool isGameStarted ;
    static bool isEndSceneActive;
    public GameObject endGameCanvas;
    public GameObject scoreAndInfo;
    private void Awake()
    {
        endGameCanvas.SetActive(false);
    }
    void Start()
    {
        scoreAndInfo.SetActive(true);
        
        isGameStarted = false;
        isEndSceneActive = false;
        playerScore = 0;
        opponentScore = 0;
        startTime = 3;
        roundCountdown = 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isEndSceneActive)
        {
            if (!isGameStarted)
            {
                startTime -= Time.deltaTime;
                if (startTime < 0)
                {
                    isGameStarted = true;
                }
            }
            else
            {
                roundCountdown -= Time.deltaTime;
            }

            if (roundCountdown < 0)
            {
                isEndSceneActive = true;
                endGameCanvas.SetActive(true);
                scoreAndInfo.SetActive(false);
                isGameStarted = false;


            }
        }


    }


    public void IncreaseScore(int increaseAmount, string playerOrOpponent)
    {
        if (playerOrOpponent == "Player")
        {
            playerScore += increaseAmount;
        }
        else if (playerOrOpponent == "Opponent")
        {
            opponentScore += increaseAmount;
        }

        else
        {
            Debug.LogError("Check score increase");
        }
    }

    static public int GetPlayerScore()
    {
        return playerScore;
    }

    static public int GetOpponentScore()
    {
        return opponentScore;
    }

    static public bool GetGameState()
    {
        return isGameStarted;
    }
}
