using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void MoveEnemy(int x, int y)
    {
        //print("ruszam sie");
        if (LeanTween.isTweening(gameObject) == false)
        {
            LeanTween.move(gameObject, transform.position + new Vector3(x, y, 0), 0.5f).setEaseInOutBack();
        }
    }
}
