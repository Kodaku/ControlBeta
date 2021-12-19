using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{
    [SerializeField] private GameObject leftPunchCollider;
    [SerializeField] private GameObject rightPunchCollider;
    [SerializeField] private GameObject leftLegCollider;
    [SerializeField] private GameObject rightLegCollider;
    [SerializeField] private bool isPlayer;
    [SerializeField] private HitDetector[] hitDetectors;
    private ColliderTypes currentColliderType;
    // Start is called before the first frame update
    void Start()
    {
        foreach(HitDetector hitDetector in hitDetectors)
        {
            hitDetector.gameObject.SetActive(false);
        }
    }

    // private void InitializeHitDetector(ColliderTypes colliderTypes, string tag)
    // {
    //     hitDetectors[(int)colliderTypes] = GameObject.FindGameObjectWithTag(tag).GetComponent<HitDetector>();
    //     hitDetectors[(int)colliderTypes].gameObject.SetActive(false);
    // }

    public void ActivateCollider(ColliderTypes colliderTypes)
    {
        SetCurrentColliderType(colliderTypes);
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
    }

    public void SetCurrentColliderType(ColliderTypes colliderTypes)
    {
        this.currentColliderType = colliderTypes;
    }

    private float DeDiscretizeDamage(DamageTypes damageTypes)
    {
        HitDetector hitDetector = hitDetectors[(int)currentColliderType];
        switch(damageTypes)
        {
            case DamageTypes.NORMAL_DAMAGE:
            {
                print("Normal Damage");
                hitDetector.SetDamage(3.0f);
                hitDetector.CanEvaluateHit(true);
                break;
            }
            case DamageTypes.STRONG_DAMAGE:
            {
                hitDetector.SetDamage(5.0f);
                hitDetector.CanEvaluateHit(true);
                break;
            }
        }
        return 0.0f;
    }
}
