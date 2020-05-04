using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;

public class uGUIController : MonoBehaviour
{
    public Button countButton;
    public Text countText;

    public Toggle checkToggle;
    public Text checkText;

    public Dropdown dropdown;
    public Text dropdownText;

    public Button keyboradButton;
    public Text inputText;
    public InputField input;

    TouchScreenKeyboard kb;

    async void Start()
    {
        countButton
            .OnClickAsObservable()
            .Subscribe(_ => { Debug.Log("click"); });

        var count = new IntReactiveProperty(0);
        countButton
            .OnClickAsObservable()
            .Subscribe(_ => count.Value++);

        count.SubscribeToText(countText);

        checkToggle
            .OnValueChangedAsObservable()
            .Subscribe(b => checkText.gameObject.SetActive(b));

        dropdown.options.Add(new Dropdown.OptionData("hoge"));
        dropdown.OnValueChangedAsObservable()
            .Select(index => dropdown.options[index].text)
            .SubscribeToText(dropdownText);
        input
            // .OnValueChangedAsObservable()
            .OnEndEditAsObservable()
            .SubscribeToText(inputText);

        if (TouchScreenKeyboard.isSupported)
        {
            keyboradButton.OnClickAsObservable().Subscribe(async _ =>
            {
                if(kb!=null) {
                    Debug.Log("already open");
                    return;
                }
                kb = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "message...");
                Debug.Log($"after open. kb={kb}, status={kb.status}, {kb.text}");
                await UniTask.WaitUntil(() => kb != null && kb.status != TouchScreenKeyboard.Status.Visible);
                Debug.Log($"after input. kb={kb}, status={kb.status}, {kb.text}, {kb.wasCanceled}");
                if (kb.status == TouchScreenKeyboard.Status.Done)
                {
                    inputText.text = kb.text;
                }

                kb = null;
            });
        }
        else
        {
            Debug.Log("TouchScreenKeyboard not supported");
        }
    }

    void OnApplicationFocus(bool focus)
    {
        Debug.Log($"OnApplicationFocus: {focus}");
        if (focus && this.kb!=null && this.kb.status == TouchScreenKeyboard.Status.Visible)
        {
            this.kb.active = false;
            this.kb = null;
            Debug.Log("force to close keyboard");
        }
    }
}
