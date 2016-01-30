using UnityEngine;
using System.Collections;

public class EntityBehaviour : MonoBehaviour
{
    private string name;
    private bool sex;
    private string gender;

    public bool virginosity;

    [Range(0, 500)]
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

    public NavMeshAgent navMe;

    private ArrayList sleepNodes;
    private ArrayList chaseNodes;
    private ArrayList chaseNodeWeights;

    private Node target;

    private bool isDay;

    private float goalTimer;
    private float goalCondition = 10;

    // Use this for initialization
    void Start()
    {
        isDay = true;

        sleepNodes = new ArrayList();
        chaseNodes = new ArrayList();
        chaseNodeWeights = new ArrayList();

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
        goalTimer += Time.deltaTime;

        if (goalTimer > goalCondition)
        {
            isDay = !isDay;
            SetTarget();
            goalTimer = 0;
        }

        if (target == null)
        {
            UpdateNodes();
            SetTarget();
        }
    }

    /// <summary>
    /// This sets the target the entity will chase, this should be called as infrequently as possible
    /// </summary>
    void SetTarget()
    {
        if (isDay)
        {
            // Keep a temp variable for the weight of the last assigned target
            float weight = 0;
            // Grab five random nodes
            for (int i = 0; i < 5; ++i)
            {
                int index = Random.Range(0, chaseNodes.Count - 1);
                Node node = (Node)chaseNodes[index];
                // If the new node has a higher weight than the current target make the target the new node
                if ((float)chaseNodeWeights[index] > weight)
                {
                    target = node;
                    weight = (float)chaseNodeWeights[index];
                }
            }
        }
        else if (culty > 0)
        {
            ArrayList nodes = new ArrayList();
            ArrayList weights = new ArrayList();

            // Go through all chaseNodes
            for (int i = 0; i < chaseNodes.Count; ++i)
            {
                // Keep only the ones that are culty
                if (((Node)chaseNodes[i]).cultyness > 0)
                {
                    nodes.Add(chaseNodes[i]);
                    weights.Add(chaseNodeWeights[i]);
                }
            }

            // Then go through the at most 5 of the culty ones and set the highest weighted one to be the target
            float weight = 0;
            for (int i = 0; i < Mathf.Min(5, nodes.Count); ++i)
            {
                int index = Random.Range(0, nodes.Count - 1);
                Node node = (Node)nodes[index];
                if ((float)weights[index] > weight)
                {
                    target = node;
                    weight = (float)weights[index];
                }
            }
        }
        else
        {
            target = (Node)sleepNodes[Random.Range(0, sleepNodes.Count - 1)];
        }

        navMe.SetDestination(target.transform.position);
    }

    public void UpdateNodes()
    {
        chaseNodes.Clear();
        chaseNodeWeights.Clear();

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
            }
            else
            {
                sleepNodes.Add(node);
            }
        }
    }

    public string GetName()
    {
        return name;
    }

    public bool GetSex()
    {
        return sex;
    }

    public string GetGender()
    {
        return gender;
    }

    public void SetCultyness(float cultyness)
    {
        culty = cultyness;
        UpdateNodes();
    }
}
