using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Cursor { Select, Cultify, Sacrifice, Obelisk, Eclipse };
    Cursor myCursor;

    public GameObject ObeliskObj;

    public static int Faith;

    public static bool canSacrifice;
    private bool isDay;

    // Use this for initialization
    void Start()
    {
        myCursor = Cursor.Select;
        Faith = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        // Aidan Vomit stuff, I sorry, sleep, wanted work
        if (Input.GetButtonDown("Fire1"))
        {
            if (myCursor == Cursor.Cultify || myCursor == Cursor.Obelisk || myCursor == Cursor.Sacrifice || myCursor == Cursor.Select)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Physics.Raycast(ray, out hit);
                if (hit.collider.gameObject.tag == "Entity")
                {
                    if (myCursor == Cursor.Cultify && Faith > 5)
                    {
                        Faith -= 5;
                        hit.collider.gameObject.BroadcastMessage("OnClick", myCursor, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("Cultified an entity");
                    }
                    else if (myCursor == Cursor.Sacrifice && Faith > 10 && canSacrifice)
                    {
                        Faith -= 10;
                        canSacrifice = false;
                        hit.collider.gameObject.BroadcastMessage("OnClick", myCursor, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("Sacrificed an entity");
                    }
                }
            }
            else
            {
                if (Faith > 100 && isDay)
                {
                    Faith -= 100;
                    DayNightCycle.Cycle.dayElapsed = DayNightCycle.Cycle.dayLength;
                }
            }
        }

        if (DayNightCycle.Cycle.dayElapsed > DayNightCycle.Cycle.dayStart && DayNightCycle.Cycle.dayElapsed < DayNightCycle.Cycle.dayLength)
        {
            if (!isDay)
            {
                isDay = true;
                canSacrifice = true;
            }
        }
        else
        {
            isDay = false;
        }
        // End of Aidan gross vomit stuff
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
