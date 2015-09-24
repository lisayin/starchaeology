﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    public Button newGameText;
    public Button loadGameText;
    public Button optionsText;
    public Button helpText;
    public Button quitText;

	void Start () {
        newGameText = newGameText.GetComponent<Button>();
        loadGameText = loadGameText.GetComponent<Button>();
        optionsText = optionsText.GetComponent<Button>();
        helpText = helpText.GetComponent<Button>();
        quitText = quitText.GetComponent<Button>();
	}

    public void NewGame() {
        Application.LoadLevel(1);
    }

    public void QuitGame () {
        Application.Quit();
    }
}