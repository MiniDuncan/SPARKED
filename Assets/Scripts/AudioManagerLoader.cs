using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerLoader : MonoBehaviour
{
    public AudioManager theAM;

    private void Awake()
    {
        if(AudioManager.instance == null)
        {
            Instantiate(theAM).SetupInstance();
        }
    }
}
