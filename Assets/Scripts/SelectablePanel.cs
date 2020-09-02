using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectablePanel : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    ScrollRectController scrollRectController;

    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scrollRectController = FindObjectOfType<ScrollRectController>();
    }

    public void Submit()
    {
        if (toggle != null)
        {
            toggle.isOn = !toggle.isOn;
        }
    }

    public void Select()
    {
        Debug.Log($"{name} {rectTransform.rect}");
        scrollRectController.Select(rectTransform);
    }
}

