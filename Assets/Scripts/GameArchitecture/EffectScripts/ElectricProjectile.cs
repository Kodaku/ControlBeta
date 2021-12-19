using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricProjectile : MonoBehaviour
{
    private Transform target;
    private Vector3 direction;
    private Vector3 startDirection;
    private float projectileDamage = 300.0f;
    [SerializeField] private float projectileSpeed;
    private bool hitted;
    // Start is called before the first frame update
    void Start()
    {
        hitted = false;
        target = GameObject.FindGameObjectWithTag("ProjectileTarget").transform;
        startDirection = GameObject.FindGameObjectWithTag("ProjectileTarget").transform.forward;

        // StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtGoal = new Vector3(target.position.x, 
										this.transform.position.y, 
										target.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;

		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
												Quaternion.LookRotation(direction), 
												Time.deltaTime*100.0f);

		if(Vector3.Distance(transform.position,lookAtGoal) > 1.0f)
		    this.transform.Translate(0,0,projectileSpeed*Time.deltaTime);
    }

    // private IEnumerator DestroyProjectile()
    // {
    //     yield return new WaitForSeconds(projectileDuration);
    //     transform.gameObject.SetActive(false);
    // }

    private void OnTriggerEnter(Collider collision)
    {
        if(!hitted)
        {
            hitted = true;
            print(collision.gameObject.tag);
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<AIBehaviourTree>().SendUpdateRequest(UpdatableIndices.HEALTH, -(int)projectileDamage);
            }
        }
    }
}
