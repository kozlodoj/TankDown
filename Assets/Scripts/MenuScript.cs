using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    //stat field
    [SerializeField]
    private TextMeshProUGUI attackP;
    [SerializeField]
    private TextMeshProUGUI defenceP;
    [SerializeField]
    private TextMeshProUGUI luckP;
    [SerializeField]
    private TextMeshProUGUI speedP;
    [SerializeField]
    private TextMeshProUGUI hpP;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI expText;
    [SerializeField]
    private TextMeshProUGUI statPointText;
    //variables for stats
    private Dictionary<string, int> Stats = new Dictionary<string, int>();
    private Dictionary<string, int> LevelXp = new Dictionary<string, int>();
    private int statPoints;
    private int currentStatPoints = 0;
    private int attackS = 1;
    private int defenceS = 1;
    private int luckS = 1;
    private int speedS = 1;
    private int hpS = 1;


    // Start is called before the first frame update
    void Awake()
    {
        UpdatePlayerStats();
        ManageButtons("", false, 0);
    }

    private void UpdatePlayerStats()
    {
        //get reference from player
        Stats = player.GetComponent<StatsScript>().GetStatDictionary();
        LevelXp = player.GetComponent<StatsScript>().GetLevelExp();
        statPoints = player.GetComponent<StatsScript>().GetStatPoints();
        //set initial text
        attackP.SetText(Stats["Attack"].ToString());
        defenceP.SetText(Stats["Defence"].ToString());
        luckP.SetText(Stats["Luck"].ToString());
        speedP.SetText(Stats["Speed"].ToString());
        hpP.SetText(Stats["HP"].ToString());
        levelText.SetText("Level: " + LevelXp["Level"].ToString());
        expText.SetText("XP: " + LevelXp["Exp"].ToString());
        statPointText.SetText("Stat Points: " + statPoints);
        //set initial stats
        attackS = Stats["Attack"];
        defenceS = Stats["Defence"];
        luckS = Stats["Luck"];
        speedS = Stats["Speed"];
        hpS = Stats["HP"];


    }

    public void AttackMinus()
    {
        if (currentStatPoints > 0 && Stats["Attack"] > attackS)
        {
            statPoints++;
            currentStatPoints--;
            Stats["Attack"]--;
            attackP.SetText(Stats["Attack"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Attack", true, attackS);
        }
    }

    public void AttackPlus()
    {
        if (statPoints > 0)
        {
            statPoints--;
            currentStatPoints++;
            Stats["Attack"]++;
            attackP.SetText(Stats["Attack"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Attack", false, attackS);
        }
    }

    public void DefenceMinus()
    {
        if (currentStatPoints > 0 && Stats["Defence"] > defenceS)
        {
            statPoints++;
            currentStatPoints--;
            Stats["Defence"]--;
            defenceP.SetText(Stats["Defence"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Defence", true, defenceS);
        }
    }

    public void DefencePlus()
    {
        if (statPoints > 0)
        {
            statPoints--;
            currentStatPoints++;
            Stats["Defence"]++;
            defenceP.SetText(Stats["Defence"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Defence", false, defenceS);
        }
    }
    public void LuckMinus()
    {
        if (currentStatPoints > 0 && Stats["Luck"] > luckS)
        {
            statPoints++;
            currentStatPoints--;
            Stats["Luck"]--;
            luckP.SetText(Stats["Luck"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Luck", true, luckS);
        }
    }

    public void LuckPlus()
    {
        if (statPoints > 0)
        {
            statPoints--;
            currentStatPoints++;
            Stats["Luck"]++;
            luckP.SetText(Stats["Luck"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Luck", false, luckS);
        }
    }

    public void SpeedMinus()
    {
        if (currentStatPoints > 0 && Stats["Speed"] > speedS)
        {
            statPoints++;
            currentStatPoints--;
            Stats["Speed"]--;
            speedP.SetText(Stats["Speed"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Speed", true, speedS);
        }
    }

    public void SpeedPlus()
    {
        if (statPoints > 0)
        {
            statPoints--;
            currentStatPoints++;
            Stats["Speed"]++;
            speedP.SetText(Stats["Speed"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("Speed", false, speedS);
        }
    }

    public void HPMinus()
    {
        if (currentStatPoints > 0 && Stats["HP"] > hpS)
        {
            statPoints++;
            currentStatPoints--;
            Stats["HP"]--;
            hpP.SetText(Stats["HP"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("HP", true, hpS);
        }
    }

    public void HPPlus()
    {
        if (statPoints > 0)
        {
            statPoints--;
            currentStatPoints++;
            Stats["HP"]++;
            hpP.SetText(Stats["HP"].ToString());
            statPointText.SetText("Stat Points: " + statPoints);
            ManageButtons("HP", false, hpS);
        }
    }

    private void ManageButtons(string name, bool isMinus, int initialStat)
    {

        if (!isMinus)
        {
            GameObject.Find("Buttons").transform.Find(name + "MinusButton").gameObject.SetActive(true);
        }
        if (isMinus && Stats[name] == initialStat)
        {
            GameObject.Find("Buttons").transform.Find(name + "MinusButton").gameObject.SetActive(false);
        }
        if (currentStatPoints == 0)
        {

            foreach (Transform child in GameObject.Find("Buttons").transform)
            {
                if (child.tag == "Minus Button")
                {
                    child.gameObject.SetActive(false);

                }
            }
        }
       
        if (statPoints == 0)
        {
            foreach (Transform child in GameObject.Find("Buttons").transform)
            {
                if (child.tag == "Plus Button")
                {
                    child.gameObject.SetActive(false);

                }
            }

        }
        else {
            foreach (Transform child in GameObject.Find("Buttons").transform)
            {
                if (child.tag == "Plus Button")
                {
                    child.gameObject.SetActive(true);

                }
            }
        }
        
    }
    public void ApplyAndExit()
    {
        player.GetComponent<StatsScript>().FillInStats(Stats);
        foreach (Transform child in GameObject.Find("Buttons").transform)
        {
            if (child.tag == "Minus Button")
            {
                child.gameObject.SetActive(false);

            }
        }
        currentStatPoints = 0;
        gameObject.SetActive(false);
    }
}
