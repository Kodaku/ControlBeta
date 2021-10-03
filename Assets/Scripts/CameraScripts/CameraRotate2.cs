using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate2 : MonoBehaviour
{
    [SerializeField] private float rotationPeriod = 5.0f;
    private Transform origin;
    private Vector3 currentPosition;
    private Vector3 nextPosition;
    private float currentPosMagnitude;
    private float nextPosMagnitude;
    private float radius;
    private float angularSpeed;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        origin = GameObject.FindGameObjectWithTag("Player").transform;
        radius = Vector3.Distance(transform.position, origin.position);
        angularSpeed = (2.0f * Mathf.PI) / rotationPeriod;
        speed = angularSpeed * radius;
        currentPosition = transform.position;
        currentPosMagnitude = currentPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        nextPosMagnitude = currentPosMagnitude + speed * Time.deltaTime;
        
    }
}
