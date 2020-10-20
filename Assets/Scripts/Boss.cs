using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public float health;

    public Enemy[] enemies;
    public int enemySummonCount;


    public GameObject deadSprite;
    public GameObject deadAnimation;

    public AudioClip clip;

    private AudioSource radio;

    private int threeFourthsHealth;
    private int halfHealth;
    private int oneFourthHealth;

    private bool threeFourthSummonComplete;
    private bool halfHealthSummonComplete;
    private bool oneFourthSummonComplete;

    private Slider healthBar;

    public int collisionDamage;

    private Animator anim;
    private GameObject player;
    private Animator cameraAnim;

    private SceneTransitions sceneTransitions;


    

    private void Start()
    {
        threeFourthsHealth = (int)decimal.Round((decimal)health * .75m, 0);
        halfHealth = (int)decimal.Round((decimal)health * .5m, 0);
        oneFourthHealth = (int)decimal.Round((decimal)health * .25m, 0);

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cameraAnim = Camera.main.GetComponent<Animator>();

        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;

        radio = GetComponent<AudioSource>();

        radio.clip = clip;
        radio.Play();

        sceneTransitions = FindObjectOfType<SceneTransitions>();

    }

    private void Update()
    {
        if (!radio.isPlaying)
        {
            radio.clip = clip;
            radio.Play();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deadAnimation, transform.position, Quaternion.identity);
            Instantiate(deadSprite, transform.position, Quaternion.identity);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransitions.LoadScene("Win");
        }

        //if(health == threeFourthsHealth || health == halfHealth || health == oneFourthHealth)
        if (health <= threeFourthsHealth && !threeFourthSummonComplete)
        {
            threeFourthSummonComplete = true;
            enemySummonCount *= 2;
            anim.SetTrigger("summon");
        }else if (health <= halfHealth && !halfHealthSummonComplete)
        {
            halfHealthSummonComplete = true;
            enemySummonCount *= 2;
            anim.SetTrigger("summon");
        }else if (health <= oneFourthHealth && !oneFourthSummonComplete)
        {
            oneFourthSummonComplete = true;
            enemySummonCount *= 2;
            anim.SetTrigger("summon");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(collisionDamage);
        }
    }

    public void Summon()
    {
        for (int i = 0; i < enemySummonCount; i++)
        {
            if (player != null)
            {

                Instantiate(enemies[Random.Range(0, 3)], transform.position, transform.rotation);
            }
        }

    }

    public void BossSpawn()
    {
        cameraAnim.SetTrigger("bossShake");
    }

}
