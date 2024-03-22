using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{

    public float speed = 10;
    public float dashSpeed = 100;
    public float dashCooldown = 3;
    private float timeAfterDash = 3;
    private Rigidbody playerRb;
    public float velocity = 10;
    public float stopModifier = 0.92f;
    public GameObject cameraH;
    private bool canDash = true;
    public float height;
    public float rotationSpeed = 0.5f;
    public float horizontalInput;
    public float verticalInput;
    public float hP = 3;

    public GameObject Ui;

    [SerializeField] Joystick moveJoystick;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        //get the camera height
        height = cameraH.GetComponent<FollowPlayer>().cameraHeight;
        //Set current weapon UI
        Ui.GetComponent<UIScript>().UpdateCurrentWeaponText(GameObject.FindGameObjectWithTag("Weapon").name);
        //Set Dash Ui
        Ui.GetComponent<UIScript>().UpdateDashCooldownText(canDash);
        //set HP text
        Ui.GetComponent<UIScript>().HpTextUpdate(hP);

    


    }

    // Update is called once per frame
    void Update()
    {


        //the input keybord
        horizontalInput = moveJoystick.Horizontal;
        verticalInput = moveJoystick.Vertical;
        Debug.Log(horizontalInput + " " + verticalInput);

        //if there is input moving with restricted velocity
        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerRb.AddForce(Vector3.forward * verticalInput * speed);
            playerRb.AddForce(Vector3.right * horizontalInput * speed);

            //rotation
            Vector3 playerPos = playerRb.position;
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime * rotationSpeed);

            playerRb.MoveRotation(targetRotation);


            //restrict velocity
            if (playerRb.velocity.magnitude > velocity)
            {
                playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, velocity);
            }
        }

        //stoping when nothing is pressed
        //if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)
            //&& !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)
            //&& !Input.GetKey(KeyCode.LeftArrow))
            if(horizontalInput == 0 && verticalInput == 0)
        {
            playerRb.velocity = playerRb.velocity * stopModifier;

            //full stop
            if (Mathf.Abs(playerRb.velocity.x) < 0.2f && Mathf.Abs(playerRb.velocity.z) < 0.2f)
            {
                playerRb.velocity = playerRb.velocity * 0f;


            }

        }

        //dashing

        

        if (canDash == false)
        {
            timeAfterDash += Time.deltaTime;
            if (timeAfterDash >= dashCooldown)
            {
                canDash = true;
                Ui.GetComponent<UIScript>().UpdateDashCooldownText(canDash);

            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Dash();
        }


        //dead
        if (hP <= 0)

        {
            Debug.Log("Game Over");
        }


    }
    //getting hit
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hP -= collision.gameObject.GetComponent<EnemyScript>().damage;
            Ui.GetComponent<UIScript>().HpTextUpdate(hP);
        }

    }

    private void Dash()
    {
        playerRb.AddForce(Vector3.forward * verticalInput * dashSpeed, ForceMode.Impulse);
        playerRb.AddForce(Vector3.right * horizontalInput * dashSpeed, ForceMode.Impulse);
        timeAfterDash = 0;
        canDash = false;
        Ui.GetComponent<UIScript>().UpdateDashCooldownText(canDash);


    }

    public IEnumerator SpeedUpTimer(float speedUpTime, float speedMultyplier)
    {
        speed *= speedMultyplier;
        velocity *= speedMultyplier;
        yield return new WaitForSeconds(speedUpTime);
        speed /= speedMultyplier;
        velocity /= speedMultyplier;
    }
    

    

}
