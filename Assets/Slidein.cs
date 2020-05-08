using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Slidein : MonoBehaviour
{
    public Button buttonMecanim;
    public Animator animator;
    public Button buttonITween;
    public GameObject panelITween;

    void Start()
    {
        // Mecanim
        buttonMecanim.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("click");
            var b = animator.GetBool("SlideIn");
            animator.SetBool("SlideIn", !b);
        });

        // iTween
        var rtrans = panelITween.GetComponent<RectTransform>();
        rtrans.anchoredPosition += new Vector2(2000, 0);

        bool slideOut = true;
        buttonITween.OnClickAsObservable().Subscribe(_ =>
        {
            iTween.MoveTo(panelITween, iTween.Hash(
                "x", (slideOut) ? 0f : 2000f,
                "islocal", true
            ));
            slideOut = !slideOut;
        });
    }
}
