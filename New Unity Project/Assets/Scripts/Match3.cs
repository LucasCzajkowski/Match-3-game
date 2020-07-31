using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Match3 : MonoBehaviour
{
    public GameObject[,] nodesArray = new GameObject[10, 10]; // board size
    public GameObject[] enemyArray = new GameObject[5]; // amount of type of enemies/nodes
    public GameObject explosionEffect;
    GameObject movedNode1;
    GameObject movedNode2;
    GameObject tempNode;
    Vector3 mouseStartPosition; // position when mouse button was pressed
    Vector3 mouseEndPosition;// position when mouse button was releaseddvs
    float swipeAngle = 0; // angle in the swipe position
    int nodeXPos;
    int nodeYPos;
    public List<GameObject> matchedEnemies;
    bool canCheckForMatch = false;
    float timeDelay = 0.5f;
    float currentTimeDelay;
    bool isDestroyingEnemyType = false;
    bool isDestroyingEnemyBlock = false;
    int e1Counter = 0;
    int e2Counter = 0;
    int e3Counter = 0;
    int e4Counter = 0;
    int e5Counter = 0;
    public Text E1;
    public Text E2;
    public Text E3;
    public Text E4;
    public Text E5;
    public AudioSource soundSwitch;
    public AudioSource soundExplode;

    void Start()
    {
        PopulateArray();
    }

    void Update()
    {
        //Debug.Log("I have " + LeanTween.tweensRunning + " animating!");
        //print("Array dimensions " + nodesArray.GetLength(1));
        //print(nodesArray[9, 9]);
        //print(movedNode1);
        //print(e1Counter + "  " + e2Counter + "  " + e3Counter + "  " + e4Counter + "  " + e5Counter);
        if (LeanTween.tweensRunning == 0)
        {
            MoveNodes();
        }

        if (IsNullPresent() == true)
        {
            Gravitize();
        }
        else 
        {
            while(currentTimeDelay < timeDelay)
            {
                currentTimeDelay += Time.deltaTime;
            }
            currentTimeDelay = 0;
            CheckForMatch();
        }
        UpdateScore();
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    void PlaySoundSwitch()
    {
        soundSwitch.Play();
    }
    void PlaySoundExplode()
    {
        soundExplode.Play();
    }

    void UpdateScore()
    {
        E1.text = e1Counter.ToString();
        E2.text = e2Counter.ToString();
        E3.text = e3Counter.ToString();
        E4.text = e4Counter.ToString();
        E5.text = e5Counter.ToString();
    }

    public void ToggleBombBlock()
    {
        isDestroyingEnemyBlock = !isDestroyingEnemyBlock;
    }
    public void ToggleDestroyEnemyType()
    {
        isDestroyingEnemyType = !isDestroyingEnemyType;
    }

    void MoveNodes()
    {
        if (Input.GetMouseButtonDown(0) && LeanTween.tweensRunning < 2)
        {
            mouseStartPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "E1" || hit.transform.tag == "E2" || hit.transform.tag == "E3" || hit.transform.tag == "E4" || hit.transform.tag == "E5")
                { 
                nodeXPos = (int)Mathf.RoundToInt(hit.transform.position.x);
                nodeYPos = (int)Mathf.RoundToInt(hit.transform.position.y);
                movedNode1 = nodesArray[nodeXPos, nodeYPos];
                }
            }
            if(isDestroyingEnemyType == true)
            {
                DestroyEnemyType(movedNode1);
                isDestroyingEnemyType = false;
            }
            if (isDestroyingEnemyBlock == true)
            {
                DestroyBlock(movedNode1);
                isDestroyingEnemyBlock = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (movedNode1 != null)
            {
                mouseEndPosition = Input.mousePosition;
                MoveEnemy(mouseStartPosition, mouseEndPosition);
                movedNode1 = null;
                PlaySoundSwitch();
                
            }
        }
    }

    void DestroyBlock(GameObject a)
    {
        int xCenter = (int)Mathf.RoundToInt(a.transform.position.x);
        int yCenter = (int)Mathf.RoundToInt(a.transform.position.y);
        //print(xCenter + "  "+yCenter);
        print("trying to destroy block");
        for (int i = xCenter - 1; i < xCenter + 2; i++)
        {
            for (int j = yCenter - 1; j < yCenter + 2; j++)
            {
                if (i >= 0 && i < nodesArray.GetLength(0) && j >= 0 && j < nodesArray.GetLength(1))
                {
                print(i + "  " + j);
                    matchedEnemies.Add(nodesArray[i, j]);
                }
            }
        }

        foreach (GameObject i in matchedEnemies)
        {
            //enemyArray[(int)Mathf.RoundToInt(i.transform.position.x), (int)Mathf.RoundToInt(i.transform.position.y)];
            Instantiate(explosionEffect, i.transform.position, Quaternion.identity);
            switch (i.tag)
            {
                case "E1":
                    e1Counter += 1;
                    break;

                case "E2":
                    e2Counter += 1;
                    break;

                case "E3":
                    e3Counter += 1;
                    break;

                case "E4":
                    e4Counter += 1;
                    break;

                case "E5":
                    e5Counter += 1;
                    break;
            }
            Destroy(i);
            PlaySoundExplode();
        }
        matchedEnemies.Clear();
    }

    void DestroyEnemyType(GameObject a)
    {
        if (LeanTween.tweensRunning == 0)
        {
            for (int i = 0; i < nodesArray.GetLength(0); i++)
            {
                for (int j = 0; j < nodesArray.GetLength(1); j++)
                {
                    if (nodesArray[i, j].tag == a.tag)
                    {
                        matchedEnemies.Add(nodesArray[i, j]);
                    }
                }
            }
            foreach (GameObject i in matchedEnemies)
            {
                //enemyArray[(int)Mathf.RoundToInt(i.transform.position.x), (int)Mathf.RoundToInt(i.transform.position.y)];
                Instantiate(explosionEffect, i.transform.position, Quaternion.identity);
                switch (i.tag)
                {
                    case "E1":
                        e1Counter += 1;
                        break;

                    case "E2":
                        e2Counter += 1;
                        break;

                    case "E3":
                        e3Counter += 1;
                        break;

                    case "E4":
                        e4Counter += 1;
                        break;

                    case "E5":
                        e5Counter += 1;
                        break;
                }
                Destroy(i);
                PlaySoundExplode();
            }
            matchedEnemies.Clear();
        }
    }

    void CheckForMatch()
    {
        if (LeanTween.tweensRunning == 0)
        {
            for (int i = 0; i < nodesArray.GetLength(0); i++)
            {
                for (int j = 0; j < nodesArray.GetLength(0); j++)
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
                    if (i > 1 && i < nodesArray.GetLength(0) - 1)// checks for --O
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
                    if (j > 1 && j < nodesArray.GetLength(1) - 1)// checks for o,,
                    {
                        if (nodesArray[i, j].tag == nodesArray[i, j - 1].tag && nodesArray[i, j].tag == nodesArray[i, j - 2].tag)
                        {
                            matchedEnemies.Add(nodesArray[i, j]);
                        }
                    }

                }
            }

            foreach (GameObject i in matchedEnemies)
            {
                //enemyArray[(int)Mathf.RoundToInt(i.transform.position.x), (int)Mathf.RoundToInt(i.transform.position.y)];
                Instantiate(explosionEffect, i.transform.position, Quaternion.identity);

                switch (i.tag)
                {
                    case "E1":
                        e1Counter += 1;
                        break;

                    case "E2":
                        e2Counter += 1;
                        break;

                    case "E3":
                        e3Counter += 1;
                        break;

                    case "E4":
                        e4Counter += 1;
                        break;

                    case "E5":
                        e5Counter += 1;
                        break;
                }

                Destroy(i);
                PlaySoundExplode();
            }
            matchedEnemies.Clear();

           
        }
    }

    void PrintEnemiesArray()
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(0); j++)
            {
                print("X: " + i + " Y: " + j + " " + nodesArray[i, j]);
            }
        }
    }

    void Gravitize()
    {
        if (LeanTween.tweensRunning == 0)
        {
            for (int i = 0; i < nodesArray.GetLength(0); i++)
            {
                for (int j = 0; j < nodesArray.GetLength(0); j++)
                {
                    if (j > 0 && nodesArray[i, j] != null && nodesArray[i, j - 1] == null)
                    {
                        nodesArray[i, j].GetComponent<Enemy>().MoveEnemy(0, -1);
                        tempNode = nodesArray[i, j];
                        nodesArray[i, j] = nodesArray[i, j - 1];
                        nodesArray[i, j - 1] = tempNode;

                        
                    }
                }
            }

            for (int i = 0; i < nodesArray.GetLength(0); i++)
            {
                if (nodesArray[i, nodesArray.GetLength(0) - 1] == null)
                {
                    nodesArray[i, nodesArray.GetLength(0) - 1] = Instantiate(enemyArray[Random.Range(0, 5)], new Vector3(i, nodesArray.GetLength(0) - 1, 0), Quaternion.identity);

                    
                }
            }
        }

    }

    void MoveEnemy(Vector3 start, Vector3 end)
    {
        if (Vector3.Distance(start, end) > 30)
        {
            swipeAngle = Mathf.Atan2((mouseEndPosition.y - mouseStartPosition.y), (mouseEndPosition.x - mouseStartPosition.x)) * 180 / Mathf.PI;
            //print("angle " + swipeAngle);

            if (swipeAngle > -45 && swipeAngle < 45)
            {
                if (nodeXPos < nodesArray.GetLength(0) - 1)
                {
                    print("Swipe Right");

                    movedNode2 = nodesArray[nodeXPos + 1, nodeYPos];

                    movedNode1.GetComponent<Enemy>().MoveEnemy(1, 0);
                    movedNode2.GetComponent<Enemy>().MoveEnemy(-1, 0);


                    tempNode = nodesArray[nodeXPos, nodeYPos];
                    nodesArray[nodeXPos, nodeYPos] = nodesArray[nodeXPos + 1, nodeYPos];
                    nodesArray[nodeXPos + 1, nodeYPos] = tempNode;

                    
                }

            }
            else if (swipeAngle > 45 && swipeAngle < 135)
            {
                if (nodeYPos < nodesArray.GetLength(1) - 1)
                {
                    print("Swipe Up");

                    movedNode2 = nodesArray[nodeXPos, nodeYPos + 1];

                    movedNode1.GetComponent<Enemy>().MoveEnemy(0, 1);
                    movedNode2.GetComponent<Enemy>().MoveEnemy(0, -1);

                    tempNode = nodesArray[nodeXPos, nodeYPos];
                    nodesArray[nodeXPos, nodeYPos] = nodesArray[nodeXPos, nodeYPos + 1];
                    nodesArray[nodeXPos, nodeYPos + 1] = tempNode;

                    
                }
            }
            else if (swipeAngle > -135 && swipeAngle < -45)
            {
                if (nodeYPos > 0)
                {
                    print("Swipe Down");

                    movedNode2 = nodesArray[nodeXPos, nodeYPos - 1];

                    movedNode1.GetComponent<Enemy>().MoveEnemy(0, -1);
                    movedNode2.GetComponent<Enemy>().MoveEnemy(0, 1);

                    tempNode = nodesArray[nodeXPos, nodeYPos];
                    nodesArray[nodeXPos, nodeYPos] = nodesArray[nodeXPos, nodeYPos - 1];
                    nodesArray[nodeXPos, nodeYPos - 1] = tempNode;

                    
                }
            }
            else
            {
                if (nodeXPos > 0)
                {
                    print("Swipe Left");

                    movedNode2 = nodesArray[nodeXPos - 1, nodeYPos];

                    movedNode1.GetComponent<Enemy>().MoveEnemy(-1, 0);
                    movedNode2.GetComponent<Enemy>().MoveEnemy(1, 0);

                    tempNode = nodesArray[nodeXPos, nodeYPos];
                    nodesArray[nodeXPos, nodeYPos] = nodesArray[nodeXPos - 1, nodeYPos];
                    nodesArray[nodeXPos - 1, nodeYPos] = tempNode;

                    
                }
            }
        }

    }

    bool IsNullPresent()
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(0); j++)
            {
                if(nodesArray[i,j] == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void PopulateArray()
    {
        for (int i = 0; i < nodesArray.GetLength(0); i++)
        {
            for (int j = 0; j < nodesArray.GetLength(1); j++)
            {
                //nodesArray[i, j] = Instantiate(enemyArray[Random.Range(0, 5)], new Vector3(i, j, 0), Quaternion.identity);
                //nodesArray[i, j] = Pooler.Instance.Pooluj("En1", new Vector3(i,j,0));
            }
        }
    }

}

