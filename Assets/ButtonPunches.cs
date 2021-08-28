using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPunches : MonoBehaviour
{
    PlayerController playerController;
    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> Data.player != null);
        playerController = Data.player.GetComponent<PlayerController>();

    }

    public void OnButtonLeftSidePressed(){
        if(playerController != null){
            playerController.HandleFightInput(true);
        }
    }
    public void OnButtonRightSidePressed(){
        if(playerController != null){
            playerController.HandleFightInput(false);
        }
    }

}
