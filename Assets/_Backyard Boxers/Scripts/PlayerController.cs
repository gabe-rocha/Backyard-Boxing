using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Player player;
    OpponentAI opponent;

    private float lastTouchX;
    private Vector3 tilt;
    private bool canMove, canSlip, canBlock, canJab, canCross, canHook, canUppercut;
    private Vector2 touchInitialPosition, touchCurrentPosition, touchEndPosition, touchDelta;

    private void Start() {

        if(Data.gameState == Data.GameStates.Fighting) {
            player = Player.Instance;
            opponent = OpponentAI.Instance;
        } else {
            player = GetComponent<Player>();
        }

        canMove = true;
        canSlip = true;
        canBlock = true;
        canJab = true;
        canCross = true;
        canHook = true;
        canUppercut = true;
    }

    void Update() {
        if(player == null || opponent == null) {
            player = Player.Instance;
            opponent = OpponentAI.Instance;
        }
        if(Data.gameState == Data.GameStates.SelectingCharacter)
            HandleCharScreenRotation();

        if(Data.gameState == Data.GameStates.Fighting) {
            // if(player == null || opponent == null){
            //     return;
            // }
            HandleAcceletometer();
            // HandleFightInput();
            HandleSwipes();
        }
    }

    private void HandleSwipes() {
        if(Input.touchCount > 0) {
            //we care just about the 1st finger
            if(Input.touches[0].phase == TouchPhase.Began) {
                touchInitialPosition = Input.touches[0].position;
                touchCurrentPosition = touchInitialPosition;
            } else if(Input.touches[0].phase == TouchPhase.Moved) {
                touchCurrentPosition = Input.touches[0].position;
            } else if(Input.touches[0].phase == TouchPhase.Ended) {
                touchEndPosition = Input.touches[0].position;
                touchDelta = touchEndPosition - touchInitialPosition;

                if(touchDelta.x < 100 && touchDelta.y > 100) {
                    //swiped up
                    Debug.Log($"Swiped up\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                    if(touchInitialPosition.x > touchEndPosition.x) {
                        UppercutLeft();
                    } else {
                        UppercutRight();
                    }
                } else if(touchDelta.x > 100 && touchDelta.y < 100) {
                    //swiped right
                    Debug.Log($"Swiped Right\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                    HookLeft();
                } else if(touchDelta.x < -100 && touchDelta.y > -100) {
                    //swiped left
                    Debug.Log($"Swiped Left\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                    HookRight();
                } else if(touchDelta.x > -100 && touchDelta.y < -100) {
                    //swiped down
                    Debug.Log($"Swiped Down\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                    //???
                } else if(touchDelta.x > 100 && touchDelta.y > 100) {
                    //Diagonal right up
                    if(Mathf.Abs(touchDelta.x) > Mathf.Abs(touchDelta.y)) {
                        Debug.Log($"Swiped Right\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                        HookLeft();
                    } else {
                        Debug.Log($"Swiped Up\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                        if(touchInitialPosition.x > touchEndPosition.x) {
                            UppercutLeft();
                        } else {
                            UppercutRight();
                        }
                    }
                } else if(touchDelta.x > 100 && touchDelta.y < 100) {
                    //Diagonal right down
                    if(Mathf.Abs(touchDelta.x) > Mathf.Abs(touchDelta.y)) {
                        Debug.Log($"Swiped Right\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                        HookLeft();
                    } else {
                        Debug.Log($"Swiped Down\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                    }
                } else if(touchDelta.x < -100 && touchDelta.y > 100) {
                    //Diagonal left up
                    if(Mathf.Abs(touchDelta.x) > Mathf.Abs(touchDelta.y)) {
                        Debug.Log($"Swiped Left\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                        HookRight();
                    } else {
                        Debug.Log($"Swiped Up\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                        if(touchInitialPosition.x > touchEndPosition.x) {
                            UppercutLeft();
                        } else {
                            UppercutRight();
                        }
                    }
                } else if(touchDelta.x < -100 && touchDelta.y < -100) {
                    //Diagonal left down
                    if(Mathf.Abs(touchDelta.x) > Mathf.Abs(touchDelta.y)) {
                        Debug.Log($"Swiped Left\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                        HookRight();
                    } else {
                        Debug.Log($"Swiped Down\ndeltaX: {touchDelta.x}\ndeltaY: {touchDelta.y}");
                    }
                } else {
                    //just a tap we handle it with invisible on-screen buttons
                }
            }
        }
    }

    private void HandleAcceletometer() {
        tilt = Input.acceleration;
        if(tilt.x > -Data.tiltMinX && tilt.x < Data.tiltMinX) {
            return;
        } else {
            if(tilt.x <= -Data.tiltMinX && canMove) {
                StartCoroutine(MoveLeft(tilt));
            } else if(tilt.x >= Data.tiltMinX && canMove) {
                StartCoroutine(MoveRight(tilt));
            }
        }
    }

    private IEnumerator MoveLeft(Vector3 tilt) {
        canMove = false;
        player.HandleTiltInput(tilt);
        opponent.HandleTiltInput(tilt);
        yield return new WaitForSeconds(Data.movementStepDelay);
        canMove = true;
    }

    private IEnumerator MoveRight(Vector3 tilt) {
        canMove = false;
        player.HandleTiltInput(tilt);
        opponent.HandleTiltInput(tilt);
        yield return new WaitForSeconds(Data.movementStepDelay);
        canMove = true;
    }

    public void Jab() {
        if(canJab) {
            StartCoroutine(JabCor());
        }
    }
    private IEnumerator JabCor() {
        canJab = false;
        player.Jab();
        yield return new WaitForSeconds(Data.fightJabDelay);
        canJab = true;
    }

    public void Cross() {
        if(canCross) {
            StartCoroutine(CrossCor());
        }
    }
    private IEnumerator CrossCor() {
        canCross = false;
        player.Cross();
        yield return new WaitForSeconds(Data.fightCrossDelay);
        canCross = true;
    }

    private void HookLeft() {
        if(canHook) {
            StartCoroutine(HookLeftCor());
        }
    }
    private IEnumerator HookLeftCor() {
        canHook = false;
        player.HookLeft();
        yield return new WaitForSeconds(Data.fightHookDelay);
        canHook = true;
    }
    private void HookRight() {
        if(canHook) {
            StartCoroutine(HookRightCor());
        }
    }
    private IEnumerator HookRightCor() {
        canHook = false;
        player.HookRight();
        yield return new WaitForSeconds(Data.fightHookDelay);
        canHook = true;
    }

    private void UppercutLeft() {
        if(canUppercut) {
            StartCoroutine(UppercutLeftCor());
        }
    }
    private IEnumerator UppercutLeftCor() {
        canUppercut = false;
        player.UppercutLeft();
        yield return new WaitForSeconds(Data.fightUppercutDelay);
        canUppercut = true;
    }

    private void UppercutRight() {
        if(canUppercut) {
            StartCoroutine(UppercutRightCor());
        }
    }
    private IEnumerator UppercutRightCor() {
        canUppercut = false;
        player.UppercutRight();
        yield return new WaitForSeconds(Data.fightUppercutDelay);
        canUppercut = true;
    }

    /*****************************************************************************/

    private void HandleCharScreenRotation() {
        if(Input.GetMouseButtonDown(0)) {
            lastTouchX = Input.mousePosition.x;
        }
        if(Input.GetMouseButton(0)) {
            var deltaX = Input.mousePosition.x - lastTouchX;
            deltaX /= -5f;
            player.Rotate(deltaX);

            lastTouchX = Input.mousePosition.x;
        }
    }
}