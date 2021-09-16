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
#endregion

#region Private Fields
    private int environmentSelected;

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
        environmentSelected = PlayerPrefs.GetInt("Environment Selected", 0);
        RefreshStage();
    }

#endregion

#region Private Methods
    private void OnButtonBackPressed() {
        Debug.Log("Button Back Pressed");
    }

    private void OnButtonPrevPressed() {
        if(environmentSelected == 0) {
            environmentSelected = listOfEnvironments.environments.Count - 1;
        } else {
            environmentSelected--;
        }
        RefreshStage();
    }

    private void OnButtonNextPressed() {
        if(environmentSelected == listOfEnvironments.environments.Count - 1) {
            environmentSelected = 0;
        } else {
            environmentSelected++;
        }
        RefreshStage();
    }

    private void RefreshStage() {
        environmentName.text = listOfEnvironments.environments[environmentSelected].environmentName;
        environmentImage.sprite = listOfEnvironments.environments[environmentSelected].environmentSprite;
        PlayerPrefs.SetInt("Environment Selected", environmentSelected);
    }

    private void OnButtonPlayPressed() {
        //SceneManager.LoadScene(Data.SceneNames)
        SceneManager.LoadScene("Fighting Demo");
    }
#endregion

#region Public Methods

#endregion
}