using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

    public float panSpeed = 100.0f;
    public float zoomSpeed = 30.0f;
    public float dragSpeed = 30.0f;
    public float rotateSpeed = 30.0f;

    static Camera mainCamera = Camera.main;
    public Transform target;
    Vector3 translation;

    private Vector3 dragOrigin;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Panning
        if (Input.GetKey(KeyCode.D))
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

        if (Input.GetButtonDown("Fire1"))
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetButton("Fire1"))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            transform.Translate(move, Space.World);
        }

        // rotation
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -rotateSpeed, 0));
            //transform.RotateAround(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, rotateSpeed, 0));
            //transform.RotateAround(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        // rotation borked
        if (Input.GetKey(KeyCode.Alpha1))
        {
            transform.Rotate(new Vector3(mainCamera.transform.position.x, -rotateSpeed, mainCamera.transform.position.z));
            //transform.RotateAround(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            transform.Rotate(new Vector3(mainCamera.transform.position.x, rotateSpeed, mainCamera.transform.position.y));
            //transform.RotateAround(Vector3.up, rotateSpeed * Time.deltaTime);
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
