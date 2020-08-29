using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake");
    }
    void OnEanble()
    {
        Debug.Log("OnEanble");
    }
    void Start()
    {
        Debug.Log("Start");
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
