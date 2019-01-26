using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    IMiniGame currentMiniGame;
    string currentNodeID;

    string cutScenePath = "";

    // Use this for initialization
    void Start ()
    {
        instance = this;

        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);
	}

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive)
        {
            MapController.instance.mapCamera.gameObject.SetActive(false);
        }

        if (scene.name == "CutScene")
        {
            CutSceneController.instance.SetCutScene(cutScenePath);
            currentMiniGame = CutSceneController.instance;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentMiniGame != null)
        {
            SceneManager.SetActiveScene(currentMiniGame.ControllerObject.scene);

            if (currentMiniGame.IsFinished())
            {
                if (currentMiniGame.DidComplete())
                {
                    MapController.instance.CompleteNode(currentNodeID);
                }

                SceneManager.UnloadSceneAsync(currentMiniGame.ControllerObject.scene);
                currentMiniGame = null;

                SceneManager.SetActiveScene(MapController.instance.gameObject.scene);
                MapController.instance.mapCamera.gameObject.SetActive(true);
            }
        }
	}

    public void GoToMiniGame(MiniGameType gameType, string nodeID)
    {
        if (currentMiniGame != null)
            Debug.LogError("A minigame is currently in progress");

        currentNodeID = nodeID;

        switch (gameType)
        {
            case MiniGameType.TEST:
                {
                    SceneManager.LoadScene("TestMiniGame", LoadSceneMode.Additive);
                }
                break;
        }
    }

    public void RunCutScene(string path, string nodeID)
    {
        if (currentMiniGame != null)
            Debug.LogError("A minigame is currently in progress");

        currentNodeID = nodeID;

        SceneManager.LoadScene("CutScene", LoadSceneMode.Additive);

        cutScenePath = path;
    }
}
