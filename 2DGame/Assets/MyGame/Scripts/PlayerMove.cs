using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool isGameStarted = false;

    //Movement direction in X axis
    private float movementVector;

    //Jump flag
    bool jumpFlag;

    //Jump cutOffSpeed
     float jumpSpeed;

    //Rigidbody of player
    Rigidbody2D rb2d;

    //Movement cutOffSpeed
    public float speed=5;

    //Is player touching ground or paddle 
    bool touchGroundFlag;

    
    private void Awake()
    {
        InitializeVariables();
    }
    private void Start()
    {
    }

    private void Update()
    {
        if (!isGameStarted )
        {
            isGameStarted = GameManager.GetGameState();
        }
        if (isGameStarted)
        {
            isGameStarted = GameManager.GetGameState();
            //Get horizontal input
            movementVector = Input.GetAxis("Horizontal");
            //move character in X axis with cutOffSpeed "cutOffSpeed"
            transform.Translate(Vector3.right * movementVector * Time.deltaTime * speed, Space.World);
            if (movementVector > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (movementVector < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            jumpFlag = Input.GetKeyDown("space") && touchGroundFlag;

            //Jump code
            if (jumpFlag)
            {
                rb2d.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

                //Set touching flag to false
                touchGroundFlag = false;
            }
        }
    }

   

    void InitializeVariables()
    {
        //Get rigidbody component
        rb2d = GetComponent<Rigidbody2D>();

        movementVector = 0;

        jumpFlag = false;

        jumpSpeed = 5;

        touchGroundFlag = true;
    }

    private void OnCollisionEnter2D(Collision2D colObj)
    {
        string objTag = colObj.gameObject.tag;

        if(objTag=="Ground" || objTag=="Plank")
        {
            touchGroundFlag = true;
        }


    }
}

