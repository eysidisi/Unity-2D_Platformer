using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{

    public Text text;
    int playerScore;
    int opponentScore;
    // Use this for initialization
    void Start()
    {
        playerScore = GameManager.GetPlayerScore();
        opponentScore = GameManager.GetOpponentScore();
        if (playerScore > opponentScore)
        {
            text.text = "YOU WIN! \n Your Score Was: " + playerScore.ToString();
        }
        else
        {
            text.text = "YOU LOSE! \n Your Score Was: " + playerScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
