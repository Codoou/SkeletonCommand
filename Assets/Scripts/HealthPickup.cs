using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Player playerScript;
    public int healAmount;

    public GameObject pickupEffect;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerScript.Heal(healAmount);
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
