using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBind : MonoBehaviour
{
    void Start()
    {
        var textUI = GetComponentInChildren<UnityEngine.UI.Text>();
        if(textUI) {
            textUI.text = gameObject.name;
        }
    }
}
