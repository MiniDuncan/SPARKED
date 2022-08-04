using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{

    public GameObject bossToActivate;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag  == "Player")
        {
            bossToActivate.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
