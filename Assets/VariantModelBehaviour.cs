using UnityEngine;
using System.Collections;

public class VariantModelBehaviour : MonoBehaviour {
    public Renderer[] variants;
	// Use this for initialization
	void Start () {
        foreach (Renderer rend in variants) {
            rend.enabled = false;

        }

        variants[Random.Range(0, variants.Length - 1)].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
