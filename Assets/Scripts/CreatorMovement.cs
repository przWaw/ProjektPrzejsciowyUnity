using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatorMovement : MonoBehaviour
{
    [SerializeField] private float swipeSpeed = 150f;
    [SerializeField] private float rotationSpeed = 1500f;
    [SerializeField] private Transform orbitContext;
    private Vector3 rotation;
    private Vector3 speed = Vector3.zero;
    private Vector2 mousePosition;
    private bool moving = false;
    private bool rotating = false;
    private bool orbiting = false;
    private bool zooming = false;


    static public Transform camPosition;
    
    void Start()
    {
        camPosition = this.transform;
       
    }

    void Update()
    {
        moving = (Input.GetMouseButton(2) || (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt)));
        rotating = (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl));
        orbiting = (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift));
        zooming = Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftAlt);
        

        Vector3 lineEnd = transform.position + 300.0f * transform.forward;
        Debug.DrawLine(transform.position, lineEnd);

        if (moving)
        {
            Move();
        }

        if (rotating)
        {
            Rotate();
        }
        
        if (orbiting)
        {
            Orbit(orbitContext);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            LookAt(orbitContext.position);
        }

        if (zooming)
        {
            MoveForward();
        }


    }

    private void MoveForward()
    {
        float vertical = Input.GetAxis("Mouse Y") * swipeSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);
    }

    private void LookAt(Vector3 position)
    {
        Vector3 relativePos = position - transform.localPosition;
        Quaternion rotationQuaternion = Quaternion.LookRotation(relativePos, Vector3.up);
        Quaternion smoothRotation = Quaternion.Slerp(transform.localRotation, rotationQuaternion, 0.2f);
        transform.localRotation = smoothRotation;
        Vector3 temp = orbitContext.transform.eulerAngles;
        temp.x = 0;
        orbitContext.transform.eulerAngles = temp; 
    }

    private void Orbit(Transform contextTransform)
    {
        float axisX = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float axisY = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        contextTransform.Rotate(axisX, 0, 0);
        contextTransform.eulerAngles += new Vector3(0, axisY, 0);
    }

    private void Rotate()
    {
        float axisX = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float axisY = -Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(axisX, 0, 0);
        transform.eulerAngles += new Vector3(0, axisY, 0);
    }

    private void Move()
    {
        float horizontal = -Input.GetAxis("Mouse X") * swipeSpeed * Time.deltaTime;
        float vertical = -Input.GetAxis("Mouse Y") * swipeSpeed * Time.deltaTime;
        transform.Translate(horizontal, vertical, 0);
    }
}
