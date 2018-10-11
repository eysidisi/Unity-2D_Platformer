using UnityEngine;
using UnityEngine.UI;
public class OpponentScore : MonoBehaviour {

    public Text scoreText;
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "OpponentScore: " + GameManager.GetOpponentScore().ToString();
	}
}
