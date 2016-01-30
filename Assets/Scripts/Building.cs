using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
    public Vector3 Size;
    public GameObject model;
    public float Scale;
    public Transform target;
    // Use this for initialization
    void Start()
    {
        GameObject building = Instantiate(model, transform.position, Quaternion.AngleAxis(Random.value * 360, Vector3.up)) as GameObject;
        building.transform.localScale = new Vector3(Scale, Scale, Scale);
        building.transform.LookAt(new Vector3(target.position.x, building.transform.position.y, target.position.z));
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.black;
        //Gizmos.DrawCube(transform.position, Size);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
