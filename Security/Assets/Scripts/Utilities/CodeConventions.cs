using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// No usings in code, always import usings above class
/// Remove unused usings
/// </summary>
public class CodeConventions : MonoBehaviour
{
    public int PublicTest { get { return privateTest; } set { privateTest = value; } }
    public int PublicTest2 { get { return privateTest; } set { privateTest = value; } }
    public static CodeConventions Instance { get { return GetInstance(); } }

    public int PublicComplicatedTest
    {
        get
        {
            int test1 = 4 / 65;
            int test2 = test1 % 42;
            return test2;
        }
    }

    //TODO: Write unity events syntax
    public static Action testEvent;

    #region can use regions
    #endregion

    private static CodeConventions instance;

    private static CodeConventions GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<CodeConventions>();
        }
        return instance;
    }

    private const string CONST_TEST = "TEST123";

    [SerializeField] [Range(0, 93)] private int serializeFieldTest;

    private int privateTest;

    /// <summary>
    ///  Can use, only above the method
    /// </summary>
    public void PublicExample()
    {

    }

    private void TodoExample()
    {
        //TODO: You can add TODO's as comment in the code
    }

    private void PrivateExample()
    {
        privateTest = 1;
        int interalTest = 2;
        interalTest = 3;

        //no interal conventions for indexes variables in for loops.\\
        for (int i = 0; i < interalTest; i++)
        {

        }

        foreach (var item in new List<int>(3))
        {

        }
    }

    private void OnEventTest()
    {

    }

    private void OnEnable()
    {
        testEvent += OnEventTest;
    }

    private void OnDisable()
    {
        testEvent -= OnEventTest;
    }
}

public enum TestType
{
    First,
    Second,
    Third
}