using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour {

    public Button MenuButton;

	// Use this for initialization
	void Start () {
        MenuButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenuScene"); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
