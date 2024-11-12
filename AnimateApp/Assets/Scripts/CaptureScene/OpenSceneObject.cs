using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneObject : MonoBehaviour
{
    public GameObject targetObject1;
    public GameObject targetObject1rec;
    public GameObject targetObject2;
    public GameObject targetObject2rec;
    public GameObject targetObject3;
    public GameObject targetObject4;


    void Start()
    {
        Debug.Log("Object open script is started.");
        if (StaticClassForPassingData.SelectObject == "First")
        {
            targetObject1.SetActive(true);
            Debug.Log("Object1 is open!");
        }
        else if (StaticClassForPassingData.SelectObject == "FirstRec")
        {
            targetObject1rec.SetActive(true);
            Debug.Log("Object1 is open!");
        }
        else if (StaticClassForPassingData.SelectObject == "Second")
        {
            targetObject2.SetActive(true);
            Debug.Log("Object2 is open!");
        }
        else if (StaticClassForPassingData.SelectObject == "SecondRec")
        {
            targetObject2rec.SetActive(true);
            Debug.Log("Object2 is open!");
        }
        else if (StaticClassForPassingData.SelectObject == "Third")
        {
            targetObject3.SetActive(true);
            Debug.Log("Object3 is open!");
        }
        else if (StaticClassForPassingData.SelectObject == "Fourth")
        {
            targetObject4.SetActive(true);
            Debug.Log("Object4 is open!");
        }
        else
        {
            Debug.LogWarning("No target object assigned!");
        }
    }



}
