using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private Player playerScript;
    private Vector2 targetPosition;
    public float speed;
    public int damage;

    public GameObject collisionAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        if (Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Instantiate(collisionAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(collisionAnimation, transform.position, Quaternion.identity);
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
