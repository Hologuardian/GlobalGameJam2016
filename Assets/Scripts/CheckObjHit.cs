using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    public int layerMask = -1;

	// Use this for initialization
	void Start () {
	
	}

    GameObject GetClickedObj()
    {
        //cast ray from main camera, starting from where mouse was clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //cast ray, return object it hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            hit.transform.gameObject.BroadcastMessage("onClick");
            return hit.transform.gameObject;
        }

        else
            return null;

    }
	
	// Update is called once per frame
	void Update () {
        GameObject clickedgmobj = null;
        if (Input.GetMouseButtonDown(0))
        {
            clickedgmobj = GetClickedObj();

            //display true
            if (clickedgmobj != null) 
            Debug.Log(clickedgmobj.name);
        }
        
	
	}
}
