using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    [SerializeField] private float rotationPeriod = 5.0f;
    private float angularSpeed;
    private float speed;
    // private float h_speed;
    private float radius;
    private float x_0;
    private float y_0;
    private float z_0;
    private float theta_horizontal;
    // private Vector3 startPosition;
    private Vector3 offset;
    private Transform target;
    private HumanPlayerMovement playerMovement;
    private bool isRotating = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<HumanPlayerMovement>();
        Vector2 centerOfRotation = new Vector2(target.position.x, target.position.z);
        Vector2 rotator = new Vector2(transform.position.x, transform.position.z);
        radius = Vector2.Distance(centerOfRotation, rotator);
        offset = transform.position - target.transform.position;
        offset.z = 0.0f;

        x_0 = target.position.x + radius;
        z_0 = target.position.z;
        y_0 = transform.position.y;

        transform.position = new Vector3(x_0, y_0, z_0);

        angularSpeed = 2 * Mathf.PI / rotationPeriod;
        speed = angularSpeed * radius;
        theta_horizontal = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L) && !playerMovement.IsMoving())
        {
            isRotating = true;
            theta_horizontal -= angularSpeed * Time.deltaTime;
            float v_x = speed * Mathf.Sin(theta_horizontal);
            float v_z =  speed * Mathf.Cos(theta_horizontal);
            x_0 += v_x * Time.deltaTime;
            z_0 += v_z * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.K) && !playerMovement.IsMoving())
        {
            isRotating = true;
            theta_horizontal += angularSpeed * Time.deltaTime;
            float v_x = speed * Mathf.Sin(Mathf.PI + theta_horizontal);
            float v_z =  speed * Mathf.Cos(Mathf.PI + theta_horizontal);
            x_0 += v_x * Time.deltaTime;
            z_0 += v_z * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.I) && !playerMovement.IsMoving())
        {
            isRotating = true;
            y_0 += 10.0f * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.M) && !playerMovement.IsMoving())
        {
            isRotating = true;
            y_0 -= 10.0f * Time.deltaTime;
        }

        if((Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.M)) && !playerMovement.IsMoving())
        {
            rotSpeed = 10.0f;
            x_0 = transform.position.x;
            z_0 = transform.position.z;
            y_0 = transform.position.y;
        }

        if((Input.GetKeyUp(KeyCode.L) || Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.I) || Input.GetKeyUp(KeyCode.M)) && !playerMovement.IsMoving())
        {
            isRotating = false;
            offset = transform.position - target.position;
            rotSpeed = 1.0f;
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = (target.position - transform.position);
        if(!isRotating)
        {
            transform.position = target.position + offset;
            // transform.position = Vector3.Lerp(transform.position, target.position + offset, 1.0f);
        }
        else
        {
            transform.position = new Vector3(x_0, y_0, z_0);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
    }
}
