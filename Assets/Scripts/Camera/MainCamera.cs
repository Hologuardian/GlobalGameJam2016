using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

    public float panSpeedDefault = 100.0f;
    public float zoomSpeed = 30.0f;
    public float dragSpeedDefault = 30.0f;
    public float rotateSpeed = 30.0f;

    private float zoom = 150;
    float dragSpeed = 30.0f;
    float panSpeed = 100.0f;

    float terrainHeight;
    static Camera mainCamera = Camera.main;
    public Transform target;
    Vector3 translation;
    public Terrain terrain;
    RaycastHit hit;
    Ray ray;


    private Vector3 dragOrigin;

    // Use this for initialization
    void Start()
    {
        ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        Vector3 objHit = new Vector3(hit.point.x, terrain.SampleHeight(hit.point), hit.point.z);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, objHit.y + zoom, mainCamera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //delta y of camera height - sampleheight
        ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        Physics.Raycast(ray, out hit);
        Vector3 objHit = new Vector3(hit.point.x, terrain.SampleHeight(hit.point), hit.point.z);
        Debug.Log(objHit + "" + zoom);
        // Panning
        if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.Translate(new Vector3(panSpeed * Time.deltaTime, 0, 0));
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, objHit.y + zoom, mainCamera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.Translate(new Vector3(-panSpeed * Time.deltaTime, 0, 0));
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, objHit.y + zoom, mainCamera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            mainCamera.transform.Translate(new Vector3(0, 0, panSpeed * Time.deltaTime));
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, objHit.y + zoom, mainCamera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.Translate(new Vector3(0, 0, -panSpeed * Time.deltaTime));
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, objHit.y + zoom, mainCamera.transform.position.z);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetButton("Fire1"))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            mainCamera.transform.Translate(move, Space.World);
        }

        // rotation
        if (Input.GetKey(KeyCode.Q))
        {
            mainCamera.transform.RotateAround(objHit, Vector3.up, -rotateSpeed);
            mainCamera.transform.LookAt(objHit);
        }
        if (Input.GetKey(KeyCode.E))
        {
            mainCamera.transform.RotateAround(objHit, Vector3.up, rotateSpeed);
            mainCamera.transform.LookAt(objHit);
            // transform.Rotate(new Vector3(0, rotateSpeed, 0));
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
            zoom -= zoomSpeed;
            zoom = Mathf.Clamp(zoom, 25, 300);
            dragSpeed = dragSpeedDefault * zoom / 150;
            panSpeed = panSpeedDefault * zoom / 150;
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, objHit.y + zoom, mainCamera.transform.position.z);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoom += zoomSpeed;
            zoom = Mathf.Clamp(zoom, 25, 300);
            dragSpeed = dragSpeedDefault * zoom / 150;
            panSpeed = panSpeedDefault * zoom / 150;
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, objHit.y + zoom, mainCamera.transform.position.z);
        }

    }
}
