using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public string levelName, displayName;

    public GameObject mapPointActive, mapPointInactive;

    private bool canLoadLevel, levelLoading;

    public int crystalsRequired;
    private bool levelUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        if(crystalsRequired > 0 && LevelManager.instance.currentCrystals < crystalsRequired)
        {
            levelUnlocked = false;
           
        } else
        {
            levelUnlocked = true;
        }

        mapPointActive.SetActive(levelUnlocked);
        mapPointInactive.SetActive(!levelUnlocked);

        if (PlayerPrefs.GetString("CurrentLevel") == levelName)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            player.charCon.Move(transform.position - player.transform.position);

            FindObjectOfType<CameraController>().SnapToTarget();

            LevelManager.instance.respawnPoint = transform.position;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(canLoadLevel && Input.GetButtonDown("Jump") && !levelLoading)
        {
            levelLoading = true;
            StartCoroutine(LoadLevelCo());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (levelUnlocked == true)
            {
                canLoadLevel = true;
            }

            LevelSelectController.instance.levelInfoBox.SetActive(true);
            LevelSelectController.instance.levelText.text = displayName;

            if (levelUnlocked)
            {
                LevelSelectController.instance.actionText.text = "Press Jump To Enter";
            }
            else
            {
                LevelSelectController.instance.actionText.text = crystalsRequired.ToString() + " Crystals required to unlock";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canLoadLevel = false;

            LevelSelectController.instance.levelInfoBox.SetActive(false);
        }
    }

    public IEnumerator LoadLevelCo()
    {
        FindObjectOfType<PlayerController>().stopMoving = true;
        UIController.instance.FadeToBlack();

        AudioManager.instance.PlaySFX(9);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel", levelName);
    }
    
}
