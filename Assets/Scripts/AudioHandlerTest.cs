using UnityEngine;
using System.Collections;

public class AudioHandlerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioHandler.Instance.PlayBackgroundMusic(AudioHandler.BackgroundMusic.BGMTRIBAL);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
	}
}
