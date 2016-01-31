using UnityEngine;
using System.Collections;

public class TreeModelBehaviour : MonoBehaviour
{
    public Renderer Trunk1;
    public Renderer Canopy1;
    public Renderer Trunk2;
    public Renderer Canopy2;
    public Renderer Trunk3;
    public Renderer Canopy3;
    // Use this for initialization
    void Start()
    {
        int i = Random.Range(0, 2);

        if (i == 0)
        {
            Trunk1.enabled = Canopy1.enabled = true;
            Trunk2.enabled = Canopy2.enabled = false;
            Trunk3.enabled = Canopy3.enabled = false;
        }
        else if (i == 1)
        {
            Trunk1.enabled = Canopy1.enabled = false;
            Trunk2.enabled = Canopy2.enabled = true;
            Trunk3.enabled = Canopy3.enabled = false;
        }
        else
        {
            Trunk1.enabled = Canopy1.enabled = false;
            Trunk2.enabled = Canopy2.enabled = false;
            Trunk3.enabled = Canopy3.enabled =true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
