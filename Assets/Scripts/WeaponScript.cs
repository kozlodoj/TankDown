using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset controlAsset;
    private InputActionMap gameplayMap;
    private InputAction aim;
    private InputAction fire;
    private InputAction reload;

    public GameObject projectile;

    public int magSize = 10;
    public float reloadSpeed = 2;
    private int mag;
    private bool isReloading;
    
    private bool shooting;
    public float fireRate = 0.3f;
    private float timeAfterShot;
    private float height;
    private GameObject player;
    private GameObject hole;
    public GameObject UI;
    
    // Start is called before the first frame update
    void Awake()
    {
        gameplayMap = controlAsset.FindActionMap("Gameplay");
        aim = gameplayMap.FindAction("Aim");
        fire = gameplayMap.FindAction("Fire");
        reload = gameplayMap.FindAction("Reload");

        //get the plyaer
        player = GameObject.Find("Player");
        //get the firing hole
        hole = GameObject.Find("Hole");
        mag = magSize;
        //camera height
        height = GameObject.Find("Main Camera").GetComponent<FollowPlayer>().cameraHeight;
        isReloading = false;
        UI.GetComponent<UIScript>().UpdateAmmoText(mag);

        aim.performed += context => LookDirection(context);

        fire.started += context => Shooting(context);
        fire.canceled += context => Shooting(context);

        reload.performed += context => StartCoroutine(Reload());

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0.5f, -0.088f);

        
        //rotate
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = height - gameObject.transform.position.y;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //gameObject.transform.LookAt(mousePos, Vector3.up);



        timeAfterShot += Time.deltaTime;
        //if mouse 1 pressed - fire

        

        if (shooting && mag != 0 && !isReloading && timeAfterShot >= fireRate)

        {
            FireOne();
            
        }

        //if (Input.GetKey(KeyCode.Mouse0) && mag != 0 && !isReloading && isAutomatic && timeAfterShot >= fireRate)

        //{
            
          //  FireOne();
            

        //}

        //if mag empty - reload
        else if (mag == 0 && !isReloading)
        {
           
            StartCoroutine(Reload());

        }

        //if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        //{
          //  isReloading = true;
          //  StartCoroutine(Reload());
            
       // }
        
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
        isReloading = true;
        UI.GetComponent<UIScript>().UpdateReloadingText(isReloading);
        yield return new WaitForSeconds(reloadSpeed);
        mag = magSize;
        UI.GetComponent<UIScript>().UpdateAmmoText(mag);
        isReloading = false;
        UI.GetComponent<UIScript>().UpdateReloadingText(isReloading);


    }

    private void LookDirection(InputAction.CallbackContext context)
    {
       
        float angleRadians = Mathf.Atan2(context.ReadValue<Vector2>().y, context.ReadValue<Vector2>().x);
        float angleDegrees = -angleRadians * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleDegrees + 90, Vector3.up);
       
    }
    private void Shooting(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0)
        {
            shooting = true;
        }
        else {
            shooting = false;
        }
    }

}
