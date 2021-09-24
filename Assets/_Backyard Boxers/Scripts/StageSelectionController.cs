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
    [SerializeField] private Animator animCameras;

#endregion

#region Private Fields
    private int environmentSelectedID;
    private int currentEnvIndexOnList = 0;
    private string sceneName;
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
        StartCoroutine(ShowLoadingScreenForABit());
        sliderLoading.value = 0;
        environmentSelectedID = PlayerPrefs.GetInt("Environment Selected ID", 0);
        RefreshStageData();
        SetCorrectEnvironment();
    }

    private void SetCorrectEnvironment() {
        foreach (var env in listOfEnvironments.environments) {
            if(env.uniqueID == environmentSelectedID) {
                return;
            }
            animCameras.SetTrigger("Next");
        }
    }

    private IEnumerator ShowLoadingScreenForABit() {
        goLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        goLoadingScreen.SetActive(false);
    }

#endregion

#region Private Methods
    private void OnButtonBackPressed() {
        goLoadingScreen.SetActive(true);
        sliderLoading.value = 0;
        sceneName = "Main Menu";
        StartCoroutine(ShowLoadingScreen());
    }

    private void OnButtonPrevPressed() {
        if(currentEnvIndexOnList == 0) {
            currentEnvIndexOnList = listOfEnvironments.environments.Count - 1;
        } else {
            currentEnvIndexOnList--;
        }
        environmentSelectedID = listOfEnvironments.environments[currentEnvIndexOnList].uniqueID;
        RefreshStageData();
        animCameras.SetTrigger("Previous"); //must be called after refreshstage
    }

    private void OnButtonNextPressed() {
        if(currentEnvIndexOnList == listOfEnvironments.environments.Count - 1) {
            currentEnvIndexOnList = 0;
        } else {
            currentEnvIndexOnList++;
        }
        environmentSelectedID = listOfEnvironments.environments[currentEnvIndexOnList].uniqueID;
        RefreshStageData();
        animCameras.SetTrigger("Next"); //must be called after refreshstage
    }

    private void RefreshStageData() {
        currentEnvIndexOnList = 0;
        foreach (var env in listOfEnvironments.environments) {
            if(env.uniqueID == environmentSelectedID) {
                environmentName.text = env.environmentName;
                // environmentImage.sprite = env.environmentSprite;
                sceneName = env.sceneName;
                PlayerPrefs.SetInt("Environment Selected ID", environmentSelectedID);
                PlayerPrefs.SetString("Environment Scene Name", sceneName);

                return;
            }
            currentEnvIndexOnList++;
        }
    }

    private void OnButtonPlayPressed() {
        // goLoadingScreen.SetActive(true);
        // StartCoroutine(ShowLoadingScreen());
    }

    private IEnumerator ShowLoadingScreen() {
        yield return new WaitForSeconds(0.25f);
        asyncLoading = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoading.isDone) {
            sliderLoading.value = asyncLoading.progress;
            yield return null;
        }
    }
#endregion

#region Public Methods

#endregion
}