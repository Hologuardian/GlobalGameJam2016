using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {

    float rotationY = 0.0f;
    Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(330.0f, rotationY, 0.0f);

        rotationY += 0.15f;

        if(rotationY == 360.0f)
        {
            rotationY = 0.0f;
        }
    }
}
