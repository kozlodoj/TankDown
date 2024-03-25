using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour

{
    [SerializeField]
    private InputActionAsset controlAsset;
    private InputActionMap gameplayMap;
    private InputAction movement;
    private InputAction dashAction;

    public float speed = 10;
    public float dashSpeed = 20;
    public float dashTime = 0.3f;
    public float dashCooldown = 3;
    private float timeAfterDash = 3;
    private Rigidbody playerRb;
    public float velocity = 10;
    public float stopModifier = 0.98f;
    public GameObject cameraH;
    private bool canDash = true;
    public float height;
    public float rotationSpeed = 2f;
    public float horizontalInput;
    public float verticalInput;
    public float hP = 3;
    private bool isDashing = false;
    private bool leftDashBound = true;

    public GameObject Ui;

    private int test;

    // Start is called before the first frame update
    void Awake()
    {
        gameplayMap = controlAsset.FindActionMap("Gameplay");
        movement = gameplayMap.FindAction("Movement");
        dashAction = gameplayMap.FindAction("Dash");

        playerRb = gameObject.GetComponent<Rigidbody>();
        //get the camera height
        height = cameraH.GetComponent<FollowPlayer>().cameraHeight;
        //Set current weapon UI
        Ui.GetComponent<UIScript>().UpdateCurrentWeaponText(GameObject.FindGameObjectWithTag("Weapon").name);
        //Set Dash Ui
        Ui.GetComponent<UIScript>().UpdateDashCooldownText(canDash);
        //set HP text
        Ui.GetComponent<UIScript>().HpTextUpdate(hP);

        dashAction.started += context => Dash();

    }

    private void OnEnable()
    {
        gameplayMap.Enable();
    }

    private void OnDisable()
    {
        gameplayMap.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        
        //the input keybord
        horizontalInput = movement.ReadValue<Vector2>().x * 2;
        verticalInput = movement.ReadValue<Vector2>().y * 2;

        
        //if there is input moving with restricted velocity
        if (horizontalInput != 0 || verticalInput != 0)
        {
            
            if (Mathf.Abs(horizontalInput) > 1 && Mathf.Abs(verticalInput) > 1)
            {
                
                playerRb.AddForce(Vector3.forward * (verticalInput / Mathf.Abs(verticalInput)) * speed);
                playerRb.AddForce(Vector3.right * (horizontalInput / Mathf.Abs(horizontalInput)) * speed);
            }
            else if (Mathf.Abs(horizontalInput) > 1)
            {
                
                playerRb.AddForce(Vector3.forward * verticalInput * speed);
                playerRb.AddForce(Vector3.right * (horizontalInput / Mathf.Abs(horizontalInput)) * speed);
            }
            else if (Mathf.Abs(verticalInput) > 1)
            {
                
                playerRb.AddForce(Vector3.forward * (verticalInput / Mathf.Abs(verticalInput)) * speed);
                playerRb.AddForce(Vector3.right * horizontalInput * speed);
            }
            else
            {
                
                playerRb.AddForce(Vector3.forward * verticalInput * speed);
                playerRb.AddForce(Vector3.right * horizontalInput * speed);
            }
            //rotation
            Vector3 playerPos = playerRb.position;
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime * rotationSpeed);

            playerRb.MoveRotation(targetRotation);


            //restrict velocity
            if (playerRb.velocity.magnitude > velocity && !isDashing)
            {
                playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, velocity);
            }
        }

        //stoping when nothing is pressed
        if (horizontalInput == 0 && verticalInput == 0)
        {
            playerRb.velocity = playerRb.velocity * stopModifier;

            //full stop
            if (Mathf.Abs(playerRb.velocity.x) < 0.2f && Mathf.Abs(playerRb.velocity.z) < 0.2f)
            {
                playerRb.velocity = playerRb.velocity * 0f;


            }

        }

        //dashing

        if (Mathf.Abs(horizontalInput) > 1.9f || Mathf.Abs(verticalInput) > 1.9f)
        {
            Dash();
        }


        if (canDash == false)
        {
            timeAfterDash += Time.deltaTime;
            if (Mathf.Abs(horizontalInput) < 1f && Mathf.Abs(verticalInput) < 1f)
            {
                leftDashBound = true;
            }
            if (timeAfterDash >= dashCooldown && leftDashBound)
            {
                canDash = true;
                Ui.GetComponent<UIScript>().UpdateDashCooldownText(canDash);

            }
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
        if (canDash && leftDashBound)
        {
            isDashing = true;
            leftDashBound = false;
            StartCoroutine(DashTimer(dashTime));
            playerRb.AddForce(Vector3.forward * (verticalInput / 2) * dashSpeed, ForceMode.Impulse);
            playerRb.AddForce(Vector3.right * (horizontalInput / 2) * dashSpeed, ForceMode.Impulse);
            timeAfterDash = 0;
            canDash = false;
            Ui.GetComponent<UIScript>().UpdateDashCooldownText(canDash);
        }


    }

    public IEnumerator SpeedUpTimer(float speedUpTime, float speedMultyplier)
    {
        speed *= speedMultyplier;
        velocity *= speedMultyplier;
        yield return new WaitForSeconds(speedUpTime);
        speed /= speedMultyplier;
        velocity /= speedMultyplier;
    }

    private IEnumerator DashTimer(float dashTime)
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;

    }



}
