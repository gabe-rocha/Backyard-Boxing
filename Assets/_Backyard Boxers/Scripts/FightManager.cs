using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour {
    public Transform playerSpawnPosition, opponentSpawnPosition, ringCenter;
    [SerializeField] private GameObject playerCharacterPrefab, opponentCharacterPrefab;
    [SerializeField] private GameObject fightUI, rosterUI, ringGirlUI;
    [SerializeField] private float countdownDuration = 3;
    [SerializeField] private TextMeshProUGUI txtVS, txtCountdown;

    private static FightManager instance;
    public static FightManager Instance { get { return instance; } }

    private Player player;
    private OpponentAI opponent;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
        EventManager.Instance.StartListening(EventManager.Events.ShowFightRoster, OnShowRoster);
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFight, OnStartFight);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
        EventManager.Instance.StopListening(EventManager.Events.ShowFightRoster, OnShowRoster);
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFight, OnStartFight);
    }

    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
        }
        // DontDestroyOnLoad(gameObject);
    }

    void Start() {
        EventManager.Instance.TriggerEvent(EventManager.Events.ShowFightRoster);

        //start fight
        // StartCoroutine(StartFight());
    }

    void OnShowRoster() {
        Debug.Log("Showing Roster");
        fightUI.SetActive(false);
        ringGirlUI.SetActive(false);
        rosterUI.SetActive(true);

        txtVS.transform.localScale = Vector3.zero;
        LeanTween.scale(txtVS.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);

        Data.gameState = Data.GameStates.ShowingRoster;
    }

    void OnShowRingGirl() {
        StartCoroutine(ShowRingGirlCor());
    }

    private IEnumerator ShowRingGirlCor() {
        Debug.Log("Showing Ring Girl");
        fightUI.SetActive(false);
        rosterUI.SetActive(false);
        ringGirlUI.SetActive(true);
        Data.gameState = Data.GameStates.ShowingRingGirl;

        yield return new WaitForSeconds(1f);
        EventManager.Instance.TriggerEvent(EventManager.Events.StartFight);
    }

    private void OnStartFight() {
        StartCoroutine(StartFightCor());
    }

    private IEnumerator StartFightCor() {
        Debug.Log("Showing Countdown");
        rosterUI.SetActive(false);
        ringGirlUI.SetActive(false);
        fightUI.SetActive(true);

        txtCountdown.text = "3";
        txtCountdown.transform.localScale = Vector3.zero;
        LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(1f);
        LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0f), 0f).setEase(LeanTweenType.easeOutBounce);

        txtCountdown.text = "2";
        LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(1f);
        LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0f), 0f).setEase(LeanTweenType.easeOutBounce);

        txtCountdown.text = "1";
        LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(1f);
        LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0f), 0f).setEase(LeanTweenType.easeOutBounce);

        txtCountdown.text = "Fight!";
        LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(1f);
        LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0f), 1f).setEase(LeanTweenType.linear);
        yield return new WaitForSeconds(1f);

        EventManager.Instance.TriggerEvent(EventManager.Events.FightStarted);
        Data.gameState = Data.GameStates.Fighting;
    }

    public void OnButtonBackPressed() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //TODO
        SceneManager.LoadScene(0); //TODO
    }

    public void OnButtonPlayPressed() {
        EventManager.Instance.TriggerEvent(EventManager.Events.ShowRingGirl);
    }

    public void OnButtonSkipRingGirlPressed() {
        EventManager.Instance.TriggerEvent(EventManager.Events.StartFight);
    }
}