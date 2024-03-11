using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadingText;
    public TextMeshProUGUI dashCooldownText;
    public TextMeshProUGUI currentWeaponText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI speedUpText;

    
    public GameObject player;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoText(int ammoCount)
    {
        ammoText.SetText("Ammo:" + ammoCount);
    }

    public void UpdateReloadingText(bool isReloading)
    {
        if (isReloading)
        {
            reloadingText.SetText("Reloading...");
        }
        if (!isReloading)
        {
            reloadingText.SetText("");
        }
    }

    public void UpdateDashCooldownText(bool isCoolingdown)
    {
        if (!isCoolingdown)
        {
            dashCooldownText.SetText("........");

        }
        else if (isCoolingdown)
        {
            dashCooldownText.SetText("DASH");
        }

    }

    public void UpdateCurrentWeaponText(string weaponName)
    {

        currentWeaponText.SetText(weaponName);
    }

    public void HpTextUpdate(float hp)
    {
        hpText.SetText("HP: " + hp);
    }

    public void UpdateSpeedUpText(int timeLeft)
    {
        speedUpText.SetText("SpeedUp: " + timeLeft);

    }

}
