using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitBox : MonoBehaviour
{
    Player player;
    OpponentAI opponent;

    bool isPlayer;

    private IEnumerator Start(){
        yield return new WaitUntil(()=> GameData.player != null && GameData.opponent != null);
        if(CompareTag("Player")){
            player = GameData.player;
            isPlayer = true;
        }
        else if(CompareTag("Opponent")){
            opponent = GameData.opponent;
            isPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameData.gameState != GameData.GameStates.Fighting)
            return;

        if(!isPlayer)
        {
            if(other.gameObject.CompareTag("Player")){
                opponent.GetHit();
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
