using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface ICubeView
{
    GameObject gameObject { get; }
    void SetColor(Color c);
}

public class ICubeViewFactory : PlaceholderFactory<ICubeView> { }

public static class ICubeViewExtension
{
    public static ICubeView Create(this ICubeViewFactory factory, string name) {
        var view = factory.Create();
        view.gameObject.name = name;
        return view;
    }
}

public class CubeView : MonoBehaviour, ICubeView
{
    GameObject ICubeView.gameObject { get => gameObject; }

    public void SetColor(Color c)
    {
        var mr = GetComponent<MeshRenderer>();
        mr.material.color = c;
    }
}
