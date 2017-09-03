using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour {

    public GameObject settingsPanel;

    public void StartGame() {
        Toolbox.FindRequiredComponent<GameMaster>().InitiateStory();
    }

	public void LoadGame(){
		Debug.LogWarning ("The loading feature is not implemented yet!");
	}

    public void ShowSettings() {
		settingsPanel.SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }
}