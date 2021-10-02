using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [Space(10)]
    [SerializeField] private float speed;
    [SerializeField] private float incrementalSpeed;
    [SerializeField] private float catchUpDistanceX;
    [SerializeField] private float catchUpSpeedMultiplier; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float playerX = playerTransform.position.x;
        if (Mathf.Abs(playerX - transform.position.x) > catchUpDistanceX) 
        {
            transform.position = new Vector3(transform.position.x + speed * catchUpSpeedMultiplier * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else 
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        speed += incrementalSpeed * Time.deltaTime;
    }
}


