using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwipe : MonoBehaviour
{
    Vector3 mouseStartPosition; // position when mouse button was pressed
    Vector3 mouseEndPosition;// position when mouse button was releaseddvs
    int nodeXPos;
    int nodeYPos;
    float swipeAngle;
    int swipeX;
    int swipeY;
    bool detectedHit = false;
    public float swipePrecision = 30f;
    bool destroyBlock = false;
    bool destroyEnemyType = false;
    

    public event OnSwipeDetected_Delegate OnSwipeEvent;
    public delegate void OnSwipeDetected_Delegate(int enX, int enY, int moveX, int moveY);
    public static InputSwipe Instance;

    private void Start()
    {
        Instance = this;
        OnSwipeEvent += Main.Instance.SwipeDetected_OnSwipeDetected;
    }

    public void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    nodeXPos = (int)Mathf.RoundToInt(hit.transform.position.x);
                    nodeYPos = (int)Mathf.RoundToInt(hit.transform.position.y);
                    detectedHit = true;
                    if (destroyBlock == true)
                    {
                        ArrayLogic.Instance.DestroyBlock(nodeXPos, nodeYPos);
                        destroyBlock = false;
                        detectedHit = false;
                    }
                    else if (destroyEnemyType == true)
                    {
                        ArrayLogic.Instance.DestroyEnemyType(ArrayLogic.Instance.nodesArray[nodeXPos, nodeYPos]);
                        destroyEnemyType = false;
                        detectedHit = false;
                    }
                    //print(hit.collider);
                }
            }
        }

        
       
        
        if (Input.GetMouseButtonUp(0))
        {
            mouseEndPosition = Input.mousePosition;
            if (Vector3.Distance(mouseStartPosition, mouseEndPosition) > swipePrecision && detectedHit == true)
            {
                CheckSwipeDirection(mouseStartPosition, mouseEndPosition);
                //print("swipnieto enemiesa X : " + nodeXPos + " Y : " + nodeYPos + " W kierunku "+" X :" +swipeX + " Y : " + swipeY);
                if (nodeXPos + swipeX < ArrayLogic.Instance.nodesArray.GetLength(0)
                    && nodeXPos + swipeX >= 0
                    && nodeYPos + swipeY < ArrayLogic.Instance.nodesArray.GetLength(1)
                    && nodeYPos + swipeY >= 0)
                {
                    if (OnSwipeEvent != null)
                    {
                        OnSwipeEvent(nodeXPos, nodeYPos, swipeX, swipeY);
                    }
                    //OnSwipeDetected?.Invoke(nodeXPos, nodeYPos, swipeX,swipeY);
                }

            }
            else
            {
                detectedHit = false;
            }
        }
        


        
    }

    void CheckSwipeDirection(Vector3 start, Vector3 end)
    {
        swipeAngle = Mathf.Atan2((mouseEndPosition.y - mouseStartPosition.y), (mouseEndPosition.x - mouseStartPosition.x)) * 180 / Mathf.PI;
        if (swipeAngle > -45 && swipeAngle < 45)
        {
            //print("Swipe Right");
            swipeX = 1;
            swipeY = 0;
        }
        else if (swipeAngle > 45 && swipeAngle < 135)
        {
            //print("Swipe Up");
            swipeX = 0;
            swipeY = 1;
        }
        else if (swipeAngle > -135 && swipeAngle < -45)
        {
            //print("Swipe Down");
            swipeX = 0;
            swipeY = -1;
        }
        else
        {
            //print("Swipe Left");
            swipeX = -1;
            swipeY = 0;
        }
    }

    public void OnButtonExplodeEnemyBlockDetected()
    {
        destroyBlock = true;
        destroyEnemyType = false;
    }
    public void OnButtonDestroyEnemyTypeDetected()
    {
        destroyEnemyType = true;
        destroyBlock = false;
    }
}
