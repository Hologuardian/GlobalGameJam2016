using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
    public Vector3 Size;
    // Use this for initialization
    void Start()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position, Size);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
