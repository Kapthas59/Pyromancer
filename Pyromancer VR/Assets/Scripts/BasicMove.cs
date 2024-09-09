using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    float xInput;
    float yInput;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.AddForce(xInput * 10, 0, yInput * 10);
    }
}
