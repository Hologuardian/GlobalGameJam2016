using UnityEngine;
using System.Collections;

public class ObeliskBehaviour : MonoBehaviour
{
    public bool isActive;

    // Use this for initialization
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Disable()
    {
        isActive = false;
    }
}
