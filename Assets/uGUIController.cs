using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using UniRx.Async.Triggers;

public class uGUIController : MonoBehaviour
{
    Button button;

    async void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await button.GetAsyncClickEventHandler(token).OnClickAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
