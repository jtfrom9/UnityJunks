using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ScrollRectController : MonoBehaviour
{
    ScrollRect scrollRect;
    void Start()
    {
        scrollRect = GetComponent<UnityEngine.UI.ScrollRect>();
    }

    public void Select(RectTransform child)
    {
        var viewPortHeight = scrollRect.viewport.rect.height;
        var contentPosY = scrollRect.content.anchoredPosition.y;
        Debug.Log($"ViewPortHeight={viewPortHeight}, contentPosY={contentPosY.ToString("F")}");

        var refY = -child.offsetMax.y;
        var visible = (contentPosY <= refY && (refY + child.rect.height) <= contentPosY + viewPortHeight);

        Debug.Log($"{child.name} is {visible} (refY={refY})");
        Debug.Log($"  anchoredPosition={child.anchoredPosition}");
        Debug.Log($"       anchoredMax={child.anchorMax}");
        Debug.Log($"       anchoredMin={child.anchorMin}");
        Debug.Log($"         offsetMax={child.offsetMax}");
        Debug.Log($"         offsetMin={child.offsetMin}");
        Debug.Log($"              rect={child.rect}");
        Debug.Log($"             pivot={child.pivot}");
        Debug.Log($"                up={child.up}");

        if (!visible)
        {
            var newY = refY + child.rect.height - viewPortHeight;
            scrollRect.content.anchoredPosition = new Vector2
            {
                x = scrollRect.content.anchoredPosition.x,
                y = (newY < 0) ? refY : newY
            };
            Debug.Log($">> new contentPosY={scrollRect.content.anchoredPosition.y.ToString("F")} = {refY} + {child.rect.height} - {viewPortHeight}");
        }
    }
}
