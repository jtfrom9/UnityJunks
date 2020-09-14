using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using libA;
using libB;

public class Cmod : MonoBehaviour
{
    void Start()
    {
        libA.A.foo();
        Bmod bmod;
    }
}
