using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    bool canCheckForInput = false;
    float animDelay = 0.1f;
    float currentDelay = 0;
    float currentDestroyDelay = 0;
    bool switchGravitize_FirstRow = true;
    public float gameSpeed = 0.5f;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (canCheckForInput)
        {
            InputSwipe.Instance.CheckForInput();
        }
       
        if (ArrayLogic.Instance.IsNullPresent() == true)
        {
            canCheckForInput = false;
            currentDelay += Time.deltaTime;
            if (currentDelay > gameSpeed + animDelay && Visuals.Instance.isAnimating == false)
            {
                if (switchGravitize_FirstRow)
                {
                    ArrayLogic.Instance.Gravitize();
                    Visuals.Instance.ApplyChangesToBoard();
                    currentDelay = 0;
                    switchGravitize_FirstRow = !switchGravitize_FirstRow;
                }
                else
                {
                    ArrayLogic.Instance.PopulateFirstRow();
                    currentDelay = 0;
                    switchGravitize_FirstRow = !switchGravitize_FirstRow;
                    currentDelay += Time.deltaTime;
                }
            }
        }
        else
        {
            currentDestroyDelay += Time.deltaTime;
            if(currentDestroyDelay > gameSpeed + animDelay)
            {
                ArrayLogic.Instance.CheckForMatch();
                ArrayLogic.Instance.DestoryMatchingEnemies();
                currentDestroyDelay = 0f;
                canCheckForInput = true;
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // For debug
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ArrayLogic.Instance.CheckForMatch();
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    ArrayLogic.Instance.DestoryMatchingEnemies();
        //}
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    ArrayLogic.Instance.Gravitize();
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    ArrayLogic.Instance.PopulateFirstRow();
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    ArrayLogic.Instance.PrintEnemiesArray();
        //}
    }

    public void SwipeDetected_OnSwipeDetected(int enX, int enY, int moveX, int moveY)
    {
        ArrayLogic.Instance.SwitchEnemies(enX, enY, moveX, moveY);
        Visuals.Instance.ApplyChangesToBoard();
        currentDestroyDelay = 0;
        canCheckForInput = false;
    }

    
}
