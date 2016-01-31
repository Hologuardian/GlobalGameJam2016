using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
        text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void ShowText()
    {
        text.CrossFadeAlpha(0, 3.0f, true);
        text.enabled = true;
    }

    void HideText()
    {
        text.enabled = false;
        text.CrossFadeAlpha(1, 0.1f, true);
    }
}
