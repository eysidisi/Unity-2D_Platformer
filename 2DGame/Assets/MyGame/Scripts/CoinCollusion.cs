using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollusion : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player" || trig.gameObject.tag == "Opponent")
        {
            gameObject.SetActive(false);
            gameManager.IncreaseScore(5, trig.gameObject.tag);
        }
    }
}
