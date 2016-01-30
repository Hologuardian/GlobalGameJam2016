using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour {

    public GameObject tree;
    public Terrain terrain;
	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < 5000; ++i)
        {
            RaycastHit hit;

            Vector2 unit = Random.insideUnitCircle * 1000;
            Ray r = new Ray(transform.position + new Vector3(unit.x, 0, unit.y), new Vector3(0, -1, 0));
            Physics.Raycast(r, out hit);
            int x = (int)(((unit.x + 1000) / 2000) * terrain.terrainData.alphamapWidth);
            int y = (int)(((unit.y + 1000) / 2000) * terrain.terrainData.alphamapHeight);
            float a = terrain.terrainData.GetAlphamaps(x, y, 1, 1)[0, 0, 0];
            Debug.Log(x + " " + y + " " + terrain.terrainData.alphamapWidth + " " + terrain.terrainData.alphamapHeight + " " + a);
            if (a > 0.8f)
                Instantiate(tree, hit.point, transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
