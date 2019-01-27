using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    bool FirstTimePlaying = true;
    public Collider2D Player;
    public AudioSource CheckpointSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.Equals(Player) && FirstTimePlaying)
        {
            CheckpointSound.Play();
            FirstTimePlaying = false;
        }
            
    }
}
