using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FaithBar : MonoBehaviour {

    Slider faithBar;

	// Use this for initialization
	void Start () {
        faithBar = GetComponent<Slider>();
        faithBar.maxValue = (int)Faith.MaxFaith;
    }
	
	// Update is called once per frame
	void Update () {
        faithBar.value = (int)Faith.CurrentFaith;
    }
}
