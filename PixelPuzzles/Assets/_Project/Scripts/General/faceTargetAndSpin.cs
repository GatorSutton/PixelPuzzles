using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceTargetAndSpin : MonoBehaviour
{

    public float rotationSpeed;
    private Transform target;
    private Vector3 rotatingVector = Vector3.up;



    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Floor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
      //  transform.LookAt(2 * transform.position - target.position, rotatingVector);
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed, Space.Self);
       // rotatingVector = Quaternion.Euler(Vector3.right) * rotatingVector;
  
    }

}