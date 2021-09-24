using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour {

#region Public Fields
#endregion

#region Private Serializable Fields

#endregion

#region Private Fields
    private TextMeshProUGUI textSeconds;
    private Coroutine countdownCor;
    private bool countdownActive = false;
    private int roundDuration, currentSeconds;
#endregion

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.FightStarted, StartCountdown);
        EventManager.Instance.StartListening(EventManager.Events.RoundEnded, StopCountdown);
        EventManager.Instance.StartListening(EventManager.Events.FightEnded, StopCountdown);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.FightStarted, StartCountdown);
        EventManager.Instance.StopListening(EventManager.Events.RoundEnded, StopCountdown);
        EventManager.Instance.StopListening(EventManager.Events.FightEnded, StopCountdown);
    }

#region MonoBehaviour CallBacks
    void Awake() {
        textSeconds = GetComponent<TextMeshProUGUI>();
        if(textSeconds == null) {
            Debug.LogError($"{name} is missing a TextMeshProUGUI");
        }
        currentSeconds = Data.roundDuration;
        textSeconds.text = currentSeconds.ToString();
    }

    void StartCountdown() {

        countdownActive = true;
        countdownCor = StartCoroutine(StartCountdownCor());
    }

    private IEnumerator StartCountdownCor() {

        while (countdownActive) {
            currentSeconds--;
            textSeconds.text = currentSeconds.ToString();

            if(currentSeconds <= 0) {
                countdownActive = false;
                EventManager.Instance.TriggerEvent(EventManager.Events.RoundTimerZeroed);
                yield break;
            }

            yield return new WaitForSeconds(1);
        }
    }
    void StopCountdown() {
        StopCoroutine(countdownCor);
    }
#endregion

#region Private Methods

#endregion

#region Public Methods

#endregion
}