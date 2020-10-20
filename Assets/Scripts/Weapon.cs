using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    public float fireConeTime;
    public GameObject fireCone;

    private Animator anim;
    private Animator cameraAnim;

    private void Start()
    {
        //Invoke("DestroyFireCone", fireConeTime);
        anim = GetComponent<Animator>();
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    void DestroyFireCone()
    {
        Instantiate(fireCone, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        //Debug.Log(shotPoint.position.ToString());
        //Debug.Log(transform.rotation.ToString());

        anim.SetBool("isShooting", false);
        if(Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, rotation);
                shotTime = Time.time + timeBetweenShots;
                Instantiate(fireCone, shotPoint.position, rotation);
                anim.SetBool("isShooting", true);

            }
        }
    }
}
