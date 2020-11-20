using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(EventTrigger))]
public class SelectablePanel : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    ScrollRectController scrollRectController;

    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scrollRectController = gameObject.GetComponentInParent<ScrollRectController>();
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
        if (scrollRectController)
        {
            Debug.Log($"parent is {scrollRectController.name}");
            scrollRectController.Select(rectTransform);
        } else {
            Debug.LogError("not scroll rect controller");
        }
    }
}

