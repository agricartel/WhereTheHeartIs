using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeCollectController : MonoBehaviour
{
    public Rigidbody2D cubeRigidBody;

    // Use this for initialization
    void Start()
    {
        cubeRigidBody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector2 force = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 10f;
        cubeRigidBody.AddForce(force, ForceMode2D.Force);
    }
}
