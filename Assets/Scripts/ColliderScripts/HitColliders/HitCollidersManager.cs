using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollidersManager : MonoBehaviour
{
    private Animator animator;
    private HitsCollector hitsCollector;
    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        hitsCollector = GetComponentInParent<HitsCollector>();
    }
    
    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "AttackCollider" && !hitsCollector.IsAlreadyHit())
        {
            // animator.Play("LowReaction");
            hitsCollector.HitCombo();
            ApplyHit(target);
        }
        else if(target.tag == "StrongAttackCollider")
        {
            if(hitsCollector.GetStrongHitsCount() == 0 && !hitsCollector.IsStrongHitCounted())
            {
                hitsCollector.CountStrongHit();
                // animator.Play("KickHeavyHit");
                hitsCollector.IncrementStrongHitsCount();
                ApplyHit(target);
            }
            else if(hitsCollector.GetStrongHitsCount() == 1 && !hitsCollector.IsStrongHitCounted())
            {
                hitsCollector.CountStrongHit();
                //TODO: the character should go backward due to the power of the attack this is better to implement during the AI development
                // gameObject.GetComponentInParent<HumanPlayerMovement>().GetStrongHit();
                // animator.Play("PunchHeavyHit");
                hitsCollector.ResetStrongHitsCount();
                ApplyHit(target);
            }
        }
    }

    private void ApplyHit(Collider target)
    {
        float damage = target.gameObject.GetComponent<BasicColliderDetector>().GetDamage();
        gameObject.GetComponentInParent<PlayerHealth>().UpdateHealth(damage);
    }
}
