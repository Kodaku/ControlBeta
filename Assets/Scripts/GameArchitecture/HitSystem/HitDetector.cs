using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    [SerializeField] protected LayerMask collisionLayer;
    [SerializeField] protected float radius = 1.0f;
    [SerializeField] protected GameObject hitFX;
    protected float damage;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        DetectCollision();
    }

    protected virtual void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);
        EvaluateHit(hit);
    }

    protected virtual void EvaluateHit(Collider[] hit)
    {

    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
