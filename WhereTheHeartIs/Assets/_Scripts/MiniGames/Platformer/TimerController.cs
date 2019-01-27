using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour, IMiniGame {

    public TMP_Text TimerText;
    //private float Timer = 120f;
    DateTime BeginTime;
    int time = 120;

    bool CompletedMission = false;

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

        time = 120 - (int)TimeLeft.TotalSeconds;

        TimerText.SetText((time).ToString());

        if (time <= 0)
        {

        }

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
            CompletedMission = true;

    }

    public bool IsFinished()
    {
        return time <= 0 || DidComplete();
    }

    public bool DidComplete()
    {
        return CompletedMission;
    }
}
