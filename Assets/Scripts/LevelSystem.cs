using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    
    [SerializeField]
    private List<int> levelsList = new List<int>();

    private int statPointsPerLvl = 2;

  

    // Start is called before the first frame update
    void Awake()
    {
        SetLevels();
       
    }

    public int Level(int xp)
    {
        
        for (int i = 0; i <= levelsList.Count; i++)
        {
            if (levelsList[i] >= xp + 1)
            {
                LevelUp();
                xp = i;
                i = levelsList.Count + 1;
                
               
            }

        }
        
        return xp;

    }

    public float XpProgressFill(int xp)
    {
        if (xp < levelsList[0])
        {
            return (float)xp / levelsList[0];
        }

        else
        {
            return ((float)xp - levelsList[Level(xp) - 1]) / (levelsList[Level(xp)] - levelsList[Level(xp) - 1]);
        }
        
    }

    public int xpNormalizer(int xp)
    {
        if (xp < levelsList[0])
        {
            return xp;
        }
        else
        {
            return xp - levelsList[Level(xp) - 1];
        }
    }

    private void SetLevels()
    {
        levelsList.Add(100);
        levelsList.Add(220);
        levelsList.Add(370);
        levelsList.Add(550);
        levelsList.Add(770);
        levelsList.Add(1040);
        levelsList.Add(1370);
        levelsList.Add(1770);
        levelsList.Add(2250);
        levelsList.Add(2830);
        levelsList.Add(3530);
        levelsList.Add(4370);
        levelsList.Add(5380);
        levelsList.Add(6600);
        levelsList.Add(8070);
        levelsList.Add(9840);
        levelsList.Add(11970);
        levelsList.Add(14530);
        levelsList.Add(17610);
        levelsList.Add(21310);

    }

    public int HP(int hpStatPoints)
    {
        hpStatPoints = 10 + (10 * hpStatPoints / 2); 
        return hpStatPoints;
    }

    private void LevelUp()
    {
        GameObject.Find("Player").GetComponent<StatsScript>().setStatPoints(statPointsPerLvl);
    }
}
