using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSBridge : MonoBehaviour
{
    public string areaName;

    // Start is called before the first frame update
    void Start()
    {
        bool isUnlocked = false;


        if (PlayerPrefs.HasKey(areaName + "_unlocked"))
        {

            if (PlayerPrefs.GetInt(areaName + "_unlocked") == 1)
            {
                isUnlocked = true;
            }

        }

        if (!isUnlocked)
        {
            gameObject.SetActive(false);
        }


    }
}