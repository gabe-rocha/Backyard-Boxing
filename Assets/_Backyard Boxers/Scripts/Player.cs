using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA.CharacterSystem;
using System;
using System.IO;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject umaHolder;
    [SerializeField] private Transform ringCenter;
    [SerializeField] private float movementStepSizeInDegrees = 15f;
    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private GameObject gloveHitBoxLeft, gloveHitBoxRight, headHitBox;


    private Transform spawnPositionForFight;
    private GameObject UMA;
    private Animator anim;
    private DynamicCharacterAvatar avatar;
    public DynamicCharacterAvatar Avatar { get => avatar; set => avatar = value; }

    internal void Rotate(float deltaX)
    {
        transform.Rotate(Vector3.up * deltaX, Space.World);
    }

    private Coroutine movingCor;

    private string avatarRecipeFilename;

    private IEnumerator Start(){
        yield return new WaitUntil(()=> Data.gameState == Data.GameStates.Loading);
        Data.player = this;

    }

    private void SetupHitBoxes()
    {
        var rightHand = transform.FindDeepChild("RightHand");
        var leftHand = transform.FindDeepChild("LeftHand");
        var head = transform.FindDeepChild("Head");

        gloveHitBoxRight.tag = "Player";
        gloveHitBoxLeft.tag = "Player";
        headHitBox.tag = "Player";

        Instantiate(gloveHitBoxRight, rightHand);
        Instantiate(gloveHitBoxLeft, leftHand);
        Instantiate(headHitBox, head);
    }

    public void SetWardrobeSlotItem(string slot, string recipe){
        if(Avatar != null){
            Avatar.SetSlot(slot, recipe);
            Avatar.BuildCharacter();
            SaveRecipe();
        }
    }

    public void SaveRecipe()
    {
        string myRecipe = avatar.GetCurrentRecipe();
        File.WriteAllText(Application.persistentDataPath + avatarRecipeFilename, myRecipe);
        Debug.Log($"File saved to: {Application.persistentDataPath}");
    }

    internal void DestroyUMA()
    {
        if(umaHolder.transform.childCount == 0) return;
        Destroy(umaHolder.transform.GetChild(0).gameObject);
        UMA = null;
    }

    public void SetUMA(GameObject newUma){
        UMA = newUma;
        avatar = null;
        avatar = UMA.GetComponent<DynamicCharacterAvatar>();
        StartCoroutine(BuildAvaterFromRecipe());

        if(Data.gameState != Data.GameStates.SelectingCharacter){
            transform.position = newUma.transform.position;
        }
        newUma.transform.parent = umaHolder.transform;

        anim = newUma.GetComponent<Animator>();
        SetupHitBoxes();
    }

    private IEnumerator BuildAvaterFromRecipe()
    {
        yield return new WaitUntil(()=>avatar != null);
        LoadRecipe();
    }

    public void LoadRecipe()
    {
        avatarRecipeFilename = $"/playerAvatar{UMA.name}Recipe"; //C:\Users\gabri\AppData\LocalLow\DefaultCompany\[production URP] Backyard Boxing
        if(File.Exists(Application.persistentDataPath + avatarRecipeFilename)){
            var myRecipe = File.ReadAllText(Application.persistentDataPath + avatarRecipeFilename);
            if(! string.IsNullOrEmpty(myRecipe)){
                avatar.ClearSlots();
                avatar.LoadFromRecipeString(myRecipe);
                // Avatar.BuildCharacter();
            }
        }
    }

    private void Update()
    {

        if(Data.gameState == Data.GameStates.Fighting)
        {
            FaceRingCenter();
        }
    }

    public void HandleTiltInput(Vector3 tilt){
        Move(tilt.x < -Data.tiltMinX);
    }

    public void Jab(bool left)
    {
        if(left){
            anim.ResetTrigger("Jab Right");
            anim.SetTrigger("Jab Left");
        }
        else{
            anim.ResetTrigger("Jab Left");
            anim.SetTrigger("Jab Right");
        }
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
    internal void Slip(bool left)
    {
        if(left){
            anim.SetTrigger("Slip Left");
        }
        else{
            anim.SetTrigger("Slip Right");
        }
    }

    private IEnumerator MoveCoRo(bool left){

        if(left)
            anim.SetTrigger("Move Left");
        else
            anim.SetTrigger("Move Right");

        var movementStep = left ? movementStepSizeInDegrees : -movementStepSizeInDegrees;

        var startTime = Time.time;
        while(Time.time < startTime + movementSpeed){
            transform.RotateAround(ringCenter.position, Vector3.up, movementStep / movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public void Block(bool start){
        if(start){
            anim.SetTrigger("Block");
            anim.SetBool("Is Blocking", true);
        }
        else{
            anim.ResetTrigger("Block");
            anim.SetBool("Is Blocking", false);
        }
    }

    private void FaceRingCenter()
    {
        if(ringCenter != null)
            transform.LookAt(ringCenter.position);
    }



}