using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Cursor { Select, Cultify, Sacrifice, Obelisk, Eclipse };
    Cursor myCursor;

    public GameObject ObeliskObj;

    public static bool canSacrifice;
    private bool isDay;

    // Use this for initialization
    void Start()
    {
        myCursor = Cursor.Select;
        canSacrifice = true;
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
                    if (myCursor == Cursor.Cultify && Faith.CurrentFaith >= Faith.cultistCost)
                    {
                        Faith.CurrentFaith -= Faith.cultistCost;
                        hit.collider.gameObject.BroadcastMessage("OnClick", myCursor, SendMessageOptions.DontRequireReceiver);
                    }
                    else if (myCursor == Cursor.Sacrifice && Faith.CurrentFaith >= Faith.sacrificeCost && canSacrifice)
                    {
                        Faith.CurrentFaith -= Faith.sacrificeCost;
                        canSacrifice = false;
                        hit.collider.gameObject.BroadcastMessage("OnClick", myCursor, SendMessageOptions.DontRequireReceiver);
                    }
                }
                else if (hit.collider.gameObject.name == "Terrain")
                {
                    if (myCursor == Cursor.Obelisk)
                    {
                        if (Faith.CurrentFaith >= Faith.obeliskCost)
                        {
                            Faith.CurrentFaith -= Faith.obeliskCost;
                            Instantiate(ObeliskObj, hit.point, new Quaternion());
                        }
                    }
                }
            }
            else
            {
                if (Faith.CurrentFaith > 100 && isDay)
                {
                    Faith.CurrentFaith -= Faith.obeliskCost;
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
