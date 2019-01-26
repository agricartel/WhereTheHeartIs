using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public BoxCollider2D cubeCollider;
    public Rigidbody2D cubeRigidBody;

    private bool IsGrounded = true;
	// Use this for initialization
	void Start ()
    {
        cubeCollider = GetComponent<BoxCollider2D>();
        cubeRigidBody = GetComponent<Rigidbody2D>();

	}
	
	
	void FixedUpdate ()
    {
        Vector2 positionx = new Vector2(Input.GetAxis("Horizontal") * 10f, 0f);
        Vector2 positiony = new Vector2(0f, 1f);
        cubeRigidBody.AddForce(positionx, ForceMode2D.Force);


        //ContactFilter2D GroundFilter = new ContactFilter2D();
        //GroundFilter.
        RaycastHit2D GroundRay = Physics2D.Raycast(transform.position, Vector2.down);
        //bool Ray = Physics.Raycast(transform.position, Vector3.down);
        //Debug.Log(Ray);
        //Debug.DrawLine(transform.position, new Vector3(GroundRay.point.x, GroundRay.point.y, transform.position.z));
        float distance;

        if (GroundRay.collider != null && GroundRay.collider != cubeCollider)
        {
            distance = Mathf.Abs(GroundRay.point.y - transform.position.y);
            //Debug.Log(distance);
            IsGrounded = distance <= 1f;
        }

        //IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f).distance <= 1.5f;
        //Debug.Log(Physics2D.Raycast(transform.position, Vector2.down, 1.5f).distance);
        if (IsGrounded && Input.GetButton("Jump"))
        {
            cubeRigidBody.AddForce(positiony, ForceMode2D.Impulse);
        }



	}
}
