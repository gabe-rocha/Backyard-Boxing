using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightResultsDisplay : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields
    [SerializeField] private TextMeshProUGUI textVictoryOrLoss, textPlayerName, textOpponentName;
    [SerializeField] private GameObject goLoadingScreen;
    [SerializeField] private Slider sliderLoading;

#endregion

#region Private Fields
    AsyncOperation asyncLoading;
#endregion

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowResults, OnShowResults);
        EventManager.Instance.StartListening(EventManager.Events.ButtonBackPressed, OnButtonBack);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowResults, OnShowResults);
        EventManager.Instance.StopListening(EventManager.Events.ButtonBackPressed, OnButtonBack);
    }

#region MonoBehaviour CallBacks
    void Awake() {
        //component = GetComponent<Component>();
        //if(component == null) {
        //Debug.LogError($"{name} is missing a component");
        //}

    }

    void Start() {

    }

    void Update() {

    }
#endregion

#region Private Methods
    private void OnShowResults() {

        textVictoryOrLoss.text = FightManager.Instance.isPlayerWinner ? "You Win!" : "You Lose";
        textPlayerName.text = Player.Instance.playerName;
        textOpponentName.text = OpponentAI.Instance.opponentName;
    }

    private void OnButtonBack() {
        goLoadingScreen.SetActive(true);
        asyncLoading = SceneManager.LoadSceneAsync(0);
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