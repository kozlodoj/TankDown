using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float hP = 10;
    private GameObject player;
    private Vector3 target;
    public float speed = 10;
    public float damage = 1;
    public float velocity = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        //track HP, destroy after 0
        if (hP <= 0)
        {
            player.GetComponent<StatsScript>().AddXp(10);
            Destroy(gameObject);
            
        }

        //find player and attack
        target = player.transform.position;
        Vector3 lookDirection = (target - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(lookDirection * speed);

        if (GetComponent<Rigidbody>().velocity.magnitude > velocity)
        {
            GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, velocity);
        }


    }

    //check for collision with projectile, substract HP
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
           hP -= collision.gameObject.GetComponent<BulletScript>().damage;
        }
    }
}
