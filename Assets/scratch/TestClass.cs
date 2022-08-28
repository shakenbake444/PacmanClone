using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestClass
{
    public static int score {get; set;}
}

public class TestClassNonStatic
{
    public static int _var {get; set;}
}

public class OtherClassThatInstatiates
{
    public OtherClassThatInstatiates()
    {
        TestClassNonStatic instance = new TestClassNonStatic();
        
        //TestClass testClass = new TestClassNonStatic();
    }
}
