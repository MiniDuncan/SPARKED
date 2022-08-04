using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(PlayerController.instance.transform.position + Vector3.up);

        AudioManager.instance.PlaySFXPitched(0);
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }

        if(impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
