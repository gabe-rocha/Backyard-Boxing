using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarsController : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields
    [SerializeField] private Slider playerHealthSlider, opponentHealthSlider;
    [SerializeField] private TextMeshProUGUI textPlayerName, textPlayerHealth, textOpponentName, textOpponentHealth;

#endregion

#region Private Fields

#endregion

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, OnStartFightCountdown);
        EventManager.Instance.StartListening(EventManager.Events.RefreshUI, OnRefreshUI);

    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, OnStartFightCountdown);
        EventManager.Instance.StopListening(EventManager.Events.RefreshUI, OnRefreshUI);
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
    void OnStartFightCountdown() {
        var playerHealth = Player.Instance.health;
        textPlayerHealth.text = playerHealth.ToString();
        playerHealthSlider.maxValue = playerHealth;
        playerHealthSlider.value = playerHealth;

        var opponentHealth = OpponentAI.Instance.health;
        textOpponentHealth.text = opponentHealth.ToString();
        opponentHealthSlider.maxValue = opponentHealth;
        opponentHealthSlider.value = opponentHealth;

        textPlayerName.text = Player.Instance.playerName;
        textOpponentName.text = OpponentAI.Instance.opponentName;
    }

    void OnRefreshUI() {
        var playerHealth = Player.Instance.health;
        textPlayerHealth.text = playerHealth.ToString();
        playerHealthSlider.value = playerHealth;

        var opponentHealth = OpponentAI.Instance.health;
        textOpponentHealth.text = opponentHealth.ToString();
        opponentHealthSlider.value = opponentHealth;
    }
#endregion

#region Public Methods

#endregion
}