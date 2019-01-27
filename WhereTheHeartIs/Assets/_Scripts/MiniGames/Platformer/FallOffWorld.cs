using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffWorld : MonoBehaviour {

    public BoxCollider2D Pit;

    public CapsuleCollider2D Checkpoint;

    public AudioSource DeathSound;

    public AudioSource CheckPointSound;

    //public GameObject Cylinder;

	// Use this for initialization
	void Start () {
        Pit = GetComponent<BoxCollider2D>();
        //Checkpoint = Cylinder.GetComponent<CapsuleCollider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.x > Checkpoint.transform.position.x)
        {
            other.transform.position = Checkpoint.transform.position;
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            DeathSound.Play();
        }
        else
        {
            other.transform.position = new Vector2(-6f, -0.5f);
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            DeathSound.Play();
        }
        
    }
}
