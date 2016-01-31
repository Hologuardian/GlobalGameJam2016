using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    enum Cursor { Select, Cultify, Sacrifice, Obelisk, Eclipse};
    Cursor myCursor;

	// Use this for initialization
	void Start () {
        myCursor = Cursor.Select;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Select()
    {
        myCursor = Cursor.Select;
        Debug.Log(myCursor);
    }

    void Cultify()
    {
        myCursor = Cursor.Cultify;
        Debug.Log(myCursor);
    }

    void Sacrifice()
    {
        myCursor = Cursor.Sacrifice;
        Debug.Log(myCursor);
    }

    void Obelisk()
    {
        myCursor = Cursor.Obelisk;
        Debug.Log(myCursor);
    }

    void DayNight()
    {
        myCursor = Cursor.Eclipse;
        Debug.Log(myCursor);
    }

    Cursor GetCursor()
    {
        return myCursor;
    }
}
