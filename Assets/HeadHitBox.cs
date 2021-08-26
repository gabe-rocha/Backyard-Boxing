using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitBox : MonoBehaviour
{
    Player player;
    OpponentAI opponent;

    bool isPlayer;

    private IEnumerator Start(){
        yield return new WaitUntil(()=> Data.player != null && Data.opponent != null);
        if(CompareTag("Player")){
            player = Data.player;
            isPlayer = true;
        }
        else if(CompareTag("Opponent")){
            opponent = Data.opponent;
            isPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Data.gameState != Data.GameStates.Fighting)
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
