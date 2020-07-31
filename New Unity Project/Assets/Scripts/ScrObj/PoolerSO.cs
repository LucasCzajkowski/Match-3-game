using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PoolerSO : ScriptableObject
{
    public GameObject PrefabObject;
    public int InicializeAmmount = 1;
    public string ObjName = "Default";
    public List<GameObject> PrefabObjectList;
    GameObject TempGameObject;

    //void Awake()
    //{
    //    //InicializeList();
    //}

    public void InicializeList()
    {
        PrefabObjectList.Clear();
        for (int i = 0; i < InicializeAmmount; i++)
        {
            TempGameObject = (GameObject)Instantiate(PrefabObject, Vector3.zero, Quaternion.identity);
            TempGameObject.SetActive(false);
            PrefabObjectList.Add(TempGameObject);
        }
    }

    public void AddObjectToList()
    {
        GameObject obj = Instantiate(PrefabObject, Vector3.zero, Quaternion.identity);
        obj.SetActive(false);
        PrefabObjectList.Add(obj);
    }

    public GameObject SpawnObject()
    {
        for (int i = 0; i < PrefabObjectList.Count; i++)
        {
            //Debug.Log(i);
            GameObject obj = (GameObject)PrefabObjectList[i];
            if (obj.activeInHierarchy == false)
            {
                //Debug.Log("liczba obiektow ktore mozna spawnowac to "+ PrefabObjectList.Count);
                //Debug.Log("znalazlem nieaktywny obiekt do wykorzystania   "+ obj );
                //Debug.Log("wyrzucam obiekt");
                return obj;
            }
            if (obj.activeInHierarchy == true && i == PrefabObjectList.Count-1)
            {
                AddObjectToList();

            }
            //else
            //{
            //    //AddObjectToList();
            //    //GameObject obj2 = (GameObject)PrefabObjectList[i];
            //    //if (obj2.activeSelf == false)
            //    //{
            //    //    return obj2;
            //    //}
            //    //else
            //    //{
            //    Debug.Log("nie znalazlem nieaktywny obiekt do wykorzystania   " );
            //    //return null;
            //    //}

            //}
        }
        return null;
    }
}
