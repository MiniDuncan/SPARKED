using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDamageController : MonoBehaviour
{
    public BossBattleController bossCon;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bossCon.DamageBoss();

            player.Bounce();
        }
    }
}
