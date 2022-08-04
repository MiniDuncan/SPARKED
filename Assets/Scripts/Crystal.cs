using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crystal : MonoBehaviour
{

    public GameObject pickupEffect;

    public string uniqueID;

    public bool keepActiveForTesting;

    private void Start()
    {
        uniqueID = SceneManager.GetActiveScene().name + uniqueID;

        if (PlayerPrefs.HasKey(uniqueID))
        {
            if(PlayerPrefs.GetInt(uniqueID) == 1)
            {
                gameObject.SetActive(false);
            }
        }

#if UNITY_EDITOR

        if (keepActiveForTesting)
        {
            gameObject.SetActive(true);
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelManager.instance.GetCrystal();

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            PlayerPrefs.SetInt(uniqueID, 1);

            AudioManager.instance.PlaySFX(5);

            Destroy(gameObject);
        }
    }
}
