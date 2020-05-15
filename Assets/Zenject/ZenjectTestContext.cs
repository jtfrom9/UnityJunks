using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenjectTestContext : MonoInstaller<ZenjectTestContext>
{
    public enum FooType { A, B };
    public FooType fooType;

    public override void InstallBindings()
    {
        Container.Bind<IFoo>().To<B>().AsCached();
    }
}
