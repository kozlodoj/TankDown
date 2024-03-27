using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsScript : LevelSystem
{


    private Dictionary<string, int> Stats = new Dictionary<string, int>();
    private int xp;
    private int level;
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
        FillInStats();
        xp = 20000;
        UI.GetComponent<UIScript>().LevelTextSetUp(Level(xp), xp);
        hp = HP(4);

        foreach (var stat in Stats)
        {
            Debug.Log(stat);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void FillInStats()
    {
        Stats.Add("HP", 1);
        Stats.Add("Attack", 1);
        Stats.Add("Defence", 1);
        Stats.Add("Speed", 1);
        Stats.Add("Luck", 1);

    }
}
