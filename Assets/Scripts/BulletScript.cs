using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2;
    public float damage = 1;

    public BulletTrailScriptableObj trailConfig;
    protected TrailRenderer trail;
    [SerializeField]
    private Renderer trailRenderer;

    private bool isDisabling = false;

    protected const string DISABLE_METHOD_NAME = "Disable";
    protected const string DO_DISABLE_METHOD_NAME = "DoDisable";


    // Start is called before the first frame update
    void Awake()
    {
        
        //get rigidbody and set in motion
        GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * speed, ForceMode.Impulse);

        trail = GetComponent<TrailRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //check for lifetime after destroy
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    //destroy on collision
    private void OnCollisionEnter(Collision collision)

    {
        OnDisable();
        Destroy(gameObject);
    }

    protected virtual void OnEnable()
    {
        trailRenderer.enabled = true;
        isDisabling = false;
        ConfigureTrail();

    }
    private void ConfigureTrail()
    {
        if (trail != null && trailConfig != null)
        {
            trailConfig.SetupTrail(trail);
        }
    }

    protected void OnDisable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        CancelInvoke(DO_DISABLE_METHOD_NAME);
        trailRenderer.enabled = false;
        if (trail != null && trailConfig != null)
        {
            isDisabling = true;
            Invoke(DO_DISABLE_METHOD_NAME, trailConfig.time);
        }
        else {
            DoDisable();
        }
    }

    protected void DoDisable()
    {
        if (trail != null && trailConfig != null)
        {
            trail.Clear();

        }
        gameObject.SetActive(false);


    }
}
