using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : IInitializable
{
    public void Initialize()
    {
        Debug.Log("Player.Initialize");
    }
}

public class Player2 : IInitializable
{
    public void Initialize()
    {
        Debug.Log("Player2.Initialize");
    }
}

public interface IEnemy
{
    void Run();
}

public class Enemy1 : IEnemy
{
    readonly Player _player;
    readonly float _speed;

    // public Enemy(float speed, Player player)
    public Enemy1(Player player, float speed)
    {
        Debug.Log($"Enemy1: speed={speed}");
        _player = player;
        _speed = speed;
    }
    public void Run() { }

    public class Factory : PlaceholderFactory<float, Enemy1> { }
}

public class Enemy2 : IEnemy
{
    readonly Player _player;
    readonly string _name;

    public Enemy2(Player player, string name)
    {
        Debug.Log($"Enemy2: name={name}");
        _player = player;
        _name = name;
    }
    public void Run() { }

    public class Factory : PlaceholderFactory<string, Enemy2> { }
}

public class EnemyFactory : PlaceholderFactory<IEnemy> { }

public class EnemyFactoryImpl: IFactory<IEnemy>
{
    Enemy1.Factory f1;
    Enemy2.Factory f2;

    public EnemyFactoryImpl(Enemy1.Factory f1, Enemy2.Factory f2)
    {
        this.f1 = f1;
        this.f2 = f2;
    }
    public IEnemy Create()
    {
        return f1.Create(0);
    }
}
