using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields
    [SerializeField] GameObject goWaitingForTapScreen, goMainMenuScreen, goPlayerMainMenu;
#endregion

#region Private Fields
    bool isWaitingForTap = true;
#endregion

#region MonoBehaviour CallBacks

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ButtonCharacterCustomizationPressed, OnLoadCharacterCustomizationScene);
        EventManager.Instance.StartListening(EventManager.Events.ButtonSelectEnvironmentPressed, OnButtonSelectEnvironmentPressed);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ButtonCharacterCustomizationPressed, OnLoadCharacterCustomizationScene);
        EventManager.Instance.StopListening(EventManager.Events.ButtonSelectEnvironmentPressed, OnButtonSelectEnvironmentPressed);
    }
    void Awake() {

    }

    void Start() {
        goMainMenuScreen.SetActive(false);
        goPlayerMainMenu.SetActive(false);
        goWaitingForTapScreen.SetActive(true);
    }

    void Update() {
        if(GameManager.Instance.gameState != GameManager.GameStates.MainMenu) {
            return;
        }

        if(isWaitingForTap && Input.GetMouseButtonDown(0)) {
            Debug.Log("Tapped");
            isWaitingForTap = false;
            ShowMainMenu();
        }
    }
#endregion

#region Private Methods
    private void ShowMainMenu() {
        goMainMenuScreen.SetActive(true);
        goPlayerMainMenu.SetActive(true);
        goWaitingForTapScreen.SetActive(false);
        GameManager.Instance.gameState = GameManager.GameStates.MainMenu;
    }

    private void OnLoadCharacterCustomizationScene() {
        if(isWaitingForTap)
            return;
        SceneManager.LoadScene("Character Selection");
    }

    private void OnButtonSelectEnvironmentPressed() {
        SceneManager.LoadScene("Environment Selection");
    }

#endregion

#region Public Methods

#endregion
}