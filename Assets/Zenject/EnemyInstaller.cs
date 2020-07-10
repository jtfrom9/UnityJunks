using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Container.Bind<Player>().AsCached();
        Container.BindInterfacesAndSelfTo<Player>().AsCached();
        Container.BindInterfacesAndSelfTo<Player2>().AsCached();

        Container.BindFactory<float, Enemy1, Enemy1.Factory>();
        Container.BindFactory<string, Enemy2, Enemy2.Factory>();

        // Container.BindFactory<IEnemy, EnemyFactory>().To<Enemy1>();
        Container.BindFactory<int, IEnemy, EnemyFactory>()
            .FromFactory<EnemyFactoryImpl>();

        Container.Bind<EnemySpawner>().AsCached();
    }
}
