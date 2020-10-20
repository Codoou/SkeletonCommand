using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public float health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    public int pickupChance;
    public GameObject[] pickups;

    public GameObject deathEffect;

    [HideInInspector]
    public Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {

            int randomNumber = Random.Range(0, 101); 
            if(randomNumber < pickupChance)
            {
                GameObject randomPickUp = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickUp, transform.position, transform.rotation);
            }

            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }




}
