using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour
{
    Player player;
    OpponentAI opponent;

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> GameData.player != null && GameData.opponent != null);
        player = GameData.player;
        opponent = GameData.opponent;
    }
    public void OnMovementButtonPressed(bool left){
        player.Move(left);
        StartCoroutine(MoveOpponent(left));
        
    }

    private IEnumerator MoveOpponent(bool left)
    {
        yield return new WaitForSeconds(0.25f);
        opponent.Move(left);
    }
}
