using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsDestroyer : MonoBehaviour
{
    public static EffectsDestroyer instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void DestroyEffect(GameObject effect)
    {
        StartCoroutine(DestroyDefinetely(effect));
    }

    private IEnumerator DestroyDefinetely(GameObject effect)
    {
        yield return new WaitForSeconds(0.7f);
        effect.gameObject.transform.parent = transform;
        effect.gameObject.SetActive(false);
    }
}
