using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed;
    public int health;

    private Rigidbody2D rb;

    private Animator anim;

    private Vector2 moveAMount;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float timeInvincibilityLasts;

    public AudioClip clip;

    //public AudioSource audioSource;

    AudioSource radio;

    private float invincibleTime;
    private bool isInvincible;

    private SpriteRenderer sprite;
    private Color defaultColor;

    private Animator cameraAnim;

    private SceneTransitions sceneTransitions;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        cameraAnim = Camera.main.GetComponent<Animator>();
        radio = GetComponent<AudioSource>();

        defaultColor = sprite.color;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAMount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Time.time >= invincibleTime)
        {
            isInvincible = false;
            sprite.color = defaultColor;
        }
        else
        {
            isInvincible = true;
            sprite.color = new Color(0, 0, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAMount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        if(!isInvincible)
        {
            invincibleTime = Time.time + timeInvincibilityLasts;
            health -= damageAmount;
            UpdateHealthUI(health);
            if (!radio.isPlaying)
            {
                radio.clip = clip;
                radio.Play();
            }

            cameraAnim.SetTrigger("shake");
        }


        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitions.LoadScene("Lose");
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);

    }

    private void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }
}
