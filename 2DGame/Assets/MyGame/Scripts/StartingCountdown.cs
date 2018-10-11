using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartingCountdown : MonoBehaviour {

    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
            scoreText.text = "Get Ready!     " + ((int)(GameManager.startTime)).ToString();

        if(GameManager.GetGameState())
        {
            
                scoreText.text = "Time Left: " + ((int)(GameManager.roundCountdown)).ToString() + " seconds";
            
        }
    }
}
