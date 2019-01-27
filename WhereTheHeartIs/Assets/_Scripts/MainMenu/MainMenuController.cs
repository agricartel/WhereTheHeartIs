using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public Button playButton;
    public Button creditsButton;
    
	void Start ()
    {
        playButton.onClick.AddListener(() => { SceneManager.LoadScene("MapScene"); });
        creditsButton.onClick.AddListener(() => { SceneManager.LoadScene("CreditsScene"); });
    }
	
}
