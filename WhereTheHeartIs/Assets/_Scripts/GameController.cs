using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;


	// Use this for initialization
	void Start ()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void GoToMiniGame(MiniGameType gameType)
    {
        switch (gameType)
        {
            case MiniGameType.TEST:
                {
                    SceneManager.LoadScene("TestMiniGame", LoadSceneMode.Additive);
                }
                break;
        }
    }
}
