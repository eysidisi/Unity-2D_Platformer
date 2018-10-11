using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAndPlatformManager : MonoBehaviour
{
    public const int numberOfPlatforms = 5;
    public  int numberOfCoins = 5;
    public bool[] isCoinOnplatform = new bool[numberOfPlatforms];
    public float[] xCoordinatesOfplatforms = new float[numberOfPlatforms];
    public float[] yCoordinatesOfplatforms = new float[numberOfPlatforms];
    public List<GameObject> coins = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnCoin", 0f, 0.5f);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            string name = "Platform" + i;
            xCoordinatesOfplatforms[i] = GameObject.Find(name).transform.position.x;
            yCoordinatesOfplatforms[i] = GameObject.Find(name).transform.position.y;
        }

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            GameObject obj = ObjectPooler.current.GetPooledObject();
            obj.transform.position = new Vector2(xCoordinatesOfplatforms[i], yCoordinatesOfplatforms[i] + 0.3f);
            obj.SetActive(true);
            isCoinOnplatform[i] = true;
            coins.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnCoin()
    {
        int i = Random.Range(0, 5);
        if (coins[i].activeSelf == false)
        {
            coins[i].SetActive(true);
        }
    }
}
