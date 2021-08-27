using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;
using System;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] GameObject umaHolder;
    [SerializeField] private Transform ringCenter;
    [SerializeField] private float movementStepSizeInDegrees = 30f;
    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private GameObject gloveHitBoxLeft, gloveHitBoxRight, headHitBox;
    [SerializeField] private GameObject bloodParticles;
    

    private Transform spawnPositionForFight;
    private GameObject UMA;
    private Animator anim;
    private DynamicCharacterAvatar avatar;
    public DynamicCharacterAvatar Avatar { get => avatar; set => avatar = value; }

    private Coroutine movingCor;

    private IEnumerator Start(){
        yield return new WaitUntil(()=> Data.gameState == Data.GameStates.Loading);
        Data.opponent = this;
    }

    internal void GetHit()
    {
        anim.ResetTrigger("Get Hit");
        anim.SetTrigger("Get Hit");
        var head = transform.FindDeepChild("Head");
        
        Instantiate(bloodParticles, head.position, bloodParticles.transform.rotation);
        ScreenFlash.instance.Flash(UnityEngine.Random.Range(0.25f, 0.5f));
    }


    public void SetUMA(GameObject newUma){
        UMA = newUma;
        Avatar = UMA.GetComponent<DynamicCharacterAvatar>();

        transform.position = newUma.transform.position;
        newUma.transform.parent = umaHolder.transform;

        anim = newUma.GetComponent<Animator>();
        SetupHitBoxes();
    }

    private void SetupHitBoxes()
    {
        var rightHand = transform.FindDeepChild("RightHand");
        var leftHand = transform.FindDeepChild("LeftHand");
        var head = transform.FindDeepChild("Head");

        gloveHitBoxRight.tag = "Opponent";
        gloveHitBoxLeft.tag = "Opponent";
        headHitBox.tag = "Opponent";

        Instantiate(gloveHitBoxRight, rightHand);
        Instantiate(gloveHitBoxLeft, leftHand);
        Instantiate(headHitBox, head);
    }

    private void Update()
    {
        if(Data.gameState == Data.GameStates.Fighting)
        {
            FaceRingCenter();
        }
    }

    private void FaceRingCenter()
    {
        transform.LookAt(ringCenter.position);
    }

    
    internal void HandleTiltInput(Vector3 tilt)
    {
        Move(tilt.x < 0);
    }
    public void Move(bool left)
    {
        if(movingCor != null){
            anim.ResetTrigger("Move Left");
            anim.ResetTrigger("Move Right");
            StopCoroutine(movingCor);
        }
        movingCor = StartCoroutine(MoveCoRo(left));
    }

    private IEnumerator MoveCoRo(bool left){

        if(left){
            anim.SetTrigger("Move Left");
        }
        else{
            anim.SetTrigger("Move Right");
        }

        var movementStep = left ? movementStepSizeInDegrees : -movementStepSizeInDegrees;

        var startTime = Time.time;
        while(Time.time < startTime + movementSpeed){
            transform.RotateAround(ringCenter.position, Vector3.up, movementStep / movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
