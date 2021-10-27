using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    [SerializeField] protected LayerMask collisionLayer;
    [SerializeField] protected float radius = 1.0f;
    [SerializeField] protected GameObject hitFX;
    protected float damage;
    protected bool canEvaluateHit = true;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        DetectCollision();
    }

    protected virtual void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);
        if(canEvaluateHit)
            EvaluateHit(hit);
    }

    protected virtual void EvaluateHit(Collider[] hit)
    {

    }

    public void CanEvaluateHit(bool canEvaluateHit)
    {
        this.canEvaluateHit = canEvaluateHit;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
