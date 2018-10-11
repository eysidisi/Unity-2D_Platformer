using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    enum Direction { Right, Left };
    enum OpponentLocation { Platform0, Platform1, Platform2, Platform3, Platform4, Ground };

    // Use this for initialization
    CoinAndPlatformManager coinAndPlatformMAnager;
    List<GameObject> coins = new List<GameObject>();
    int closestCoinNumber;
    bool isCoinAtTheSamePlatform;
    bool isCoinAtUpperPlatform;
    bool isPlatformReachable;
    bool isPlatformCloseEnough;
    bool isCoinAtParallelPlatform;
    bool isGameStarted = false;
    float speed = 3f;
    float jumpSpeed = 5f;
    float platformLength = 1.6f;
    bool jumpFlag;
    int targetPlatform;
    OpponentLocation opponentLocation;
    void Start()
    {
        coinAndPlatformMAnager = FindObjectOfType<CoinAndPlatformManager>();
        coins = coinAndPlatformMAnager.coins;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted )
        {
            isGameStarted = GameManager.GetGameState();
        }
        if (isGameStarted)
        {
            isGameStarted = GameManager.GetGameState();
            closestCoinNumber = FindClosestCoin(coins);
            isCoinAtTheSamePlatform = CheckIfCoinAtTheSamePlatform(closestCoinNumber);
            targetPlatform = closestCoinNumber;
            if (closestCoinNumber == -1)
            {

            }
            else if (isCoinAtTheSamePlatform)
            {
                GoAndGet(coins[closestCoinNumber]);
            }
            else
            {
                isCoinAtUpperPlatform = CheckIfCoinAtUpperLevel(closestCoinNumber);
                isCoinAtParallelPlatform = CheckIfCoinIsAtParallelPlatform(targetPlatform);

                if (isCoinAtUpperPlatform)
                {
                    targetPlatform = closestCoinNumber;

                    isPlatformReachable = CheckIfPlatformReachable(targetPlatform);

                    while (isPlatformReachable == false)
                    {
                        targetPlatform = FindNewTargetPlatform(targetPlatform);
                        isPlatformReachable = CheckIfPlatformReachable(targetPlatform);
                    }

                    if (isPlatformReachable)
                    {
                        isPlatformCloseEnough = CheckIfPlatformCloseEnough(targetPlatform);

                        if (isPlatformCloseEnough)
                        {
                            Jump();
                        }
                        else if (!isPlatformCloseEnough && jumpFlag)
                        {
                            GetCloseToPlatform(targetPlatform);

                        }
                        if (jumpFlag == false)
                        {
                            GetToThePlatform(targetPlatform);
                        }
                    }


                }

                else if (isCoinAtParallelPlatform)
                {
                    targetPlatform = FindNewTargetPlatformForParallelCoin(targetPlatform);
                    isPlatformReachable = CheckIfPlatformReachable(targetPlatform);

                    while (isPlatformReachable == false)
                    {
                        targetPlatform = FindNewTargetPlatform(targetPlatform);
                        isPlatformReachable = CheckIfPlatformReachable(targetPlatform);
                    }
                    if (isPlatformReachable)
                    {
                        isPlatformCloseEnough = CheckIfPlatformCloseEnough(targetPlatform);

                        if (isPlatformCloseEnough)
                        {
                            Jump();
                        }
                        else if (!isPlatformCloseEnough && jumpFlag)
                        {
                            GetCloseToPlatform(targetPlatform);

                        }
                        if (jumpFlag == false)
                        {
                            GetToThePlatform(targetPlatform);
                        }
                    }

                }

                else
                {
                    CalculationsForCoinAtLowerLevel(closestCoinNumber);
                }
            }

        }
    }



    int FindClosestCoin(List<GameObject> coins)
    {
        int closestCoinNumber = -1;
        float distance = float.MaxValue;
        for (int i = 0; i < coins.Count; i++)
        {



            if (coins[i].activeSelf == true)
            {
                if (CalculateDistance(coins[i].transform.position) < distance)
                {
                    distance = CalculateDistance(coins[i].transform.position);
                    closestCoinNumber = i;
                }
            }
        }

        return closestCoinNumber;
    }

    float CalculateDistance(Vector3 pos)
    {
        float distance = (transform.position - pos).magnitude;
        return distance;
    }

    bool CheckIfCoinAtTheSamePlatform(int coinNumber)
    {


        if (coinNumber == (int)opponentLocation)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void GoAndGet(GameObject coin)
    {
        if (coin.transform.position.x - 0.1f > transform.position.x)
        {
            Move(Direction.Right);
        }
        else if (coin.transform.position.x + 0.1f < transform.position.x)
        {
            Move(Direction.Left);
        }
    }

    void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.back, +5);
                break;
            case Direction.Left:
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.back, -5);
                break;
            default:
                break;
        }
    }

    bool CheckIfCoinAtUpperLevel(int coinNumber)
    {
        switch (coinNumber)
        {
            case 0:
                if (opponentLocation == OpponentLocation.Ground)
                    return true;
                else return false;

            case 1:
                if (opponentLocation == OpponentLocation.Ground || opponentLocation == OpponentLocation.Platform0 ||
                   opponentLocation == OpponentLocation.Platform4)
                    return true;
                else return false;
            case 2:
                if (opponentLocation == OpponentLocation.Platform2)
                    return false;
                else
                    return true;
            case 3:
                if (opponentLocation == OpponentLocation.Ground || opponentLocation == OpponentLocation.Platform0 ||
                   opponentLocation == OpponentLocation.Platform4)
                    return true;
                else
                    return false;
            case 4:
                if (opponentLocation == OpponentLocation.Ground)
                    return true;
                else return false;
            default:
                return false;

        }

    }

    bool CheckIfPlatformReachable(int platformNumber)
    {
        switch (platformNumber)
        {
            case 0:
                if (opponentLocation == OpponentLocation.Ground)
                    return true;
                else
                    return false;

            case 1:
                if (opponentLocation == OpponentLocation.Platform0)
                    return true;
                else
                    return false;

            case 2:
                if (opponentLocation == OpponentLocation.Platform1 || opponentLocation == OpponentLocation.Platform3)
                    return true;
                else
                    return false;

            case 3:
                if (opponentLocation == OpponentLocation.Platform4)
                    return true;
                else
                    return false;

            case 4:
                if (opponentLocation == OpponentLocation.Ground)
                    return true;
                else
                    return false;

            default:
                return false;

        }
    }

    bool CheckIfPlatformCloseEnough(int platformNumber)
    {
        if (((transform.position.x > coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] + platformLength / 2 +0.7f) &&
            (transform.position.x < coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] + platformLength / 2 + 2.0f)) ||
            ((transform.position.x < coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] - platformLength / 2 -0.7f) &&
            (transform.position.x > coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] - platformLength / 2 - 2.0f)))
            return true;
        else
            return false;
    }

    bool CheckIfCoinIsAtParallelPlatform(int platformNumber)
    {
        switch (platformNumber)
        {
            case 0:
                if (opponentLocation == OpponentLocation.Platform4)
                    return true;
                else
                    return false;
            case 1:
                if (opponentLocation == OpponentLocation.Platform3)
                    return true;
                else
                    return false;
            case 3:
                if (opponentLocation == OpponentLocation.Platform1)
                    return true;
                else
                    return false;
            case 4:
                if (opponentLocation == OpponentLocation.Platform0)
                    return true;
                else
                    return false;
            default:
                return false;

        }
    }

    void Jump()
    {
        if (jumpFlag)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
        jumpFlag = false;
    }

    void GetToThePlatform(int platformNumber)
    {
        if (coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] + 0.05f > transform.position.x)
        {
            Move(Direction.Right);
        }

        else if (coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] - 0.05f < transform.position.x)
        {
            Move(Direction.Left);
        }
    }

    void GetCloseToPlatform(int platformNumber)
    {
        if ((coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] + platformLength / 2 + 1.0f) < transform.position.x)
        {
            Move(Direction.Left);
        }
        else if ((coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber] - platformLength / 2 - 1.0f) > transform.position.x)
        {
            Move(Direction.Right);
        }
        else if ((coinAndPlatformMAnager.xCoordinatesOfplatforms[platformNumber]) > transform.position.x)
        {
            Move(Direction.Left);
        }
        else 
        {
            Move(Direction.Right);
        }
    }

    int FindNewTargetPlatform(int platformNumber)
    {
        switch (platformNumber)
        {
            case 1:
                if (transform.position.x > 0)
                {
                    return 2;
                }
                else return 0;

            case 2:
                if (transform.position.x > 0)
                {
                    return 3;
                }
                else
                    return 1;
            case 3:
                if (transform.position.x > 0)
                {
                    return 4;
                }
                else return 2;

            default:
                return 0;
        }
    }

    void CalculationsForCoinAtLowerLevel(int closestCoin)
    {
        if (opponentLocation == OpponentLocation.Platform2)
        {
            switch (closestCoin)
            {
                case 0:
                    Jump();
                    GetToThePlatform(closestCoin);
                    break;
                case 4:
                    Jump();
                    GetToThePlatform(closestCoin);
                    break;
                default:
                    GetToThePlatform(closestCoin);
                    break;
            }

        }
        else if (opponentLocation == OpponentLocation.Platform1)
        {
            switch (closestCoin)
            {
                case 0:

                    GetToThePlatform(closestCoin);
                    break;
                case 4:
                    Jump();
                    GetToThePlatform(2);
                    break;
                default:
                    break;
            }
        }
        else if (opponentLocation == OpponentLocation.Platform3)
        {
            switch (closestCoin)
            {
                case 0:
                    Jump();
                    GetToThePlatform(2);

                    break;
                case 4:

                    GetToThePlatform(closestCoin);
                    break;
                default:
                    break;
            }
        }
    }

    int FindNewTargetPlatformForParallelCoin(int platformNumber)
    {
        switch (platformNumber)
        {
            case 0:
                return 3;

            case 4:
                return 1;

            case 1:
                return 2;

            case 3:
                return 2;

            default:
                Debug.Log("paralel platform bulmada hata");
                return 0;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Platform0":
                opponentLocation = OpponentLocation.Platform0;
                jumpFlag = true;
                break;
            case "Platform1":
                opponentLocation = OpponentLocation.Platform1;
                jumpFlag = true;
                break;
            case "Platform2":
                opponentLocation = OpponentLocation.Platform2;
                jumpFlag = true;
                break;
            case "Platform3":
                opponentLocation = OpponentLocation.Platform3;
                jumpFlag = true;
                break;
            case "Platform4":
                opponentLocation = OpponentLocation.Platform4;
                jumpFlag = true;
                break;
            case "Ground":
                opponentLocation = OpponentLocation.Ground;
                jumpFlag = true;
                break;
            default:
                break;
        }
    }
}
