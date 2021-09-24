using System;
using System.Collections;
using System.Collections.Generic;
using UMA.CharacterSystem;
using UnityEngine;

public class OpponentAI : MonoBehaviour {

    public static OpponentAI Instance { get => instance; private set => instance = value; }
    private static OpponentAI instance;

    // [SerializeField] GameObject umaHolder;
    [SerializeField] private Transform ringCenter, rosterLookAt;
    [SerializeField] private Transform spawnPositionForRoster, spawnPositionForFight;
    [SerializeField] private float movementStepSizeInDegrees = 30f;
    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private GameObject gloveHitBoxLeft, gloveHitBoxRight, headHitBox;
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private List<GameObject> listOfOpponents;
    [SerializeField] internal int maxHealth, health;

    // private GameObject UMA;

    internal string opponentName;
    private Animator anim;
    private GameObject model;
    // private DynamicCharacterAvatar avatar;
    // public DynamicCharacterAvatar Avatar { get => avatar; set => avatar = value; }

    private Coroutine movingCor;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowingRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, OnStartFight);
        EventManager.Instance.StartListening(EventManager.Events.ShowResults, OnShowResults);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowingRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, OnStartFight);
        EventManager.Instance.StopListening(EventManager.Events.ShowResults, OnShowResults);
    }
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }

        if(listOfOpponents == null || listOfOpponents.Count == 0) {
            Debug.LogError("Opponent has no characters");
        }
    }
    private void Start() {
        //Spawn Character
        var characterModel = listOfOpponents[PlayerPrefs.GetInt("CurrentOpponent", 0)];
        model = Instantiate(characterModel, spawnPositionForRoster.position, Quaternion.identity, transform);
        anim = model.GetComponent<Animator>();
        FaceRingCenter();
        SetupHitBoxes();
        //SetupClothes();

        maxHealth = PlayerPrefs.GetInt("Opponent Max Health", 1000);
        health = maxHealth;
        opponentName = PlayerPrefs.GetString("Opponent Name", "John Doe");
    }

    private void Update() {

        if(Data.gameState == Data.GameStates.ShowingRingGirl || Data.gameState == Data.GameStates.Fighting) {
            FaceRingCenter();
        } else if(Data.gameState == Data.GameStates.ShowingRoster || Data.gameState == Data.GameStates.ShowingResults) {
            FaceRosterCamera();
        }
    }

    private void SetupHitBoxes() {
        var rightHand = transform.FindDeepChild("middle_01_r");
        var leftHand = transform.FindDeepChild("middle_01_l");
        var head = transform.FindDeepChild("head");

        gloveHitBoxRight.tag = "Opponent";
        gloveHitBoxLeft.tag = "Opponent";
        headHitBox.tag = "Opponent";

        Instantiate(gloveHitBoxRight, rightHand.position, Quaternion.identity, rightHand);
        Instantiate(gloveHitBoxLeft, leftHand.position, Quaternion.identity, leftHand);
        Instantiate(headHitBox, head.position, Quaternion.identity, head);
    }

    internal void GetHit(int damageAmount) {
        // if(isBlocking) {
        // health -= damageAmount / 2f;
        // } else {
        anim.ResetTrigger("Get Hit");
        anim.SetTrigger("Get Hit");
        var head = transform.FindDeepChild("head");
        Instantiate(bloodParticles, head.position, Quaternion.identity, null);
        // ScreenFlash.instance.Flash(UnityEngine.Random.Range(0.25f, 0.5f));

        health -= damageAmount;
        if(health <= 0) {
            health = 0;
            anim.SetTrigger("Faint");
            anim.SetLayerWeight(1, 0);
            EventManager.Instance.TriggerEvent(EventManager.Events.OpponentKO);
        }
        // }

        EventManager.Instance.TriggerEvent(EventManager.Events.RefreshUI);

    }

    private void FaceRingCenter() {
        model.transform.LookAt(ringCenter.position);
    }

    internal void HandleTiltInput(Vector3 tilt) {
        Move(tilt.x < 0);
    }
    public void Move(bool left) {
        if(movingCor != null) {
            anim.ResetTrigger("Move Left");
            anim.ResetTrigger("Move Right");
            StopCoroutine(movingCor);
        }
        movingCor = StartCoroutine(MoveCoRo(left));
    }

    private IEnumerator MoveCoRo(bool left) {

        if(left) {
            anim.SetTrigger("Move Left");
        } else {
            anim.SetTrigger("Move Right");
        }

        var movementStep = left ? movementStepSizeInDegrees : -movementStepSizeInDegrees;

        var startTime = Time.time;
        while (Time.time < startTime + movementSpeed) {
            transform.RotateAround(ringCenter.position, Vector3.up, movementStep / movementSpeed * Time.deltaTime);
            yield return null;
        }

    }
    private void FaceRosterCamera() {
        if(rosterLookAt != null) {
            model.transform.LookAt(rosterLookAt.position);
        }
    }

    private void OnShowingRingGirl() {
        model.SetActive(false);
    }
    private void OnStartFight() {
        StartCoroutine(OnStartFightCor());
    }

    private IEnumerator OnStartFightCor() {
        yield return new WaitForSeconds(0.55f);
        model.transform.position = spawnPositionForFight.position;
        model.SetActive(true);
    }

    void OnShowResults() {
        StartCoroutine(OnShowResultsCor());
    }

    private IEnumerator OnShowResultsCor() {
        yield return new WaitForSeconds(1f);
        model.transform.position = spawnPositionForRoster.position;
        FaceRosterCamera();
        if(FightManager.Instance.isPlayerWinner) {
            anim.SetTrigger("Be Sad");
        }
        yield return null;
    }
}