using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields
    [SerializeField] GameObject goWaitingForTapScreen, goMainMenuScreen;
#endregion

#region Private Fields
    bool isWaitingForTap = true;
#endregion

#region MonoBehaviour CallBacks
    void Awake() {
        //component = GetComponent<Component>();
        //if(component == null) {
        //Debug.LogError($"{name} is missing a component");
        //}

    }

    void Start() {
        goMainMenuScreen.SetActive(false);
        goWaitingForTapScreen.SetActive(true);
    }

    void Update() {
        if(GameManager.Instance.gameState != GameManager.GameStates.MainMenu) {
            return;
        }

        if(isWaitingForTap && Input.GetMouseButtonDown(0)) {
            Debug.Log("Tapped");
            isWaitingForTap = false;
            goWaitingForTapScreen.SetActive(false);
            ShowMainMenu();
        }
    }

#endregion

#region Private Methods
    private void ShowMainMenu() {
        goMainMenuScreen.SetActive(true);
    }

#endregion

#region Public Methods

#endregion
}