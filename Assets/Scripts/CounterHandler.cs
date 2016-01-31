using UnityEngine;
using System.Collections;

public class CounterHandler : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
    {
        AudioHandler.Instance.PlayBackgroundMusic(AudioHandler.BackgroundMusic.BGMTRIBAL);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
