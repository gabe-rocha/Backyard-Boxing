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

    // private GameObject UMA;
    private Animator anim;
    private static Player instance;
    private GameObject model;
    private Coroutine movingCor;
    // private DynamicCharacterAvatar avatar;
    // public DynamicCharacterAvatar Avatar { get => avatar; set => avatar = value; }

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

    public void Jab(bool left) {
        if(left) {
            anim.ResetTrigger("Jab Right");
            anim.SetTrigger("Jab Left");
        } else {
            anim.ResetTrigger("Jab Left");
            anim.SetTrigger("Jab Right");
        }
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
        } else {
            anim.SetTrigger("Slip Right");
        }
    }

    private IEnumerator MoveCoRo(bool left) {

        if(left)
            anim.SetTrigger("Move Left");
        else
            anim.SetTrigger("Move Right");

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
        } else {
            anim.ResetTrigger("Block");
            anim.SetBool("Is Blocking", false);
        }
    }

    private void FaceRingCenter() {
        if(ringCenter != null)
            model.transform.LookAt(ringCenter.position);
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