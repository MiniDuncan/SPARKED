using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Image fadeScreen;
    private bool isFadingToBlack, isFadingFromBlack;
    public float fadeSpeed = 2f;

    public Slider healthSlider;
    public TMP_Text healthText;
    public TMP_Text timeText;

    public TMP_Text coinText, crystalText;

    public GameObject pauseScreen;

    public string mainMenu, levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(isFadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)
                );
        }

        if (isFadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime)
                );
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void FadeToBlack()
    {
        isFadingToBlack = true;
        isFadingFromBlack = false;
    }

    public void FadeFromBlack()
    {
        isFadingToBlack = false;
        isFadingFromBlack = true;
    }

    public void UpdateHealthDisplay(int health)
    {
        healthText.text = "Health: " + health + "/" + PlayerHealthController.instance.maxHealth;

        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = health;
    }

    public void PauseUnpause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);

        if (pauseScreen.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }

    public void GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSelect);
    }

}
