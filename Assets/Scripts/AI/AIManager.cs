using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Node
{
    public Transform transform;

    public bool sleepyness;

    [Range(-100, 100)]
    public float cultyness;
    [Range(-100, 100)]
    public float religyness;
    [Range(-100, 100)]
    public float mournyness;
    [Range(-100, 100)]
    public float farmyness;
    [Range(-100, 100)]
    public float socialness;
    [Range(-100, 100)]
    public float wanderyness;

    public Node()
    {
        AIManager.nodes.Add(this);
    }
}

public class AIManager : MonoBehaviour
{
    public static ArrayList nodes;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
