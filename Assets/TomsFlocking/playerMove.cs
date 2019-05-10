using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {


    public int speed = 10;

    private Rigidbody rb;
    private bool isMoving;
    private Vector3 vel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {

        #region playerControls

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(0, 0, -speed);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.x > 20 || rb.velocity.x < -20)
            {
                rb.velocity.Set(20, rb.velocity.y, rb.velocity.z);
            }

            if (rb.velocity.z > 20 || rb.velocity.z < -20)
            {
                rb.velocity.Set(rb.velocity.x, rb.velocity.y, 20);
            }
        }
        else
        {
            rb.velocity = Vector3.zero;  
        }

        #endregion
    }
}
