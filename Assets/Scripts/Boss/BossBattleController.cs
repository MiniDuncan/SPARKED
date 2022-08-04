using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattleController : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public Slider healthSlider;

    public Transform[] spawnPoints;
    public GameObject bossObject;
    public float waitBeforeSpawn;
    private int lastSpawn;

    public GameObject smokeEffect;

    public GameObject theShot;
    public Transform shotPoint;

    public float timeBetweenShots1, timeBetweenShots2, timeBetweenShots3;
    private float shotCounter;

    public GameObject levelExit;
    public string areaToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (smokeEffect != null)
        {
            Instantiate(smokeEffect, bossObject.transform.position, bossObject.transform.rotation);
            AudioManager.instance.PlaySFX(1);
        }

        shotCounter = timeBetweenShots1;

        AudioManager.instance.PlayMusic(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (bossObject.activeSelf)
        {
            bossObject.transform.LookAt(PlayerController.instance.transform);
            bossObject.transform.rotation = Quaternion.Euler(0f, bossObject.transform.rotation.eulerAngles.y, 0f);

            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                Instantiate(theShot, shotPoint.position, shotPoint.rotation);


                ResetShotCounter();
            }
        }
    }

    public void DamageBoss()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            StartCoroutine(EndBattleCo());

            currentHealth = 0;
        }
        else
        {
            StartCoroutine(SpawnCo());
        }

        healthSlider.value = currentHealth;
    }

    IEnumerator SpawnCo()
    {
        bossObject.SetActive(false);

        if(smokeEffect != null)
        {
            Instantiate(smokeEffect, bossObject.transform.position, bossObject.transform.rotation);
            AudioManager.instance.PlaySFX(1);
        }

        yield return new WaitForSeconds(waitBeforeSpawn);

        int posSelect = Random.Range(0, spawnPoints.Length);

        int tracker = 0;
        while(posSelect == lastSpawn && tracker < 100)
        {
            posSelect = Random.Range(0, spawnPoints.Length);

            tracker++;
        }

        lastSpawn = posSelect;

        bossObject.transform.position = spawnPoints[posSelect].position;

        bossObject.SetActive(true);

        if (smokeEffect != null)
        {
            Instantiate(smokeEffect, bossObject.transform.position, bossObject.transform.rotation);
            AudioManager.instance.PlaySFX(1);
        }

    }

    void ResetShotCounter()
    {
        if(currentHealth > maxHealth * .5f)
        {
            shotCounter = timeBetweenShots1;
        } else if(currentHealth > maxHealth * .25f)
        {
            shotCounter = timeBetweenShots2;
        } else
        {
            shotCounter = timeBetweenShots3;
        }
    }

    IEnumerator EndBattleCo()
    {
        bossObject.SetActive(false);

        AudioManager.instance.PlaySFX(2);

        if (smokeEffect != null)
        {
            Instantiate(smokeEffect, bossObject.transform.position, bossObject.transform.rotation);
            AudioManager.instance.PlaySFX(1);
        }

        PlayerPrefs.SetInt(areaToUnlock + "_unlocked", 1);

        yield return new WaitForSeconds(1f);

        levelExit.SetActive(true);
        gameObject.SetActive(false);
    }
}
