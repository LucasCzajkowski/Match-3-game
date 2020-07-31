using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visuals : MonoBehaviour // updates the board graphics
{
    public static Visuals Instance;
    public bool isAnimating = false;
    float animSpeed;
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        animSpeed = Main.Instance.gameSpeed;
    }

    private void Update()
    {
        CheckIfIsAnimating();
        //Debug.Log("animating visuals : " + isAnimating);
    }

    public void PopulateBoard()
    {
        for (int i = 0; i < ArrayLogic.Instance.nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < ArrayLogic.Instance.nodesArray.GetLength(1); j++)
            {
                ArrayLogic.Instance.nodesArray[i, j].transform.position = new Vector3(i, j, 0);
                ArrayLogic.Instance.nodesArray[i, j].SetActive(true);
            }
        }
    }

    public void ApplyChangesToBoard()
    {
        //print("Applying changes to board Visuals");
        for (int i = 0; i < ArrayLogic.Instance.nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < ArrayLogic.Instance.nodesArray.GetLength(1); j++)
            {
                if (ArrayLogic.Instance.nodesArray[i, j] != null)
                {
                    //print("Checking for bord change");
                    int nodeXPos = (int)Mathf.RoundToInt(ArrayLogic.Instance.nodesArray[i, j].transform.position.x);
                    int nodeYPos = (int)Mathf.RoundToInt(ArrayLogic.Instance.nodesArray[i, j].transform.position.y);
                    
                    if (nodeXPos != i || nodeYPos != j)
                    {
                        LeanTween.move(ArrayLogic.Instance.nodesArray[i, j], new Vector3(i, j, 0), animSpeed).setEaseInOutBack();
                    }
                }
                
            }
        }
    }

    public void ApplyDestroyEffect()
    {
        for (int i = 0; i < ArrayLogic.Instance.nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < ArrayLogic.Instance.nodesArray.GetLength(1); j++)
            {
                if (ArrayLogic.Instance.nodesArray[i, j] == null)
                { 
                    Pooler.Instance.Pooluj("Exp", new Vector3(i, j, 0), true); 
                }
            }
        }
                
    }

    void CheckIfIsAnimating()
    {
        if (LeanTween.tweensRunning == 0)
        {
            isAnimating = false;
        }
        else
        {
            isAnimating = true;
        }
    }
}
