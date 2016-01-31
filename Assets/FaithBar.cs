using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FaithBar : MonoBehaviour {

    Slider faithBar;
    public Text faithNum;

    // Use this for initialization
    void Start () {
        faithBar = GetComponent<Slider>();
        faithBar.maxValue = (int)Faith.MaxFaith;
    }
	
	// Update is called once per frame
	void Update () {
        faithBar.value = (int)Faith.CurrentFaith;
        faithNum.text = (int)Faith.CurrentFaith + " / " + (int)Faith.MaxFaith;
    }
}
