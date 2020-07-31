using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text E1;
    public Text E2;
    public Text E3;
    public Text E4;
    public Text E5;
    int e1Counter = 0;
    int e2Counter = 0;
    int e3Counter = 0;
    int e4Counter = 0;
    int e5Counter = 0;

    public static Score Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void updateScore(GameObject obj)
    {
        switch (obj.tag)
        {
            case "E1":
                e1Counter += 1;
                E1.text = e1Counter.ToString();
                break;

            case "E2":
                e2Counter += 1;
                E2.text = e2Counter.ToString();
                break;

            case "E3":
                e3Counter += 1;
                E3.text = e3Counter.ToString();
                break;

            case "E4":
                e4Counter += 1;
                E4.text = e4Counter.ToString();
                break;

            case "E5":
                e5Counter += 1;
                E5.text = e5Counter.ToString();
                break;
        }

    }
}
