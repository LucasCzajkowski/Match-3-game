using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLogic : MonoBehaviour
{
    public static ArrayLogic Instance;

    public GameObject[,] nodesArray = new GameObject[10, 10]; // board size
    public List<string> enemiesList = new List<string>();
    public GameObject tempNode;
    public List<GameObject> matchedEnemies;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PopulateArray();
        Visuals.Instance.PopulateBoard();
    }

    public void SwitchEnemies(int enX, int enY, int moveX, int moveY)
    {
        if (enX + moveX < nodesArray.GetLength(0) && enX + moveX >= 0 && enY + moveY < nodesArray.GetLength(1) && enY + moveY >= 0)
        {
            tempNode = nodesArray[enX, enY];
            nodesArray[enX, enY] = nodesArray[enX + moveX, enY + moveY];
            nodesArray[enX + moveX, enY + moveY] = tempNode;
            Sounds.Instance.PlaySoundSwitch();
        }
    }

    public void PopulateArray()
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(1); j++)
            {
                //nodesArray[i, j] = Instantiate(enemyArray[Random.Range(0, 5)], new Vector3(i, j, 0), Quaternion.identity);
                nodesArray[i, j] = Pooler.Instance.Pooluj(enemiesList[UnityEngine.Random.Range(0, enemiesList.Count)], new Vector3(100, 100, 0), true);

            }
        }
    }

    public void PrintEnemiesArray()// Debug only
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(0); j++)
            {
                print("X: " + i + " Y: " + j + " " + nodesArray[i, j]);
            }
        }
    }

    public void CheckForMatch()
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(0); j++)
            {
                if (nodesArray[i, j] != null)
                {
                    if (i > 0 && i < nodesArray.GetLength(0) - 1)// checks for -O-
                    {
                        if (nodesArray[i, j].tag == nodesArray[i - 1, j].tag && nodesArray[i, j].tag == nodesArray[i + 1, j].tag)
                        {
                            matchedEnemies.Add(nodesArray[i, j]);
                        }
                    }

                    if (j > 0 && j < nodesArray.GetLength(1) - 1)// checks for ^o,
                    {
                        if (nodesArray[i, j].tag == nodesArray[i, j - 1].tag && nodesArray[i, j].tag == nodesArray[i, j + 1].tag)
                        {
                            matchedEnemies.Add(nodesArray[i, j]);
                        }
                    }

                    if (i >= 0 && i < nodesArray.GetLength(0) - 2)// checks for O--
                    {
                        if (nodesArray[i, j].tag == nodesArray[i + 1, j].tag && nodesArray[i, j].tag == nodesArray[i + 2, j].tag)
                        {
                            matchedEnemies.Add(nodesArray[i, j]);
                        }
                    }
                    if (i > 1 && i < nodesArray.GetLength(0))// checks for --O
                    {
                        if (nodesArray[i, j].tag == nodesArray[i - 1, j].tag && nodesArray[i, j].tag == nodesArray[i - 2, j].tag)
                        {
                            matchedEnemies.Add(nodesArray[i, j]);
                        }
                    }

                    if (j >= 0 && j < nodesArray.GetLength(1) - 2)// checks for ^^o
                    {
                        if (nodesArray[i, j].tag == nodesArray[i, j + 1].tag && nodesArray[i, j].tag == nodesArray[i, j + 2].tag)
                        {
                            matchedEnemies.Add(nodesArray[i, j]);
                        }
                    }
                    if (j > 1 && j < nodesArray.GetLength(1))// checks for o,,
                    {
                        if (nodesArray[i, j].tag == nodesArray[i, j - 1].tag && nodesArray[i, j].tag == nodesArray[i, j - 2].tag)
                        {
                            matchedEnemies.Add(nodesArray[i, j]);
                        }
                    }
                }

            }
        }
    }

    public void DestoryMatchingEnemies()
    {
        foreach (GameObject i in matchedEnemies)
        {
            i.SetActive(false);
            Score.Instance.updateScore(i);
            int nodeXPos = (int)Mathf.RoundToInt(i.transform.position.x);
            //print(nodeXPos);
            int nodeYPos = (int)Mathf.RoundToInt(i.transform.position.y);
            //print(nodeYPos);
            //nodesArray[(int)Mathf.RoundToInt.transform.position.x, (int)Mathf.RoundToInt.transform.position.y] = null;
            nodesArray[nodeXPos, nodeYPos] = null;
            Sounds.Instance.PlaySoundExplode();
        }
        
        Visuals.Instance.ApplyDestroyEffect();
        matchedEnemies.Clear();
    }

    public void Gravitize()
    {

        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(0); j++)
            {
                if (j > 0 && nodesArray[i, j] != null && nodesArray[i, j - 1] == null)
                //if (j > 0 && nodesArray[i, j].activeSelf != false && nodesArray[i, j - 1].activeSelf == false)
                {
                    //nodesArray[i, j].GetComponent<Enemy>().MoveEnemy(0, -1);
                    tempNode = nodesArray[i, j];
                    nodesArray[i, j] = nodesArray[i, j - 1];
                    nodesArray[i, j - 1] = tempNode;
                }
            }
            //print("Poszlo gravitize");
            Visuals.Instance.ApplyChangesToBoard();
        }
    }

    public void PopulateFirstRow()
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            if (nodesArray[i, nodesArray.GetLength(0) - 1] == null)
            {
                //nodesArray[i, nodesArray.GetLength(0) - 1] = Instantiate(enemyArray[Random.Range(0, 5)], new Vector3(i, nodesArray.GetLength(0) - 1, 0), Quaternion.identity);
                nodesArray[i, nodesArray.GetLength(0) - 1] = Pooler.Instance.Pooluj(enemiesList[UnityEngine.Random.Range(0, enemiesList.Count)], new Vector3(i, nodesArray.GetLength(0) - 1, 0), true);
            }
        }
    }

    public bool IsNullPresent()
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(0); j++)
            {
                if (nodesArray[i, j] == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void DestroyEnemyType(GameObject a)
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(1); j++)
            {
                if (nodesArray[i, j] != null && nodesArray[i, j].tag == a.tag)
                {
                    matchedEnemies.Add(nodesArray[i, j]);
                }
            }
        }
        DestoryMatchingEnemies();
    }

    public void DestroyBlock(int xCenter, int yCenter)
    {
        print("trying to destroy block");
        for (int i = xCenter - 1; i < xCenter + 2; i++)
        {
            for (int j = yCenter - 1; j < yCenter + 2; j++)
            {
                if (i >= 0 && i < nodesArray.GetLength(0) && j >= 0 && j < nodesArray.GetLength(1))
                {
                    print(i + "  " + j);
                    if(nodesArray[i, j] != null)
                    {
                        matchedEnemies.Add(nodesArray[i, j]);
                    }
                    
                }
            }
        }
        DestoryMatchingEnemies();
    }
}
