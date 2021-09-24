using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPunches : MonoBehaviour {
    PlayerController playerController;
    private void Start() {
        playerController = Player.Instance.GetComponent<PlayerController>();

    }

    public void OnButtonLeftSidePressed() {
        if(playerController != null) {
            playerController.Jab();
        }
    }
    public void OnButtonRightSidePressed() {
        if(playerController != null) {
            playerController.Cross();
        }
    }

}