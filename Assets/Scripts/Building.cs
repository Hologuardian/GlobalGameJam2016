using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
    public GameObject entity;

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
        building.transform.parent = transform;
        if (entity != null)
        {
            GameObject ent;
            switch (model.name)
            {
                case "hut":
                    // This means make people
                    // Between 2 and 4 narmys
                    int num = Random.Range(2, 4);
                    for (int i = 0; i < num; ++i)
                    {
                        ent = Instantiate(entity, transform.position, new Quaternion()) as GameObject;
                        ent.GetComponent<EntityBehaviour>().CreateAsRandom();
                    }
                    building.tag = "House";
                    break;
                case "church":
                    // This means make a single priest
                    // 1 faithy
                    ent = Instantiate(entity, transform.position, new Quaternion()) as GameObject;
                    ent.GetComponent<EntityBehaviour>().faithy = 100;
                    ent.GetComponent<EntityBehaviour>().UpdateRole();
                    break;
                case "altar":
                    // This means make an acolyte
                    // 1 mostly culty
                    ent = Instantiate(entity, transform.position, new Quaternion()) as GameObject;
                    ent.GetComponent<EntityBehaviour>().culty = 50;
                    ent.GetComponent<EntityBehaviour>().UpdateRole();
                    building.tag = "Altar";
                    break;
                default:
                    // This means ignore the motherfucker
                    break;
            }
        }
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
