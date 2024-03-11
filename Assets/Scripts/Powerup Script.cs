using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{

    public GameObject Ui;

    [SerializeField] float speedUpTime = 5f;
    [SerializeField] float speedMultyplier = 3f;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedUp"))
        {
            Destroy(other.gameObject);
            StartCoroutine(gameObject.GetComponent<PlayerController>().SpeedUpTimer(speedUpTime, speedMultyplier));
            Ui.GetComponent<UIScript>().UpdateSpeedUpText(5);

        }
    }


}
