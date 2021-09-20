using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitBox : MonoBehaviour {
    Player player;
    OpponentAI opponent;

    bool isPlayer;

    private void Start() {
        // yield return new WaitUntil(()=> Data.player != null && Data.opponent != null);
        if(CompareTag("Player")) {
            player = Player.Instance;
            isPlayer = true;
        } else if(CompareTag("Opponent")) {
            opponent = OpponentAI.Instance;
            isPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        // if(Data.gameState != Data.GameStates.Fighting)
        // return;

        if(!isPlayer) {
            if(other.gameObject.CompareTag("Player")) {
                var lastPunchDamage = Player.Instance.lastPunchDamage;
                opponent.GetHit(lastPunchDamage);
            }
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if(!isPlayer)
    //     {
    //         if(other.gameObject.CompareTag("Player")){
    //             Debug.Log("Opponent Hit");
    //         }
    //     }
    // }
}