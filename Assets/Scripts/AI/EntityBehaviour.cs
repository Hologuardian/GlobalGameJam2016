using System;
using UnityEngine;
using System.Collections;

public class EntityBehaviour : MonoBehaviour
{
    public bool sex;
    public string gender;

    [Range(0,500)]
    public float avoidRange;

    [Range(-100, 100)]
    public float culty;
    [Range(-100, 100)]
    public float faithy;
    [Range(-100, 100)]
    public float mourny;
    [Range(-100, 100)]
    public float farmy;
    [Range(-100, 100)]
    public float socialy;
    [Range(-100, 100)]
    public float wandery;
    [Range(-100, 100)]
    public float sleepyness;

    private ArrayList sleepNodes;
    private ArrayList chaseNodes;
    private ArrayList chaseNodeWeights;
    private ArrayList avoidNodes;
    private ArrayList avoidNodeWeights;

    private Node target;

    // Use this for initialization
    void Start()
    {
        foreach (Node node in AIManager.nodes)
        {
            if (!node.sleepyness)
            {
                float weight = ((node.cultyness * culty) + (node.religyness * faithy) + (node.mournyness * mourny) + (node.farmyness * farmy) + (node.socialness * socialy) + (node.wanderyness * wandery)) / 6;
                if (weight > 0)
                {
                    chaseNodes.Add(node);
                    chaseNodeWeights.Add(weight);
                }
                else if (weight < 0)
                {
                    avoidNodes.Add(node);
                    avoidNodeWeights.Add(weight);
                }
            }
            else
            {
                sleepNodes.Add(node);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // This is where the entity will chase down its target, while avoiding avoids
    }

    /// <summary>
    /// This sets the target the entity will chase, this should be called as infrequently as possible
    /// </summary>
    void SetTarget()
    {
        
    }
}
