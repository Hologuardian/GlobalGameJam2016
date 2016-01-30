using UnityEngine;
using System.Collections;

public class NodePopulateTest : MonoBehaviour
{
    public GameObject node;

    // Use this for initialization
    void Start()
    {
        float rad = 0;
        float ang = 0;

        for (int i = 0; i < Random.Range(50,100); ++i)
        {
            rad = Random.Range(0.0f, 250.0f);
            ang = Random.Range(0.0f, Mathf.PI * 2.0f);

            RaycastHit hit = new RaycastHit();
            Vector3 top = new Vector3(Mathf.Cos(ang) * rad + 1600, 1000, Mathf.Sin(ang) * rad + 600);
            Ray ray = new Ray(top, new Vector3(0, -1, 0));
            Physics.Raycast(ray, out hit);

            GameObject nodeInst = Instantiate(node, hit.point, new Quaternion()) as GameObject;
            Node nodeInstNode = nodeInst.GetComponent<NodeTest>().node;
            nodeInstNode.cultyness = Random.Range(-100, 100);
            nodeInstNode.farmyness = Random.Range(-100, 100);
            nodeInstNode.mournyness = Random.Range(-100, 100);
            nodeInstNode.religyness = Random.Range(-100, 100);
            nodeInstNode.socialness = Random.Range(-100, 100);
            nodeInstNode.wanderyness = Random.Range(-100, 100);

            if (Random.Range(0,5) <= 1)
            {
                nodeInstNode.sleepyness = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
