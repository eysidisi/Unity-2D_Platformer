
using UnityEngine;
using UnityEngine.UI;
public class PlayerScore : MonoBehaviour {

    public Text scoreText;
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "PlayerScore: " + GameManager.GetPlayerScore().ToString();
	}
}
