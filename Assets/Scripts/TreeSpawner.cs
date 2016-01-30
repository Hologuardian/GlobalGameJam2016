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

            Vector2 unit = Random.insideUnitCircle * 1000 + new Vector2(1000, 1000);
            Vector3 point = new Vector3(unit.x, terrain.SampleHeight(new Vector3(unit.x, 0, unit.y)), unit.y);
            int x = (int)(((unit.x) / 2000) * terrain.terrainData.alphamapWidth);
            int y = (int)(((unit.y) / 2000) * terrain.terrainData.alphamapHeight);
            float a = terrain.terrainData.GetAlphamaps(x, y, 1, 1)[0, 0, 0];
            Debug.Log(x + " " + y + " " + terrain.terrainData.alphamapWidth + " " + terrain.terrainData.alphamapHeight + " " + a);
            if (a > 0.8f)
                Instantiate(tree, point, transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
