using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsScript : LevelSystem
{


    private Dictionary<string, int> Stats = new Dictionary<string, int>()
    {
        { "Attack", 1 },
        { "Defence", 1 },
        { "Luck", 1 },
        { "Speed", 1 },
        { "HP", 1 }
    };
    private int xp = 0;
    private int level;
    private int statPoints = 5;
    [SerializeField]
    private float hp;
    private float attack;
    private float defence;
    private float luck;
    private float speed;

    [SerializeField]
    private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        
        
        level = Level(xp);
        hp = HP(4);
        UI.GetComponent<UIScript>().LevelTextSetUp(Level(xp), xp);
        UI.GetComponent<UIScript>().HpTextUpdate(hp);


    }

   
    //getting hit
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp -= collision.gameObject.GetComponent<EnemyScript>().damage;
            UI.GetComponent<UIScript>().HpTextUpdate(hp);
        }

    }

    public void FillInStats(Dictionary<string, int> stats)
    {
        Stats = stats;
       

    }
    public Dictionary<string, int> GetStatDictionary()
    {
        var result = new Dictionary<string, int>();
        foreach (var stat in Stats)
        {
            result.Add(stat.Key, stat.Value);
            
        }


        return result;
    }
    public Dictionary<string, int> GetLevelExp()
    {
        var result = new Dictionary<string, int>();
        result.Add("Level", level);
        result.Add("Exp", xp);
        return result;

    }

    public int GetStatPoints()
    {
        return statPoints;
    }

    public void AddXp(int xpToAdd)
    {
        xp += xpToAdd;
        UI.GetComponent<UIScript>().LevelTextSetUp(Level(xp), xpNormalizer(xp));
        UI.GetComponent<UIScript>().addXp(XpProgressFill(xp));
        
    }

    public void setStatPoints(int amount)
    {
        statPoints = amount;
    }
}
