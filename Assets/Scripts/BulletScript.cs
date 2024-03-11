using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2;
    public float damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        
        //get rigidbody and set in motion
        GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * speed, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        //check for lifetime after destroy
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    //destroy on collision
    private void OnCollisionEnter(Collision collision)

    { 
        Destroy(gameObject);
    }
}
