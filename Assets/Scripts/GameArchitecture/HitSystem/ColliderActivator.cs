using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{
    [SerializeField] private GameObject leftPunchCollider;
    [SerializeField] private GameObject rightPunchCollider;
    [SerializeField] private GameObject leftLegCollider;
    [SerializeField] private GameObject rightLegCollider;
    protected HitDetector[] hitDetectors = new HitDetector[4];
    // Start is called before the first frame update
    void Start()
    {
        InitializeHitDetector(ColliderTypes.LEFT_PUNCH_COLLIDER, "LeftLegCollider");
        InitializeHitDetector(ColliderTypes.RIGHT_PUNCH_COLLIDER, "RightPunchCollider");
        InitializeHitDetector(ColliderTypes.LEFT_LEG_COLLIDER, "LeftLegCollider");
        InitializeHitDetector(ColliderTypes.RIGHT_LEG_COLLIDER, "RightLegCollider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeHitDetector(ColliderTypes colliderTypes, string tag)
    {
        hitDetectors[(int)colliderTypes] = GameObject.FindGameObjectWithTag(tag).GetComponent<HitDetector>();
    }

    private void ActivateCollider(ColliderTypes colliderTypes, DamageTypes damageTypes)
    {
        float damage = DeDiscretizeDamage(damageTypes);
        switch(colliderTypes)
        {
            case ColliderTypes.LEFT_PUNCH_COLLIDER:
            {
                leftPunchCollider.SetActive(true);
                break;
            }
            case ColliderTypes.RIGHT_PUNCH_COLLIDER:
            {
                rightPunchCollider.SetActive(true);
                break;
            }
            case ColliderTypes.LEFT_LEG_COLLIDER:
            {
                leftLegCollider.SetActive(true);
                break;
            }
            case ColliderTypes.RIGHT_LEG_COLLIDER:
            {
                rightLegCollider.SetActive(true);
                break;
            }
        }
        hitDetectors[(int)colliderTypes].SetDamage(damage);
    }

    private float DeDiscretizeDamage(DamageTypes damageTypes)
    {
        switch(damageTypes)
        {
            case DamageTypes.NORMAL_DAMAGE:
            {
                return 3.0f;
            }
            case DamageTypes.STRONG_DAMAGE:
            {
                return 5.0f;
            }
        }
        return 0.0f;
    }
}
