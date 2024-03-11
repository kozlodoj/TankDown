using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject projectile;

    public int magSize = 10;
    public float reloadSpeed = 2;
    private int mag;
    private bool isReloading;
    public bool isAutomatic = false;
    public float fireRate = 0.3f;
    private float timeAfterShot;
    private float height;
    private GameObject player;
    private GameObject hole;
    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {

        //get the plyaer
        player = GameObject.Find("Player");
        //get the firing hole
        hole = GameObject.Find("Hole");
        mag = magSize;
        //camera height
        height = GameObject.Find("Main Camera").GetComponent<FollowPlayer>().cameraHeight;
        isReloading = false;
        UI.GetComponent<UIScript>().UpdateAmmoText(mag);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0.5f, -0.088f);

        //rotate
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = height - gameObject.transform.position.y;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.LookAt(mousePos, Vector3.up);
        

        timeAfterShot += Time.deltaTime;
        //if mouse 1 pressed - fire

        

        if (Input.GetKeyDown(KeyCode.Mouse0) && mag != 0 && !isReloading && !isAutomatic)

        {
            FireOne();
            
        }

        if (Input.GetKey(KeyCode.Mouse0) && mag != 0 && !isReloading && isAutomatic && timeAfterShot >= fireRate)

        {
            
            FireOne();
            

        }

        //if mag empty - reload
        else if (mag == 0 && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());

        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());
            
        }
        
    }

    //fire
    void FireOne()
    {
        Instantiate(projectile, hole.transform.position, transform.rotation);
        mag--;
        UI.GetComponent<UIScript>().UpdateAmmoText(mag);
        timeAfterShot = 0;


    }

    //reload
    IEnumerator Reload()
    {
        UI.GetComponent<UIScript>().UpdateReloadingText(isReloading);
        yield return new WaitForSeconds(reloadSpeed);
        mag = magSize;
        UI.GetComponent<UIScript>().UpdateAmmoText(mag);
        isReloading = false;
        UI.GetComponent<UIScript>().UpdateReloadingText(isReloading);


    }

}
