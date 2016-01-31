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
    public static NameManager nameManager;

    [Header("Basics")]
    public string name;
    public bool isBaby;
    public bool sex;
    public string gender;

    public string role = "Narmy";

    [Header("Render Stuff")]
    public GameObject character;
    public Component[] components;

    [Header("Attributes")]
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

    [Header("Conditionals")]
    public bool isSacrificable;
    public bool canBreed;

    [Header("Navi")]
    public NavMeshAgent navMe;

    [Header("Reproduction")]
    public GameObject Baby;
    public GameObject Adult;

    private ArrayList sleepNodes;
    private ArrayList chaseNodes;
    private ArrayList chaseNodeWeights;

    private Node target;

    private float updateElapsed;
    private static float updateGoal = 0.5f;

    private static int Count;

    private int daysAsChild;

    private bool isDay;

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

        if (UnityEngine.Random.Range(0, 9) <= 4)
        {
            sex = true;
        }
        else
        {
            sex = false;
        }

        SetAppearance();

        if (!sex)
            name = nameManager.names[UnityEngine.Random.Range(0, (nameManager.names.Length / 2) - 1)];
        else
            name = nameManager.names[UnityEngine.Random.Range((nameManager.names.Length / 2) - 1, nameManager.names.Length - 1)];

        canBreed = true;
        isSacrificable = false;

        ++Count;
    }

    // Update is called once per frame
    void Update()
    {
        updateElapsed += Time.deltaTime;

        if (DayNightCycle.Cycle.dayElapsed > DayNightCycle.Cycle.dayStart && DayNightCycle.Cycle.dayElapsed < DayNightCycle.Cycle.dayLength)
        {
            if (!isDay)
            {
                isDay = true;
                SetTarget();

                if (isBaby)
                {
                    ++daysAsChild;
                    UpdateChild();
                }
            }
            if (!isBaby)
            {
                UpdateDay();
            }
        }
        else
        {
            if (isDay)
            {
                isDay = false;
                SetTarget();
            }
            if (!isBaby)
                UpdateNight();
        }

        if (target == null)
        {
            UpdateNodes();
            SetTarget();
        }
    }

    void UpdateDay()
    {
        if (updateElapsed < updateGoal)
        {
            return;
        }

        updateElapsed -= UnityEngine.Random.Range(updateGoal * 0.5f, updateGoal * 2f);

        // Node-stuff and cult things
        // Have a chance of becoming breed ready again
        if (!canBreed && UnityEngine.Random.Range(0, 9) == 0)
        {
            canBreed = true;
        }

        // Checking to see if culty and near obelisks
        if (culty > 0)
        {
            GameObject obj = BehaviourUtil.NearestObjectByTag(this.gameObject, "Obelisk", 10);
            if (obj != null)
            {
                // Increment faith goes here
                //if (obj.GetComponent<ObeliskBehaviour>.isActive)
                //{
                    Faith.faith += (Faith.obeliskGain / Faith.obeliskTimer) * Time.deltaTime;
                //}
            }
        }

        // Checking to see if faithy and near obelisks
        if (faithy > 0)
        {
            GameObject obelisk = BehaviourUtil.NearestObjectByTag(this.gameObject, "Obelisk", 10);
            if (obelisk != null)
            {
                // Disable the obelisk
                obelisk.BroadcastMessage("Disable");
            }
        }
    }

    void UpdateNight()
    {
        if (updateElapsed < updateGoal)
        {
            return;
        }

        updateElapsed -= UnityEngine.Random.Range(updateGoal * 0.5f, updateGoal * 2f);

        // Breeding and sacrificing

        // Checking to see if you are flagged for sacrifice
        if (isSacrificable)
        {
            GameObject house = BehaviourUtil.NearestObjectByTag(this.gameObject, "House", 10);
            if (house != null)
            {
                role = "Virginy";
                SetAppearance();
                // Set Destination for sacrifice tablet
                GameObject altar = GameObject.Find("Altar");// BehaviourUtil.NearestObjectByTag(this.gameObject, "Altar", 3000);
                navMe.SetDestination(altar.transform.position);
                Debug.Log(altar);
                Debug.Log(navMe);
                // If at sacrifice sacrific, and gain faiths!
                if ((transform.position - altar.transform.position).magnitude <= 20)
                {
                    // Handle the sacrifice shit.
                    ArrayList cultists = BehaviourUtil.SurroundingObjectsByTag(this.gameObject, "Entity", 20);
                    // Reward faith based on number of cultists present

                    AudioHandler.Instance.PlayVoiceOver(AudioHandler.VoiceOvers.WILHELMSCREAM);

                    --Count;
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            // Checking to see if you are culty to go do culty stuffs
            if (culty > 0)
            {
                GameObject house = BehaviourUtil.NearestObjectByTag(this.gameObject, "House", 10);
                if (house != null)
                {
                    GameObject altar = GameObject.Find("Altar");
                    navMe.SetDestination(altar.transform.position);
                }
            }
            else
            {
                if (Count < 100)
                {
                    // Checking to see if opposite gender is close by (3-5) to breed
                    GameObject house = BehaviourUtil.NearestObjectByTag(this.gameObject, "House", 10);
                    if (house != null)
                    {
                        ArrayList entities = BehaviourUtil.SurroundingObjectsByTag(this.gameObject, "Entity", 5);
                        if (entities != null)
                        {
                            foreach (GameObject obj in entities)
                            {
                                EntityBehaviour ent = obj.GetComponent<EntityBehaviour>();
                                if (ent != null)
                                {
                                    if (ent.sex != sex)
                                    {
                                        if (!sex)
                                        {
                                            if (canBreed && Baby != null)
                                            {
                                                // Make baby
                                                canBreed = !canBreed;
                                                Instantiate(Baby, transform.position, new Quaternion());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void UpdateChild()
    {
        if (daysAsChild >= 3)
        {
            Debug.Log("Grow up");
            Instantiate(Adult, transform.position, new Quaternion());
            --Count;
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// This sets the target the entity will chase, this should be called as infrequently as possible
    /// </summary>
    void SetTarget()
    {
        if (isDay)
        {
            if (chaseNodes.Count > 0)
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
        }
        else if (culty > 0)
        {
            if (chaseNodes.Count > 0)
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

                if (nodes.Count > 0)
                {
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
            }
        }
        else
        {
            target = (Node)sleepNodes[UnityEngine.Random.Range(0, sleepNodes.Count - 1)];
        }

        if (target != null)
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

    public void SetAppearance()
    {
        switch (role)
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

    public void UpdateRole()
    {
        if (faithy >= 50)
        {
            role = "Faithy";
            Debug.Log(role);
            // Faithy
        }
        else if (culty >= 75)
        {
            role = "Really Culty";
            // Really Culty
        }
        else if (culty >= 50)
        {
            role = "Mostly Culty";
            Debug.Log(role);
            // Mostly Culty
        }
        else if (culty >= 25)
        {
            role = "Kinda Culty";
            // Kinda Culty
        }
        else if (socialy > Mathf.Max(farmy, wandery) && socialy >= 50)
        {
            role = "Socialy";
            // Socially
        }
        else if (farmy > Mathf.Max(socialy, wandery) && farmy >= 50)
        {
            role = "Farmy";
            // Farmy
        }
        else if (wandery > Mathf.Max(farmy, socialy) && wandery >= 50)
        {
            role = "Wandery";
            // Wandery
        }
        else
        {
            role = "Narmy";
            // Narmy
        }

        SetAppearance();
        UpdateNodes();
        SetTarget();
    }

    void SetComponentVisibility(bool state, string name)
    {
        foreach (Component comp in components)
        {
            if (comp.name == name)
            {
                comp.component.GetComponent<Renderer>().enabled = state;
            }
        }
    }

    /// <summary>
    /// Call this when you are spawning a sim you want to have assigned a random role, that is not religious
    /// </summary>
    public void CreateAsRandom()
    {
        farmy = (UnityEngine.Random.Range(-100, 100) + UnityEngine.Random.Range(-100, 100)) / 2;
        socialy = (UnityEngine.Random.Range(-100, 100) + UnityEngine.Random.Range(-100, 100)) / 2;
        wandery = (UnityEngine.Random.Range(-100, 100) + UnityEngine.Random.Range(-100, 100)) / 2;

        UpdateRole();
        SetAppearance();
        UpdateNodes();
        SetTarget();
    }

    void OnClick(HUD.Cursor cursor)
    {
        switch(cursor)
        {
            case HUD.Cursor.Cultify:
                if (culty + 30 <= 100)
                {
                    culty += 30;
                    Debug.Log("I am being cultified: " + culty);
                }
                else
                {
                    HUD.Faith += 5;
                    Debug.Log("I am already culty: " + culty);
                }
                break;
            case HUD.Cursor.Sacrifice:
                if (!isSacrificable && culty <= 0 && faithy <= 25)
                {
                    isSacrificable = true;
                    Debug.Log("I am ready to be sacrificed");
                }
                else
                {
                    HUD.Faith += 10;
                    HUD.canSacrifice = true;
                    Debug.Log("I am already going to be sacrificed");
                }
                break;
            default:
                break;
        }

        UpdateRole();
        SetAppearance();
        UpdateNodes();
        SetTarget();
    }
}
