using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicColliderDetector : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private float damage;
    private ParticleSystem particleInstance;
    void OnTriggerEnter(Collider target)
    {
        if(target.gameObject.tag == "HitCollider")
        {
            particleInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            EffectsDestroyer.instance.DestroyEffect(particleInstance);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
