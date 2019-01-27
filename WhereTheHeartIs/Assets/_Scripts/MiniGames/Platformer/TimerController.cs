using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour, IMiniGame {

    public TMP_Text TimerText;
    //private float Timer = 120f;
    DateTime BeginTime;
    int time = 100;

    bool CompletedMission = false;

    public AudioSource FlagPole;

    public AudioSource DeathTone;

    bool OutOfTime = false;

    public GameObject ControllerObject
    {
        get
        {
            return gameObject;
        }
    }


    // Use this for initialization
    void Start () {
        //Timer.SetText("");
        BeginTime = DateTime.Now;
        if (GameController.instance != null)
            GameController.instance.SetMinigame(this);
	}
	
	// Update is called once per frame
	void Update () {
        TimeSpan TimeLeft = DateTime.Now - BeginTime;

        time = 100 - (int)TimeLeft.TotalSeconds;

        TimerText.SetText((time).ToString());

        if (time <= 0 && !OutOfTime)
        {
            DeathTone.Play();

            OutOfTime = true;
        }

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        FlagPole.Play();

        if (other.gameObject.name == "Player")
            CompletedMission = true;


        Waiter();
        Debug.Log("Yes");
    }

    public bool IsFinished()
    {
        return time <= 0 || DidComplete();
    }

    public bool DidComplete()
    {
        return CompletedMission;
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(10);
    }
}
