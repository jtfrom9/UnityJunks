using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IFoo
{
    void Foo();
}
public class A : IFoo
{
    public void Foo()
    {
        Debug.Log("A");
    }
}
public class B : IFoo
{
    public void Foo()
    {
        Debug.Log("B");
    }
}

public class ZenjectTest : MonoBehaviour
{
    // [Inject]
    IFoo foo;

    void Start()
    {
        foo.Foo();
    }

    [Inject]
    public void set(IFoo foo)
    {
        Debug.Log("set");
        this.foo = foo;
    }
}
