using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler Instance;
    public List<PoolerSO> ListaPoolowania;

    void Awake()
    {
        Instance = this;
        Inicialize();
    }

    void Inicialize()
    {
        for (int i = 0; i < ListaPoolowania.Count; i++)
        {
            ListaPoolowania[i].InicializeList();
        }
    }

    public GameObject Pooluj(string name, Vector3 pos, bool isActive)
    {
        for (int i = 0; i < ListaPoolowania.Count; i++)
        {
            if (ListaPoolowania[i].ObjName == name)
            {

                GameObject obj = ListaPoolowania[i].SpawnObject();
                //print(ListaPoolowania[i].ObjName + "  przeszukano obiekty");
                //print("znalazlem objekt typu PoolerSO o nazwie " + name + " ");
                obj.transform.position = pos;
                obj.SetActive(isActive);
                return obj;
            }
            else
            {
                
                if (i >= ListaPoolowania.Count)
                {
                    //print("Nie znalazlem obkektu typu PoolerSO o nazwie " + name + " ");
                    return null;
                }
                
            }
        }
        return null;
    }
}
