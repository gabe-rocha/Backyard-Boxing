using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour {
    public Transform playerSpawnPosition, opponentSpawnPosition, ringCenter;
    [SerializeField] private GameObject playerCharacterPrefab, opponentCharacterPrefab;
    [SerializeField] private GameObject fightUI, rosterUI, ringGirlUI, resultsUI;
    [SerializeField] private float countdownDuration = 3;
    [SerializeField] private TextMeshProUGUI txtVS, txtCountdown;
    [SerializeField] private GameObject goLoadingScreen;

    private static FightManager instance;
    public static FightManager Instance { get { return instance; } }

    private Player player;
    private OpponentAI opponent;
    private Coroutine corShowingRingGirl;

    internal bool isPlayerWinner = false;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
        EventManager.Instance.StartListening(EventManager.Events.ShowFightRoster, OnShowRoster);
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.ButtonSkipRingGirl, OnButtonSkipRingGirlPressed);
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, StartFightCountdown);
        EventManager.Instance.StartListening(EventManager.Events.OpponentKO, OnOpponentKO);
        EventManager.Instance.StartListening(EventManager.Events.PlayerKO, OnPlayerKO);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ButtonPlayPressed, OnButtonPlayPressed);
        EventManager.Instance.StopListening(EventManager.Events.ShowFightRoster, OnShowRoster);
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.ButtonSkipRingGirl, OnButtonSkipRingGirlPressed);
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, StartFightCountdown);
        EventManager.Instance.StopListening(EventManager.Events.OpponentKO, OnOpponentKO);
        EventManager.Instance.StopListening(EventManager.Events.PlayerKO, OnPlayerKO);
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

    IEnumerator Start() {
        yield return new WaitForSeconds(0.25f);
        EventManager.Instance.TriggerEvent(EventManager.Events.ShowFightRoster);

        StartCoroutine(ShowLoadingScreenForABit());

        //start fight
        // StartCoroutine(StartFight());
    }

    private IEnumerator ShowLoadingScreenForABit() {
        goLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        goLoadingScreen.SetActive(false);
    }

    void OnShowRoster() {
        Debug.Log("Showing Roster");
        fightUI.SetActive(false);
        ringGirlUI.SetActive(false);
        resultsUI.SetActive(false);
        rosterUI.SetActive(true);

        txtVS.transform.localScale = Vector3.zero;
        LeanTween.scale(txtVS.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);

        Data.gameState = Data.GameStates.ShowingRoster;
    }

    void OnShowRingGirl() {
        corShowingRingGirl = StartCoroutine(ShowRingGirlCor());
    }

    private IEnumerator ShowRingGirlCor() {
        yield return new WaitForSeconds(0.55f); //screen fade
        Debug.Log("Showing Ring Girl");
        fightUI.SetActive(false);
        rosterUI.SetActive(false);
        resultsUI.SetActive(false);
        ringGirlUI.SetActive(true);
        Data.gameState = Data.GameStates.ShowingRingGirl;

        yield return new WaitForSeconds(5f);

        EventManager.Instance.TriggerEvent(EventManager.Events.StartFightCountdown);
    }

    private void StartFightCountdown() {
        StartCoroutine(StartFightCountdownCor());
    }

    private IEnumerator StartFightCountdownCor() {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("Showing Countdown");
        rosterUI.SetActive(false);
        ringGirlUI.SetActive(false);
        resultsUI.SetActive(false);
        fightUI.SetActive(true);

        txtCountdown.text = "Round 1";
        txtCountdown.transform.localScale = Vector3.zero;
        LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(2f);
        LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0f), 0f).setEase(LeanTweenType.easeOutBounce);

        txtCountdown.text = "Fight!";
        LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 0.5f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(1f);
        LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0.2f), 0.25f).setEase(LeanTweenType.linear);

        // txtCountdown.text = "1";
        // LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        // yield return new WaitForSeconds(1f);
        // LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0f), 0f).setEase(LeanTweenType.easeOutBounce);

        // txtCountdown.text = "Fight!";
        // LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        // yield return new WaitForSeconds(1f);
        // LeanTween.scale(txtCountdown.gameObject, new Vector2(0f, 0f), 1f).setEase(LeanTweenType.linear);

        yield return new WaitForSeconds(0.25f);

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
        if(corShowingRingGirl != null) {
            StopCoroutine(corShowingRingGirl);
        }

        EventManager.Instance.TriggerEvent(EventManager.Events.StartFightCountdown);
    }

    void OnOpponentKO() {
        isPlayerWinner = true;
        Data.gameState = Data.GameStates.FightEnd;
        EventManager.Instance.TriggerEvent(EventManager.Events.FightEnded);
        ShowResults();
    }

    void OnPlayerKO() {
        isPlayerWinner = false;
        Data.gameState = Data.GameStates.FightEnd;
        EventManager.Instance.TriggerEvent(EventManager.Events.FightEnded);
        ShowResults();
    }

    private void ShowResults() {
        StartCoroutine(ShowResultsCor());
    }

    private IEnumerator ShowResultsCor() {
        yield return new WaitForSeconds(5f); //Camera Rotating Around Ring
        Data.gameState = Data.GameStates.ShowingResults;
        EventManager.Instance.TriggerEvent(EventManager.Events.ShowResults);
        yield return new WaitForSeconds(0.9f); //Fading out

        Debug.Log("Showing Results");
        rosterUI.SetActive(false);
        ringGirlUI.SetActive(false);
        fightUI.SetActive(false);
        resultsUI.SetActive(true);

        // txtCountdown.text = "Round 1";
        // txtCountdown.transform.localScale = Vector3.zero;
        // LeanTween.scale(txtCountdown.gameObject, new Vector2(1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);

        yield return null;
    }

}