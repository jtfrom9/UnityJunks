using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectablePanel : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    public void Submit()
    {
        toggle.isOn = !toggle.isOn;
    }
}

