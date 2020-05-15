using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface ICubeView
{
    void SetColor(Color c);
}

public class CubeView : MonoBehaviour, ICubeView
{
    public void SetColor(Color c)
    {
        var mr = GetComponent<MeshRenderer>();
        mr.material.color = c;
    }
}

public class ICubeViewFactory : PlaceholderFactory<ICubeView> { }
