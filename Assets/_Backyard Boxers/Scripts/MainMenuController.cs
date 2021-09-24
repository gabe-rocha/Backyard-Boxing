using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields
    [SerializeField] GameObject goWaitingForTapScreen, goMainMenuScreen, goLoadingScreen, goPlayerSpawnPointFactory, goPlayerSpawnPointAlleyway;
    [SerializeField] private GameObject pfPlayer;
    [SerializeField] private Slider sliderLoading;
    [SerializeField] private Animator animCameraAngles;

    private GameObject goPlayer;

#endregion

#region Private Fields
    bool isWaitingForTap = true;
    AsyncOperation asyncLoading;
#endregion

#region MonoBehaviour CallBacks

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ButtonCharacterCustomizationPressed, OnLoadCharacterCustomizationScene);
        EventManager.Instance.StartListening(EventManager.Events.ButtonSelectEnvironmentPressed, OnButtonSelectEnvironmentPressed);
        EventManager.Instance.StartListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ButtonCharacterCustomizationPressed, OnLoadCharacterCustomizationScene);
        EventManager.Instance.StopListening(EventManager.Events.ButtonSelectEnvironmentPressed, OnButtonSelectEnvironmentPressed);
        EventManager.Instance.StopListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
    }
    void Awake() {
        if(animCameraAngles == null) {
            Debug.LogError($"{name} is missing animCameraAngles");
        }
    }

    void Start() {
        if(GameManager.Instance.firstLoad) {
            goMainMenuScreen.SetActive(false);
            goWaitingForTapScreen.SetActive(true);
        } else {
            ShowMainMenu();
        }

        //Instantiate player
        int environmentSelectedID = PlayerPrefs.GetInt("Environment Selected ID", 0);
        if(environmentSelectedID == 0) {
            goPlayer = Instantiate(pfPlayer, goPlayerSpawnPointFactory.transform);
        } else {
            goPlayer = Instantiate(pfPlayer, goPlayerSpawnPointAlleyway.transform);
        }

        for (var i = 0; i < environmentSelectedID; i++) {
            animCameraAngles.SetTrigger("Next");
        }
    }

    void Update() {
        if(GameManager.Instance.gameState != GameManager.GameStates.MainMenu) {
            return;
        }

        if(isWaitingForTap && Input.GetMouseButtonDown(0)) {
            ShowMainMenu();
        }
    }
#endregion

#region Private Methods
    private void ShowMainMenu() {
        Debug.Log("Tapped");
        isWaitingForTap = false;
        // goMainMenuScreen.SetActive(true);
        goWaitingForTapScreen.SetActive(false);
        StartCoroutine(ShowLoadingScreenForABit());
        GameManager.Instance.gameState = GameManager.GameStates.MainMenu;
        EventManager.Instance.TriggerEvent(EventManager.Events.MainMenuTapped);

    }

    private IEnumerator ShowLoadingScreenForABit() {
        goLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        goLoadingScreen.SetActive(false);
    }

    private void OnLoadCharacterCustomizationScene() {
        goLoadingScreen.SetActive(true);
        asyncLoading = SceneManager.LoadSceneAsync("Character Selection");
        StartCoroutine(ShowLoadingScreen());
    }
    private void OnButtonSelectEnvironmentPressed() {
        goLoadingScreen.SetActive(true);
        sliderLoading.value = 0;
        asyncLoading = SceneManager.LoadSceneAsync("Environment Selection");
        StartCoroutine(ShowLoadingScreen());
    }

    private void OnButtonPlayPressed() {
        goLoadingScreen.SetActive(true);
        sliderLoading.value = 0;
        string sceneName = PlayerPrefs.GetString("Environment Scene Name", "Abandoned Factory Fighting Scene"); //The default is the abandoned factory
        asyncLoading = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(ShowLoadingScreen());
    }

    private IEnumerator ShowLoadingScreen() {
        while (!asyncLoading.isDone) {
            sliderLoading.value = asyncLoading.progress;
            yield return null;
        }
    }
#endregion

#region Public Methods

#endregion
}