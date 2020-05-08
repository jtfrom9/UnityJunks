using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Slidein : MonoBehaviour
{
    public Button button;
    public Animator animator;

    void Start()
    {
        button.OnClickAsObservable().Subscribe(_ => {
            Debug.Log("click");
            var b = animator.GetBool("SlideIn");
            animator.SetBool("SlideIn", !b);
        });
    }
}
