using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public float panSpeed = 100.0f;
    public float zoomSpeed = 30.0f;
    public float dragSpeed = 30.0f;
    public float rotateSpeed = 30.0f;

    static Camera mainCamera = Camera.main;
    public Transform target;
    Vector3 translation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // Panning
	    if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(panSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-panSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, panSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -panSpeed * Time.deltaTime));
        }
        if(Input.GetButtonDown("Fire2"))
        {
            transform.position += new Vector3(Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime, 0);
                //-= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime, 0,
                           //    Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime);
        }

        // rotation
        if(Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(Vector3.up, -rotateSpeed * Time.deltaTime);           
        }
        if(Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        // zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            mainCamera.transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            mainCamera.transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
        }
    }
}
