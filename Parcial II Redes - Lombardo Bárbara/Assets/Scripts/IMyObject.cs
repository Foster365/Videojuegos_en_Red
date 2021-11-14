using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMyObject
{

    object Function1();
    object Function2(object arg1);
    object Function3(object arg1, object arg2);

}

class MyObject: IMyObject
{

    public object Function1() { return null; }
    public object Function2(object arg1) { return null; }
    public object Function3(object arg1, object arg2) { return null; }

}

class MyOBjectInterceptor : IMyObject
{

    readonly IMyObject MyObject;
    public object Function1()
    {
        Debug.Log("Intercepted Function1");
        return MyObject.Function1();
    }

    public object Function2(object arg1)
    {
        Debug.Log("Intercepted Function2");
        return MyObject.Function2(arg1);
    }

    public object Function3(object arg1, object arg2)
    {
        Debug.Log("Intercepted Function3");
        return MyObject.Function3(arg1, arg2);
    }
}
