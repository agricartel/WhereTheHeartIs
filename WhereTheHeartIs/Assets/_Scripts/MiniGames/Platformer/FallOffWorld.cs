using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffWorld : MonoBehaviour {

    public BoxCollider2D Pit;

	// Use this for initialization
	void Start () {
        Pit = GetComponent<BoxCollider2D>();
        //Player = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Yes");
        other.transform.position = new Vector2(-6f,-0.5f);
        other.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }
}
