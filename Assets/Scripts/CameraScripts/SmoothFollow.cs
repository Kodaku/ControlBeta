using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 6.3f;

    public float height = 3.5f;

    public float height_Dumping = 3.15f;

    public float rotation_Dumping = 0.27f;
    private Vector3 startPosition;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        offset = startPosition - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // float wanted_Rotation_Angle = target.eulerAngles.y;
        // float wantedHeight = target.position.y + height;//above the player

        // float current_Rotation_Angle = transform.eulerAngles.y;
        // float currentHeight = transform.position.y;

        // current_Rotation_Angle = Mathf.LerpAngle(current_Rotation_Angle, wanted_Rotation_Angle, rotation_Dumping * Time.deltaTime);
        // currentHeight = Mathf.Lerp(currentHeight, wantedHeight, height_Dumping * Time.deltaTime);

        // Quaternion current_Rotation = Quaternion.Euler(0.0f, current_Rotation_Angle, 0.0f);

        // transform.position = target.position + startPosition;
        // // transform.position = Vector3.Lerp(transform.position, target.position, 0.1f);
        // // transform.position -= current_Rotation * Vector3.forward * distance;//give distance to the camera from the player

        // transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);//the y is a little bit above
        transform.position = target.transform.position + offset;
    }
}//end class
