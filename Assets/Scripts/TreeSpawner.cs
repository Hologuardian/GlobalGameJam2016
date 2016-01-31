using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Determ
{
    public int Sample = 0;
    public float Determinator = 0.5f;
    public bool less = false;
}

public class TreeSpawner : MonoBehaviour
{

    public GameObject tree;
    public Terrain terrain;
    public float verticalOffset;
    public int spawnNumber = 5000;
    public Determ[] determ;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < spawnNumber; ++i)
        {
            Vector2 unit = UnityEngine.Random.insideUnitCircle * 1000 + new Vector2(1000, 1000);
            Vector3 point = new Vector3(unit.x, terrain.SampleHeight(new Vector3(unit.x, 0, unit.y)) - verticalOffset, unit.y);
            int x = (int)(((unit.x) / 2000) * terrain.terrainData.alphamapWidth);
            int y = (int)(((unit.y) / 2000) * terrain.terrainData.alphamapHeight);
            bool spawn = false;
            var alphaMap = terrain.terrainData.GetAlphamaps(x, y, 1, 1);
            foreach (Determ d in determ)
            {
                float a = alphaMap[0, 0, d.Sample];
                if(d.less)
                {
                    if (a <= d.Determinator && a > 0)
                    {
                        spawn = true;
                    }
                }
                else
                {
                    if (a > d.Determinator)
                    {
                        spawn = true;
                    }
                }
            }
            if (spawn)
            {
                Vector2 rand = UnityEngine.Random.insideUnitCircle;
                GameObject obj = Instantiate(tree, point, Quaternion.LookRotation(new Vector3(rand.x, 0, rand.y))) as GameObject;
                float width = (UnityEngine.Random.value * 0.2f) + 0.9f;
                obj.transform.localScale = new Vector3((UnityEngine.Random.value * 0.5f) + 0.75f, width, width);
                obj.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
