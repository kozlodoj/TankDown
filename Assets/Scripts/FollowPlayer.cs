using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    public float cameraHeight;
    public float delay = 10;

    private float xPos;
    private float zPos;

    // Start is called before the first frame update
    void Start()
    {
        //set the camera above the player
        transform.position = new Vector3(player.transform.position.x, cameraHeight, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        //find distance on x axis
        float distX = Mathf.Round(Mathf.Abs(player.transform.position.x - transform.position.x) * 10f ) * 0.1f;

        //see if distance is bigger then delay
        if (distX > delay + 0.01f)
        {
            
            // set possition of x
            if (player.GetComponent<Rigidbody>().velocity.x < 0)
            {
                xPos = player.transform.position.x + delay;
            }

            if (player.GetComponent<Rigidbody>().velocity.x > 0)
            {
                xPos = player.transform.position.x - delay;
            }

        }
        //find distance on z axis
        float distZ = Mathf.Round(Mathf.Abs(player.transform.position.z - transform.position.z) * 10f) * 0.1f;

        //see if distance is bigger then delay
        if (distZ > delay + 0.01f)
        {

            //set possition of z
            if (player.GetComponent<Rigidbody>().velocity.z < 0)
            {
                zPos = player.transform.position.z + delay;
            }

            if (player.GetComponent<Rigidbody>().velocity.z > 0)
            {
                zPos = player.transform.position.z - delay;
            }

        }

        //set the camera
        transform.position = new Vector3(xPos, cameraHeight, zPos);

    }
}
