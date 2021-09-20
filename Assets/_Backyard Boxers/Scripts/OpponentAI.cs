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

    // private GameObject UMA;
    private Animator anim;
    private GameObject model;
    // private DynamicCharacterAvatar avatar;
    // public DynamicCharacterAvatar Avatar { get => avatar; set => avatar = value; }

    private Coroutine movingCor;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowingRingGirl);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowingRingGirl);
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
    }

    private void Update() {
        if(Data.gameState == Data.GameStates.ShowingRingGirl || Data.gameState == Data.GameStates.Fighting) {
            FaceRingCenter();
        } else if(Data.gameState == Data.GameStates.ShowingRoster) {
            FaceCamera();
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

    internal void GetHit() {
        anim.ResetTrigger("Get Hit");
        anim.SetTrigger("Get Hit");
        var head = transform.FindDeepChild("head");

        Instantiate(bloodParticles, head.position, bloodParticles.transform.rotation);
        ScreenFlash.instance.Flash(UnityEngine.Random.Range(0.25f, 0.5f));
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
    private void FaceCamera() {
        if(rosterLookAt != null) {
            model.transform.LookAt(rosterLookAt.position);
        }
    }

    private void OnShowingRingGirl() {
        model.transform.position = spawnPositionForFight.position;
    }
}