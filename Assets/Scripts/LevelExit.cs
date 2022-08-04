using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad;

    private bool exiting;

    public Animator anim;

    public Transform camPos;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!exiting)
            {
                exiting = true;
                anim.SetBool("active", true);

                LevelManager.instance.EndLevel(levelToLoad);

                FindObjectOfType<CameraController>().endCamPos = camPos;

                AudioManager.instance.PlayMusic(4);

                AudioManager.instance.PlaySFX(8);
            }
        }
    }
}
