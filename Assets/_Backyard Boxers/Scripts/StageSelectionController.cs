using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectionController : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields
    [SerializeField] EnvironmentListSO listOfEnvironments;
    [SerializeField] Image environmentImage;
    [SerializeField] TextMeshProUGUI environmentName;
    [SerializeField] private GameObject goLoadingScreen;
    [SerializeField] private Slider sliderLoading;

#endregion

#region Private Fields
    private int environmentSelectedID;
    private int currentEnvIndexOnList = 0;
    private string environmentSceneName;
    private AsyncOperation asyncLoading;

#endregion

#region MonoBehaviour CallBacks

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ButtonBackPressed, OnButtonBackPressed);
        EventManager.Instance.StartListening(EventManager.Events.ButtonPrevPressed, OnButtonPrevPressed);
        EventManager.Instance.StartListening(EventManager.Events.ButtonNextPressed, OnButtonNextPressed);
        EventManager.Instance.StartListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
    }

    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ButtonBackPressed, OnButtonBackPressed);
        EventManager.Instance.StopListening(EventManager.Events.ButtonPrevPressed, OnButtonPrevPressed);
        EventManager.Instance.StopListening(EventManager.Events.ButtonNextPressed, OnButtonNextPressed);
        EventManager.Instance.StopListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
    }
    void Awake() {
        if(listOfEnvironments == null || listOfEnvironments.environments.Count == 0) {
            Debug.LogError($"{name} - Environments List SO is empty!");
        }
    }

    void Start() {
        goLoadingScreen.SetActive(false);
        sliderLoading.value = 0;
        environmentSelectedID = PlayerPrefs.GetInt("Environment Selected ID", 0);
        RefreshStage();
    }

#endregion

#region Private Methods
    private void OnButtonBackPressed() {
        Debug.Log("Button Back Pressed");
    }

    private void OnButtonPrevPressed() {
        if(currentEnvIndexOnList == 0) {
            currentEnvIndexOnList = listOfEnvironments.environments.Count - 1;
        } else {
            currentEnvIndexOnList--;
        }
        environmentSelectedID = listOfEnvironments.environments[currentEnvIndexOnList].uniqueID;
        RefreshStage();
    }

    private void OnButtonNextPressed() {
        if(currentEnvIndexOnList == listOfEnvironments.environments.Count - 1) {
            currentEnvIndexOnList = 0;
        } else {
            currentEnvIndexOnList++;
        }
        environmentSelectedID = listOfEnvironments.environments[currentEnvIndexOnList].uniqueID;
        RefreshStage();
    }

    private void RefreshStage() {

        currentEnvIndexOnList = 0;
        foreach (var env in listOfEnvironments.environments) {
            if(env.uniqueID == environmentSelectedID) {
                environmentName.text = env.environmentName;
                environmentImage.sprite = env.environmentSprite;
                environmentSceneName = env.sceneName;
                PlayerPrefs.SetInt("Environment Selected ID", environmentSelectedID);
                return;
            }
            currentEnvIndexOnList++;
        }
    }

    private void OnButtonPlayPressed() {
        goLoadingScreen.SetActive(true);
        asyncLoading = SceneManager.LoadSceneAsync(environmentSceneName);
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