using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Component
{
    public GameObject component;
    public string name;
}

public class EntityBehaviour : MonoBehaviour
{
    public string name;
    public bool sex;
    public string gender;

    public string role = "Narmy";

    public GameObject character;
    public Component[] components;

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

        SetAppearance();
    }

    // Update is called once per frame
    void Update()
    {
        goalTimer += Time.deltaTime;

        if (goalTimer > goalCondition)
        {
            CreateAsRandom();

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
                int index = UnityEngine.Random.Range(0, chaseNodes.Count - 1);
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

            // Then go through at most 5 of the culty ones and set the highest weighted one to be the target
            float weight = 0;
            for (int i = 0; i < Mathf.Min(5, nodes.Count); ++i)
            {
                int index = UnityEngine.Random.Range(0, nodes.Count - 1);
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
            target = (Node)sleepNodes[UnityEngine.Random.Range(0, sleepNodes.Count - 1)];
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

    void SetAppearance()
    {
        switch(role)
        {
            case "Narmy":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(true, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            case "Farmy":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(true, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(true, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(true, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            case "Kinda Culty":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(true, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(true, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(true, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            case "Mostly Culty":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(true, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(true, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(true, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(true, "Mask");
                    SetComponentVisibility(true, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            case "Really Culty":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(true, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(true, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(true, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(true, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(true, "Mask");
                    SetComponentVisibility(true, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(true, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            case "Faithy":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(true, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(true, "Robehair");
                    SetComponentVisibility(true, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            case "Virginy":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(true, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(true, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(false, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(true, "Virginhood");
                }
                break;
            case "Wandery":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(true, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(true, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(true, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            case "Charmy":
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(true, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(true, "Hair");
                    SetComponentVisibility(true, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
            default:
                if (sex)
                {
                    // Male
                    SetComponentVisibility(false, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(false, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                else
                {
                    // Female
                    SetComponentVisibility(true, "Bewbs");
                    SetComponentVisibility(false, "Culthood");
                    SetComponentVisibility(true, "Hair");
                    SetComponentVisibility(false, "Mask");
                    SetComponentVisibility(false, "Robehair");
                    SetComponentVisibility(false, "Priestrobe");
                    SetComponentVisibility(false, "Shank");
                    SetComponentVisibility(false, "Sickle");
                    SetComponentVisibility(true, "Skirt");
                    SetComponentVisibility(false, "Torch");
                    SetComponentVisibility(false, "Virginhood");
                }
                break;
        }
    }

    void SetRole(string _role)
    {
        role = _role;
        SetAppearance();
    }

    void UpdateRole()
    {
        if (faithy > 50)
        {
            role = "Faithy";
            // Faithy
        }
        else if (culty > 75)
        {
            role = "Really Culty";
            // Really Culty
        }
        else if (culty > 50)
        {
            role = "Mostly Culty";
            // Mostly Culty
        }
        else if (culty > 25)
        {
            role = "Kinda Culty";
            // Kinda Culty
        }

        if (socialy > Mathf.Max(farmy, wandery) && socialy > 50)
        {
            role = "Socialy";
            // Socially
        }
        else if (farmy > Mathf.Max(socialy, wandery) && farmy > 50)
        {
            role = "Farmy";
            // Farmy
        }
        else if (wandery > Mathf.Max(farmy, socialy) && wandery > 50)
        {
            role = "Wandery";
            // Wandery
        }
        else
        {
            role = "Narmy";
            // Narmy
        }

        Debug.Log(role);
    }

    void SetComponentVisibility(bool state, string name)
    {
        foreach (Component comp in components)
        {
            if (comp.name == name)
            {
                comp.component.GetComponent<Renderer>().enabled = state;
                break;
            }
        }
    }

    /// <summary>
    /// Call this when you are spawning a sim you want to have assigned a random role, that is not religious
    /// </summary>
    public void CreateAsRandom()
    {
        farmy = UnityEngine.Random.Range(-100, 100);
        socialy = UnityEngine.Random.Range(-100, 100);
        wandery = UnityEngine.Random.Range(-100, 100);

        UpdateRole();
        SetAppearance();
    }
}
