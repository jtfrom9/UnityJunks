using UnityEngine;
using Zenject;

public class CubeFactoryInstaller : MonoInstaller
{
    public GameObject prefab;
    public override void InstallBindings()
    {
        Container.BindFactory<ICubeView, ICubeViewFactory>()
            .FromComponentInNewPrefab(prefab);
    }
}