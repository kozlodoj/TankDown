using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Trail Config", menuName = "ScriptableObject/Bullet Trail Config")]
public class BulletTrailScriptableObj : ScriptableObject
{

    public AnimationCurve widthCurve;
    public float time = 0.5f;
    public float minVertexDistance = 0.1f;
    public Gradient colorGradient;
    public Material material;
    public int cornerVertices;
    public int endCapVertices;


    public void SetupTrail(TrailRenderer trailRenderer)
    {
        trailRenderer.widthCurve = widthCurve;
        trailRenderer.time = time;
        trailRenderer.minVertexDistance = minVertexDistance;
        trailRenderer.colorGradient = colorGradient;
        trailRenderer.sharedMaterial = material;
        trailRenderer.numCornerVertices = cornerVertices;
        trailRenderer.numCapVertices = endCapVertices;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
