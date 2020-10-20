using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public float damage;

    public GameObject explosion;
    public GameObject soundObject;

    public bool destroyOnCollision = true;

     

    // Start is called before the first frame update
    void Start()
    {
        // Destroy(gameObject, lifeTime); 
        Invoke("DestroyProjectile", lifeTime);
        if(soundObject != null)
        {
            Instantiate(soundObject, transform.position, transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") 
        {
            
            collision.GetComponent<Enemy>().TakeDamage(damage);
            if (destroyOnCollision)
                DestroyProjectile();
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
            if (destroyOnCollision)
                DestroyProjectile();
        }
    }
}
