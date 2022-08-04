using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    public string firstLevel, levelSelect;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("GameStarted"))
        {
            if(PlayerPrefs.GetInt("GameStarted") == 1)
            {
                continueButton.SetActive(true);
            }
        }

        AudioManager.instance.PlayMusic(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("GameStarted", 1);

        PlayerPrefs.SetString("CurrentLevel", firstLevel);

        SceneManager.LoadScene(firstLevel);
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
}
