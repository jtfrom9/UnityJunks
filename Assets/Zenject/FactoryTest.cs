using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner
{
    Enemy1.Factory f1;
    Enemy2.Factory f2;

    public IEnemy Spawn(float v)
    {
        return f1.Create(v);
    }

    public IEnemy Spawn(string n)
    {
        return f2.Create(n);
    }

    public EnemySpawner(Enemy1.Factory f1, Enemy2.Factory f2)
    {
        this.f1 = f1;
        this.f2 = f2;
    }
}

public class FactoryTest : MonoBehaviour
{
    Enemy1.Factory _enemy1Factory;
    EnemySpawner spawner;
    EnemyFactory enemyFactory;
    ICubeViewFactory cubeViewFactory;

    [Inject]
    public void Construct(Enemy1.Factory e1, EnemySpawner spawner, EnemyFactory efactory, ICubeViewFactory cubeViewFactory)
    {
        this._enemy1Factory = e1;
        this.spawner = spawner;
        this.enemyFactory = efactory;
        this.cubeViewFactory = cubeViewFactory;
    }

    void Awake()
    {
        Debug.Log($"FactoryTest.Awake: {_enemy1Factory}");

        Debug.Log(spawner.Spawn(0).GetType());
        Debug.Log(spawner.Spawn("hoge").GetType());

        Debug.Log("--- create from EnemyFactoryImpl");
        Debug.Log(enemyFactory.Create(1).GetType());

        var cv = cubeViewFactory.Create("test cube");
        cv.SetColor(Color.red);
    }

    void Start()
    {
        Debug.Log($"FactoryTest.Start: {_enemy1Factory}");
        var enemy = _enemy1Factory.Create(Random.Range(1F, 100F));
    }
}
