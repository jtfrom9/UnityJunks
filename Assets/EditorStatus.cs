using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class PlaymodeStateObserver
{
    static PlaymodeStateObserver()
    {
        UnityEditor.EditorApplication.playModeStateChanged += (state) => {
            Debug.Log(state);
        };
    }
}

public class EditorStatus: MonoBehaviour
{
    void Start()
    {
        Debug.Log("Start");
    }
    void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
    }
    void OnApplicationPause()
    {
        Debug.Log("OnApplicationPause");
    }
}
