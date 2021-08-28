using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player player;
    OpponentAI opponent;

    private float lastTouchX;
    private Vector3 tilt;
    private bool canMoveLeft, canMoveRight, canSlipLeft, canSlipRight, canBlock, canJabLeft, canJabRight, canHookLeft, canHookRight, canUppercutLeft, canUppercutRight;

    private IEnumerator Start()
    {

        if(Data.gameState == Data.GameStates.Fighting){
            yield return new WaitUntil(()=> Data.player != null && Data.opponent != null);
            player = Data.player;
            opponent = Data.opponent;
        }
        else{
            player = GetComponent<Player>();
        }

        canMoveLeft = true;
        canMoveRight = true;
        canSlipLeft = true;
        canSlipRight = true;
        canBlock = true;
        canJabLeft = true;
        canJabRight = true;
        canHookLeft = true;
        canHookRight = true;
        canUppercutLeft = true;
        canUppercutRight = true;
    }

    void Update()
    {
        if(player == null || opponent == null){
            player = Data.player;
            opponent = Data.opponent;
        }
        if(Data.gameState == Data.GameStates.SelectingCharacter)
            HandleCharScreenRotation();

        if(Data.gameState == Data.GameStates.Fighting) {
            // if(player == null || opponent == null){
            //     return;
            // }
            HandleAcceletometer();
            // HandleFightInput();
        }
    }

    private void HandleAcceletometer()
    {
        tilt = Input.acceleration;
        if(tilt.x > -Data.tiltMinX && tilt.x < Data.tiltMinX){
            return;
        }else{
            if(tilt.x <= -Data.tiltMinX && canMoveLeft){
                StartCoroutine(MoveLeft(tilt));
            }
            else if(tilt.x >= Data.tiltMinX && canMoveRight){
                StartCoroutine(MoveRight(tilt));
            }
        }
    }
    private IEnumerator MoveLeft(Vector3 tilt){
        canMoveLeft = false;
        player.HandleTiltInput(tilt);
        opponent.HandleTiltInput(tilt);
        yield return new WaitForSeconds(Data.movementStepDelay);
        canMoveLeft = true;
    }

    private IEnumerator MoveRight(Vector3 tilt){
        canMoveRight = false;
        player.HandleTiltInput(tilt);
        opponent.HandleTiltInput(tilt);
        yield return new WaitForSeconds(Data.movementStepDelay);
        canMoveRight = true;
    }

    public void HandleFightInput(bool left)
    {
        // if(Input.GetMouseButtonDown(0) &&
        //     Input.mousePosition.x < Screen.width/2f &&
        //     Input.mousePosition.y > Screen.height * 0.33f &&
        // else if(Input.GetMouseButtonDown(0) &&
        //     Input.mousePosition.x > Screen.width/2f &&
        //     Input.mousePosition.y > Screen.height * 0.33f &&

        if(left && canJabLeft){
                StartCoroutine(JabLeft());
        }
        else if(!left && canJabRight){
                StartCoroutine(JabRight());
        }
    }

    private IEnumerator JabLeft(){
        canJabLeft = false;
        player.Jab(true);
        yield return new WaitForSeconds(Data.fightJabDelay);
        canJabLeft = true;
    }
    private IEnumerator JabRight(){
        canJabRight = false;
        player.Jab(false);
        yield return new WaitForSeconds(Data.fightJabDelay);
        canJabRight = true;
    }

    private void HandleCharScreenRotation()
    {
        if(Input.GetMouseButtonDown(0)){
            lastTouchX = Input.mousePosition.x;
        }
        if(Input.GetMouseButton(0)){
            var deltaX = Input.mousePosition.x - lastTouchX;
            deltaX /= -5f;
            player.Rotate(deltaX);

            lastTouchX = Input.mousePosition.x;
        }
    }
}
