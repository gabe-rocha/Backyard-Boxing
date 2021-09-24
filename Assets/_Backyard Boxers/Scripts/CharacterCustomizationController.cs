using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCustomizationController : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields

    [SerializeField] private GameObject goLoadingScreen;
    [SerializeField] private Slider sliderLoading;
    [SerializeField] private GameObject pfPlayer;
    [SerializeField] private Transform playerSpawnTransform;

    internal GameObject goPlayer;
#endregion

#region Private Fields
    private AsyncOperation asyncLoading;

#endregion

#region MonoBehaviour CallBacks

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ButtonBackPressed, OnButtonBackPressed);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ButtonBackPressed, OnButtonBackPressed);
    }

    void Start() {
        //Load Player Character with Clothes and Accessories
        goPlayer = Instantiate(pfPlayer, playerSpawnTransform); //player character puts on its clothes automatically
        StartCoroutine(ShowLoadingScreenForABit());
    }

    private IEnumerator ShowLoadingScreenForABit() {
        goLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        goLoadingScreen.SetActive(false);
    }
#endregion

#region Private Methods

    private void OnButtonBackPressed() {
        EventManager.Instance.TriggerEvent(EventManager.Events.OnCharacterCustomizationDone);
        StartCoroutine(LoadMainMenuCor());
    }

    private IEnumerator LoadMainMenuCor() {
        goLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        asyncLoading = SceneManager.LoadSceneAsync("Main Menu");

        while (!asyncLoading.isDone) {
            sliderLoading.value = asyncLoading.progress;
            yield return null;
        }
    }
#endregion

#region Public Methods

#endregion
}