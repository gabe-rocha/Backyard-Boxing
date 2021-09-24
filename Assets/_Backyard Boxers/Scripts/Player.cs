using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UMA.CharacterSystem;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get => instance; set => instance = value; }

    [SerializeField] private Transform ringCenter, rosterLookAt;
    [SerializeField] private Transform spawnPositionForRoster, spawnPositionForFight;
    [SerializeField] private float movementStepSizeInDegrees = 15f;
    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private GameObject gloveHitBoxLeft, gloveHitBoxRight, headHitBox;
    [SerializeField] private List<GameObject> listOfCharacters;
    [SerializeField] internal int maxHealth, health;

    // private GameObject UMA;
    private Animator anim;
    private static Player instance;
    private GameObject model;
    private Coroutine movingCor;

    internal string playerName;
    internal int lastPunchDamage;
    // private DynamicCharacterAvatar avatar;
    // public DynamicCharacterAvatar Avatar { get => avatar; set => avatar = value; }

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowingRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, OnStartFight);
        EventManager.Instance.StartListening(EventManager.Events.OpponentKO, OnOpponentKO);
        EventManager.Instance.StartListening(EventManager.Events.ShowResults, OnShowResults);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowingRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, OnStartFight);
        EventManager.Instance.StopListening(EventManager.Events.OpponentKO, OnOpponentKO);
        EventManager.Instance.StopListening(EventManager.Events.ShowResults, OnShowResults);
    }

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }

        if(listOfCharacters == null || listOfCharacters.Count == 0) {
            Debug.LogError("Player has no characters");
        }
    }

    private void Start() {
        //Spawn Character
        var characterModel = listOfCharacters[PlayerPrefs.GetInt("PlayerCharacter", 0)];
        // model = Instantiate(characterModel, spawnPositionForFight.position, Quaternion.identity, transform);
        model = Instantiate(characterModel, spawnPositionForRoster.position, Quaternion.identity, transform);
        anim = model.GetComponent<Animator>();
        FaceRingCenter();
        SetupHitBoxes();
        //SetupClothes();

        maxHealth = PlayerPrefs.GetInt("Player Max Health", 1000);
        health = maxHealth;
        playerName = PlayerPrefs.GetString("Player Name", "Player 1");
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

        gloveHitBoxRight.tag = "Player";
        gloveHitBoxLeft.tag = "Player";
        headHitBox.tag = "Player";

        Instantiate(gloveHitBoxRight, rightHand.position, Quaternion.identity, rightHand);
        Instantiate(gloveHitBoxLeft, leftHand.position, Quaternion.identity, leftHand);
        Instantiate(headHitBox, head.position, Quaternion.identity, head);
    }

    internal void Rotate(float deltaX) {
        transform.Rotate(Vector3.up * deltaX, Space.World);
    }

    public void HandleTiltInput(Vector3 tilt) {
        Move(tilt.x < -Data.tiltMinX);
    }

    public void Move(bool left) {
        if(movingCor != null) {
            anim.ResetTrigger("Move Left");
            anim.ResetTrigger("Move Right");
            StopCoroutine(movingCor);
        }
        movingCor = StartCoroutine(MoveCoRo(left));
    }
    internal void Slip(bool left) {
        if(left) {
            anim.SetTrigger("Slip Left");
            EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Slip Left");
        } else {
            anim.SetTrigger("Slip Right");
            EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Slip Right");
        }
    }

    private IEnumerator MoveCoRo(bool left) {

        if(left) {
            anim.SetTrigger("Move Left");
            EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Step Left");
        } else {
            anim.SetTrigger("Move Right");
            EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Step Right");
        }

        var movementStep = left ? movementStepSizeInDegrees : -movementStepSizeInDegrees;

        var startTime = Time.time;
        while (Time.time < startTime + movementSpeed) {
            transform.RotateAround(ringCenter.position, Vector3.up, movementStep / movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public void Block(bool start) {
        if(start) {
            anim.SetTrigger("Block");
            anim.SetBool("Is Blocking", true);
            EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Block");
        } else {
            anim.ResetTrigger("Block");
            anim.SetBool("Is Blocking", false);
        }
    }

    public void Jab() {
        ResetAllAnimationTriggers();
        anim.SetTrigger("Jab");
        EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Jab");
        lastPunchDamage = PlayerPrefs.GetInt("Player Jab Damage", 50);
    }

    public void Cross() {
        ResetAllAnimationTriggers();
        anim.SetTrigger("Cross");
        EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Cross");
        lastPunchDamage = PlayerPrefs.GetInt("Player Cross Damage", 100);
    }
    public void HookLeft() {
        ResetAllAnimationTriggers();
        anim.SetTrigger("Hook Left");
        StartCoroutine(MoveForwardToHit());
        EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Left Hook");
        lastPunchDamage = PlayerPrefs.GetInt("Player Hook Damage", 150);
    }
    public void HookRight() {
        ResetAllAnimationTriggers();
        anim.SetTrigger("Hook Right");
        StartCoroutine(MoveForwardToHit());
        EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Right Hook");
        lastPunchDamage = PlayerPrefs.GetInt("Player Hook Damage", 150);
    }
    public void UppercutLeft() {
        ResetAllAnimationTriggers();
        anim.SetTrigger("Uppercut Left");
        StartCoroutine(MoveForwardToHit());
        EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Left Uppercut");
        lastPunchDamage = PlayerPrefs.GetInt("Player Uppercut Damage", 250);
    }
    public void UppercutRight() {
        ResetAllAnimationTriggers();
        anim.SetTrigger("Uppercut Right");
        StartCoroutine(MoveForwardToHit());
        EventManager.Instance.TriggerEventWithStringParam(EventManager.Events.LastMove, "Right Uppercut");
        lastPunchDamage = PlayerPrefs.GetInt("Player Uppercut Damage", 250);
    }

    private IEnumerator MoveForwardToHit() {
        float startTime = Time.time;
        while (Time.time < startTime + 0.25f) {
            model.transform.position = model.transform.position + (model.transform.forward * Time.deltaTime);
            yield return null;
        }

        startTime = Time.time;
        while (Time.time < startTime + 0.25f) {
            model.transform.position = model.transform.position - (model.transform.forward * Time.deltaTime);
            yield return null;
        }
    }

    private void FaceRingCenter() {
        if(ringCenter != null)
            model.transform.LookAt(ringCenter.position);
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

    void OnOpponentKO() {
        StartCoroutine(OnOpponentKOCor());
    }
    IEnumerator OnOpponentKOCor() {
        yield return new WaitForSeconds(1f);
        anim.SetLayerWeight(1, 0);
        anim.SetTrigger("Cheer");
    }

    void OnShowResults() {
        StartCoroutine(OnShowResultsCor());
    }

    private IEnumerator OnShowResultsCor() {
        yield return new WaitForSeconds(1f);
        model.transform.position = spawnPositionForRoster.position;
        FaceRosterCamera();
        if(!FightManager.Instance.isPlayerWinner) {
            anim.SetTrigger("Be Sad");
        }
        yield return null;
    }

    private void ResetAllAnimationTriggers() {
        anim.ResetTrigger("Jab");
        anim.ResetTrigger("Cross");
        anim.ResetTrigger("Hook Left");
        anim.ResetTrigger("Hook Right");
        anim.ResetTrigger("Uppercut Left");
        anim.ResetTrigger("Uppercut Right");
        anim.ResetTrigger("Move Left");
        anim.ResetTrigger("Move Right");
        anim.ResetTrigger("Slip Left");
        anim.ResetTrigger("Slip Right");
        // anim.ResetTrigger("Get Hit");
        // anim.ResetTrigger("Block");
    }

}