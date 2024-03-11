using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject Ui;
  
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Weapon Pickup"))
        {
            //deactivate weapons
            foreach (GameObject weapon in weapons)
            {
                weapon.gameObject.SetActive(false);

            }
        }
        //activate basic canon
        if (other.name == "Basic Canon")
        {
            Destroy(other.gameObject);
            weapons[0].gameObject.SetActive(true);
            Ui.GetComponent<UIScript>().UpdateCurrentWeaponText(GameObject.FindGameObjectWithTag("Weapon").name);


        }
        //activate AR
        if(other.name == "Auto Rifle")
        {
            
            Destroy(other.gameObject);
            weapons[1].gameObject.SetActive(true);
            Ui.GetComponent<UIScript>().UpdateCurrentWeaponText(GameObject.FindGameObjectWithTag("Weapon").name);

        }
    }
}
