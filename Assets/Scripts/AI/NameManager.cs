using UnityEngine;
using System.Collections;

public class NameManager : MonoBehaviour
{
    public TextAsset namesList;

    public string[] names;

    // Use this for initialization
    void Start()
    {
        if (EntityBehaviour.nameManager != this)
        {
            EntityBehaviour.nameManager = this;
        }

        char[] delimiters = { '\n' };
        names = namesList.text.Split(delimiters);
    }
}
