﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IFoo
{
    void Foo();
}
public interface IBar
{
    void Bar();
}
public class A : IFoo, IBar
{
    public void Foo()
    {
        Debug.Log("A.Foo");
    }
    public void Bar()
    {
        Debug.Log("A.Bar");
    }
}
public class B : IFoo, IBar
{
    public void Foo()
    {
        Debug.Log("B");
    }
    public void Bar()
    {
        Debug.Log("B.Bar");
    }
}

public class C: IInitializable, ITickable
{
    public void Initialize()
    {
        Debug.Log($"C.Initialize {GetHashCode()}");
    }
    public void Tick()
    {
        Debug.Log($"C.Tick {GetHashCode()}");
    }
}

public class D: IInitializable, ITickable
{
    public void Initialize()
    {
        Debug.Log($"D.Initialize {GetHashCode()}");
    }
    public void Tick()
    {
        Debug.Log($"D.Tick {GetHashCode()}");
    }
}

public class E1
{
}
public class E2: E1
{}
public class E3
{
    [Inject] E1 e1;
    [Inject] IInitializable initializable;
}

public class E
{
    [Inject] IInitializable initializable;
}

public class ZenjectTest : MonoBehaviour
{
    IFoo foo;
    IBar bar;
    B b;

    void Start()
    {
        foo.Foo();
    }

    [Inject]
    public void set(IFoo foo, IBar bar, B b)
    {
        this.foo = foo;
        this.bar = bar;
        Debug.Log($"foo={foo.GetHashCode()}, bar={bar.GetHashCode()}, b={b.GetHashCode()}");
        Debug.Log($"foo={(foo as B).GetHashCode()}, bar={(bar as B).GetHashCode()}");
    }
}

public class ZenjectTest2
{
    public enum Type { A, B };
    Type type;
    IFoo foo;
    public ZenjectTest2(Type type, IFoo foo)
    {
        this.foo = foo;
        this.type = type;
    }
}
