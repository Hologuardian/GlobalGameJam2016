using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuScript : MonoBehaviour {


    public Button startText;
    public Button exitText;

	// Use this for initialization
	void Start () {

        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
	}

    public void exitPress()
    {
        Application.Quit();
    }

    public void startPress()
    {
        Application.LoadLevel(1);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
