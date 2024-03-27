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
    public TextMeshProUGUI LevelText;


    public GameObject player;

    [SerializeField]
    private GameObject menu;

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

    public void LevelTextSetUp(int level, int xp)
    {
        LevelText.SetText("Level " + level + " XP " + xp);
    }

    public void EnterMenu()
    {
        menu.SetActive(true);
    }
}
