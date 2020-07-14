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
        // create another instance for each other
        Container.Bind<IFoo>().To<B>().AsCached();
        Container.Bind<IBar>().To<B>().AsCached();
        Container.Bind<B>().To<B>().AsCached();

        // // same instance for IFoo, IBar
        // Container.BindInterfacesTo<B>().AsCached();
        // // another instance for B
        // Container.Bind<B>().To<B>().AsCached();

        // same instance for all(IFoo, IBar, B)
        // Container.BindInterfacesAndSelfTo<B>().AsCached();

        // Error
        // Assert hit! Found multiple creation bindings for type 'B' in addition to AsSingle. 
        // The AsSingle binding must be the definitive creation binding.  
        // If this is intentional, use AsCached instead of AsSingle.
        // Container.Bind<IFoo>().To<B>().AsCached();
        // Container.Bind<IBar>().To<B>().AsSingle(); // occur error

        Container.Bind<ZenjectTest2>()
            .AsSingle()
            .WithArguments(ZenjectTest2.Type.A)
            .NonLazy();

        Container.Bind<IInitializable>()
        // Container.BindInterfacesAndSelfTo<C>()
        // Container.Bind(typeof(IInitializable),typeof(C))
            .To<C>()
            .AsCached()
            .NonLazy();

        Container.Bind<ITickable>()
            .To<C>()
            .AsCached()
            .NonLazy();

        Container.BindInterfacesTo<D>()
            .AsSingle()
            .NonLazy();

        // Container.Bind<E>().To<E>().AsSingle().NonLazy();

        Container.Bind<E1>().To<E1>().AsSingle().NonLazy();
        Container.Bind<E1>().To<E2>().AsSingle().NonLazy();
        // error. E3 has [Inject] for E1 and IInitializable.
        // These are Binded doubly above
        // Container.Bind<E3>().To<E3>().AsSingle().NonLazy();
    }
}
